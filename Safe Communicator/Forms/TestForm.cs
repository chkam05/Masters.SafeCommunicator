using Safe_Communicator.Crypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Safe_Communicator.Forms {

    public partial class Test : Form {

        private ICrypt  ersa;
        private ICrypt  elgamal;

        #region Window Functions
        // ##########################################################################################
        public Test() {
            InitializeComponent();
        }

        // ------------------------------------------------------------------------------------------
        private void TestForm_Load( object sender, EventArgs e ) {
            ersa        =   new ERSA();
            elgamal     =   new ElGamal();
        }

        // ------------------------------------------------------------------------------------------
        private void TestForm_Shown( object sender, EventArgs e ) {
            textBoxConsole.AppendText( "Klucz publiczny RSA" + Environment.NewLine );
            textBoxConsole.AppendText( ersa.GetPublicKey() + Environment.NewLine );
            textBoxConsole.AppendText( Environment.NewLine );
            textBoxConsole.AppendText( "Klucz publiczny ElGamal" + Environment.NewLine );
            textBoxConsole.AppendText( elgamal.GetPublicKey() + Environment.NewLine );
            textBoxConsole.AppendText( Environment.NewLine );
        }

        // ------------------------------------------------------------------------------------------
        private void TestForm_FormClosing( object sender, FormClosingEventArgs e ) {
            //
        }

        // ------------------------------------------------------------------------------------------
        private void TestForm_FormClosed( object sender, FormClosedEventArgs e ) {
            //
        }

        #endregion Window Functions
        #region Buttons
        // ##########################################################################################
        private void ButtonRSACrypt_Click( object sender, EventArgs e ) {
            string result = ersa.Encrypt( textBoxRSACrypt.Text, ersa.GetPublicKey() );
            textBoxRSADecrypt.Text = result;
        }

        // ------------------------------------------------------------------------------------------
        private void ButtonRSADecrypt_Click( object sender, EventArgs e ) {
            string result = ersa.Decrypt( textBoxRSACrypt.Text );
            textBoxRSADecrypt.Text = result;
        }

        // ------------------------------------------------------------------------------------------
        private void ButtonElGamalCrypt_Click( object sender, EventArgs e ) {
            string result = elgamal.Encrypt( textBoxElGamalCrypt.Text, elgamal.GetPublicKey() );
            textBoxElGamalDecrypt.Text = result;
        }

        // ------------------------------------------------------------------------------------------
        private void ButtonElGamalDecrypt_Click( object sender, EventArgs e ) {
            string result = elgamal.Decrypt( textBoxElGamalCrypt.Text );
            textBoxElGamalDecrypt.Text = result;
        }

        #endregion Buttons
        // ##########################################################################################

    }

}
