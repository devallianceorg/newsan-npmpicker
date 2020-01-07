using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.IO;
using M77;
using CogiscanManager.Controles;

namespace NPMPicker
{
    class CM
    {
        private string ruta = Config.CM_pt200;

        /*
        *  iniciar() 
        *  INICIA PROCESOS
        */
        public void iniciar()
        {
            Log.msg("[+] MODULO CM");
            List<string> estaciones = GetEstaciones();
            try
            {
                foreach (string estacion in estaciones)
                {
                    string[] get_nombre_maquina = estacion.Split('-');
                    string nombre_estacion = get_nombre_maquina[0].ToString();
                    int numero_estacion = int.Parse(estacion.Substring((estacion.Length - 1), 1));

                    string ruta_estacion = ruta + estacion + @"\";

                    List<string> mpr = getMPR(ruta_estacion);
                    foreach (string archivo_mpr in mpr)
                    {
                        string mprFullPath = ruta_estacion + archivo_mpr;
                        readMPR(mprFullPath, nombre_estacion, numero_estacion);
                    }

                    // Luego de procesar los MPR, si son las 23 hs, elimina todos los mpr de la estacion
                    if (RestartCAMX.isDeleteMprMode())
                    {
                        RestartCAMX.DoTheJob(ruta_estacion);
                    }
                }
            }
            catch(Exception ex)
            { Log.msg("[Error] No se puede Iniciar el modulo CM: " + ex.Message); }
            
            
            Log.msg("**** Procesos finalizados ****");
        }


        private void readMPR(string mpr,string nombre_estacion, int cm) {
            StreamReader reader = new StreamReader(mpr);
            string op = "";
            string contenido = reader.ReadToEnd();
            reader.Close();
            string programName = "";
            string pickCount = "";
            string pickData = "";
            string recogData = "";
            string programN = "";
            string[] progHeader = { };
            string[] pickCountN= { };
            string[] pickDataN = { };
            string[] recogDataN = { };
            try
            { 
                op = Cogiscan.DB2.getPo();
                programName = extraerTag(contenido, "programa", "Index");
                pickCount = extraerTag(contenido, "TakeUpCount", "TakeUpMiss");
                pickData = extraerTag(contenido, "TakeUpMiss", "ChipRcgMiss");
                recogData = extraerTag(contenido, "ChipRcgMiss", "ChipStanding");

                progHeader = Regex.Split(programName, @"([A-z0-9\-?]+)");
                programN = decodeProgramName(progHeader);
                pickCountN = pickCount.Trim().Split('\n');
                pickDataN = pickData.Trim().Split('\n');
                recogDataN = recogData.Trim().Split('\n');
            }
            catch(Exception e)
            { e.Message.ToString(); }

            List<Pickinfo> pickcount = explodeData(pickCountN, "pickcount");
            List<Pickinfo> pick = explodeData(pickDataN, "pickup");
            List<Pickinfo> recog = explodeData(recogDataN, "recog");

            // Una vez calculado los errores, junto la informacion reconocimiento con pick para guardar en DB.
            foreach(Pickinfo rec in recog) {              
                Pickinfo findPick = pick.Find(x => x.feeder == rec.feeder && x.lado == rec.lado);
                if (findPick == null)
                {
                    pick.Add(rec);
                }
                else
                {
                    findPick.error_recog = rec.error_recog;
                }
            }

            // Recorro Pick
            foreach (Pickinfo db in pick)
            {
                int total_error = db.error_pickup + db.error_recog;
                if (total_error >= Config.config_limite_error)
                {
                    Pickinfo findPickCount = pickcount.Find(x => x.feeder == db.feeder && x.lado == db.lado);
                    if (findPickCount != null)
                    {
                        db.total_pickup = findPickCount.total_pickup;
                    }
                    Log.msg("ADD: " + nombre_estacion + "," + db.tabla + "," + db.feeder_completo +", Part# "+ db.partnumber + " Prog: " + programN + " Error:" + total_error + " Pick:" + db.total_pickup);

                    PickerDB pq = new PickerDB();
                    pq.linea = Config.config_linea.ToString();
                    pq.maquina = nombre_estacion;
                    pq.modulo = cm;
                    pq.tabla = db.tabla;
                    pq.feeder = db.feeder_completo;
                    pq.partNumber = db.partnumber;
                    pq.programa = programN;
                    pq.op = op;
                    pq.total_error = total_error;
                    pq.total_pickup = db.total_pickup;
                    pq.turno = Config.turno_actual;
                    pq.insert();
                }
            }
        }

