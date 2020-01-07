using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using M77;

namespace NPMPicker
{
    public partial class Form1 : Form
    {

        public static ListBox debug;
        public static ListBox cogiscan;

        public Form1()
        {
            InitializeComponent();

            // Sistema de TRAY
            this.Icon = Properties.Resources.Tray;
            Tray.TrayIcon.Icon = Properties.Resources.Tray;
            Tray.TrayIcon.Visible = false;
            this.Resize += new EventHandler(Form_Resize);
            Tray.TrayIcon.MouseClick += new MouseEventHandler(TrayIcon_MouseClick);

            this.WindowState = FormWindowState.Minimized;

            Tray.mainForm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           label2.Text = "Creado por Matias Flores";
           vrTxt.Text = "Version: "+Utilidades.Version();
           //pickControl.TabPages[1].Enabled = false;
           confCliente.Enabled = false;
           confServer.Enabled = false;

            debug = listBox1;
            cogiscan = listCogiscan;

            setConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.config_limite_error = int.Parse(errores_desde.Value.ToString());
            Config.config_linea = int.Parse(lineaid.Value.ToString());
            Config.camx = int.Parse(hrCamx.Value.ToString());

            Config.CM_pt200 = txtCM.Text.ToString();
            Config.NPM_LNB = txtLNB.Text.ToString(); 
            Config.adminPass = txtAdmin.Text.ToString(); 

            Config.server = txtHost.Text.ToString(); 
            Config.db = txtDb.Text.ToString(); 
            Config.user = txtUser.Text.ToString();
            Config.pass = txtPass.Text.ToString();  

            if (moduloNPM.Checked)
            {
                Config.config_modulo = "npm";
            }
            else {
                Config.config_modulo = "cm";
            }

            // Guardo nueva configuracion.
            Config.AppConfigSave();
            setConfig();

            MessageBox.Show("Configuracion guardada.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void iniciarTimer() {
            // Cada 10 minutos es ejecutado.
            Log.msg("[+] Iniciando Timer");
            timer1.Interval = 600000;//600000
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Start();
        }

        public void Start() {
            
            // Obtengo turno actual.
            Config.turno_actual = PickerDB.getTurno();

            bool releaseDebug = false;

            // Inicio modulo
            //try
            //{
                if (!releaseDebug)
                {
                    switch (Config.config_modulo)
                    {
                        case "npm":
                            NPM npm = new NPM();
                            npm.iniciar();
                            break;
                        case "cm":
                            CM cm = new CM();
                            cm.iniciar();
                            break;
                    }
                }

                try
                { PickerDB.ping(Config.config_linea.ToString(), Config.config_modulo.ToString()); }
                catch(Exception ex) { Log.msg("[Error] No se puede actualizar el ping en el servidor: " + ex.Message); }
                
            //}
            //catch (Exception ex)
            //{
            //    Log.msg("-------- ERROR ----------");
            //    Log.msg(ex.Message);
            //    Log.msg(ex.Source);
            //    Log.msg("-------- ///// ----------");
            //}
        }

        public void setConfig()
        {
            Mysql.errorDebug = false;

            // Si esta disponible una nueva version, actualizo la aplicacion.
            if (Utilidades.Actualizar_version())
            {
                // Cargo archivo de configuracion
                Config.start();

                errores_desde.Value = Config.config_limite_error;
                lineaid.Value = Config.config_linea;
                hrCamx.Value = Config.camx;
                txtCM.Text = Config.CM_pt200;
                txtLNB.Text = Config.NPM_LNB;
                txtAdmin.Text = Config.adminPass;

                txtHost.Text = Config.server;
                txtDb.Text = Config.db;
                txtUser.Text = Config.user;
                txtPass.Text = Config.pass;

                moduloNPM.Checked = false;
                moduloCM.Checked = false;

                switch (Config.config_modulo)
                {
                    case "npm":
                        moduloNPM.Checked = true;
                    break;
                    case "cm":
                        moduloCM.Checked = true;
                    break;
                }

                Log.msg("------------------");
                Log.msg("Linea: " + Config.config_linea);
                Log.msg("Modulo:" + Config.config_modulo);
                Log.msg("Limite error:" + Config.config_limite_error);
                Log.msg("------------------");

                iniciarTimer();              
            }
        }
       
        #region SISTEMA_TRAY
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Tray.salir)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            { // if the form has been minimised
                this.Hide(); // hide the form
                Tray.TrayIcon.Visible = true; // display the tray icon
//                this.TrayIcon.BalloonTipTitle = "NPMPicker";
  //              this.TrayIcon.BalloonTipText = "El programa se encuentra minimizado\nClick en el icono para mostrar la aplicacion...";
    //            this.TrayIcon.ShowBalloonTip(200);
            }
        }


        private void frmMain_Resize(object sender, EventArgs e)
        {
            NotifyIcon noti = new NotifyIcon();
            noti.Icon = Properties.Resources.Tray;
            if (FormWindowState.Minimized == this.WindowState)
            {
                noti.Visible = true;
                noti.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                noti.Visible = false;
            }
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Tray.TrayIcon.Visible = false;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    break;
                case MouseButtons.Right:
                    ToolStripItem menu = new ToolStripMenuItem("Salir de aplicacion", null, Tray.finalizarAplicacion);
                    ContextMenuStrip cm = new ContextMenuStrip();
                    cm.Items.Add(menu);

                    Point point = new Point(Cursor.Position.X, Cursor.Position.Y);

                    cm.Show(point);
                    break;
            }
        }

        //private void finalizarAplicacion(object sender, EventArgs e)
        //{
        //    Tray.salir = true;
        //    Tray.TrayIcon.Visible = false;
        //    this.Show();
        //    Application.Exit();
        //}
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            string clave = inClave.Text;

            string btn = btnLogin.Text;
            switch (btn) {
                case "Acceder":
                    if (clave == Config.adminPass)
                    {
                        //pickControl.TabPages[1].Enabled = true;
                        confCliente.Enabled = true;
                        confServer.Enabled = true;
                        inClave.Enabled = false;

                        btnLogin.Text = "Finalizar";
//                        pickControl.SelectTab(1);
                    }
                break;
                case "Finalizar":
                    //pickControl.TabPages[1].Enabled = false;
                    confCliente.Enabled = false;
                    confServer.Enabled = false;
                    inClave.Enabled = true;

                    btnLogin.Text = "Acceder";
                break;
            }

            inClave.Text = "";
        }
    }
}
