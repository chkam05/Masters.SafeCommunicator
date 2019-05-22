namespace Safe_Communicator.Forms
{
    partial class Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            this.fLPanelTest = new System.Windows.Forms.FlowLayoutPanel();
            this.fLPanelRSA = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTitleRSA = new System.Windows.Forms.Label();
            this.textBoxRSACrypt = new System.Windows.Forms.TextBox();
            this.textBoxRSADecrypt = new System.Windows.Forms.TextBox();
            this.fLPanelRSAButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRSADecrypt = new System.Windows.Forms.Button();
            this.buttonRSACrypt = new System.Windows.Forms.Button();
            this.flPanelElGamal = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTitleElGamal = new System.Windows.Forms.Label();
            this.textBoxElGamalCrypt = new System.Windows.Forms.TextBox();
            this.textBoxElGamalDecrypt = new System.Windows.Forms.TextBox();
            this.flPanelElGamalButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonElGamalDecrypt = new System.Windows.Forms.Button();
            this.buttonElGamalCrypt = new System.Windows.Forms.Button();
            this.fLPanelConsole = new System.Windows.Forms.FlowLayoutPanel();
            this.labelConsole = new System.Windows.Forms.Label();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.fLPanelTest.SuspendLayout();
            this.fLPanelRSA.SuspendLayout();
            this.fLPanelRSAButtons.SuspendLayout();
            this.flPanelElGamal.SuspendLayout();
            this.flPanelElGamalButtons.SuspendLayout();
            this.fLPanelConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // fLPanelTest
            // 
            this.fLPanelTest.AutoScroll = true;
            this.fLPanelTest.Controls.Add(this.fLPanelRSA);
            this.fLPanelTest.Controls.Add(this.flPanelElGamal);
            this.fLPanelTest.Controls.Add(this.fLPanelConsole);
            this.fLPanelTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLPanelTest.Location = new System.Drawing.Point(0, 0);
            this.fLPanelTest.Margin = new System.Windows.Forms.Padding(0);
            this.fLPanelTest.Name = "fLPanelTest";
            this.fLPanelTest.Size = new System.Drawing.Size(800, 450);
            this.fLPanelTest.TabIndex = 0;
            // 
            // fLPanelRSA
            // 
            this.fLPanelRSA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fLPanelRSA.Controls.Add(this.labelTitleRSA);
            this.fLPanelRSA.Controls.Add(this.textBoxRSACrypt);
            this.fLPanelRSA.Controls.Add(this.textBoxRSADecrypt);
            this.fLPanelRSA.Controls.Add(this.fLPanelRSAButtons);
            this.fLPanelRSA.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPanelRSA.Location = new System.Drawing.Point(8, 8);
            this.fLPanelRSA.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.fLPanelRSA.Name = "fLPanelRSA";
            this.fLPanelRSA.Size = new System.Drawing.Size(288, 141);
            this.fLPanelRSA.TabIndex = 6;
            this.fLPanelRSA.WrapContents = false;
            // 
            // labelTitleRSA
            // 
            this.labelTitleRSA.Location = new System.Drawing.Point(16, 16);
            this.labelTitleRSA.Margin = new System.Windows.Forms.Padding(16, 16, 16, 0);
            this.labelTitleRSA.Name = "labelTitleRSA";
            this.labelTitleRSA.Size = new System.Drawing.Size(256, 13);
            this.labelTitleRSA.TabIndex = 0;
            this.labelTitleRSA.Text = "RSA";
            // 
            // textBoxRSACrypt
            // 
            this.textBoxRSACrypt.Location = new System.Drawing.Point(16, 33);
            this.textBoxRSACrypt.Margin = new System.Windows.Forms.Padding(16, 4, 16, 0);
            this.textBoxRSACrypt.Name = "textBoxRSACrypt";
            this.textBoxRSACrypt.Size = new System.Drawing.Size(256, 20);
            this.textBoxRSACrypt.TabIndex = 1;
            // 
            // textBoxRSADecrypt
            // 
            this.textBoxRSADecrypt.Location = new System.Drawing.Point(16, 57);
            this.textBoxRSADecrypt.Margin = new System.Windows.Forms.Padding(16, 4, 16, 0);
            this.textBoxRSADecrypt.Name = "textBoxRSADecrypt";
            this.textBoxRSADecrypt.ReadOnly = true;
            this.textBoxRSADecrypt.Size = new System.Drawing.Size(256, 20);
            this.textBoxRSADecrypt.TabIndex = 3;
            // 
            // fLPanelRSAButtons
            // 
            this.fLPanelRSAButtons.Controls.Add(this.buttonRSADecrypt);
            this.fLPanelRSAButtons.Controls.Add(this.buttonRSACrypt);
            this.fLPanelRSAButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.fLPanelRSAButtons.Location = new System.Drawing.Point(16, 93);
            this.fLPanelRSAButtons.Margin = new System.Windows.Forms.Padding(16, 16, 16, 0);
            this.fLPanelRSAButtons.Name = "fLPanelRSAButtons";
            this.fLPanelRSAButtons.Size = new System.Drawing.Size(256, 23);
            this.fLPanelRSAButtons.TabIndex = 5;
            // 
            // buttonRSADecrypt
            // 
            this.buttonRSADecrypt.Location = new System.Drawing.Point(181, 0);
            this.buttonRSADecrypt.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRSADecrypt.Name = "buttonRSADecrypt";
            this.buttonRSADecrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonRSADecrypt.TabIndex = 2;
            this.buttonRSADecrypt.Text = "Decrypt";
            this.buttonRSADecrypt.UseVisualStyleBackColor = true;
            this.buttonRSADecrypt.Click += new System.EventHandler(this.ButtonRSADecrypt_Click);
            // 
            // buttonRSACrypt
            // 
            this.buttonRSACrypt.Location = new System.Drawing.Point(106, 0);
            this.buttonRSACrypt.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRSACrypt.Name = "buttonRSACrypt";
            this.buttonRSACrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonRSACrypt.TabIndex = 4;
            this.buttonRSACrypt.Text = "Encrypt";
            this.buttonRSACrypt.UseVisualStyleBackColor = true;
            this.buttonRSACrypt.Click += new System.EventHandler(this.ButtonRSACrypt_Click);
            // 
            // flPanelElGamal
            // 
            this.flPanelElGamal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flPanelElGamal.Controls.Add(this.labelTitleElGamal);
            this.flPanelElGamal.Controls.Add(this.textBoxElGamalCrypt);
            this.flPanelElGamal.Controls.Add(this.textBoxElGamalDecrypt);
            this.flPanelElGamal.Controls.Add(this.flPanelElGamalButtons);
            this.flPanelElGamal.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flPanelElGamal.Location = new System.Drawing.Point(304, 8);
            this.flPanelElGamal.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.flPanelElGamal.Name = "flPanelElGamal";
            this.flPanelElGamal.Size = new System.Drawing.Size(288, 141);
            this.flPanelElGamal.TabIndex = 7;
            this.flPanelElGamal.WrapContents = false;
            // 
            // labelTitleElGamal
            // 
            this.labelTitleElGamal.Location = new System.Drawing.Point(16, 16);
            this.labelTitleElGamal.Margin = new System.Windows.Forms.Padding(16, 16, 16, 0);
            this.labelTitleElGamal.Name = "labelTitleElGamal";
            this.labelTitleElGamal.Size = new System.Drawing.Size(256, 13);
            this.labelTitleElGamal.TabIndex = 0;
            this.labelTitleElGamal.Text = "ElGamal";
            // 
            // textBoxElGamalCrypt
            // 
            this.textBoxElGamalCrypt.Location = new System.Drawing.Point(16, 33);
            this.textBoxElGamalCrypt.Margin = new System.Windows.Forms.Padding(16, 4, 16, 0);
            this.textBoxElGamalCrypt.Name = "textBoxElGamalCrypt";
            this.textBoxElGamalCrypt.Size = new System.Drawing.Size(256, 20);
            this.textBoxElGamalCrypt.TabIndex = 1;
            // 
            // textBoxElGamalDecrypt
            // 
            this.textBoxElGamalDecrypt.Location = new System.Drawing.Point(16, 57);
            this.textBoxElGamalDecrypt.Margin = new System.Windows.Forms.Padding(16, 4, 16, 0);
            this.textBoxElGamalDecrypt.Name = "textBoxElGamalDecrypt";
            this.textBoxElGamalDecrypt.ReadOnly = true;
            this.textBoxElGamalDecrypt.Size = new System.Drawing.Size(256, 20);
            this.textBoxElGamalDecrypt.TabIndex = 3;
            // 
            // flPanelElGamalButtons
            // 
            this.flPanelElGamalButtons.Controls.Add(this.buttonElGamalDecrypt);
            this.flPanelElGamalButtons.Controls.Add(this.buttonElGamalCrypt);
            this.flPanelElGamalButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flPanelElGamalButtons.Location = new System.Drawing.Point(16, 93);
            this.flPanelElGamalButtons.Margin = new System.Windows.Forms.Padding(16, 16, 16, 0);
            this.flPanelElGamalButtons.Name = "flPanelElGamalButtons";
            this.flPanelElGamalButtons.Size = new System.Drawing.Size(256, 23);
            this.flPanelElGamalButtons.TabIndex = 5;
            // 
            // buttonElGamalDecrypt
            // 
            this.buttonElGamalDecrypt.Location = new System.Drawing.Point(181, 0);
            this.buttonElGamalDecrypt.Margin = new System.Windows.Forms.Padding(0);
            this.buttonElGamalDecrypt.Name = "buttonElGamalDecrypt";
            this.buttonElGamalDecrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonElGamalDecrypt.TabIndex = 2;
            this.buttonElGamalDecrypt.Text = "Decrypt";
            this.buttonElGamalDecrypt.UseVisualStyleBackColor = true;
            this.buttonElGamalDecrypt.Click += new System.EventHandler(this.ButtonElGamalDecrypt_Click);
            // 
            // buttonElGamalCrypt
            // 
            this.buttonElGamalCrypt.Location = new System.Drawing.Point(106, 0);
            this.buttonElGamalCrypt.Margin = new System.Windows.Forms.Padding(0);
            this.buttonElGamalCrypt.Name = "buttonElGamalCrypt";
            this.buttonElGamalCrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonElGamalCrypt.TabIndex = 4;
            this.buttonElGamalCrypt.Text = "Encrypt";
            this.buttonElGamalCrypt.UseVisualStyleBackColor = true;
            this.buttonElGamalCrypt.Click += new System.EventHandler(this.ButtonElGamalCrypt_Click);
            // 
            // fLPanelConsole
            // 
            this.fLPanelConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fLPanelConsole.Controls.Add(this.labelConsole);
            this.fLPanelConsole.Controls.Add(this.textBoxConsole);
            this.fLPanelConsole.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPanelConsole.Location = new System.Drawing.Point(8, 157);
            this.fLPanelConsole.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.fLPanelConsole.Name = "fLPanelConsole";
            this.fLPanelConsole.Size = new System.Drawing.Size(416, 228);
            this.fLPanelConsole.TabIndex = 8;
            this.fLPanelConsole.WrapContents = false;
            // 
            // labelConsole
            // 
            this.labelConsole.Location = new System.Drawing.Point(16, 16);
            this.labelConsole.Margin = new System.Windows.Forms.Padding(16, 16, 16, 0);
            this.labelConsole.Name = "labelConsole";
            this.labelConsole.Size = new System.Drawing.Size(384, 13);
            this.labelConsole.TabIndex = 1;
            this.labelConsole.Text = "Console";
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.BackColor = System.Drawing.Color.Black;
            this.textBoxConsole.ForeColor = System.Drawing.Color.White;
            this.textBoxConsole.Location = new System.Drawing.Point(16, 33);
            this.textBoxConsole.Margin = new System.Windows.Forms.Padding(16, 4, 16, 0);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ReadOnly = true;
            this.textBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConsole.Size = new System.Drawing.Size(384, 176);
            this.textBoxConsole.TabIndex = 0;
            this.textBoxConsole.Text = "Test";
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fLPanelTest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Test";
            this.Text = "Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            this.fLPanelTest.ResumeLayout(false);
            this.fLPanelRSA.ResumeLayout(false);
            this.fLPanelRSA.PerformLayout();
            this.fLPanelRSAButtons.ResumeLayout(false);
            this.flPanelElGamal.ResumeLayout(false);
            this.flPanelElGamal.PerformLayout();
            this.flPanelElGamalButtons.ResumeLayout(false);
            this.fLPanelConsole.ResumeLayout(false);
            this.fLPanelConsole.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPanelTest;
        private System.Windows.Forms.Label labelTitleRSA;
        private System.Windows.Forms.TextBox textBoxRSACrypt;
        private System.Windows.Forms.FlowLayoutPanel fLPanelRSAButtons;
        private System.Windows.Forms.Button buttonRSADecrypt;
        private System.Windows.Forms.Button buttonRSACrypt;
        private System.Windows.Forms.FlowLayoutPanel fLPanelRSA;
        private System.Windows.Forms.TextBox textBoxRSADecrypt;
        private System.Windows.Forms.FlowLayoutPanel flPanelElGamal;
        private System.Windows.Forms.Label labelTitleElGamal;
        private System.Windows.Forms.TextBox textBoxElGamalCrypt;
        private System.Windows.Forms.TextBox textBoxElGamalDecrypt;
        private System.Windows.Forms.FlowLayoutPanel flPanelElGamalButtons;
        private System.Windows.Forms.Button buttonElGamalDecrypt;
        private System.Windows.Forms.Button buttonElGamalCrypt;
        private System.Windows.Forms.FlowLayoutPanel fLPanelConsole;
        private System.Windows.Forms.Label labelConsole;
        private System.Windows.Forms.TextBox textBoxConsole;
    }
}