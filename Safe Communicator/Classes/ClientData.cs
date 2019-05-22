using Safe_Communicator.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Classes {

    // ####################################################################################################
    //   xxx    x       xxxxx   xxxxx   x   x   xxxxx      xxxx     xxx    xxxxx    xxx 
    //  x   x   x         x     x       xx  x     x         x  x   x   x     x     x   x
    //  x       x         x     xxxx    x x x     x         x  x   xxxxx     x     xxxxx
    //  x   x   x         x     x       x  xx     x         x  x   x   x     x     x   x
    //   xxx    xxxxx   xxxxx   xxxxx   x   x     x        xxxx    x   x     x     x   x
    // ####################################################################################################
    public class ClientData {
        
        public  int             Identifier          { get; set; }   =   0;
        public  string          Name                { get; set; }   =   "Client";

        private CryptType       cryptType                           =   0;
        private bool            encryption                          =   false;
        private string          clientPublicKey;

        public  int             ReconnectAttempt    { get; set; }   =   0;

        private Socket          socket;
        private int             bufferSize;
        private byte[]          buffer;
        private AsyncCallback   reciver;

        // ##########################################################################################
        //   xxx     xxx    x   x    xxxx   xxxxx   xxxx    x   x    xxx    xxxxx    xxx    xxxx 
        //  x   x   x   x   xx  x   x         x     x   x   x   x   x   x     x     x   x   x   x
        //  x       x   x   x x x    xxx      x     xxxx    x   x   x         x     x   x   xxxx 
        //  x   x   x   x   x  xx       x     x     x   x   x   x   x   x     x     x   x   x   x
        //   xxx     xxx    x   x   xxxx      x     x   x    xxx     xxx      x      xxx    x   x
        // ##########################################################################################
        /// <summary> Konstruktor klasy ClientData, przechowującej dane o podłączonym kliencie. </summary>
        /// <param name="socket"> Interfejs gniazda połaczenia. </param>
        /// <param name="bufferSize"> Rozmiar ramki na wysyłane dane. </param>
        /// <param name="reciver"> Funkcja wykonywana podczas obierania danych. </param>
        public ClientData( Socket socket, int bufferSize, AsyncCallback reciver ) {
            this.socket         =   socket;
            this.bufferSize     =   bufferSize;
            this.buffer         =   new byte[ bufferSize ];
            this.reciver        =   reciver;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Destruktor obiektu klasy ClientData. </summary>
        ~ClientData() {
            Disconnect();
            ShutDown();
        }

        // ##########################################################################################
        //  xxxxx   x   x   x   x    xxx    xxxxx    xxx    x   x    xxxx
        //  x       x   x   xx  x   x   x     x     x   x   xx  x   x    
        //  xxxx    x   x   x x x   x         x     x   x   x x x    xxx 
        //  x       x   x   x  xx   x   x     x     x   x   x  xx       x
        //  x        xxx    x   x    xxx      x      xxx    x   x   xxxx 
        // ##########################################################################################
        /// <summary> Funkcja uruchamiająca tryb oczekiwania na odebranie wiadomości. </summary>
        public void BeginReciveMessages() {
            try { socket.BeginReceive( buffer, 0, buffer.Length, SocketFlags.None, reciver, this ); }
            catch ( NullReferenceException ) { /* socket is current NULL */ }
            catch ( SocketException ) { /* Socket is currently disconnected */ }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja odłączająca wybranego klienta od serwera. </summary>
        public void Disconnect() {
            try { socket.Disconnect( false ); }
            catch ( NullReferenceException ) { /* Socket is current NULL */ }
            catch ( ObjectDisposedException ) { /* Socekt is currently NULLED */ }
            catch ( SocketException ) { /* Socket is currently disconnected */ }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wyłączająca połączenie wybranego klienta z serwerem. </summary>
        public void ShutDown() {
            Disconnect();

            try { socket.Close(); }
            catch ( NullReferenceException ) { /* Socket is current NULL */ }
            catch ( SocketException ) { /* Socket is currently disconnected */ }

            reciver =   null;
            socket  =   null;
        }

        // ##########################################################################################
        //   xxx     xxx    x   x   xxxxx   xxxxx    xxxx
        //  x   x   x   x   xx  x   x         x     x    
        //  x       x   x   x x x   xxxx      x     x  xx
        //  x   x   x   x   x  xx   x         x     x   x
        //   xxx     xxx    x   x   x       xxxxx    xxxx
        // ##########################################################################################
        /// <summary> Informacja o podłączeniu klienta do serwera. </summary>
        public bool IsConnected {
            get { return Socket == null ? false : Socket.Connected; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająca Adres IP klienta. </summary>
        /// <returns> Adres IP </returns>
        public IPAddress GetIPAddress() {
            if ( socket == null ) { return null; }
            IPEndPoint  remoteEndPoint  =   (IPEndPoint) socket.RemoteEndPoint;
            IPAddress   remoteIpAddress =   remoteEndPoint.Address;
            return remoteIpAddress.MapToIPv4();
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Rodzaj szyfrowania </summary>
        public CryptType CryptType {
            get { return this.cryptType; }
            set { this.cryptType = value; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Zewnętrzny klucz publiczny. </summary>
        public string Key {
            get { return this.clientPublicKey; }
            set { this.clientPublicKey = value; }
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Informacja czy szyfrowanie jest aktywne. </summary>
        public bool Encrypted {
            get { return this.encryption; }
            set { this.encryption = value; }
        }

        // ------------------------------------------------------------------------------------------
        public  Socket          Socket      { get { return this.socket; } }
        public  int             BufferSize  { get { return this.bufferSize; } }
        public  byte[]          Buffer      { get { return this.buffer; } }
        public  AsyncCallback   Reciver     { get { return this.reciver; } }

        // ##########################################################################################

    }

    // ####################################################################################################
}
