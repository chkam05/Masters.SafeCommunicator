using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    // ##########################################################################################
    public class ERSA {
        
        private string          publicKey;
        private RSAParameters   privateKey;
        private int             keyLong         =   2048;

        // ################################################################################
        public ERSA() {
            GenerateKeys();
        }

        // ################################################################################
        public void GenerateKeys() {
            var generator               =   new RSACryptoServiceProvider(keyLong);
            generator.PersistKeyInCsp   =   false;
            publicKey                   =   generator.ToXmlString( false );
            privateKey                  =   generator.ExportParameters( true );
        }

        // --------------------------------------------------------------------------------
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

        // ################################################################################
        public string PublicStringKey { get { return publicKey; } }

        // ################################################################################
    }

    // ##########################################################################################
}
