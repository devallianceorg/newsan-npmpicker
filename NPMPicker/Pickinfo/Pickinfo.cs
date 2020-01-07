using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPMPicker
{
    class Pickinfo
    {
        public string partnumber = "";
        public string feeder = "";
        public string lado = "";
        public string tabla = "";
        public string feeder_completo = "";
        public string programa = "";
        public string op = "";

        public int error_pickup = 0;
        public int error_recog = 0;
        public int total_pickup = 0;
        public int total_error = 0;
        
        // NPM
        public int historial = 0;
        public int npm = 1;
    }
}
