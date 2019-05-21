using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Crypt {

    public interface ICrypt {

        // ##########################################################################################        
        /// <summary> Funkcja generująca klucze. </summary>
        void GenerateKeys();

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja szyfrująca dane z użyciem zewnętrznego klucza publicznego. </summary>
        /// <param name="data"> Wiadomość do zaszyfrowania. </param>
        /// <param name="publicKey"> Klucz publiczny. </param>
        /// <returns> Zaszyfrowana wiadomość. </returns>
        string Encrypt( string data, string publicKey );

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja deszyfrująca wiadomość z użyciem wewnętrznego klucza prywatnego. </summary>
        /// <param name="data"> Zaszywfrowana wiadomość. </param>
        /// <returns> Zdeszyfrowana wiadomość. </returns>
        string Decrypt( string data );

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająca klucz publiczny. </summary>
        /// <returns> Klucz publiczny. </returns>
        string GetPublicKey();

        // ##########################################################################################

    }

}
