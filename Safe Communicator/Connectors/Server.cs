using Safe_Communicator.Classes;
using Safe_Communicator.Enumerators;
using Safe_Communicator.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Message = Safe_Communicator.Classes.Message;

namespace Safe_Communicator.Connectors {

    // ####################################################################################################
    //   xxxx   xxxxx   xxxx    x   x   xxxxx   xxxx 
    //  x       x       x   x   x   x   x       x   x
    //   xxx    xxxx    xxxx    x   x   xxxx    xxxx 
    //      x   x       x   x    x x    x       x   x
    //  xxxx    xxxxx   x   x     x     xxxxx   x   x
    // ####################################################################################################
    public class Server {
        
        public  delegate        void    InvokeInterface();
        public  delegate        void    InvokeCommand( object sender, EventArgs e );

        private static readonly string  _messageStart               =   "( i ) Setting up server...";
        private static readonly string  _messageStartListening      =   "( i ) Start listening on port {0} on address {1}...";
        private static readonly string  _messageRestartListening    =   "( i ) Waiting for client...";
        private static readonly string  _messageDisconnecting       =   "( i ) Disconnecting client [{0}] {1} on {2}...";
        private static readonly string  _messageDisconnectingAll    =   "( i ) Disconnecting all clients...";
        private static readonly string  _messageClosing             =   "( i ) Shutting down server...";
        private static readonly string  _messageReconnectRefused    =   "( ! ) Connection lost. Re-connecting attempt {0}...";
        private static readonly string  _messageUnknownHost         =   "unknown host";
        private static readonly string  _messageIsUpException       =   "( ! ) Request checking Server Up failed./n{0}";

        // ------------------------------------------------------------------------------------------
        public  string              Username            { get; }        =   "Server";
        public  string              ServerIP            { get; }        =   "127.0.0.1";
        public  int                 Port                { get; }        =   65534;
        public  int                 BufferSize          { get; }        =   2048;

        private byte[]              buffer;
        private Socket              srvSocket;
        private IPEndPoint          srvEndPoint;
        private AsyncCallback       srvAccept;
        private int                 backLog                             =   1;
        private int                 srvIdCounter                        =   0;
        
        private List<ClientData>    srvClients;
        public  TextBox             SrvOutput           { get; set; }
        public  int                 ReConnectTimeOut    { get; set; }   =   0;
        public  InvokeCommand       FuncShutDown        { get; set; }

        // ##########################################################################################
        //   xxx     xxx    x   x    xxxx   xxxxx   xxxx    x   x    xxx    xxxxx    xxx    xxxx 
        //  x   x   x   x   xx  x   x         x     x   x   x   x   x   x     x     x   x   x   x
        //  x       x   x   x x x    xxx      x     xxxx    x   x   x         x     x   x   xxxx 
        //  x   x   x   x   x  xx       x     x     x   x   x   x   x   x     x     x   x   x   x
        //   xxx     xxx    x   x   xxxx      x     x   x    xxx     xxx      x      xxx    x   x
        // ##########################################################################################
        /// <summary> Konstruktor obiektu klasy Server. </summary>
        /// <param name="username"> Nazwa serwera, która będzie zwracana jako informacja. </param>
        /// <param name="ip"> Adres protokołu internetowego serwera. </param>
        /// <param name="port"> Port protokołu sieciowego serwera. </param>
        public Server( string username, string ip, int port ) {
            this.Username   =   username;
            this.ServerIP   =   ip;
            this.Port       =   port;
        }

