using Safe_Communicator.Classes;
using Safe_Communicator.Crypt;
using Safe_Communicator.Enumerators;
using Safe_Communicator.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Message = Safe_Communicator.Classes.Message;

namespace Safe_Communicator.Connectors {

    public class Client {

        public  delegate        void    InvokeInterface();
        public  delegate        void    InvokeCommand( object sender, EventArgs e );
        public  delegate        void    InvokeMessage( string message, MessageModifier modifier );

        private static readonly string  _messageStart               =   "( i ) Setting up client...";
        private static readonly string  _messageConnecting          =   "( i ) Connecting to server...";
        private static readonly string  _messageReconnect           =   "( ! ) Connection Filed. Re-connecting attempt {0}...";
        private static readonly string  _messageConnected           =   "( i ) Connected to {0}";
        private static readonly string  _messageDisconnecting       =   "( i ) Disconnecting...";
        private static readonly string  _messageClosing             =   "( i ) Shutting down client...";
        private static readonly string  _messageConnectionRefused   =   "( ! ) Server has refiused connection...{0}{1}";
        private static readonly string  _messageReconnectRefused    =   "( ! ) Connection lost. Re-connecting attempt {0}...";

        // ------------------------------------------------------------------------------------------
        private int             identifier                          =   -1;
        public  string          Username            { get; }        =   "Client";
        public  string          ServerIP            { get; }        =   "127.0.0.1";
        public  int             Port                { get; }        =   65534;
        public  int             BufferSize          { get; }        =   2048;

        private CryptType       cryptType                           =   0;
        private bool            encryption                          =   false;
        private ICrypt          encryptionServices;
        private string          serverPublicKey;

        private string          srvName                             =   "Server";
        private Socket          cliSocket;
        public  int             Sender              { get; set; }   =   0;
        public  List<string[]>  Clients             { get; set; }

        private Thread          thrConnection;
        private Thread          thrReciver;

        public  RichTextBox     CliOutput           { get; set; }
        public  ListView        CliList             { get; set; }
        public  int             ConnectionTimeOut   { get; set; }   =   32;
        public  int             ReConnectTimeOut    { get; set; }   =   0;
        public  InvokeMessage   FuncShowMessage     { get; set; }
        public  InvokeCommand   FuncDisconnect      { get; set;}
        public  InvokeCommand   FuncShutDown        { get; set; }

        #region Contructor
        // ##########################################################################################
        /// <summary> Konstruktor obiektu klasy Client. </summary>
        /// <param name="username"> Rozpoznawalna nazwa użytkownika. </param>
        /// <param name="ip"> Adres protokołu internetowego serwera. </param>
        /// <param name="port"> Port protokołu sieciowego serwera. </param>
        /// <param name="ctype"> Rodzaj szyfrowania wiadomości. </param>
        public Client( string username, string ip, int port, CryptType ctype = 0 ) {
            this.Username   =   username;
            this.ServerIP   =   ip;
            this.Port       =   port;
            this.cryptType  =   ctype;

            this.encryptionServices = new ERSA();
        }

