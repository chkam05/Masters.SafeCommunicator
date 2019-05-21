using Safe_Communicator.Classes;
using Safe_Communicator.Crypt;
using Safe_Communicator.Connectors;
using Safe_Communicator.Enumerators;
using Safe_Communicator.Tool;
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

    // ####################################################################################################
    //  x   x    xxx    xxxxx   x   x      xxxxx    xxx    xxxx    x   x
    //  xx xx   x   x     x     xx  x      x       x   x   x   x   xx xx
    //  x x x   xxxxx     x     x x x      xxxx    x   x   xxxx    x x x
    //  x   x   x   x     x     x  xx      x       x   x   x   x   x   x
    //  x   x   x   x   xxxxx   x   x      x        xxx    x   x   x   x
    // ####################################################################################################
    public partial class MainForm : Form {

        private Client  client;
        private Server  server;
        private string  inside_IP   =   "127.0.0.1";
        private string  outside_IP  =   "";

        // ##########################################################################################
        //   xxx     xxx    x   x    xxxx   xxxxx   xxxx    x   x    xxx    xxxxx    xxx    xxxx 
        //  x   x   x   x   xx  x   x         x     x   x   x   x   x   x     x     x   x   x   x
        //  x       x   x   x x x    xxx      x     xxxx    x   x   x         x     x   x   xxxx 
        //  x   x   x   x   x  xx       x     x     x   x   x   x   x   x     x     x   x   x   x
        //   xxx     xxx    x   x   xxxx      x     x   x    xxx     xxx      x      xxx    x   x
        // ##########################################################################################
        public MainForm() {
            InitializeComponent();

            inside_IP   =   Tools.GetInsideIP();
            outside_IP  =   Tools.GetOutsideIP();
        }

        // ##########################################################################################
        //  x   x   xxxxx   x   x   xxxx     xxx    x   x
        //  x   x     x     xx  x    x  x   x   x   x   x
        //  x x x     x     x x x    x  x   x   x   x x x
        //  xx xx     x     x  xx    x  x   x   x   xx xx
        //  x   x   xxxxx   x   x   xxxx     xxx    x   x
        // ##########################################################################################
        private void MainForm_Load(object sender, EventArgs e) {
            labelMenuInsideIP.Text      =   inside_IP;
            labelMenuOutsideIP.Text     =   outside_IP;
            LoadData();

            panelMenu.Visible           =   true;
            panelClient.Visible         =   false;
            panelServer.Visible         =   false;
        }

        // ------------------------------------------------------------------------------------------
        private void MainForm_Shown(object sender, EventArgs e) {
            //
        }

        // ------------------------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if ( client == null ? false : client.IsActive ) {
                if ( Messages.CloseClient() ) { client.ShutDown(); }
                else { e.Cancel = true; }                
            }

            if ( server == null ? false : server.IsActive ) {
                if ( Messages.CloseServer( server.ConnectedCount ) ) { server.ShutDown(); }
                else { e.Cancel = true; }
                server.ShutDown();
            }

            SaveData();
        }

        // ------------------------------------------------------------------------------------------
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            //
        }

        // ##########################################################################################
        //  xxx     x   x   xxxxx   xxxxx    xxx    x   x    xxxx
        //  x  x    x   x     x       x     x   x   xx  x   x    
        //  xxxx    x   x     x       x     x   x   x x x    xxx 
        //  x   x   x   x     x       x     x   x   x  xx       x
        //  xxxx     xxx      x       x      xxx    x   x   xxxx 
        // ##########################################################################################
        private void buttonClient_Click(object sender, EventArgs e) {
            Errors      error_code  =   Errors.NO_ERROR;
            CryptType   ctype       =   (CryptType) comboBoxMenuCrypt.SelectedIndex;
            if ( !DataCheck.CheckLogin( textBoxMenuLogin.Text, out error_code ) ) { Messages.IncorrectLoginError( error_code ); }
            if ( !DataCheck.CheckIP( textBoxMenuIP.Text, out error_code ) ) { Messages.IncorrectIPError( error_code ); }
            if ( !DataCheck.CheckPort( textBoxMenuPort.Text, out error_code ) ) { Messages.IncorrectPortError( error_code ); }
            int.TryParse( textBoxMenuPort.Text, out int port );

            client                      =   new Client( textBoxMenuLogin.Text, textBoxMenuIP.Text, port, ctype );
            client.CliOutput            =   rTextBoxClientMessage;
            client.CliList              =   listViewClientUsers;
            client.FuncShowMessage      =   ClientShowMessage;
            client.FuncDisconnect       =   ClientDisableSend;
            client.FuncShutDown         =   ClientLogout;
            panelMenu.Visible           =   false;
            panelClient.Visible         =   true;
            menuClient.Visible          =   true;
            textBoxClient.Enabled       =   true;
            buttonClientSend.Enabled    =   true;
            rTextBoxClientMessage.Text  =   "";
            bgWorkerMenuTime.WorkerSupportsCancellation     =   true;
            bgWorkerMenuTime.RunWorkerAsync();
            client.Start();
        }

        // ------------------------------------------------------------------------------------------
        private void buttonServer_Click(object sender, EventArgs e) {
            Errors  error_code  =   Errors.NO_ERROR;
            if ( !DataCheck.CheckLogin( textBoxMenuLogin.Text, out error_code ) ) { Messages.IncorrectLoginError( error_code ); }
            if ( !DataCheck.CheckIP( textBoxMenuIP.Text, out error_code ) ) { Messages.IncorrectIPError( error_code ); }
            if ( !DataCheck.CheckPort( textBoxMenuPort.Text, out error_code ) ) { Messages.IncorrectPortError( error_code ); }
            int.TryParse( textBoxMenuPort.Text, out int port );

            server                      =   new Server( textBoxMenuLogin.Text, textBoxMenuIP.Text, port );
            server.SrvOutput            =   textBoxServerConsole;
            server.FuncShutDown         =   ServerLogout;
            panelMenu.Visible           =   false;
            panelServer.Visible         =   true;
            menuServer.Visible          =   true;
            textBoxServerConsole.Text   =   "";
            bgWorkerMenuTime.WorkerSupportsCancellation     =   true;
            bgWorkerMenuTime.RunWorkerAsync();
            server.Start();
        }

        // ##########################################################################################
        //  x   x    xxx    xxxxx   x   x      x   x   xxxxx   x   x   x   x
        //  xx xx   x   x     x     xx  x      xx xx   x       xx  x   x   x
        //  x x x   xxxxx     x     x x x      x x x   xxxx    x x x   x   x
        //  x   x   x   x     x     x  xx      x   x   x       x  xx   x   x
        //  x   x   x   x   xxxxx   x   x      x   x   xxxxx   x   x    xxx 
        // ##########################################################################################
        private void bgWorkerMenuTimeFunc(object sender, DoWorkEventArgs e) {
            while( true ) {
                DateTime    dateTime    =   DateTime.Now;
                string      result      =   string.Format( "{0}:{1}:{2}   {3} {4} {5} {6}",
                    (dateTime.Hour < 10 ? "0" + dateTime.Hour.ToString() : dateTime.Hour.ToString()),
                    (dateTime.Minute < 10 ? "0" + dateTime.Minute.ToString() : dateTime.Minute.ToString()),
                    (dateTime.Second < 10 ? "0" + dateTime.Second.ToString() : dateTime.Second.ToString()),
                    DataStatic.ShortDayOfWeek[(int) dateTime.DayOfWeek], dateTime.Day,
                    DataStatic.ShortMonthName[dateTime.Month-1], dateTime.Year );
                
                if ( menuClient.Visible ) { menuItemClientTimeDate.Text = result; }
                if ( menuServer.Visible ) { menuItemServerTimeDate.Text = result; }
                if ( ((BackgroundWorker)sender).CancellationPending ) { break; }
                Task.Delay( 1000 );
            }
        }

        // ------------------------------------------------------------------------------------------
        private void ClientLogout(object sender, EventArgs e) {
            client.ShutDown();
            bgWorkerMenuTime.CancelAsync();
            menuClient.Visible      =   false;
            panelClient.Visible     =   false;
            panelMenu.Visible       =   true;
            client                  =   null;
        }

        // ------------------------------------------------------------------------------------------
        private void ServerLogout(object sender, EventArgs e) {
            server.ShutDown();
            bgWorkerMenuTime.CancelAsync();
            menuServer.Visible      =   false;
            panelServer.Visible     =   false;
            panelMenu.Visible       =   true;
            server                  =   null;
        }

        // ------------------------------------------------------------------------------------------
        private void Shutdown(object sender, EventArgs e) {
            this.Close();
            Application.Exit();
        }

        // ##########################################################################################
        //   xxxx   xxxxx   xxxx    x   x   xxxxx   xxxx 
        //  x       x       x   x   x   x   x       x   x
        //   xxx    xxxx    xxxx    x   x   xxxx    xxxx 
        //      x   x       x   x    x x    x       x   x
        //  xxxx    xxxxx   x   x     x     xxxxx   x   x
        // ##########################################################################################
        private void ServerSendMessage(object sender, EventArgs e) {
            string  message     =   textBoxServer.Text;
            textBoxServer.Text  =   "";

            ServerShowMessage( message );
            ServerSendMessage( message );
        }

        // ------------------------------------------------------------------------------------------
        private void ServerEnterMessage(object sender, KeyEventArgs e) {
            if ( e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter ) {
                //  Nothing to do
            } else if ( e.KeyCode == Keys.Enter ) {
                ServerSendMessage( sender, null );
                e.Handled = true;
            }
        }

        // ------------------------------------------------------------------------------------------
        private void ServerShowMessage( string message ) {
            textBoxServerConsole.AppendText( message + Environment.NewLine );
        }

        // ------------------------------------------------------------------------------------------
        private void ServerSendMessage( string message ) {
            server.SendCommand( message );
        }

        // ##########################################################################################
        //   xxx    x       xxxxx   xxxxx   x   x   xxxxx
        //  x   x   x         x     x       xx  x     x  
        //  x       x         x     xxxx    x x x     x  
        //  x   x   x         x     x       x  xx     x  
        //   xxx    xxxxx   xxxxx   xxxxx   x   x     x  
        // ##########################################################################################
        private void ClientSendMessage(object sender, EventArgs e) {
            string  message     =   textBoxClient.Text;
            textBoxClient.Text  =   "";

            if ( !client.IsConnected ) {
                ClientShowMessage( "( ! ) Client Disconnected, try re-connect from main menu.", MessageModifier.SERVER );
            }

            ClientSendMessage( message );
        }

        // -----------------------------------------------------------------------------------------
        private void ClientEnterMessage(object sender, KeyEventArgs e) {
            if ( e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter ) {
                //  Nothing to do
            } else if ( e.KeyCode == Keys.Enter ) {
                ClientSendMessage( sender, null );
                e.Handled = true;
            }
        }

        // -----------------------------------------------------------------------------------------
        private void ClientShowMessage( string message, MessageModifier modifier ) {
            rTextBoxClientMessage.Select( rTextBoxClientMessage.Text.Length, 0 );

            switch( modifier ) {
                case MessageModifier.INCOMING:
                    rTextBoxClientMessage.SelectionBackColor    =   Color.White;
                    rTextBoxClientMessage.SelectionColor        =   Color.Black;
                    rTextBoxClientMessage.SelectionAlignment    =   HorizontalAlignment.Left;
                    break;
                
                case MessageModifier.INFORMATION:
                    rTextBoxClientMessage.SelectionBackColor    =   Color.LawnGreen;
                    rTextBoxClientMessage.SelectionColor        =   Color.Black;
                    rTextBoxClientMessage.SelectionAlignment    =   HorizontalAlignment.Left;
                    break;

                case MessageModifier.OUTGOING:
                    rTextBoxClientMessage.SelectionBackColor    =   Color.DodgerBlue;
                    rTextBoxClientMessage.SelectionColor        =   Color.White;
                    rTextBoxClientMessage.SelectionAlignment    =   HorizontalAlignment.Right;
                    break;

                case MessageModifier.SERVER:
                    rTextBoxClientMessage.SelectionBackColor    =   Color.Pink;
                    rTextBoxClientMessage.SelectionColor        =   Color.Red;
                    rTextBoxClientMessage.SelectionAlignment    =   HorizontalAlignment.Left;
                    break;

            }

            rTextBoxClientMessage.AppendText( message + Environment.NewLine );
            rTextBoxClientMessage.SelectionBackColor    =   SystemColors.Control;
            rTextBoxClientMessage.SelectionColor        =   Color.Black;
        }

        // ------------------------------------------------------------------------------------------
        private void ClientSendMessage( string message ) {
            client.SendCommand(
                (message.EndsWith( Environment.NewLine )) ? message.Substring(0,message.Length-2) : message
            );
        }

        // ------------------------------------------------------------------------------------------
        private void ClientSelectReciver(object sender, EventArgs e) {
            if ( sender == null ) { return; }
            if ( ((ListView) sender).SelectedItems.Count <= 0 ) { return; }

            ListViewItem    item    =   ((ListView) sender).SelectedItems[0];
            if ( int.TryParse( item.Text, out int index ) ) {
                if ( client != null ) { client.Sender = index; }
            }
        }

        // ------------------------------------------------------------------------------------------
        private void ClientDisableSend(object sender, EventArgs e) {
            listViewClientUsers.Items.Clear();
            textBoxClient.Enabled       =   false;
            buttonClientSend.Enabled    =   false;
        }

        // ##########################################################################################
        //   xxxx    xxx    x   x   xxxxx      xxxx     xxx    xxxxx    xxx 
        //  x       x   x   x   x   x           x  x   x   x     x     x   x
        //   xxx    xxxxx   x   x   xxxx        x  x   xxxxx     x     xxxxx
        //      x   x   x    x x    x           x  x   x   x     x     x   x
        //  xxxx    x   x     x     xxxxx      xxxx    x   x     x     x   x
        // ##########################################################################################
        private void SaveData() {
            var data = Properties.Settings.Default;
            data.NickName   =   textBoxMenuLogin.Text;
            data.IPAddress  =   textBoxMenuIP.Text;
            data.Port       =   textBoxMenuPort.Text;
            data.Crypt      =   comboBoxMenuCrypt.SelectedIndex;
            data.Save();
        }

        // ------------------------------------------------------------------------------------------
        private void LoadData() {
            var data = Properties.Settings.Default;

            if ( data.Saved ) {
                textBoxMenuLogin.Text               =   data.NickName;
                textBoxMenuIP.Text                  =   data.IPAddress;
                textBoxMenuPort.Text                =   data.Port;
                comboBoxMenuCrypt.SelectedIndex     =   data.Crypt;
            } else {
                comboBoxMenuCrypt.SelectedIndex     =   0;
                textBoxMenuIP.Text                  =   inside_IP;
            }
        }

        // ##########################################################################################

    }

    // ####################################################################################################
}
