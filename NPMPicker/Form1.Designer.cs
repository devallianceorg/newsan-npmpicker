namespace NPMPicker
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.errores_desde = new System.Windows.Forms.NumericUpDown();
            this.lineaid = new System.Windows.Forms.NumericUpDown();
            this.confCliente = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.hrCamx = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAdmin = new System.Windows.Forms.TextBox();
            this.txtLNB = new System.Windows.Forms.TextBox();
            this.txtCM = new System.Windows.Forms.TextBox();
            this.moduloCM = new System.Windows.Forms.RadioButton();
            this.moduloNPM = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pickControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabCogiscan = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listCogiscan = new System.Windows.Forms.ListBox();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.vrTxt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.inClave = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.confServer = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errores_desde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineaid)).BeginInit();
            this.confCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrCamx)).BeginInit();
            this.pickControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabCogiscan.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.confServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MistyRose;
            this.button1.Location = new System.Drawing.Point(44, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 24);
            this.button1.TabIndex = 9;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // errores_desde
            // 
            this.errores_desde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.errores_desde.Location = new System.Drawing.Point(95, 20);
            this.errores_desde.Name = "errores_desde";
            this.errores_desde.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.errores_desde.Size = new System.Drawing.Size(54, 20);
            this.errores_desde.TabIndex = 0;
            this.errores_desde.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lineaid
            // 
            this.lineaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lineaid.Location = new System.Drawing.Point(95, 46);
            this.lineaid.Name = "lineaid";
            this.lineaid.Size = new System.Drawing.Size(54, 20);
            this.lineaid.TabIndex = 1;
            this.lineaid.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // confCliente
            // 
            this.confCliente.Controls.Add(this.label12);
            this.confCliente.Controls.Add(this.label11);
            this.confCliente.Controls.Add(this.hrCamx);
            this.confCliente.Controls.Add(this.label10);
            this.confCliente.Controls.Add(this.label9);
            this.confCliente.Controls.Add(this.label8);
            this.confCliente.Controls.Add(this.txtAdmin);
            this.confCliente.Controls.Add(this.txtLNB);
            this.confCliente.Controls.Add(this.txtCM);
            this.confCliente.Controls.Add(this.moduloCM);
            this.confCliente.Controls.Add(this.moduloNPM);
            this.confCliente.Controls.Add(this.label3);
            this.confCliente.Controls.Add(this.label1);
            this.confCliente.Controls.Add(this.errores_desde);
            this.confCliente.Controls.Add(this.lineaid);
            this.confCliente.Location = new System.Drawing.Point(7, 63);
            this.confCliente.Name = "confCliente";
            this.confCliente.Size = new System.Drawing.Size(241, 206);
            this.confCliente.TabIndex = 4;
            this.confCliente.TabStop = false;
            this.confCliente.Text = "Configuracion cliente";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(150, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "(hora)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Ejecutar CAMX:";
            // 
            // hrCamx
            // 
            this.hrCamx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.hrCamx.Location = new System.Drawing.Point(95, 157);
            this.hrCamx.Name = "hrCamx";
            this.hrCamx.Size = new System.Drawing.Size(54, 20);
            this.hrCamx.TabIndex = 7;
            this.hrCamx.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Clave admin:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Ruta LNB:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Ruta PT200:";
            // 
            // txtAdmin
            // 
            this.txtAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAdmin.Location = new System.Drawing.Point(95, 131);
            this.txtAdmin.Name = "txtAdmin";
            this.txtAdmin.PasswordChar = '*';
            this.txtAdmin.Size = new System.Drawing.Size(128, 20);
            this.txtAdmin.TabIndex = 4;
            // 
            // txtLNB
            // 
            this.txtLNB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtLNB.Location = new System.Drawing.Point(95, 105);
            this.txtLNB.Name = "txtLNB";
            this.txtLNB.Size = new System.Drawing.Size(128, 20);
            this.txtLNB.TabIndex = 3;
            // 
            // txtCM
            // 
            this.txtCM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCM.Location = new System.Drawing.Point(95, 79);
            this.txtCM.Name = "txtCM";
            this.txtCM.Size = new System.Drawing.Size(128, 20);
            this.txtCM.TabIndex = 2;
            // 
            // moduloCM
            // 
            this.moduloCM.AutoSize = true;
            this.moduloCM.BackColor = System.Drawing.Color.Transparent;
            this.moduloCM.Location = new System.Drawing.Point(166, 46);
            this.moduloCM.Name = "moduloCM";
            this.moduloCM.Size = new System.Drawing.Size(41, 17);
            this.moduloCM.TabIndex = 4;
            this.moduloCM.Text = "CM";
            this.moduloCM.UseVisualStyleBackColor = false;
            // 
            // moduloNPM
            // 
            this.moduloNPM.AutoSize = true;
            this.moduloNPM.BackColor = System.Drawing.Color.Transparent;
            this.moduloNPM.Checked = true;
            this.moduloNPM.Location = new System.Drawing.Point(166, 22);
            this.moduloNPM.Name = "moduloNPM";
            this.moduloNPM.Size = new System.Drawing.Size(49, 17);
            this.moduloNPM.TabIndex = 4;
            this.moduloNPM.TabStop = true;
            this.moduloNPM.Text = "NPM";
            this.moduloNPM.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Linea:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Errores desde:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pickControl
            // 
            this.pickControl.Controls.Add(this.tabPage1);
            this.pickControl.Controls.Add(this.tabCogiscan);
            this.pickControl.Controls.Add(this.tabConfig);
            this.pickControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pickControl.Location = new System.Drawing.Point(0, 0);
            this.pickControl.Name = "pickControl";
            this.pickControl.SelectedIndex = 0;
            this.pickControl.Size = new System.Drawing.Size(854, 303);
            this.pickControl.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(846, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(840, 271);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(5, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(827, 251);
            this.listBox1.TabIndex = 7;
            // 
            // tabCogiscan
            // 
            this.tabCogiscan.Controls.Add(this.groupBox1);
            this.tabCogiscan.Location = new System.Drawing.Point(4, 22);
            this.tabCogiscan.Name = "tabCogiscan";
            this.tabCogiscan.Padding = new System.Windows.Forms.Padding(3);
            this.tabCogiscan.Size = new System.Drawing.Size(846, 277);
            this.tabCogiscan.TabIndex = 2;
            this.tabCogiscan.Text = "Cogiscan";
            this.tabCogiscan.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listCogiscan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(840, 271);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // listCogiscan
            // 
            this.listCogiscan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listCogiscan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCogiscan.ForeColor = System.Drawing.Color.White;
            this.listCogiscan.FormattingEnabled = true;
            this.listCogiscan.Location = new System.Drawing.Point(5, 18);
            this.listCogiscan.Name = "listCogiscan";
            this.listCogiscan.Size = new System.Drawing.Size(830, 248);
            this.listCogiscan.TabIndex = 7;
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.vrTxt);
            this.tabConfig.Controls.Add(this.label2);
            this.tabConfig.Controls.Add(this.pictureBox1);
            this.tabConfig.Controls.Add(this.groupBox4);
            this.tabConfig.Controls.Add(this.confServer);
            this.tabConfig.Controls.Add(this.confCliente);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(846, 277);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Configuracion";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // vrTxt
            // 
            this.vrTxt.ForeColor = System.Drawing.Color.DimGray;
            this.vrTxt.Location = new System.Drawing.Point(626, 256);
            this.vrTxt.Name = "vrTxt";
            this.vrTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vrTxt.Size = new System.Drawing.Size(210, 13);
            this.vrTxt.TabIndex = 13;
            this.vrTxt.Text = "Version: ";
            this.vrTxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(503, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Creado por Matias Flores";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NPMPicker.Properties.Resources.vendetta;
            this.pictureBox1.InitialImage = global::NPMPicker.Properties.Resources.vendetta;
            this.pictureBox1.Location = new System.Drawing.Point(501, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(337, 268);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.inClave);
            this.groupBox4.Controls.Add(this.btnLogin);
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 53);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Login";
            // 
            // inClave
            // 
            this.inClave.Location = new System.Drawing.Point(6, 19);
            this.inClave.Name = "inClave";
            this.inClave.PasswordChar = '*';
            this.inClave.Size = new System.Drawing.Size(156, 20);
            this.inClave.TabIndex = 2;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(166, 17);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(68, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Acceder";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button2_Click);
            // 
            // confServer
            // 
            this.confServer.Controls.Add(this.button1);
            this.confServer.Controls.Add(this.button3);
            this.confServer.Controls.Add(this.label7);
            this.confServer.Controls.Add(this.label6);
            this.confServer.Controls.Add(this.label5);
            this.confServer.Controls.Add(this.label4);
            this.confServer.Controls.Add(this.txtPass);
            this.confServer.Controls.Add(this.txtUser);
            this.confServer.Controls.Add(this.txtDb);
            this.confServer.Controls.Add(this.txtHost);
            this.confServer.Location = new System.Drawing.Point(254, 6);
            this.confServer.Name = "confServer";
            this.confServer.Size = new System.Drawing.Size(241, 265);
            this.confServer.TabIndex = 5;
            this.confServer.TabStop = false;
            this.confServer.Text = "Configuracion Servidor";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button3.Location = new System.Drawing.Point(122, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 24);
            this.button3.TabIndex = 10;
            this.button3.Text = "Ejecutar";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Clave:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Usuario:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Database:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Hostname:";
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPass.Location = new System.Drawing.Point(83, 111);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(128, 20);
            this.txtPass.TabIndex = 8;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUser.Location = new System.Drawing.Point(83, 85);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(128, 20);
            this.txtUser.TabIndex = 7;
            // 
            // txtDb
            // 
            this.txtDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDb.Location = new System.Drawing.Point(83, 59);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(128, 20);
            this.txtDb.TabIndex = 6;
            // 
            // txtHost
            // 
            this.txtHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtHost.Location = new System.Drawing.Point(83, 33);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(128, 20);
            this.txtHost.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 303);
            this.Controls.Add(this.pickControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "PickINFO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errores_desde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineaid)).EndInit();
            this.confCliente.ResumeLayout(false);
            this.confCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrCamx)).EndInit();
            this.pickControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabCogiscan.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.confServer.ResumeLayout(false);
            this.confServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown errores_desde;
        private System.Windows.Forms.NumericUpDown lineaid;
        private System.Windows.Forms.GroupBox confCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton moduloCM;
        private System.Windows.Forms.RadioButton moduloNPM;
        private System.Windows.Forms.TabControl pickControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAdmin;
        private System.Windows.Forms.TextBox txtLNB;
        private System.Windows.Forms.TextBox txtCM;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox confServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox inClave;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label vrTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabCogiscan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listCogiscan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown hrCamx;
    }
}

