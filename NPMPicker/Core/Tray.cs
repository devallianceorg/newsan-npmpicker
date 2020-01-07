using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Data;
using M77;
using System.Net;
using System.Deployment.Application;

namespace NPMPicker
{
    class Tray
    {
        public static bool salir = false;
        public static NotifyIcon TrayIcon = new NotifyIcon();
        public static Form mainForm;

        public static void finalizarAplicacion(object sender, EventArgs e)
        {
            Tray.salir = true;
            Tray.TrayIcon.Visible = false;
            mainForm.Show();
            Application.Exit();
        }

        public static void restartAplicacion()
        {
            Tray.salir = true;
            Tray.TrayIcon.Visible = false;
            mainForm.Show();
            Application.Restart();
        }
    }
}
