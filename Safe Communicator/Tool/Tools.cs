using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    public static class Tools {

        #region Cryptography Tools
        // ##########################################################################################
        /// <summary> Potęgowanie modułowe. </summary>
        /// <param name="a">  </param>
        /// <param name="b">  </param>
        /// <param name="c">  </param>
        /// <returns>  </returns>
        public static long ExpMod( long a, long b, long c ) {
            long    x   =   1;
            long    y   =   a;
            long    bt  =   b;

            while ( bt > 0 ) {
                if ( bt % 2  == 0 ) x = (x * y) % c;
                y   =   (y * y) % c;
                bt  =   (int) bt / 2;
            }

            return x % c;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja losująca wartości liczbowe long (64bit) z przedziału. </summary>
        /// <param name="min"> Minimalna wartość przedziału. </param>
        /// <param name="max"> Maksymalna wartość przedziału. </param>
        /// <param name="random"> Losowa liczba. </param>
        /// <returns></returns>
        public static long LongRandom( long min, long max, Random random ) {
            byte[]  buffer      =   new byte[8];
            random.NextBytes( buffer );
            long    longRandom  =   BitConverter.ToInt64( buffer, 0 );
            long    result      =   Math.Abs( longRandom % (max - min) + min );
            return  result;
        }

        #endregion Cryptography Tools
        #region IP Tools
        // ##########################################################################################
        /// <summary> Funkcja zwracający wewnętrzny adres IP. </summary>
        /// <returns> Wewnętrzyny adres IP w postaci ciągu tekstowego. </returns>
        public static string GetInsideIP() {
            string      localIP     =   "127.0.0.1";
            Socket      socket      =   new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
            IPEndPoint  endPoint;

            try {
                socket.Connect( "8.8.8.8", 65530 );
                endPoint    =   socket.LocalEndPoint as IPEndPoint;
                localIP     =   endPoint.Address.ToString();

            } catch( Exception exc ) {
                Console.WriteLine( exc.ToString() );
            }

            return localIP;
        }
        
        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająza zewnętrzny adres IP. </summary>
        /// <returns> Zewnętrzny adres IP w postaci ciągu tekstowego. </returns>
        public static string GetOutsideIP() {
            try {
                // string  ext_ip  =   new WebClient().DownloadString("http://icanhazip.com");
                // string  ext_ip  =   new WebClient().DownloadString("http://bot.whatismyipaddress.com");
                // string  ext_ip  =   new WebClient().DownloadString("http://ipinfo.io/ip");
                // return  ext_ip;

                WebRequest      request     =   WebRequest.Create( "http://checkip.dyndns.org" );
                WebResponse     response    =   request.GetResponse();
                StreamReader    stream      =   new StreamReader( response.GetResponseStream() );
                string          result      =   stream.ReadToEnd().Trim();
                string[]        array_res   =   result.Split(':');

                result      =   array_res[1].Substring(1);
                array_res   =   result.Split('<');
                result      =   array_res[0];
                return result;

            } catch( Exception exc ) {
                Console.WriteLine( exc.ToString() );
                return "";
            }
        }

        #endregion IP Tools
        #region String Tools
        // ##########################################################################################
        /// <summary> Funkcja rozdzielająca ciąg znaków na tablice podciągów dzielonych nową linią. </summary>
        /// <param name="text"> Ciąg znaków (wiadomość). </param>
        /// <returns> Tablica podciągów (Dane po dekapsulacji). </returns>
        public static string[] ReadLines( string text ) {
            return text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja łącząca podciągi znaków na ciąg znaków połączonych ze sobą znakiem. </summary>
        /// <param name="text_array"> Tablica podciągów (Danych do enkapsulacji). </param>
        /// <param name="start_index"> Indeks podciągu początkowego. </param>
        /// <param name="end_index"> Indeks podciągu końcowego. </param>
        /// <param name="connector"> Znak łączący (domyślnie spacja) </param>
        /// <returns> Ciąg znaków (Dane po enkapsulacji). </returns>
        public static string ConcatLines( string[] text_array, int start_index, int end_index, string connector = "\r\n" ) {
            string  result  =   "";
            int     start   =   Math.Min( start_index, text_array.Length-1 );
            int     end     =   Math.Min( end_index, text_array.Length );
            for ( int i = start; i < end; i++ ) {
                result  +=  ( text_array[i] + ( i+1 < end ? connector : "" ) );
            }

            return  result;
        }

        #endregion String Tools
        // ##########################################################################################

    }

}
