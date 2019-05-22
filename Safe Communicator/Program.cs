using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Safe_Communicator {

    // ####################################################################################################
    //  xxxx    xxxx     xxx     xxxx   xxxx     xxx    x   x
    //  x   x   x   x   x   x   x       x   x   x   x   xx xx
    //  xxxx    xxxx    x   x   x  xx   xxxx    xxxxx   x x x
    //  x       x   x   x   x   x   x   x   x   x   x   x   x
    //  x       x   x    xxx     xxxx   x   x   x   x   x   x
    // ####################################################################################################
    public static class Program {
        
        private static  bool    enableTest  =   false;

        [STAThread]
        /// <summary> Funkcja główna, tworząca okno główne i uruchamiające wątek do jego obsługi. </summary>
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if ( enableTest )
                Application.Run( new Forms.Test() );
            else
                Application.Run( new Forms.MainForm() );
        }

    }

    // ####################################################################################################
}
