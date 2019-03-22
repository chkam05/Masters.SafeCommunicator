using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    // ####################################################################################################
    //  xxxx     xxx    xxxxx    xxx        xxx    x   x   xxxxx    xxx    x   x
    //   x  x   x   x     x     x   x      x   x   x   x   x       x   x   x  x 
    //   x  x   xxxxx     x     xxxxx      x       xxxxx   xxxx    x       xxx  
    //   x  x   x   x     x     x   x      x   x   x   x   x       x   x   x  x 
    //  xxxx    x   x     x     x   x       xxx    x   x   xxxxx    xxx    x   x
    // ####################################################################################################
    public static class DataCheck {

        /// <summary> Funckja sprawdzająca poprawność wpisanego loginu użytkownika. </summary>
        /// <param name="login"> Zmienna przechowująca login użytkownika. </param>
        /// <param name="error_code"> Informacja zwrotna o potencjalnych błędach. </param>
        /// <returns> True: Jeżeli login jest poprawny, False: Jeżeli login nie jest poprawny. </returns>
        public static bool CheckLogin( string login, out Errors error_code ) {
            error_code  =   Errors.NO_ERROR;
            if ( login.Length <= 0 ) { error_code = Errors.INVALID_FORMAT; }
            return true;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funckja sprawdzająca poprawność wpisanego adresu IP. </summary>
        /// <param name="ip"> Zmienna przechowująca adres IP. </param>
        /// <param name="error_code"> Informacja zwrotna o potencjalnych błędach. </param>
        /// <returns> True: Jeżeli adres IP jest poprawny, False: Jeżeli adres IP nie jest poprawny. </returns>
        public static bool CheckIP( string ip, out Errors error_code ) {
            string[]    segments    =   ip.Split('.');
            int         convert     =   0;
                        error_code  =   Errors.NO_ERROR;
            if ( segments.Length != 4 ) { error_code = Errors.INVALID_FORMAT; return false; }
            foreach( string s in segments ) {
                if ( !int.TryParse( s, out convert ) ) { error_code = Errors.INVALID_FORMAT; return false; }
                if ( convert < 0 || convert > 255 ) { error_code = Errors.INVALID_FORMAT; return false; }
            }
            return true;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja sprawdzająca poprawność wpisanego portu interfejsu sieciowego. </summary>
        /// <param name="port"> Zmienna przechowująca port interfejsu sieciowego. </param>
        /// <param name="error_code"> Informacja zwrotna o potencjalnych błędach. </param>
        /// <returns> True: Jeżeli port interfejsu sieciowego jest poprawny, False: Jeżeli nie jest poprawny. </returns>
        public static bool CheckPort( string port, out Errors error_code ) {
            int     convert     =   0;
                    error_code  =   Errors.NO_ERROR;
            if ( !int.TryParse( port, out convert ) ) { error_code = Errors.INVALID_FORMAT; return false; }
            if ( convert < 0 || convert > 65535 ) { error_code = Errors.NO_ERROR; return false; }
            return true;
        }

        // ------------------------------------------------------------------------------------------
    }

    // ####################################################################################################
}