        /*
         * explodeData()
         * ANALIZA ARCHIVO MPR Y CALCULA ERRORES
         */
        private List<Pickinfo> explodeData(string[] lineas, string modo)
        {
            List<Pickinfo> data = new List<Pickinfo>();
            List<string> headvar = new List<string>();
            // Remuevo header

            bool header = true;
            foreach (string linea in lineas)
            {
                string[] vari = linea.Split(' ');

                if (header)
                {
                    foreach (string variable in vari)
                    {
                        headvar.Add(variable);
                    }
                    header = false;
                }
                else
                {
                    int index = 0;
                    int error = 0;
                    Pickinfo feed = new Pickinfo();

                    foreach (string variable in vari)
                    {
                        string setVar = headvar[index];
                        string setVal = variable;
                        if (setVar.All(Char.IsLetter))
                        {
                            if (!setVal.Equals("0"))
                            {
                                switch (setVar)
                                {
                                    case "Address":
                                        int feed_completo = int.Parse(setVal.Substring(2));
                                        int feed_tabla = int.Parse(setVal[0].ToString());

                                        feed.tabla = feed_tabla.ToString();
                                        feed.feeder = setVal;
                                        feed.feeder_completo = feed_completo.ToString();

                                    break;
                                    case "SubAdd":
                                        feed.lado = setVal;
                                        string side = "";
                                        switch (int.Parse(feed.lado))
                                        {
                                            case 0:
                                                side = "";
                                                break;
                                            case 1:
                                                side = "-L";
                                                break;
                                            case 2:
                                                side = "-R";
                                                break;
                                        }
                                        feed.feeder_completo = feed.feeder_completo + side;
                                    break;
                                    case "Name":
                                        feed.partnumber = setVal;
                                    break;
                                    default:
                                        if (setVar.StartsWith("NP"))
                                        {
                                            error = error + int.Parse(setVal);
                                        }
                                    break;
                                }
                            }
                        }
                        index++;
                    }

                    switch (modo)
                    {
                        case "pickup":
                            feed.error_pickup = error;
                        break;
                        case "recog":
                            feed.error_recog = error;
                        break;
                        case "pickcount":
                            feed.total_pickup = error;
                        break;
                    }

                    data.Add(feed);
                }
            }
            return data;                
        }

        /*
         * extraerTags()
         * EXTRAE LA INFORMACION ENTRE 2 TAGS PREVIAMENTE DECLARADOS
         */
        private string extraerTag(string s,string tag_ini,string tag_end) {
            var startTag = "[" + tag_ini + "]";
            int startIndex = 0;
            if (!tag_ini.Contains("programa"))
            {
                startIndex = s.IndexOf(startTag) + startTag.Length;
            }
            int endIndex = s.IndexOf("[" + tag_end + "]", startIndex);
            return s.Substring(startIndex, endIndex - startIndex).Replace("\0", "");
        }

        private List<string> GetEstaciones()
        {
            List<string> ls = new List<string>();

            Archivos ar = new Archivos();
            DirectoryInfo dir = new DirectoryInfo(ruta);
            DirectoryInfo[] estaciones = dir.GetDirectories();
            if (estaciones.Length > 0)
            {
                foreach (DirectoryInfo estacion in estaciones)
                {
                    if (estacion.Name.Contains("CM602"))
                    {
                        ls.Add(estacion.Name);
                    }
                }
            }
            return ls;
        }

        /*
         * getMPR()
         * OBTIENE LAS DOS ESTACIONES CON LAS ULTIMAS ACTUALIZACIONES DE LA CM 
         */
        private List<string> getMPR(string ruta_estacion) {
            Archivos ar = new Archivos();
            DirectoryInfo dir = new DirectoryInfo(ruta_estacion);
            FileInfo[] files = dir.GetFiles();

            List<string> ls = new List<string>();

            if (files.Length > 0)
            {
                var archivos = files.OrderByDescending(f => f.Name);
                string primer_estacion = "";

                foreach (FileInfo archivo in archivos)
                {
                    string[] file_format = archivo.Name.Split(new char[] { '-' });
                    string estacion_actual = file_format[2].ToString();

                    DateTime eh = archivo.CreationTime;

                    if (primer_estacion.Equals(""))
                    {
                        primer_estacion = estacion_actual;
                        ls.Add(archivo.ToString());
                    }
                    else
                    {
                        if (!estacion_actual.Equals(primer_estacion))
                        {
                            ls.Add(archivo.ToString());
                            break;
                        }
                    }
                }
            }
            return ls;
        }

        private string decodeProgramName(string[] encabezado)
        {
            string[] lineaDePrograma = encabezado[3].Substring(7).Split(' ');

            return lineaDePrograma[0];
        }
    }
}
