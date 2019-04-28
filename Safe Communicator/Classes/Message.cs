using Safe_Communicator.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Classes {

    // ####################################################################################################
    //  x   x   xxxxx    xxxx    xxxx    xxx     xxxx   xxxxx
    //  xx xx   x       x       x       x   x   x       x    
    //  x x x   xxxx     xxx     xxx    xxxxx   x  xx   xxxx 
    //  x   x   x           x       x   x   x   x   x   x    
    //  x   x   xxxxx   xxxx    xxxx    x   x    xxxx   xxxxx
    // ####################################################################################################
    public class Message {
        
        public  int         senderId    { get; set; }
        public  int         reciverId   { get; set; }
        public  DateTime    sendDate    { get; set; }
        public  string      command     { get; set; }
        public  string      message     { get; set; }

        // ##########################################################################################
        //   xxx     xxx    x   x    xxxx   xxxxx   xxxx    x   x    xxx    xxxxx    xxx    xxxx 
        //  x   x   x   x   xx  x   x         x     x   x   x   x   x   x     x     x   x   x   x
        //  x       x   x   x x x    xxx      x     xxxx    x   x   x         x     x   x   xxxx 
        //  x   x   x   x   x  xx       x     x     x   x   x   x   x   x     x     x   x   x   x
        //   xxx     xxx    x   x   xxxx      x     x   x    xxx     xxx      x      xxx    x   x
        // ##########################################################################################
        public Message( int sender_id, int reciver_id, DateTime time, string command, string message ) {
            this.senderId   =   sender_id;
            this.reciverId  =   reciver_id;
            this.sendDate   =   time;
            this.command    =   command;
            this.message    =   message;
        }

        // ------------------------------------------------------------------------------------------
        public void Encrypt( ERSA ersa, string publicKey ) {
            this.message    =   ersa.Encrypt( this.message, publicKey );
        }

        // ------------------------------------------------------------------------------------------
        public void Decrypt( ERSA ersa, bool decrypt ) {
            if ( decrypt ) { this.message = ersa.Decrypt( this.message ); }
        }

        // ##########################################################################################
        //  xxxx    xxxxx    xxx    xxxx    xxxxx   xxxx 
        //  x   x   x       x   x    x  x   x       x   x
        //  xxxx    xxxx    xxxxx    x  x   xxxx    xxxx 
        //  x   x   x       x   x    x  x   x       x   x
        //  x   x   xxxxx   x   x   xxxx    xxxxx   x   x
        // ##########################################################################################
        public static Message ReadMessage( string message ) {
            string[]    lines   =   Tools.ReadLines( message );
            
            if ( !int.TryParse( lines[0], out int sender_id ) ) { sender_id = 0; }
            if ( !int.TryParse( lines[1], out int reciver_id ) ) { reciver_id = 0; }
            if ( !DateTime.TryParse( lines[2], out DateTime time_send ) ) { time_send = DateTime.Now; }

            return new Message( sender_id, reciver_id, time_send, lines[3], Tools.ConcatLines( lines, 4, lines.Length, Environment.NewLine ) );
        }

        // ##########################################################################################
        //   xxx     xxx    x   x   xxxx    xxxxx   x       xxxxx
        //  x   x   x   x   xx xx   x   x     x     x       x    
        //  x       x   x   x x x   xxxx      x     x       xxxx 
        //  x   x   x   x   x   x   x         x     x       x    
        //   xxx     xxx    x   x   x       xxxxx   xxxxx   xxxxx
        // ##########################################################################################
        public override string ToString() {
            return  senderId.ToString() + Environment.NewLine +
                    reciverId.ToString() + Environment.NewLine +
                    sendDate.ToString() + Environment.NewLine +
                    command.ToString() + Environment.NewLine +
                    message.ToString();
        }

        // ##########################################################################################
    }

    // ####################################################################################################
}
