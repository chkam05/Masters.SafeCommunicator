using Safe_Communicator.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Crypt {

    public class ElGamal {
        
        private long[]  publicKey;
        private long[]  privateKey;
        private int     keyLong         =   2048;

        // ################################################################################
        /// <summary> Konstruktor klasy szyfrowania ElGamal. </summary>
        public ElGamal() {
            GenerateKeys();
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja generująca klucze. </summary>
        public void GenerateKeys() {
            Random  random      =   new Random();
            long    qValue      =   Tools.LongRandom( (long) Math.Pow(10, 1), (long) Math.Pow(10, 10), random );
            long    gValue      =   Tools.LongRandom( 2, qValue, random );
            GeneratePrivateKey( qValue, gValue, random );
            GeneratePublicKey( qValue, gValue, this.privateKey, random );
        }

        // --------------------------------------------------------------------------------
        /// <summary>  </summary>
        /// <param name="a">  </param>
        /// <param name="b">  </param>
        /// <returns>  </returns>
        private long gcd( long a, long b ) {
            if ( a < b ) return gcd( b, a );
            else if ( a % b == 0 ) return b;
            else return gcd( b, a % b );
        }

        // --------------------------------------------------------------------------------
        /// <summary>  </summary>
        /// <param name="qValue">  </param>
        /// <param name="random">  </param>
        /// <returns>  </returns>
        private long GenerateKey( long qValue, Random random ) {
            long key = Tools.LongRandom( (long) Math.Pow(10, 1), qValue, random );
            while ( gcd( qValue, key ) != 1 )
                key = Tools.LongRandom( (long) Math.Pow(10, 1), qValue, random );
            return key;
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja generująca klucz prywatny. </summary>
        /// <param name="qValue">  </param>
        /// <param name="gValue">  </param>
        /// <param name="random">  </param>
        private void GeneratePrivateKey( long qValue, long gValue, Random random ) {
            long    key     =   GenerateKey( qValue, random );
            long    hValue  =   Tools.ExpMod( gValue, key, qValue );
            this.privateKey =   new long[] { gValue, qValue, key, hValue };
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja generująca klucz publiczny. </summary>
        /// <param name="qValue">  </param>
        /// <param name="gValue">  </param>
        /// <param name="privateKey">  </param>
        /// <param name="random">  </param>
        /// <returns></returns>
        private long GeneratePublicKey( long qValue, long gValue, long[] privateKey, Random random ) {
            long    key     =   GenerateKey( qValue, random );
            long    sValue  =   Tools.ExpMod( privateKey[3], key, qValue );
            long    pValue  =   Tools.ExpMod( gValue, key, qValue );
            privateKey[0]   =   pValue;
            this.publicKey  =   new long[] { qValue, gValue, key, sValue };
            return key;
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja szyfrująca dane z użyciem zewnętrznego klucza publicznego. </summary>
        /// <param name="data"> Wiadomość do zaszyfrowania. </param>
        /// <param name="publicKey"> Klucz publiczny. </param>
        /// <returns> Zaszyfrowana wiadomość. </returns>
        public string Encrypt( string data, string publicKey ) {
            string      result  =   "";
            string[]    keyData =   Tools.ReadLines( publicKey );
            long        sValue  =   SplitPublicKey( publicKey )[3];

            for ( int i = 0; i < data.Length; i++ ) {
                result += (sValue * (int) data[i]).ToString();
                if ( i < data.Length - 1 ) { result += '-'; }
            }

            return result;
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja deszyfrująca wiadomość z użyciem wewnętrznego klucza prywatnego. </summary>
        /// <param name="data"> Zaszywfrowana wiadomość. </param>
        /// <returns> Zdeszyfrowana wiadomość. </returns>
        public string Decrypt( string data ) {
            string      result  =   "";
            string[]    split   =   data.Split( new char[] {'-'} );
            long        hValue  =   this.privateKey[3];

            for ( int i = 0; i < split.Length; i++ ) {
                result  +=  (char) (long.Parse( split[i] ) / hValue);
            }

            return result;
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja przetwarzająca klucz z postaci ciągu tekstowego na klucz. </summary>
        /// <param name="publicKey"> Klucz w postaci ciągu tekstowego. </param>
        /// <returns> Klucz publiczny. </returns>
        private long[] SplitPublicKey( string publicKey ) {
            string[] split = publicKey.Split( new char[] { ' ' } );
            long[] result = new long[split.Length];
            for ( int i = 0; i < split.Length; i++ ) {
                result[i] = long.Parse( split[i] );
            }
            return result;
        }

        // --------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająca klucz publiczny w postaci ciągu tekstowego. </summary>
        /// <returns> Klucz publiczny w postaci ciągu tekstowego. </returns>
        public string GetPublicKey() {
            string result = "";
            for ( int i = 0; i < publicKey.Length; i++ ) {
                result += publicKey[i].ToString();
                if ( i < publicKey.Length - 1 ) { result += " "; }
            }
            return result;
        }

        // ################################################################################
    }

}
