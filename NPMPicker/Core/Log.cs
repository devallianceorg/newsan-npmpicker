using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPMPicker
{
    class Log
    {
        public static void msg(string msg)
        {
            Form1.debug.Items.Add(msg);
        }

        public static void cogiscan(string msg)
        {
            Form1.cogiscan.Items.Add(msg);
        }

        
    }
}