        #endregion Constructor
        #region Client Mangament
        // ##########################################################################################
        /// <summary> Funkcja uruchamiająca funkcje klienta. </summary>
        public void Start() {
            UpdateUI( _messageStart, MessageModifier.INFORMATION );
            cliSocket   =   new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            ConnectClient();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zamykająca działanie klienta wraz z jego elementami. </summary>
        public void ShutDown() {
            UpdateUI( _messageClosing, MessageModifier.SERVER );

            try { thrReciver.Abort(); }
            catch ( NullReferenceException ) { /* thrReciver is current NULL */ }
            catch ( ThreadAbortException ) { /* thrReciver is not running */ }

            try { thrConnection.Abort(); }
            catch ( NullReferenceException ) { /* thrReciver is current NULL */ }
            catch ( ThreadAbortException ) { /* thrReciver is not running */ }
            
            Disconnect();

            try { cliSocket.Close(); }
            catch ( NullReferenceException ) { /* cliSocket is current NULL */ }

            thrReciver      =   null;
            thrConnection   =   null;
            cliSocket       =   null;
        }

        #endregion Client Mangament
        #region Communication Handler
        // ##########################################################################################
        /// <summary> Funkcja uruchamiająca wątek oczekiwania na odebranie wiadomości z serwera. </summary>
        private void ReciveMessage() {
            if ( thrReciver != null && thrReciver.ThreadState == ThreadState.Running ) { return; }

            ThreadStart thrReciverFunc  =   new ThreadStart( LoopRecive );
                        thrReciver      =   new Thread( thrReciverFunc );
            
            thrReciver.Start();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja odbierająca wiadomości z serwera. </summary>
        private void LoopRecive() {
            if ( cliSocket == null ) { return; }

            int     refuseAttempts  =   0;
            while ( cliSocket.Connected ) {
                try {
                    byte[]  buffer      =   new byte[ BufferSize ];
                    int     bufferSize  =   cliSocket.Receive( buffer );
                    byte[]  message     =   new byte[ bufferSize ];

                    if ( bufferSize == 0 ) { LoopReconnect( true, ref refuseAttempts ); continue; }
                    else { refuseAttempts = 0; }

                    Array.Copy( buffer, message, bufferSize );
                    Message newMessage      =   Message.ReadMessage( Encoding.ASCII.GetString( message ) );
                    
                    // DECRYPTION
                    newMessage.Decrypt( encryptionServices, encryption );
                    
                    bool    executed        =   ExecutServerCommand( newMessage );

                    if ( !executed ) {
                        int         senderId        =   newMessage.senderId;
                        int         reciverId       =   newMessage.reciverId;
                        string[]    senders         =   (from c in Clients where c[0] == senderId.ToString() select c[1]).ToArray();
                        string      sendDate        =   newMessage.sendDate.ToString();
                        
                        UpdateUI(
                            sendDate + " " + (senders.Length > 0 ? senders[0] : senderId.ToString()) + Environment.NewLine + newMessage.message,
                            MessageModifier.INCOMING
                        );
                    }

                } catch ( SocketException exc ) {
                    UpdateUI( string.Format( _messageConnectionRefused, Environment.NewLine, exc.ToString() ), MessageModifier.SERVER );
                    ShutDown();
                    return;

                }
            }
        }

        #endregion Communiation Handler
        #region Message Functions
        // ##########################################################################################
        /// <summary> Funkcja wykonująca wywołanie polecenia z okna wiadomości klienta. </summary>
        /// <param name="message"> Polecenie z okna wiadomości klienta. </param>
        public void SendCommand( string message ) {
            bool        executed    =   ExecutClientCommand( message );
            DateTime    sendDate    =   DateTime.Now;
            string[]    recivers    =   (from c in Clients where c[0] == Sender.ToString() select c[1]).ToArray();
            
            if ( !executed ) {
                UpdateUI(
                    sendDate.ToString() + " " + (recivers.Length > 0 ? recivers[0] : Sender.ToString()) + Environment.NewLine + message,
                    MessageModifier.OUTGOING
                );
                Message newMessage  =   new Message( identifier, Sender, sendDate, "", message );
                SendMessage( newMessage );
            }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wysyłająca wiadomość do podłączonego serwera. </summary>
        /// <param name="message"> Wiadomość wysyłana do serwera. </param>
        public void SendMessage( Message message ) {
            if ( cliSocket == null ) { return; }
            if ( !cliSocket.Connected ) { return; }

            // ENCRYPTION
            if ( encryption ) { message.Encrypt( encryptionServices, serverPublicKey ); }

            byte[]  buffer      =   Encoding.ASCII.GetBytes( message.ToString() );
            cliSocket.Send( buffer );
        }

        #endregion Message Functions
        #region Commands from Client
        // ##########################################################################################
        /// <summary> Funkcja sprawdzająca poprawość polecenia i egzekwująca jego wykonanie. </summary>
        /// <param name="command"> Polecenie (komenda) dla klienta. </param>
        /// <returns> Informacja o poprawnym wykonaniu polecenia. </returns>
        private bool ExecutClientCommand( string command ) {
            string[]    arguments   =   command.Split( ' ' );
            bool        result      =   true;

            if ( arguments.Length <= 0 ) { result = false; }
            else if ( arguments[0].Length <= 0 ) { result = false; }
            else if ( arguments[0].ToLower() == "/help" ) { ShowHelpCommand(); }
            else if ( arguments[0].ToLower() == "/list" ) { RequestListCommand(); }
            else if ( arguments[0].ToLower() == "/disconnect" ) { InvokeUI( FuncShutDown ); }
            else if ( arguments[0][0] == '/' ) { UpdateUI( "( ! ) Syntax Command is invalid.", MessageModifier.SERVER ); }
            else { result = false; }
            return result;
        }

        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wyświetlajaca pomoc dla wspieranych poleceń. </summary>
        private void ShowHelpCommand() {
            string  message =   
                "<none>                    - Sends message to selected client" + Environment.NewLine +
                "/disconnect               - Shutdown the server" + Environment.NewLine +
                "/help                     - Shows help message" + Environment.NewLine +
                "/list                     - Shows list of current connected clients";
            UpdateUI( message, MessageModifier.INFORMATION );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wysyłająca do serwera dane konfiguracyjne klienta. </summary>
        private void ConfigureServerCommand() {
            string  publicKey   =   "";

            //  Konfiguracja publicznego klucza szyfrującego wiadomości do wsyłania do serwera.
            switch ( cryptType ) {
                case CryptType.RSA:
                    publicKey   =   encryptionServices.GetPublicKey();
                    break;
                case CryptType.ElGamal:
                    break;
            }
            
            string  content     =   Tools.ConcatLines(
                new string[] { Username, ((int)this.cryptType).ToString(), publicKey, "<end>" },
                0, 4, Environment.NewLine );
            
            Message newMessage  =   new Message( identifier, 0, DateTime.Now, "/config", content );
            SendMessage( newMessage );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wysyłająca do serwera prośbę o przesłanie listy podłączonych klientów. </summary>
        private void RequestListCommand() {
            Message newMessage  =   new Message( identifier, 0, DateTime.Now, "/list", "" );
            SendMessage( newMessage );
        }

        #endregion Commands from Client
        #region Commands from Server
        // ##########################################################################################
        /// <summary> Funkcja sprawdzająca poprawość polecenia zdalengo i egzekwująca jego wykonanie. </summary>
        /// <param name="message"> Paczka wiadomości z poleceniem zdalnym (komendą) serwera. </param>
        /// <returns> Informacja o poprawnym wykonaniu polecenia. </returns>
        private bool ExecutServerCommand( Message message ) {
            string[]    arguments   =   message.command.Split( ' ' );
            bool        result      =   true;

            if ( arguments.Length <= 0 ) { result = false; }
            else if ( arguments[0].Length <= 0 ) { result = false; }
            else if ( arguments[0].ToLower() == "/config" ) { ConfigureClientCommand( message.message ); }
            else if ( arguments[0].ToLower() == "/list" ) { ReadListCommand( message.message ); }
            else if ( arguments[0].ToLower() == "/disconnect" ) { InvokeUI( FuncShutDown ); }
            else if ( arguments[0][0] == '/' ) { UpdateUI( "( ! ) Syntax Command is invalid.", MessageModifier.SERVER ); }
            else { result = false; }
            return result;
        }

        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja konfigurująca klienta z danymi serwera. </summary>
        /// <param name="message"> Wiadomość konfiguracyjna z serwera. </param>
        private void ConfigureClientCommand( string message ) {
            string[]    arguments   =   Tools.ReadLines( message );

            try {
                srvName             =   arguments[0];
                identifier          =   int.Parse( arguments[1] );
                // RECIVE KEY
                serverPublicKey     =   arguments[2];
            }
            catch ( IndexOutOfRangeException ) { /* Not transferred all required data */ }
            catch ( Exception ) { /* Unknown Data Error Exception */ }

            try { if ( arguments[1] != "" ) { encryption = true; } } catch { /* NOT CRYPTED */ }
            UpdateList();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja odbierająca listę podłączonych klientów. </summary>
        /// <param name="message"> Wiadomość z listą podłączonych klientów. </param>
        private void ReadListCommand( string message ) {
            UpdateClients( message );
        }

        #endregion Commands from Server
        #region Connection Mangament
        // ##########################################################################################
        /// <summary> Funkcja uruchamiająca próbę połączenia klienta z serwerem. </summary>
        private void ConnectClient() {
            if ( cliSocket.Connected ) { return; }

            ThreadStart thrConnectionFunc   =   new ThreadStart( LoopConnect );
                        thrConnection       =   new Thread( thrConnectionFunc );
            
            thrConnection.Start();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wykonująca akcję podłączania klienta do serwera. </summary>
        private void LoopConnect() {
            int         loopBack    =   1;
            IPAddress   ipAddress   =   IPAddress.Parse( ServerIP );

            UpdateUI( _messageConnecting, MessageModifier.INFORMATION );
            while ( !cliSocket.Connected && loopBack < ConnectionTimeOut ) {
                try {
                    loopBack++;
                    cliSocket.Connect( ipAddress, Port );
                } catch (SocketException) {
                    UpdateUI( string.Format( _messageReconnect, loopBack.ToString() ), MessageModifier.SERVER );
                }
            }

            if ( cliSocket.Connected ) {
                UpdateUI( string.Format( _messageConnected, ServerIP ), MessageModifier.INFORMATION );
                ReciveMessage();

                ConfigureServerCommand();
            }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja próbująca wznowić połączenie po nie oczekiwanym jego zerwaniu. </summary>
        /// <param name="inThread"> Informacja czy funkcja jest wykonywana w osobnym wątku. </param>
        /// <param name="attempt"> Ilość prób ponawiania połączenia. </param>
        private void LoopReconnect( bool inThread, ref int attempt ) {
            if ( attempt >= ReConnectTimeOut ) { throw new SocketException(); }
            if ( inThread ) { Thread.Sleep( 1000 ); }
            attempt++;
            UpdateUI( string.Format( _messageReconnectRefused, attempt.ToString() ), MessageModifier.SERVER );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja odłączająca klienta od serwera. </summary>
        public void Disconnect() {
            UpdateUI( _messageDisconnecting, MessageModifier.INFORMATION );

            try { cliSocket.Disconnect( false ); }
            catch ( NullReferenceException ) { /* cliSocket is current NULL */ }
            catch ( SocketException ) { /* cliSocket is currently disconnected */ }

            if ( FuncDisconnect != null ) { InvokeUI( FuncDisconnect ); }
        }

        #endregion Connection Mangament
        #region Clients List Mangament
        // ##########################################################################################
        /// <summary> Funkcja aktualizująca wewnętrzną strukturę listy podłączonych klientów. </summary>
        /// <param name="message"> Lista podłączonych klientów. </param>
        private void UpdateClients( string message ) {
            if ( Clients == null ) { Clients = new List<string[]>(); }
            Clients.Clear();
            Clients.Add( new string[] { 0.ToString(), srvName, ServerIP } );

            foreach( string client in Tools.ReadLines( message ) ) {
                string[]    args    =   client.Split( ' ' );
                Clients.Add( new string[] {args[0], Tools.ConcatLines( args, 1, args.Length-1, " "), args[args.Length-1]} );
            }

            UpdateList();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja aktualizująca listę podłączonych klientów w interfejsie. </summary>
        private void UpdateList() {
            string[]    serverItem  =   new string[] { 0.ToString(), srvName, ServerIP };
            CliList.Invoke( new InvokeInterface( () => { CliList.Items.Clear(); } ) );

            if ( Clients == null ) { Clients = new List<string[]>(); }
            if ( Clients.Count <= 0 ) { Clients.Add( serverItem ); } else { Clients[0] = serverItem; }

            foreach( string[] client in Clients ) {
                ListViewItem    item    =   new ListViewItem( client );
                CliList.Invoke( new InvokeInterface( () => { CliList.Items.Add( item ); } ) );
            }
        }

        #endregion Clients List Mangament
        #region Interface
        // ##########################################################################################
        /// <summary> Funkcja aktualizująca podgląd wiadomości w interfejsie aplikacji. </summary>
        /// <param name="message"> Wiadomość która ma zostać wyświetlona. </param>
        private void UpdateUI( string message, MessageModifier modifier ) {
            CliOutput.Invoke( new InvokeInterface( () => { FuncShowMessage( message, modifier ); } ) );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja uruchamiająca wybrane funkcje dla interfejsu użytkownika. </summary>
        /// <param name="function"> Wywoływana funkcja interfejsu użytkownika. </param>
        private void InvokeUI( InvokeCommand function ) {
            CliOutput.Invoke( new InvokeInterface( () => {
                function( null, null );
            } ));
        }

        #endregion Interface
        #region Client Status
        // ##########################################################################################
        /// <summary> Informacja o aktywnosci klienta. </summary>
        public bool IsActive {
            get { return cliSocket == null ? false : true; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Informacja o podłączeniu klienta do serwera. </summary>
        public bool IsConnected {
            get { return cliSocket == null ? false : cliSocket.Connected; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Identyfikator klienta. </summary>
        public int Identifier {
            get { return this.identifier; }
        }

        #endregion Client Status
        // ##########################################################################################

    }

}
