using Safe_Communicator.Crypt;
using Safe_Communicator.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Classes {

    public class Message {
        
        public  int         senderId    { get; set; }
        public  int         reciverId   { get; set; }
        public  DateTime    sendDate    { get; set; }
        public  string      command     { get; set; }
        public  string      message     { get; set; }

        #region Constructor
        // ##########################################################################################
        /// <summary> Konstruktor klasy paczki wiadomości. </summary>
        /// <param name="sender_id"> ID nadawcy. </param>
        /// <param name="reciver_id"> ID odbiorcy. </param>
        /// <param name="time"> Data nadania wiadomości. </param>
        /// <param name="command"> Zawarte polecenie (komenda) serwer - klient. </param>
        /// <param name="message"> Zawartość wiadomości. </param>
        public Message( int sender_id, int reciver_id, DateTime time, string command, string message ) {
            this.senderId   =   sender_id;
            this.reciverId  =   reciver_id;
            this.sendDate   =   time;
            this.command    =   command;
            this.message    =   message;
        }

        #endregion Constructor
        #region Cryptography
        // ##########################################################################################
        /// <summary> Funkcja szyfrująca wewnętrznie wiadomość. </summary>
        /// <param name="crypt"> Klasa szyfrująca. </param>
        /// <param name="publicKey"> Klucz publiczny. </param>
        public void Encrypt( ICrypt crypt, string publicKey ) {
            this.message = crypt.Encrypt( this.message, publicKey );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja deszyfrująca wewnętrznie wiadomość. </summary>
        /// <param name="crypt"> Klasa szyfrująca. </param>
        /// <param name="decrypt"> Informacja czy wiadomość jest zaszyfrowana. </param>
        public void Decrypt( ICrypt crypt, bool decrypt ) {
            if ( decrypt ) { this.message = crypt.Decrypt( this.message ); }
        }

        #endregion Cryptography
        #region Message Mangament
        // ##########################################################################################
        /// <summary> Funkcja konwertująca ciąg danych sprasowanych na wiadomość. </summary>
        /// <param name="message"> Ciąg znaków sprasowanej wiadomości. </param>
        /// <returns></returns>
        public static Message ReadMessage( string message ) {
            string[]    lines   =   Tools.ReadLines( message );
            
            if ( !int.TryParse( lines[0], out int sender_id ) ) { sender_id = 0; }
            if ( !int.TryParse( lines[1], out int reciver_id ) ) { reciver_id = 0; }
            if ( !DateTime.TryParse( lines[2], out DateTime time_send ) ) { time_send = DateTime.Now; }

            return new Message( sender_id, reciver_id, time_send, lines[3], Tools.ConcatLines( lines, 4, lines.Length, Environment.NewLine ) );
        }

        // ------------------------------------------------------------------------------------------
        /// <summary> Funkcja zwracająca wiadomość jako sprasowany ciąg znaków do wysłania. </summary>
        /// <returns> Wiadomość jako sprasowany ciąg znaków. </returns>
        public override string ToString() {
            return  senderId.ToString() + Environment.NewLine +
                    reciverId.ToString() + Environment.NewLine +
                    sendDate.ToString() + Environment.NewLine +
                    command.ToString() + Environment.NewLine +
                    message.ToString();
        }

        #endregion Message Mangament
        // ##########################################################################################
    }

}
