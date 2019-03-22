using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Safe_Communicator.Tool {

    // ####################################################################################################
    //  x   x   xxxxx    xxxx    xxxx    xxx     xxxx   xxxxx    xxxx
    //  xx xx   x       x       x       x   x   x       x       x    
    //  x x x   xxxx     xxx     xxx    xxxxx   x  xx   xxxx     xxx 
    //  x   x   x           x       x   x   x   x   x   x           x
    //  x   x   xxxxx   xxxx    xxxx    x   x    xxxx   xxxxx   xxxx 
    // ####################################################################################################
    public static class Messages {

        // ##########################################################################################
        //  xxxx     xxx    xxxxx    xxx       xxxxx   xxxx    xxxx     xxx    xxxx     xxxx
        //   x  x   x   x     x     x   x      x       x   x   x   x   x   x   x   x   x    
        //   x  x   xxxxx     x     xxxxx      xxxx    xxxx    xxxx    x   x   xxxx     xxx 
        //   x  x   x   x     x     x   x      x       x   x   x   x   x   x   x   x       x
        //  xxxx    x   x     x     x   x      xxxxx   x   x   x   x    xxx    x   x   xxxx 
        // ##########################################################################################
        /// <summary> Funckja wyświetlająca powiadomienie o błędynie wpisanym loginie. </summary>
        /// <param name="error_code"> Informacja o zaistniałych błędach. </param>
        public static void IncorrectLoginError( Errors error_code ) {
            string  title   =   "Błąd danych.";
            string  message =   "Wpisano niepoprawny login użytkownika.";

            switch ( error_code ) {
                case Errors.NO_ERROR:
                    return;
            }

            MessageBox.Show( title, message, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funckja wyświetlająca powiadomienie o błędynie wpisanym adresie protokołu sieciowego. </summary>
        /// <param name="error_code"> Informacja o zaistniałych błędach. </param>
        public static void IncorrectIPError( Errors error_code ) {
            string  title   =   "Błąd danych.";
            string  message =   "Wpisano niepoprawny adres protokołu sieciowego (IP).";

            switch ( error_code ) {
                case Errors.NO_ERROR:
                    return;
            }

            MessageBox.Show( title, message, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funckja wyświetlająca powiadomienie o błędynie wpisanym porcie interfejsu sieciowego. </summary>
        /// <param name="error_code"> Informacja o zaistniałych błędach. </param>
        public static void IncorrectPortError( Errors error_code ) {
            string  title   =   "Błąd danych.";
            string  message =   "Wpisano niepoprawny port interfejsu sieciowego.";

            switch ( error_code ) {
                case Errors.INVALID_RANGE:
                    message =   "Wpisany port interfejsu sieciowego znajduje się poza zakresem (0 - 65535).";
                    break;
                case Errors.NO_ERROR:
                    return;
            }

            MessageBox.Show( title, message, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        // ##########################################################################################
        //  xxxx    xxxxx    xxx    xxxxx    xxxx   xxxxx    xxx    x   x    xxxx
        //   x  x   x       x   x     x     x         x     x   x   xx  x   x    
        //   x  x   xxxx    x         x      xxx      x     x   x   x x x    xxx 
        //   x  x   x       x   x     x         x     x     x   x   x  xx       x
        //  xxxx    xxxxx    xxx    xxxxx   xxxx    xxxxx    xxx    x   x   xxxx 
        // ##########################################################################################
        /// <summary> Funkcja wyświetlająca powiadomienie zapytanie o wyłączenie serwera. </summary>
        /// <returns> Wyłącznik serwera. </returns>
        public static bool CloseServer( int Clients = 0 ) {
            string          title   =   "Zamykanie serwera.";
            string          message =   string.Format( 
                "Serwer jest obecnie uruchomiony./nCzy chcesz zakończyć działanie serwera?/nAktualnie podłączonych jest {0} klientów", Clients );
            DialogResult    result  =   MessageBox.Show( title, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
            return result == DialogResult.Yes ? true : false;
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja wyświetlająca powiadomienie zapytanie o wyłączenie klienta. </summary>
        /// <returns> Wyłącznik klienta. </returns>
        public static bool CloseClient() {
            string          title   =   "Zamykanie klienta.";
            string          message =   "Czy chcesz zerwać połączenie z serwerem i zakończyć działanie aplikacji?";
            DialogResult    result  =   MessageBox.Show( title, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
            return result == DialogResult.Yes ? true : false;
        }

        // ##########################################################################################
        //  xxxxx   x   x   xxxxx    xxx    xxxx    x   x    xxx    xxxxx   xxxxx    xxx    x   x
        //    x     xx  x   x       x   x   x   x   xx xx   x   x     x       x     x   x   xx  x
        //    x     x x x   xxxx    x   x   xxxx    x x x   xxxxx     x       x     x   x   x x x
        //    x     x  xx   x       x   x   x   x   x   x   x   x     x       x     x   x   x  xx
        //  xxxxx   x   x   x        xxx    x   x   x   x   x   x     x     xxxxx    xxx    x   x
        // ##########################################################################################
        public static void IPAddresses() {
            string  title   =   "Twoje adresy IP";
            string  message =   "Wewnętrzny adres IP: " + Tools.GetInsideIP()
                            +   "Zewnętrzny adres IP: " + Tools.GetOutsideIP();
            MessageBox.Show( message, title, MessageBoxButtons.OK, MessageBoxIcon.Information );
        }

        // ##########################################################################################
    }

    // ####################################################################################################
}
