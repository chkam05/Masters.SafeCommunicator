using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    public enum Errors {
        NO_ERROR        =   0,
        INVALID_FORMAT  =   1,
        INVALID_RANGE   =   2,
    }

    // ####################################################################################################
    //  xxxxx    xxx     xxx    x        xxxx
    //    x     x   x   x   x   x       x    
    //    x     x   x   x   x   x        xxx 
    //    x     x   x   x   x   x           x
    //    x      xxx     xxx    xxxxx   xxxx 
    // ####################################################################################################
    public static class Tools {
        
        // ##########################################################################################
        //  xxxxx   xxxx        xxx    xxxx    xxxx    xxxx    xxxxx    xxxx    xxxx   xxxxx    xxxx
        //    x     x   x      x   x    x  x    x  x   x   x   x       x       x       x       x    
        //    x     xxxx       xxxxx    x  x    x  x   xxxx    xxxx     xxx     xxx    xxxx     xxx 
        //    x     x          x   x    x  x    x  x   x   x   x           x       x   x           x
        //  xxxxx   x          x   x   xxxx    xxxx    x   x   xxxxx   xxxx    xxxx    xxxxx   xxxx 
        // ##########################################################################################
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

        // ##########################################################################################
        //   xxxx   xxxxx   xxxx    xxxxx   x   x    xxxx
        //  x         x     x   x     x     xx  x   x    
        //   xxx      x     xxxx      x     x x x   x  xx
        //      x     x     x   x     x     x  xx   x   x
        //  xxxx      x     x   x   xxxxx   x   x    xxxx
        // ##########################################################################################
        public static string[] ReadLines( string text ) {
            return text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );
        }

        // ------------------------------------------------------------------------------------------
        public static string ConcatLines( string[] text_array, int start_index, int end_index, string connector ) {
            string  result  =   "";
            int     start   =   Math.Min( start_index, text_array.Length-1 );
            int     end     =   Math.Min( end_index, text_array.Length );
            for ( int i = start; i < end; i++ ) {
                result  +=  ( text_array[i] + ( i+1 < end ? connector : "" ) );
            }

            return  result;
        }

        // ##########################################################################################
    }

    // ####################################################################################################
}