        // ##########################################################################################
        //  x   x    xxx    x   x    xxx     xxxx   x   x   xxxxx   x   x   xxxxx
        //  xx xx   x   x   xx  x   x   x   x       xx xx   x       xx  x     x  
        //  x x x   xxxxx   x x x   xxxxx   x  xx   x x x   xxxx    x x x     x  
        //  x   x   x   x   x  xx   x   x   x   x   x   x   x       x  xx     x  
        //  x   x   x   x   x   x   x   x    xxxx   x   x   xxxxx   x   x     x  
        // ##########################################################################################
        /// <summary>  Funkcja konfigurująca i uruchamiająca funkcje serwera z jego podstawowymi elementami. </summary>
        public void Start() {
            UpdateUI( _messageStart );
            srvSocket   =   new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
            srvEndPoint =   new IPEndPoint( IPAddress.Parse( ServerIP ), Port );
            srvAccept   =   new AsyncCallback( AcceptClient );
            buffer      =   new byte[ BufferSize ];

            srvSocket.Bind( srvEndPoint );
            UpdateUI( string.Format( _messageStartListening, Port, ServerIP ) );
            srvSocket.Listen( backLog );
            StartListen();
        }
        
        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja uruchamiająca oczekiwanie na podłączenie się następnego klienta. </summary>
        private void StartListen() {
            srvIdCounter ++;

            try {
                srvSocket.BeginAccept( srvAccept, null );
                UpdateUI( _messageRestartListening );
            }
            catch ( NullReferenceException ) { /* srvSocket is current NULL */ }
            catch ( ObjectDisposedException ) { /* srvSocket set to NULL */ }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zamykająca działanie serwera wraz z jego elementami. </summary>
        public void ShutDown() {
            UpdateUI( _messageClosing );
            DisconnectAll();

            try { srvSocket.Close(); }
            catch ( NullReferenceException ) { /* cliSocket is current NULL */ }

            srvSocket       =   null;
            srvEndPoint     =   null;
            srvAccept       =   null;
            buffer          =   null;
        }

        // ##########################################################################################
        //  x   x    xxx    x   x   xxxx    x       xxxxx   xxxx 
        //  x   x   x   x   xx  x    x  x   x       x       x   x
        //  xxxxx   xxxxx   x x x    x  x   x       xxxx    xxxx 
        //  x   x   x   x   x  xx    x  x   x       x       x   x
        //  x   x   x   x   x   x   xxxx    xxxxx   xxxxx   x   x
        // ##########################################################################################
        /// <summary> Funkcja przyłączająca klienta do serwera. </summary>
        /// <param name="asyncResult"> Podłączany klient. </param>
        private void AcceptClient( IAsyncResult asyncResult ) {
            if ( srvClients == null ) { srvClients = new List<ClientData>(); }

            try {
                Socket          socket  =   srvSocket.EndAccept( asyncResult );
                AsyncCallback   reciver =   new AsyncCallback( ReciveCallback );
                ClientData      client  =   new ClientData( socket, BufferSize, reciver );

                client.Identifier   =   srvIdCounter;
                client.Name         =   string.Format( "Client {0}", srvIdCounter.ToString() );
                srvClients.Add( client );
                client.BeginReciveMessages();
            }
            catch ( NullReferenceException ) { /* srvSocket is current NULL */ }
            catch ( ObjectDisposedException ) { /* srvSocket set to NULL */ }

            StartListen();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja odbierająca wiadomość od klienta. </summary>
        /// <param name="asyncResult"> Podłączony klient. </param>
        private void ReciveCallback( IAsyncResult asyncResult ) {
            try {
                ClientData  client      =   (ClientData) asyncResult.AsyncState;
                Socket      socket      =   client.Socket;

                int         bufferSize  =   socket.EndReceive( asyncResult );
                byte[]      buffer      =   new byte[ bufferSize ];

                if ( bufferSize == 0 ) { LoopReconnect( asyncResult, client ); return; }
                else { client.ReconnectAttempt = 0; }

                Array.Copy( client.Buffer, buffer, bufferSize );
                Message newMessage      =   Message.ReadMessage( Encoding.ASCII.GetString( buffer ) );
                bool    executed        =   ExecuteClientCommand( newMessage, client );
                
                if ( !executed ) {
                    int     senderId    =   newMessage.senderId;
                    int     reciverId   =   newMessage.reciverId;
                    string  sendDate    =   newMessage.sendDate.ToString();
                    string  content     =   newMessage.message;

                    if ( reciverId == 0 ) {
                        UpdateUI( sendDate + " [ " + client.Name + " ] " + Environment.NewLine + newMessage.message );
                    } else {
                        SendMessage( GetClient( reciverId ), newMessage );
                    }
                }

                client.BeginReciveMessages();
            }
            catch ( NullReferenceException ) { /* socket is currently NULL */ }
            catch ( ObjectDisposedException ) { /* socket is currently NULLable */ }
            catch ( SocketException ) { /* socket is currently Disconected */ }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja kończąca dla funkcji wysyłania wiadomości do klienta. </summary>
        /// <param name="asyncResult"> Klient do którego ma zostać wysłana wiadomość. </param>
        private void SendCallback( IAsyncResult asyncResult ) {
            ClientData  client      =   (ClientData) asyncResult.AsyncState;
            Socket      socekt      =   client.Socket;

            socekt.EndSend( asyncResult );
        }

        // ##########################################################################################
        //  xxxxx   x   x   x   x    xxx    xxxxx   xxxxx    xxx    x   x    xxxx
        //  x       x   x   xx  x   x   x     x       x     x   x   xx  x   x    
        //  xxxx    x   x   x x x   x         x       x     x   x   x x x    xxx 
        //  x       x   x   x  xx   x   x     x       x     x   x   x  xx       x
        //  x        xxx    x   x    xxx      x     xxxxx    xxx    x   x   xxxx 
        // ##########################################################################################
        public void SendCommand( string message ) {
            bool    executed    =   ExecuteServerCommand( message );
            if ( !executed ) { SendBroadcast( message ); }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wysyłająca wiadomość do podłączonego klienta lub grupy. </summary>
        /// <param name="message"> Wiadomość wysyłana do klienta. </param>
        private void SendBroadcast( string message ) {
            if ( srvClients == null ) { return; }
            foreach ( ClientData client in srvClients ) {
                Message newMessage  =   new Message( 0, client.Identifier, DateTime.Now, "", message );
                SendMessage( client, newMessage );
            }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wysyłająca wiadomość do określonego podłączonego. </summary>
        /// <param name="client"> Podłączony klient. </param>
        /// <param name="message"> Wiadomość wysyłana do klienta. </param>
        private void SendMessage( ClientData client, Message message ) {
            Socket          socket  =   client.Socket;
            byte[]          buffer  =   Encoding.ASCII.GetBytes( message.ToString() );
            Console.Write( message.ToString() );
            AsyncCallback   sender  =   new AsyncCallback( SendCallback );

            socket.BeginSend( buffer, 0, buffer.Length, SocketFlags.None, sender, client );
            client.BeginReciveMessages();
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
        private bool ExecuteServerCommand( string command ) {
            string[]    arguments   =   command.Split( ' ' );
            bool        result      =   true;

            if ( arguments.Length <= 0 ) { result = false; }
            else if ( arguments[0].Length <= 0 ) { result = false; }
            else if ( arguments[0].ToLower() == "/help" ) { ShowHelpCommand(); }
            else if ( arguments[0].ToLower() == "/kick" ) { KickClientCommand( arguments ); }
            else if ( arguments[0].ToLower() == "/list" ) { ShowClientListCommand(); }
            else if ( arguments[0].ToLower() == "/send" ) { SendMessageCommand( arguments ); }
            else if ( arguments[0].ToLower() == "/shutdown" ) { InvokeUI( FuncShutDown ); }
            else if ( arguments[0][0] == '/' ) { UpdateUI( "( ! ) Syntax Command is invalid." ); }
            else { result = false; }
            return result;
        }

        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        private void ShowHelpCommand() {
            string  message =   
                "<none>                    - Sends message to all connected clients" + Environment.NewLine +
                "/help                     - Shows help message" + Environment.NewLine +
                "/kick <id/name>           - Kick client with selected ID" + Environment.NewLine +
                "/list                     - Shows list of current connected clients" + Environment.NewLine +
                "/send <id/name> <message> - Sends message to client with selected ID" + Environment.NewLine +
                "/shutdown                 - Shutdown the server";
            UpdateUI( message );
        }

        // ------------------------------------------------------------------------------------------
        private void KickClientCommand( string[] arguments ) {
            int         identifier  =   -1;
            ClientData  client;

            try {
                if ( int.TryParse( arguments[1], out identifier ) ) { client = GetClient( identifier ); }
                else { client = GetClient( Tools.ConcatLines( arguments, 1, arguments.Length, " " ) ); }
                if ( client == null ) { throw new Exception(); }
                else { DisconnectClient( client ); }
            }
            catch( IndexOutOfRangeException ) { UpdateUI( "( ! ) Syntax Command is invalid. Missing <id/name> argument." ); return; }
            catch ( Exception ) { UpdateUI( "( ! ) Client with specified identifier, does not exist." ); return; }
        }

        // ------------------------------------------------------------------------------------------
        private void ShowClientListCommand() {
            if ( srvClients.Count <= 0 ) { UpdateUI( "( i ) At this point, there are no connected clients." ); }
            foreach( string[] client in GetClientList( null ) ) {
                UpdateUI( client[0] + " " + client[1] + " " + client[2] );
            }
        }

        // ------------------------------------------------------------------------------------------
        private void SendMessageCommand( string[] arguments ) {
            int         identifier  =   -1;
            ClientData  client;

            try {
                if ( int.TryParse( arguments[1], out identifier ) ) { client = GetClient( identifier ); }
                else { client = GetClient( arguments[1] ); }
                if ( client == null ) { throw new Exception(); }
            }
            catch ( IndexOutOfRangeException ) { UpdateUI( "( ! ) Syntax Command is invalid. Missing <id/name> argument." ); return; }
            catch ( Exception ) { UpdateUI( "( ! ) Client with specified identifier, does not exist." ); return; }

            try {
                string  content     =   Tools.ConcatLines( arguments, 2, arguments.Length, Environment.NewLine );
                Message message     =   new Message( 0, identifier, DateTime.Now, "", content );
                SendMessage( client, message );
            }
            catch ( IndexOutOfRangeException ) { UpdateUI( "( ! ) Syntax Command is invalid. Missing <message> argument." ); return; }
            catch ( Exception ) { return; }
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
        private bool ExecuteClientCommand( Message message, ClientData sender ) {
            string[]    arguments   =   message.command.Split( ' ' );
            bool        result      =   true;

            if ( arguments.Length <= 0 ) { result = false; }
            else if ( arguments[0].Length <= 0 ) { result = false; }
            else if ( arguments[0] == "/config" ) { ConfigureCommand( message.message, sender ); }
            else if ( arguments[0] == "/list" ) { SendListCommand( sender ); }
            else if ( arguments[0][0] == '/' ) { UpdateUI( "( ! ) Syntax Command is invalid." ); }
            else { result = false; }
            return result;
        }

        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        private void ConfigureCommand( string message, ClientData sender ) {
            string[]    arguments   =   Tools.ReadLines( message );

            try {
                sender.Name =   arguments[0];
                sender.Key  =   arguments[1];
            }
            catch ( IndexOutOfRangeException ) { /* Not transferred all required data */ }
            catch ( Exception ) { /* Unknown Data Error Exception */ }

            string  content     =   Tools.ConcatLines(
                new string[] { Username, sender.Identifier.ToString(), "<end>" }, 0, 3, Environment.NewLine );
            Message newMessage  =   new Message( 0, sender.Identifier, DateTime.Now, "/config", content );
            SendMessage( sender, newMessage );
            UpdateClientList();
        }

        // ------------------------------------------------------------------------------------------
        private void SendListCommand( ClientData sender ) {
            string[][]  clients     =   GetClientList( sender );
            string      content     =   "";
            string      line        =   "";

            for ( int i = 0; i < clients.Length; i++ ) {
                line    =   clients[i][0] + " " + clients[i][1] + " " + clients[i][2];
                content +=  ( line + ( i < clients.Length - 1 ? Environment.NewLine : "" ) );
            }

            Message newMessage  =   new Message( 0, sender.Identifier, DateTime.Now, "/list", content );
            SendMessage( sender, newMessage );
        }

        // ##########################################################################################
        //   xxx    x       xxxxx   xxxxx   x   x   xxxxx    xxxx
        //  x   x   x         x     x       xx  x     x     x    
        //  x       x         x     xxxx    x x x     x      xxx 
        //  x   x   x         x     x       x  xx     x         x
        //   xxx    xxxxx   xxxxx   xxxxx   x   x     x     xxxx 
        // ##########################################################################################
         /// <summary> Funkcja wykonująca akcję odnowienia połączenia z klientem. </summary>
        private void LoopReconnect( IAsyncResult asyncResult, ClientData client ) {
            if ( client.ReconnectAttempt >= ReConnectTimeOut ) { DisconnectClient( client ); }
            if ( asyncResult != null ) {
                WaitHandle  waitHandle  =   asyncResult.AsyncWaitHandle;
                waitHandle.WaitOne( 1000 );
            }

            client.ReconnectAttempt++;
            UpdateUI( string.Format( _messageReconnectRefused, client.ReconnectAttempt.ToString() ) );
        }

        // ------------------------------------------------------------------------------------------
        public ClientData GetClient( int identifier ) {
            try { return srvClients[ srvClients.FindIndex( ClientData => ClientData.Identifier == identifier ) ]; }
            catch ( NullReferenceException ) { return null; }
            catch ( IndexOutOfRangeException ) { return null; }
        }

        // ------------------------------------------------------------------------------------------
        public ClientData GetClient( string name ) {
            try { return srvClients[ srvClients.FindIndex( ClientData => ClientData.Name == name ) ]; }
            catch ( NullReferenceException ) { return null; }
            catch ( IndexOutOfRangeException ) { return null; }
        }

        // ------------------------------------------------------------------------------------------
        public ClientData GetClient( IPAddress ipAddress ) {
            try { return srvClients[ srvClients.FindIndex( ClientData => ClientData.GetIPAddress() == ipAddress ) ]; }
            catch ( NullReferenceException ) { return null; }
            catch ( IndexOutOfRangeException ) { return null; }
        }

        // ------------------------------------------------------------------------------------------
        public string[][] GetClientList( ClientData currentClient ) {
            List<string[]>    result  =   new List<string[]>();
            foreach( ClientData client in srvClients ) {
                if ( currentClient != null ) { if ( client == currentClient ) { continue; } }
                result.Add( new string[] {
                    client.Identifier.ToString(),
                    client.Name,
                    client.GetIPAddress().ToString()
                } );
            }
            return result.ToArray();
        }

        // ------------------------------------------------------------------------------------------
        private void UpdateClientList() {
            if ( srvClients == null ) { return; }
            foreach ( ClientData client in srvClients ) { SendListCommand( client ); }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja rozłączająca określonego klienta od serwera. </summary>
        /// <param name="client"></param>
        public void DisconnectClient( ClientData client ) {
            if ( client != null ) {
                string  address =   client.GetIPAddress().ToString();
                string  message =   string.Format( _messageDisconnecting, client.Identifier, client.Name, address ?? _messageUnknownHost );
                
                UpdateUI( message );
                client.ShutDown();
            }

            if ( srvClients.Contains( client ) ) {
                srvClients.Remove( client );
                UpdateClientList();
            }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja dołączająca wszystkich klientów od serwera. </summary>
        public void DisconnectAll() {
            UpdateUI( _messageDisconnectingAll );
            if ( srvClients == null ) { return; }
            if ( srvClients.Count <= 0 ) { return; }

            for ( int i = srvClients.Count-1; i >= 0; i-- ) { DisconnectClient( srvClients[i] ); }
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
        private void UpdateUI( string message ) {
            SrvOutput.Invoke( new InvokeInterface( () => {
                SrvOutput.AppendText( message + Environment.NewLine );
            } ));
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja uruchamiająca wybrane funkcje dla interfejsu użytkownika. </summary>
        /// <param name="function"> Wywoływana funkcja interfejsu użytkownika. </param>
        private void InvokeUI( InvokeCommand function ) {
            SrvOutput.Invoke( new InvokeInterface( () => {
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
        /// <summary> Informacja o aktywności serwera. </summary>
        public bool IsActive {
            get { return srvSocket == null ? false : true; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Informacja o aktuwnosci serwera (oczekującego na klienta). </summary>
        public bool IsUp {
            get {
                bool        result      =   false;
                IPAddress   ipAddress   =   IPAddress.Parse( ServerIP );
                double      timeout     =   10000;

                try {
                    using ( TcpClient tcp = new TcpClient() ) {
                        IAsyncResult    asyncResult =   tcp.BeginConnect( ipAddress, Port, null, tcp );
                        WaitHandle      waitHandle  =   asyncResult.AsyncWaitHandle;

                        try {
                            if ( !waitHandle.WaitOne( TimeSpan.FromMilliseconds(timeout), false ) ) {
                                tcp.EndConnect( asyncResult );
                                tcp.Close();
                                throw new SocketException();
                            }
                            result  =   true;
                            tcp.EndConnect( asyncResult );

                        } finally { waitHandle.Close(); }
                    }

                } catch ( SocketException e ) {
                    UpdateUI( string.Format( _messageIsUpException, e.ToString() ) );
                    result  =   false;
                }

                return result;
            }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Informacja o podłaczeniu klientów do serwera. </summary>
        /// <param name="clients"> Ilość podłączonych serwerów. </param>
        /// <returns></returns>
        public bool IsConnected( out int clients ) {
            bool    result  =   false;
                    clients =   0;
            
            foreach ( ClientData client in srvClients ) {
                if ( client.IsConnected ) { clients++; result = true; }
            }
            
            return result;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Zwraca ilość podłączonych klientów. </summary>
        public int ConnectedCount {
            get { return srvClients == null ? 0 : srvClients.Count; }
        }

        // ##########################################################################################

    }

    // ####################################################################################################
}
