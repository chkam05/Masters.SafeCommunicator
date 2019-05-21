using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Crypt {

    public class ERSA : ICrypt {
        
        private string          publicKey;
        private RSAParameters   privateKey;
        private int             keyLong         =   2048;

        // ################################################################################
        /// <summary> Konstruktor klasy szyfrowania RSA. </summary>
        public ERSA() {
            GenerateKeys();
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja generująca klucze. </summary>
        public void GenerateKeys() {
            var generator               =   new RSACryptoServiceProvider(keyLong);
            generator.PersistKeyInCsp   =   false;
            publicKey                   =   generator.ToXmlString( false );
            privateKey                  =   generator.ExportParameters( true );
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja szyfrująca dane z użyciem zewnętrznego klucza publicznego. </summary>
        /// <param name="data"> Wiadomość do zaszyfrowania. </param>
        /// <param name="publicKey"> Klucz publiczny. </param>
        /// <returns> Zaszyfrowana wiadomość. </returns>
        public string Encrypt( string data, string publicKey ) {
            byte[]  result;
            byte[]  byteData            =   Encoding.ASCII.GetBytes( data );
            var     encryptor           =   new RSACryptoServiceProvider(keyLong);
            encryptor.PersistKeyInCsp   =   false;
            encryptor.FromXmlString( publicKey );
            result  =   encryptor.Encrypt( byteData, true );
            return  BitConverter.ToString( result );
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja deszyfrująca wiadomość z użyciem wewnętrznego klucza prywatnego. </summary>
        /// <param name="data"> Zaszywfrowana wiadomość. </param>
        /// <returns> Zdeszyfrowana wiadomość. </returns>
        public string Decrypt( string data ) {
            byte[]      result;
            String[]    stringArray         =   data.Split('-');
            byte[]      byteData            =   new byte[ stringArray.Length ];
            for ( int i = 0; i < stringArray.Length; i++ ) 
                byteData[ i ] = Convert.ToByte( stringArray[i], 16 );
            
            var         encryptor           =   new RSACryptoServiceProvider(keyLong);
            encryptor.PersistKeyInCsp       =   false;
            encryptor.ImportParameters( privateKey );
            result  =   encryptor.Decrypt( byteData, true );
            return  Encoding.ASCII.GetString( result );
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająca klucz publiczny. </summary>
        /// <returns> Klucz publiczny. </returns>
        public string GetPublicKey() { return publicKey; }

        // ################################################################################
    }

}
