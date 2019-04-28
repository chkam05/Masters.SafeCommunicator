using Safe_Communicator.Classes;
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

    // ####################################################################################################
    //   xxx    x       xxxxx   xxxxx   x   x   xxxxx
    //  x   x   x         x     x       xx  x     x  
    //  x       x         x     xxxx    x x x     x  
    //  x   x   x         x     x       x  xx     x  
    //   xxx    xxxxx   xxxxx   xxxxx   x   x     x  
    // ####################################################################################################
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

        private bool            encryption                          =   false;
        private ERSA            encryptionServices;
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

        // ##########################################################################################
        //   xxx     xxx    x   x    xxxx   xxxxx   xxxx    x   x    xxx    xxxxx    xxx    xxxx 
        //  x   x   x   x   xx  x   x         x     x   x   x   x   x   x     x     x   x   x   x
        //  x       x   x   x x x    xxx      x     xxxx    x   x   x         x     x   x   xxxx 
        //  x   x   x   x   x  xx       x     x     x   x   x   x   x   x     x     x   x   x   x
        //   xxx     xxx    x   x   xxxx      x     x   x    xxx     xxx      x      xxx    x   x
        // ##########################################################################################
        /// <summary> Konstruktor obiektu klasy Client. </summary>
        /// <param name="username"> Rozpoznawalna nazwa użytkownika. </param>
        /// <param name="ip"> Adres protokołu internetowego serwera. </param>
        /// <param name="port"> Port protokołu sieciowego serwera. </param>
        public Client( string username, string ip, int port ) {
            this.Username   =   username;
            this.ServerIP   =   ip;
            this.Port       =   port;

            this.encryptionServices = new ERSA();
        }

        // ##########################################################################################
        //  x   x    xxx    x   x    xxx     xxxx   x   x   xxxxx   x   x   xxxxx
        //  xx xx   x   x   xx  x   x   x   x       xx xx   x       xx  x     x  
        //  x x x   xxxxx   x x x   xxxxx   x  xx   x x x   xxxx    x x x     x  
        //  x   x   x   x   x  xx   x   x   x   x   x   x   x       x  xx     x  
        //  x   x   x   x   x   x   x   x    xxxx   x   x   xxxxx   x   x     x  
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

        // ##########################################################################################
        //  x   x    xxx    x   x   xxxx    x       xxxxx   xxxx 
        //  x   x   x   x   xx  x    x  x   x       x       x   x
        //  xxxxx   xxxxx   x x x    x  x   x       xxxx    xxxx 
        //  x   x   x   x   x  xx    x  x   x       x       x   x
        //  x   x   x   x   x   x   xxxx    xxxxx   xxxxx   x   x
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

        // ##########################################################################################
        //  xxxxx   x   x   x   x    xxx    xxxxx   xxxxx    xxx    x   x    xxxx
        //  x       x   x   xx  x   x   x     x       x     x   x   xx  x   x    
        //  xxxx    x   x   x x x   x         x       x     x   x   x x x    xxx 
        //  x       x   x   x  xx   x   x     x       x     x   x   x  xx       x
        //  x        xxx    x   x    xxx      x     xxxxx    xxx    x   x   xxxx 
        // ##########################################################################################
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

        // ##########################################################################################
        //   xxx    x       xxxxx   xxxxx   x   x   xxxxx
        //  x   x   x         x     x       xx  x     x  
        //  x       x         x     xxxx    x x x     x  
        //  x   x   x         x     x       x  xx     x  
        //   xxx    xxxxx   xxxxx   xxxxx   x   x     x  
        //
        //   xxx     xxx    x   x   x   x    xxx    x   x   xxxx     xxxx
        //  x   x   x   x   xx xx   xx xx   x   x   xx  x    x  x   x    
        //  x       x   x   x x x   x x x   xxxxx   x x x    x  x    xxx 
        //  x   x   x   x   x   x   x   x   x   x   x  xx    x  x       x
        //   xxx     xxx    x   x   x   x   x   x   x   x   xxxx    xxxx 
        // ##########################################################################################
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
        private void ShowHelpCommand() {
            string  message =   
                "<none>                    - Sends message to selected client" + Environment.NewLine +
                "/disconnect               - Shutdown the server" + Environment.NewLine +
                "/help                     - Shows help message" + Environment.NewLine +
                "/list                     - Shows list of current connected clients";
            UpdateUI( message, MessageModifier.INFORMATION );
        }

        // ------------------------------------------------------------------------------------------
        private void ConfigureServerCommand() {
            // SEND KEY
            string  publicKey   =   encryptionServices.PublicStringKey;
            string  content     =   Tools.ConcatLines( new string[] { Username, publicKey, "<end>" }, 0, 3, Environment.NewLine );
            Message newMessage  =   new Message( identifier, 0, DateTime.Now, "/config", content );
            SendMessage( newMessage );
        }

        // ------------------------------------------------------------------------------------------
        private void RequestListCommand() {
            Message newMessage  =   new Message( identifier, 0, DateTime.Now, "/list", "" );
            SendMessage( newMessage );
        }

        // ##########################################################################################
        //   xxxx   xxxxx   xxxx    x   x   xxxxx   xxxx 
        //  x       x       x   x   x   x   x       x   x
        //   xxx    xxxx    xxxx    x   x   xxxx    xxxx 
        //      x   x       x   x    x x    x       x   x
        //  xxxx    xxxxx   x   x     x     xxxxx   x   x
        //
        //   xxx     xxx    x   x   x   x    xxx    x   x   xxxx     xxxx
        //  x   x   x   x   xx xx   xx xx   x   x   xx  x    x  x   x    
        //  x       x   x   x x x   x x x   xxxxx   x x x    x  x    xxx 
        //  x   x   x   x   x   x   x   x   x   x   x  xx    x  x       x
        //   xxx     xxx    x   x   x   x   x   x   x   x   xxxx    xxxx 
        // ##########################################################################################
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
        private void ReadListCommand( string message ) {
            UpdateClients( message );
        }

        // ##########################################################################################
        //   xxxx   xxxxx   xxxx    x   x   xxxxx   xxxx 
        //  x       x       x   x   x   x   x       x   x
        //   xxx    xxxx    xxxx    x   x   xxxx    xxxx 
        //      x   x       x   x    x x    x       x   x
        //  xxxx    xxxxx   x   x     x     xxxxx   x   x
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

        // ##########################################################################################
        //   xxx    x       xxxxx   xxxxx   x   x   xxxxx    xxxx
        //  x   x   x         x     x       xx  x     x     x    
        //  x       x         x     xxxx    x x x     x      xxx 
        //  x   x   x         x     x       x  xx     x         x
        //   xxx    xxxxx   xxxxx   xxxxx   x   x     x     xxxx 
        // ##########################################################################################
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

        // ##########################################################################################
        //  xxxxx   x   x   xxxxx   xxxxx   xxxx    xxxxx    xxx     xxx    xxxxx
        //    x     xx  x     x     x       x   x   x       x   x   x   x   x    
        //    x     x x x     x     xxxx    xxxx    xxxx    xxxxx   x       xxxx 
        //    x     x  xx     x     x       x   x   x       x   x   x   x   x    
        //  xxxxx   x   x     x     xxxxx   x   x   x       x   x    xxx    xxxxx
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

        // ##########################################################################################
        //   xxx     xxx    x   x   xxxxx   xxxxx    xxxx
        //  x   x   x   x   xx  x   x         x     x    
        //  x       x   x   x x x   xxxx      x     x  xx
        //  x   x   x   x   x  xx   x         x     x   x
        //   xxx     xxx    x   x   x       xxxxx    xxxx
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
        public int Identifier {
            get { return this.identifier; }
        }

        // ##########################################################################################

    }

    // ####################################################################################################
}
