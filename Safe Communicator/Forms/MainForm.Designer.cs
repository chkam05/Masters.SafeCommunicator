using System;

namespace Safe_Communicator.Forms {

    partial class MainForm {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            try { base.Dispose(disposing); }
            catch ( Exception ) { return; }
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "Server",
            "127.0.0.1"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuClient = new System.Windows.Forms.MenuStrip();
            this.menuItemClient = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClientBack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClientClsoe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClientTimeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServer = new System.Windows.Forms.MenuStrip();
            this.menuItemServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemServerBack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemServerClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemServerTimeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.panelServer = new System.Windows.Forms.Panel();
            this.textBoxServerConsole = new System.Windows.Forms.TextBox();
            this.panelServerConsole = new System.Windows.Forms.Panel();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.buttonConsoleCommit = new System.Windows.Forms.Button();
            this.panelClient = new System.Windows.Forms.Panel();
            this.panelClientMessage = new System.Windows.Forms.Panel();
            this.rTextBoxClientMessage = new System.Windows.Forms.RichTextBox();
            this.labelClientTitle = new System.Windows.Forms.Label();
            this.panelClientEdit = new System.Windows.Forms.Panel();
            this.textBoxClient = new System.Windows.Forms.TextBox();
            this.buttonClientSend = new System.Windows.Forms.Button();
            this.panelClientUsers = new System.Windows.Forms.Panel();
            this.listViewClientUsers = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelClientUsers = new System.Windows.Forms.Label();
            this.fLPanelClientControl = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonServer = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelMenuInfo = new System.Windows.Forms.Panel();
            this.textBoxMenuInfo = new System.Windows.Forms.TextBox();
            this.fLPanelMenuData = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMenuTitle = new System.Windows.Forms.Label();
            this.labelMenuLogin = new System.Windows.Forms.Label();
            this.textBoxMenuLogin = new System.Windows.Forms.TextBox();
            this.labelMenuIP = new System.Windows.Forms.Label();
            this.textBoxMenuIP = new System.Windows.Forms.TextBox();
            this.labelMenuPort = new System.Windows.Forms.Label();
            this.textBoxMenuPort = new System.Windows.Forms.TextBox();
            this.labelMenuInsideIPTitle = new System.Windows.Forms.Label();
            this.labelMenuInsideIP = new System.Windows.Forms.Label();
            this.labelMenuOutsideIPTitle = new System.Windows.Forms.Label();
            this.labelMenuOutsideIP = new System.Windows.Forms.Label();
            this.tLPanelMenuLaunch = new System.Windows.Forms.TableLayoutPanel();
            this.buttonClient = new System.Windows.Forms.Button();
            this.bgWorkerMenuTime = new System.ComponentModel.BackgroundWorker();
            this.labelMenuCrypt = new System.Windows.Forms.Label();
            this.comboBoxMenuCrypt = new System.Windows.Forms.ComboBox();
            this.menuClient.SuspendLayout();
            this.menuServer.SuspendLayout();
            this.panelServer.SuspendLayout();
            this.panelServerConsole.SuspendLayout();
            this.panelClient.SuspendLayout();
            this.panelClientMessage.SuspendLayout();
            this.panelClientEdit.SuspendLayout();
            this.panelClientUsers.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelMenuInfo.SuspendLayout();
            this.fLPanelMenuData.SuspendLayout();
            this.tLPanelMenuLaunch.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuClient
            // 
            this.menuClient.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemClient,
            this.menuItemClientTimeDate});
            this.menuClient.Location = new System.Drawing.Point(0, 0);
            this.menuClient.Name = "menuClient";
            this.menuClient.Size = new System.Drawing.Size(784, 24);
            this.menuClient.TabIndex = 0;
            this.menuClient.Text = "menuStrip1";
            this.menuClient.Visible = false;
            // 
            // menuItemClient
            // 
            this.menuItemClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemClientBack,
            this.menuItemClientClsoe});
            this.menuItemClient.Name = "menuItemClient";
            this.menuItemClient.Size = new System.Drawing.Size(50, 20);
            this.menuItemClient.Text = "Client";
            // 
            // menuItemClientBack
            // 
            this.menuItemClientBack.Name = "menuItemClientBack";
            this.menuItemClientBack.Size = new System.Drawing.Size(118, 22);
            this.menuItemClientBack.Text = "Wyloguj";
            this.menuItemClientBack.Click += new System.EventHandler(this.ClientLogout);
            // 
            // menuItemClientClsoe
            // 
            this.menuItemClientClsoe.Name = "menuItemClientClsoe";
            this.menuItemClientClsoe.Size = new System.Drawing.Size(118, 22);
            this.menuItemClientClsoe.Text = "Zamknij";
            this.menuItemClientClsoe.Click += new System.EventHandler(this.Shutdown);
            // 
            // menuItemClientTimeDate
            // 
            this.menuItemClientTimeDate.Enabled = false;
            this.menuItemClientTimeDate.Name = "menuItemClientTimeDate";
            this.menuItemClientTimeDate.Size = new System.Drawing.Size(103, 20);
            this.menuItemClientTimeDate.Text = "00:00 01.01.1970";
            // 
            // menuServer
            // 
            this.menuServer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemServer,
            this.menuItemServerTimeDate});
            this.menuServer.Location = new System.Drawing.Point(0, 0);
            this.menuServer.Name = "menuServer";
            this.menuServer.Size = new System.Drawing.Size(784, 24);
            this.menuServer.TabIndex = 1;
            this.menuServer.Text = "menuStrip2";
            this.menuServer.Visible = false;
            // 
            // menuItemServer
            // 
            this.menuItemServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemServerBack,
            this.menuItemServerClose});
            this.menuItemServer.Name = "menuItemServer";
            this.menuItemServer.Size = new System.Drawing.Size(51, 20);
            this.menuItemServer.Text = "Server";
            // 
            // menuItemServerBack
            // 
            this.menuItemServerBack.Name = "menuItemServerBack";
            this.menuItemServerBack.Size = new System.Drawing.Size(148, 22);
            this.menuItemServerBack.Text = "Menu Główne";
            this.menuItemServerBack.Click += new System.EventHandler(this.ServerLogout);
            // 
            // menuItemServerClose
            // 
            this.menuItemServerClose.Name = "menuItemServerClose";
            this.menuItemServerClose.Size = new System.Drawing.Size(148, 22);
            this.menuItemServerClose.Text = "Zamknij";
            this.menuItemServerClose.Click += new System.EventHandler(this.Shutdown);
            // 
            // menuItemServerTimeDate
            // 
            this.menuItemServerTimeDate.Enabled = false;
            this.menuItemServerTimeDate.Name = "menuItemServerTimeDate";
            this.menuItemServerTimeDate.Size = new System.Drawing.Size(103, 20);
            this.menuItemServerTimeDate.Text = "00:00 01.01.1970";
            // 
            // panelServer
            // 
            this.panelServer.Controls.Add(this.textBoxServerConsole);
            this.panelServer.Controls.Add(this.panelServerConsole);
            this.panelServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelServer.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.panelServer.Location = new System.Drawing.Point(0, 0);
            this.panelServer.Margin = new System.Windows.Forms.Padding(0);
            this.panelServer.Name = "panelServer";
            this.panelServer.Size = new System.Drawing.Size(784, 441);
            this.panelServer.TabIndex = 0;
            // 
            // textBoxServerConsole
            // 
            this.textBoxServerConsole.BackColor = System.Drawing.Color.Black;
            this.textBoxServerConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxServerConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxServerConsole.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxServerConsole.ForeColor = System.Drawing.Color.White;
            this.textBoxServerConsole.Location = new System.Drawing.Point(0, 0);
            this.textBoxServerConsole.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxServerConsole.Multiline = true;
            this.textBoxServerConsole.Name = "textBoxServerConsole";
            this.textBoxServerConsole.ReadOnly = true;
            this.textBoxServerConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxServerConsole.Size = new System.Drawing.Size(784, 417);
            this.textBoxServerConsole.TabIndex = 0;
            this.textBoxServerConsole.Text = "Safe Communicator 1.0 Console\r\nAgata Dziurka & Kamil Karpiński Copyright\r\n";
            // 
            // panelServerConsole
            // 
            this.panelServerConsole.BackColor = System.Drawing.SystemColors.Control;
            this.panelServerConsole.Controls.Add(this.textBoxServer);
            this.panelServerConsole.Controls.Add(this.buttonConsoleCommit);
            this.panelServerConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelServerConsole.ForeColor = System.Drawing.Color.White;
            this.panelServerConsole.Location = new System.Drawing.Point(0, 417);
            this.panelServerConsole.Margin = new System.Windows.Forms.Padding(0);
            this.panelServerConsole.Name = "panelServerConsole";
            this.panelServerConsole.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panelServerConsole.Size = new System.Drawing.Size(784, 24);
            this.panelServerConsole.TabIndex = 1;
            // 
            // textBoxServer
            // 
            this.textBoxServer.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxServer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxServer.ForeColor = System.Drawing.Color.Black;
            this.textBoxServer.Location = new System.Drawing.Point(2, 0);
            this.textBoxServer.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(684, 20);
            this.textBoxServer.TabIndex = 1;
            this.textBoxServer.Text = "/command";
            this.textBoxServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerEnterMessage);
            // 
            // buttonConsoleCommit
            // 
            this.buttonConsoleCommit.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonConsoleCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConsoleCommit.ForeColor = System.Drawing.Color.Black;
            this.buttonConsoleCommit.Location = new System.Drawing.Point(686, 0);
            this.buttonConsoleCommit.Margin = new System.Windows.Forms.Padding(0);
            this.buttonConsoleCommit.Name = "buttonConsoleCommit";
            this.buttonConsoleCommit.Size = new System.Drawing.Size(96, 24);
            this.buttonConsoleCommit.TabIndex = 0;
            this.buttonConsoleCommit.Text = "Commit";
            this.buttonConsoleCommit.UseVisualStyleBackColor = true;
            this.buttonConsoleCommit.Click += new System.EventHandler(this.ServerSendMessage);
            // 
            // panelClient
            // 
            this.panelClient.Controls.Add(this.panelClientMessage);
            this.panelClient.Controls.Add(this.panelClientUsers);
            this.panelClient.Controls.Add(this.fLPanelClientControl);
            this.panelClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClient.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.panelClient.Location = new System.Drawing.Point(0, 0);
            this.panelClient.Margin = new System.Windows.Forms.Padding(0);
            this.panelClient.Name = "panelClient";
            this.panelClient.Size = new System.Drawing.Size(784, 441);
            this.panelClient.TabIndex = 0;
            // 
            // panelClientMessage
            // 
            this.panelClientMessage.Controls.Add(this.rTextBoxClientMessage);
            this.panelClientMessage.Controls.Add(this.labelClientTitle);
            this.panelClientMessage.Controls.Add(this.panelClientEdit);
            this.panelClientMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClientMessage.Location = new System.Drawing.Point(256, 32);
            this.panelClientMessage.Margin = new System.Windows.Forms.Padding(0);
            this.panelClientMessage.Name = "panelClientMessage";
            this.panelClientMessage.Size = new System.Drawing.Size(528, 409);
            this.panelClientMessage.TabIndex = 6;
            // 
            // rTextBoxClientMessage
            // 
            this.rTextBoxClientMessage.BackColor = System.Drawing.SystemColors.Control;
            this.rTextBoxClientMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTextBoxClientMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTextBoxClientMessage.Location = new System.Drawing.Point(0, 32);
            this.rTextBoxClientMessage.Margin = new System.Windows.Forms.Padding(0);
            this.rTextBoxClientMessage.Name = "rTextBoxClientMessage";
            this.rTextBoxClientMessage.ReadOnly = true;
            this.rTextBoxClientMessage.Size = new System.Drawing.Size(528, 329);
            this.rTextBoxClientMessage.TabIndex = 1;
            this.rTextBoxClientMessage.Text = "";
            // 
            // labelClientTitle
            // 
            this.labelClientTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelClientTitle.Location = new System.Drawing.Point(0, 0);
            this.labelClientTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelClientTitle.Name = "labelClientTitle";
            this.labelClientTitle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.labelClientTitle.Size = new System.Drawing.Size(528, 32);
            this.labelClientTitle.TabIndex = 5;
            this.labelClientTitle.Text = "Rozmowa z użytkownikiem:";
            this.labelClientTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelClientEdit
            // 
            this.panelClientEdit.Controls.Add(this.textBoxClient);
            this.panelClientEdit.Controls.Add(this.buttonClientSend);
            this.panelClientEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelClientEdit.Location = new System.Drawing.Point(0, 361);
            this.panelClientEdit.Margin = new System.Windows.Forms.Padding(0);
            this.panelClientEdit.Name = "panelClientEdit";
            this.panelClientEdit.Size = new System.Drawing.Size(528, 48);
            this.panelClientEdit.TabIndex = 3;
            // 
            // textBoxClient
            // 
            this.textBoxClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxClient.Location = new System.Drawing.Point(0, 0);
            this.textBoxClient.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxClient.Multiline = true;
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.Size = new System.Drawing.Size(456, 48);
            this.textBoxClient.TabIndex = 0;
            this.textBoxClient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClientEnterMessage);
            // 
            // buttonClientSend
            // 
            this.buttonClientSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClientSend.Location = new System.Drawing.Point(456, 0);
            this.buttonClientSend.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClientSend.Name = "buttonClientSend";
            this.buttonClientSend.Size = new System.Drawing.Size(72, 48);
            this.buttonClientSend.TabIndex = 1;
            this.buttonClientSend.Text = "Wyślij";
            this.buttonClientSend.UseVisualStyleBackColor = true;
            this.buttonClientSend.Click += new System.EventHandler(this.ClientSendMessage);
            // 
            // panelClientUsers
            // 
            this.panelClientUsers.Controls.Add(this.listViewClientUsers);
            this.panelClientUsers.Controls.Add(this.labelClientUsers);
            this.panelClientUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelClientUsers.Location = new System.Drawing.Point(0, 32);
            this.panelClientUsers.Margin = new System.Windows.Forms.Padding(0);
            this.panelClientUsers.Name = "panelClientUsers";
            this.panelClientUsers.Size = new System.Drawing.Size(256, 409);
            this.panelClientUsers.TabIndex = 2;
            // 
            // listViewClientUsers
            // 
            this.listViewClientUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colName,
            this.colIP});
            this.listViewClientUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewClientUsers.FullRowSelect = true;
            this.listViewClientUsers.GridLines = true;
            this.listViewClientUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewClientUsers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.listViewClientUsers.Location = new System.Drawing.Point(0, 32);
            this.listViewClientUsers.Margin = new System.Windows.Forms.Padding(0);
            this.listViewClientUsers.MultiSelect = false;
            this.listViewClientUsers.Name = "listViewClientUsers";
            this.listViewClientUsers.ShowGroups = false;
            this.listViewClientUsers.Size = new System.Drawing.Size(256, 377);
            this.listViewClientUsers.TabIndex = 9;
            this.listViewClientUsers.UseCompatibleStateImageBehavior = false;
            this.listViewClientUsers.View = System.Windows.Forms.View.Details;
            this.listViewClientUsers.SelectedIndexChanged += new System.EventHandler(this.ClientSelectReciver);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 48;
            // 
            // colName
            // 
            this.colName.Width = 128;
            // 
            // colIP
            // 
            this.colIP.Text = "IP";
            this.colIP.Width = 128;
            // 
            // labelClientUsers
            // 
            this.labelClientUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelClientUsers.Location = new System.Drawing.Point(0, 0);
            this.labelClientUsers.Margin = new System.Windows.Forms.Padding(0);
            this.labelClientUsers.Name = "labelClientUsers";
            this.labelClientUsers.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.labelClientUsers.Size = new System.Drawing.Size(256, 32);
            this.labelClientUsers.TabIndex = 6;
            this.labelClientUsers.Text = "Rozmowa z użytkownikiem:";
            this.labelClientUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fLPanelClientControl
            // 
            this.fLPanelClientControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.fLPanelClientControl.Location = new System.Drawing.Point(0, 0);
            this.fLPanelClientControl.Margin = new System.Windows.Forms.Padding(0);
            this.fLPanelClientControl.Name = "fLPanelClientControl";
            this.fLPanelClientControl.Size = new System.Drawing.Size(784, 32);
            this.fLPanelClientControl.TabIndex = 0;
            // 
            // buttonServer
            // 
            this.buttonServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonServer.Location = new System.Drawing.Point(396, 4);
            this.buttonServer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonServer.Name = "buttonServer";
            this.buttonServer.Size = new System.Drawing.Size(120, 56);
            this.buttonServer.TabIndex = 1;
            this.buttonServer.Text = "Uruchom Serwer";
            this.buttonServer.UseVisualStyleBackColor = true;
            this.buttonServer.Click += new System.EventHandler(this.buttonServer_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.panelMenuInfo);
            this.panelMenu.Controls.Add(this.fLPanelMenuData);
            this.panelMenu.Controls.Add(this.tLPanelMenuLaunch);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(784, 441);
            this.panelMenu.TabIndex = 2;
            // 
            // panelMenuInfo
            // 
            this.panelMenuInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuInfo.Controls.Add(this.textBoxMenuInfo);
            this.panelMenuInfo.Location = new System.Drawing.Point(396, 20);
            this.panelMenuInfo.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenuInfo.Name = "panelMenuInfo";
            this.panelMenuInfo.Size = new System.Drawing.Size(367, 337);
            this.panelMenuInfo.TabIndex = 1;
            // 
            // textBoxMenuInfo
            // 
            this.textBoxMenuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMenuInfo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMenuInfo.Location = new System.Drawing.Point(0, 0);
            this.textBoxMenuInfo.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxMenuInfo.Multiline = true;
            this.textBoxMenuInfo.Name = "textBoxMenuInfo";
            this.textBoxMenuInfo.ReadOnly = true;
            this.textBoxMenuInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMenuInfo.Size = new System.Drawing.Size(367, 337);
            this.textBoxMenuInfo.TabIndex = 0;
            this.textBoxMenuInfo.Text = resources.GetString("textBoxMenuInfo.Text");
            // 
            // fLPanelMenuData
            // 
            this.fLPanelMenuData.AutoScroll = true;
            this.fLPanelMenuData.BackColor = System.Drawing.Color.Black;
            this.fLPanelMenuData.BackgroundImage = global::Safe_Communicator.Properties.Resources._411175;
            this.fLPanelMenuData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fLPanelMenuData.Controls.Add(this.labelMenuTitle);
            this.fLPanelMenuData.Controls.Add(this.labelMenuLogin);
            this.fLPanelMenuData.Controls.Add(this.textBoxMenuLogin);
            this.fLPanelMenuData.Controls.Add(this.labelMenuIP);
            this.fLPanelMenuData.Controls.Add(this.textBoxMenuIP);
            this.fLPanelMenuData.Controls.Add(this.labelMenuPort);
            this.fLPanelMenuData.Controls.Add(this.textBoxMenuPort);
            this.fLPanelMenuData.Controls.Add(this.labelMenuCrypt);
            this.fLPanelMenuData.Controls.Add(this.comboBoxMenuCrypt);
            this.fLPanelMenuData.Controls.Add(this.labelMenuInsideIPTitle);
            this.fLPanelMenuData.Controls.Add(this.labelMenuInsideIP);
            this.fLPanelMenuData.Controls.Add(this.labelMenuOutsideIPTitle);
            this.fLPanelMenuData.Controls.Add(this.labelMenuOutsideIP);
            this.fLPanelMenuData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLPanelMenuData.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPanelMenuData.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fLPanelMenuData.ForeColor = System.Drawing.Color.White;
            this.fLPanelMenuData.Location = new System.Drawing.Point(0, 0);
            this.fLPanelMenuData.Margin = new System.Windows.Forms.Padding(0);
            this.fLPanelMenuData.Name = "fLPanelMenuData";
            this.fLPanelMenuData.Size = new System.Drawing.Size(784, 377);
            this.fLPanelMenuData.TabIndex = 0;
            this.fLPanelMenuData.WrapContents = false;
            // 
            // labelMenuTitle
            // 
            this.labelMenuTitle.AutoSize = true;
            this.labelMenuTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMenuTitle.ForeColor = System.Drawing.Color.White;
            this.labelMenuTitle.Location = new System.Drawing.Point(24, 24);
            this.labelMenuTitle.Margin = new System.Windows.Forms.Padding(24, 24, 0, 32);
            this.labelMenuTitle.Name = "labelMenuTitle";
            this.labelMenuTitle.Size = new System.Drawing.Size(241, 25);
            this.labelMenuTitle.TabIndex = 0;
            this.labelMenuTitle.Text = "Safe Communicator";
            // 
            // labelMenuLogin
            // 
            this.labelMenuLogin.AutoSize = true;
            this.labelMenuLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuLogin.ForeColor = System.Drawing.Color.White;
            this.labelMenuLogin.Location = new System.Drawing.Point(24, 81);
            this.labelMenuLogin.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.labelMenuLogin.Name = "labelMenuLogin";
            this.labelMenuLogin.Size = new System.Drawing.Size(159, 18);
            this.labelMenuLogin.TabIndex = 1;
            this.labelMenuLogin.Text = "Nazwa użytkownika";
            // 
            // textBoxMenuLogin
            // 
            this.textBoxMenuLogin.Location = new System.Drawing.Point(24, 99);
            this.textBoxMenuLogin.Margin = new System.Windows.Forms.Padding(24, 0, 0, 16);
            this.textBoxMenuLogin.Name = "textBoxMenuLogin";
            this.textBoxMenuLogin.Size = new System.Drawing.Size(256, 26);
            this.textBoxMenuLogin.TabIndex = 2;
            this.textBoxMenuLogin.Text = "User";
            // 
            // labelMenuIP
            // 
            this.labelMenuIP.AutoSize = true;
            this.labelMenuIP.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuIP.ForeColor = System.Drawing.Color.White;
            this.labelMenuIP.Location = new System.Drawing.Point(24, 141);
            this.labelMenuIP.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.labelMenuIP.Name = "labelMenuIP";
            this.labelMenuIP.Size = new System.Drawing.Size(242, 18);
            this.labelMenuIP.TabIndex = 3;
            this.labelMenuIP.Text = "Adres Protokołu Internetowego";
            // 
            // textBoxMenuIP
            // 
            this.textBoxMenuIP.Location = new System.Drawing.Point(24, 159);
            this.textBoxMenuIP.Margin = new System.Windows.Forms.Padding(24, 0, 0, 16);
            this.textBoxMenuIP.Name = "textBoxMenuIP";
            this.textBoxMenuIP.Size = new System.Drawing.Size(256, 26);
            this.textBoxMenuIP.TabIndex = 4;
            this.textBoxMenuIP.Text = "127.0.0.1";
            // 
            // labelMenuPort
            // 
            this.labelMenuPort.AutoSize = true;
            this.labelMenuPort.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuPort.ForeColor = System.Drawing.Color.White;
            this.labelMenuPort.Location = new System.Drawing.Point(24, 201);
            this.labelMenuPort.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.labelMenuPort.Name = "labelMenuPort";
            this.labelMenuPort.Size = new System.Drawing.Size(203, 18);
            this.labelMenuPort.TabIndex = 5;
            this.labelMenuPort.Text = "Port protokołu sieciowego";
            // 
            // textBoxMenuPort
            // 
            this.textBoxMenuPort.Location = new System.Drawing.Point(24, 219);
            this.textBoxMenuPort.Margin = new System.Windows.Forms.Padding(24, 0, 0, 16);
            this.textBoxMenuPort.Name = "textBoxMenuPort";
            this.textBoxMenuPort.Size = new System.Drawing.Size(256, 26);
            this.textBoxMenuPort.TabIndex = 6;
            this.textBoxMenuPort.Text = "65534";
            // 
            // labelMenuInsideIPTitle
            // 
            this.labelMenuInsideIPTitle.AutoSize = true;
            this.labelMenuInsideIPTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuInsideIPTitle.ForeColor = System.Drawing.Color.White;
            this.labelMenuInsideIPTitle.Location = new System.Drawing.Point(24, 321);
            this.labelMenuInsideIPTitle.Margin = new System.Windows.Forms.Padding(24, 0, 0, 2);
            this.labelMenuInsideIPTitle.Name = "labelMenuInsideIPTitle";
            this.labelMenuInsideIPTitle.Size = new System.Drawing.Size(166, 18);
            this.labelMenuInsideIPTitle.TabIndex = 7;
            this.labelMenuInsideIPTitle.Text = "Wewnętrzny adres IP";
            // 
            // labelMenuInsideIP
            // 
            this.labelMenuInsideIP.AutoSize = true;
            this.labelMenuInsideIP.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuInsideIP.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMenuInsideIP.ForeColor = System.Drawing.Color.White;
            this.labelMenuInsideIP.Location = new System.Drawing.Point(24, 341);
            this.labelMenuInsideIP.Margin = new System.Windows.Forms.Padding(24, 0, 0, 16);
            this.labelMenuInsideIP.Name = "labelMenuInsideIP";
            this.labelMenuInsideIP.Size = new System.Drawing.Size(89, 18);
            this.labelMenuInsideIP.TabIndex = 8;
            this.labelMenuInsideIP.Text = "127.0.0.1";
            // 
            // labelMenuOutsideIPTitle
            // 
            this.labelMenuOutsideIPTitle.AutoSize = true;
            this.labelMenuOutsideIPTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuOutsideIPTitle.ForeColor = System.Drawing.Color.White;
            this.labelMenuOutsideIPTitle.Location = new System.Drawing.Point(24, 375);
            this.labelMenuOutsideIPTitle.Margin = new System.Windows.Forms.Padding(24, 0, 0, 2);
            this.labelMenuOutsideIPTitle.Name = "labelMenuOutsideIPTitle";
            this.labelMenuOutsideIPTitle.Size = new System.Drawing.Size(162, 18);
            this.labelMenuOutsideIPTitle.TabIndex = 9;
            this.labelMenuOutsideIPTitle.Text = "Zewnętrzny adres IP";
            // 
            // labelMenuOutsideIP
            // 
            this.labelMenuOutsideIP.AutoSize = true;
            this.labelMenuOutsideIP.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuOutsideIP.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMenuOutsideIP.ForeColor = System.Drawing.Color.White;
            this.labelMenuOutsideIP.Location = new System.Drawing.Point(24, 395);
            this.labelMenuOutsideIP.Margin = new System.Windows.Forms.Padding(24, 0, 0, 24);
            this.labelMenuOutsideIP.Name = "labelMenuOutsideIP";
            this.labelMenuOutsideIP.Size = new System.Drawing.Size(67, 18);
            this.labelMenuOutsideIP.TabIndex = 10;
            this.labelMenuOutsideIP.Text = "0.0.0.0";
            // 
            // tLPanelMenuLaunch
            // 
            this.tLPanelMenuLaunch.ColumnCount = 4;
            this.tLPanelMenuLaunch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLPanelMenuLaunch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tLPanelMenuLaunch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tLPanelMenuLaunch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLPanelMenuLaunch.Controls.Add(this.buttonClient, 1, 0);
            this.tLPanelMenuLaunch.Controls.Add(this.buttonServer, 2, 0);
            this.tLPanelMenuLaunch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tLPanelMenuLaunch.Location = new System.Drawing.Point(0, 377);
            this.tLPanelMenuLaunch.Margin = new System.Windows.Forms.Padding(0);
            this.tLPanelMenuLaunch.Name = "tLPanelMenuLaunch";
            this.tLPanelMenuLaunch.RowCount = 1;
            this.tLPanelMenuLaunch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPanelMenuLaunch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tLPanelMenuLaunch.Size = new System.Drawing.Size(784, 64);
            this.tLPanelMenuLaunch.TabIndex = 0;
            // 
            // buttonClient
            // 
            this.buttonClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClient.Location = new System.Drawing.Point(268, 4);
            this.buttonClient.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(120, 56);
            this.buttonClient.TabIndex = 2;
            this.buttonClient.Text = "Uruchom Klienta";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new System.EventHandler(this.buttonClient_Click);
            // 
            // bgWorkerMenuTime
            // 
            this.bgWorkerMenuTime.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerMenuTimeFunc);
            // 
            // labelMenuCrypt
            // 
            this.labelMenuCrypt.AutoSize = true;
            this.labelMenuCrypt.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuCrypt.ForeColor = System.Drawing.Color.White;
            this.labelMenuCrypt.Location = new System.Drawing.Point(24, 261);
            this.labelMenuCrypt.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.labelMenuCrypt.Name = "labelMenuCrypt";
            this.labelMenuCrypt.Size = new System.Drawing.Size(155, 18);
            this.labelMenuCrypt.TabIndex = 11;
            this.labelMenuCrypt.Text = "Rodzaj szyfrowania";
            // 
            // comboBoxMenuCrypt
            // 
            this.comboBoxMenuCrypt.FormattingEnabled = true;
            this.comboBoxMenuCrypt.Items.AddRange(new object[] {
            "Brak szyfrowania",
            "Szyfrowanie RSA",
            "Szyfrowanie ElGamal\'a"});
            this.comboBoxMenuCrypt.Location = new System.Drawing.Point(24, 279);
            this.comboBoxMenuCrypt.Margin = new System.Windows.Forms.Padding(24, 0, 0, 16);
            this.comboBoxMenuCrypt.Name = "comboBoxMenuCrypt";
            this.comboBoxMenuCrypt.Size = new System.Drawing.Size(256, 26);
            this.comboBoxMenuCrypt.TabIndex = 13;
            this.comboBoxMenuCrypt.Text = "Brak szyfrowania";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelServer);
            this.Controls.Add(this.panelClient);
            this.Controls.Add(this.menuClient);
            this.Controls.Add(this.menuServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuClient;
            this.MinimumSize = new System.Drawing.Size(399, 398);
            this.Name = "MainForm";
            this.Text = "Safe Communicator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuClient.ResumeLayout(false);
            this.menuClient.PerformLayout();
            this.menuServer.ResumeLayout(false);
            this.menuServer.PerformLayout();
            this.panelServer.ResumeLayout(false);
            this.panelServer.PerformLayout();
            this.panelServerConsole.ResumeLayout(false);
            this.panelServerConsole.PerformLayout();
            this.panelClient.ResumeLayout(false);
            this.panelClientMessage.ResumeLayout(false);
            this.panelClientEdit.ResumeLayout(false);
            this.panelClientEdit.PerformLayout();
            this.panelClientUsers.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelMenuInfo.ResumeLayout(false);
            this.panelMenuInfo.PerformLayout();
            this.fLPanelMenuData.ResumeLayout(false);
            this.fLPanelMenuData.PerformLayout();
            this.tLPanelMenuLaunch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuClient;
        private System.Windows.Forms.MenuStrip menuServer;
        private System.Windows.Forms.Panel panelServer;
        private System.Windows.Forms.Panel panelClient;
        private System.Windows.Forms.ToolStripMenuItem menuItemServer;
        private System.Windows.Forms.ToolStripMenuItem menuItemClient;
        private System.Windows.Forms.Button buttonServer;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.FlowLayoutPanel fLPanelMenuData;
        private System.Windows.Forms.TableLayoutPanel tLPanelMenuLaunch;
        private System.Windows.Forms.Label labelMenuTitle;
        private System.Windows.Forms.Label labelMenuLogin;
        private System.Windows.Forms.TextBox textBoxMenuLogin;
        private System.Windows.Forms.Label labelMenuIP;
        private System.Windows.Forms.TextBox textBoxMenuIP;
        private System.Windows.Forms.Label labelMenuPort;
        private System.Windows.Forms.TextBox textBoxMenuPort;
        private System.Windows.Forms.Button buttonClient;
        private System.Windows.Forms.TextBox textBoxMenuInfo;
        private System.Windows.Forms.TextBox textBoxServerConsole;
        private System.Windows.Forms.Panel panelServerConsole;
        private System.Windows.Forms.Button buttonConsoleCommit;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Panel panelClientEdit;
        private System.Windows.Forms.Panel panelClientUsers;
        private System.Windows.Forms.RichTextBox rTextBoxClientMessage;
        private System.Windows.Forms.FlowLayoutPanel fLPanelClientControl;
        private System.Windows.Forms.Button buttonClientSend;
        private System.Windows.Forms.TextBox textBoxClient;
        private System.Windows.Forms.Label labelClientTitle;
        private System.Windows.Forms.Label labelClientUsers;
        private System.Windows.Forms.ToolStripMenuItem menuItemClientBack;
        private System.Windows.Forms.ToolStripMenuItem menuItemClientClsoe;
        private System.Windows.Forms.ToolStripMenuItem menuItemClientTimeDate;
        private System.Windows.Forms.ToolStripMenuItem menuItemServerBack;
        private System.Windows.Forms.ToolStripMenuItem menuItemServerClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemServerTimeDate;
        private System.Windows.Forms.Panel panelClientMessage;
        private System.ComponentModel.BackgroundWorker bgWorkerMenuTime;
        private System.Windows.Forms.Label labelMenuInsideIPTitle;
        private System.Windows.Forms.Label labelMenuInsideIP;
        private System.Windows.Forms.Label labelMenuOutsideIPTitle;
        private System.Windows.Forms.Label labelMenuOutsideIP;
        private System.Windows.Forms.ListView listViewClientUsers;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colIP;
        private System.Windows.Forms.Panel panelMenuInfo;
        private System.Windows.Forms.Label labelMenuCrypt;
        private System.Windows.Forms.ComboBox comboBoxMenuCrypt;
    }

}

