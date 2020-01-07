using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace NPMPicker
{
    class NPM
    {
        private string session = "";
        private string sessionProg = "";

        private static string conf_login = "http://" + Config.NPM_LNB + "/lws/Login.do";
        //private static string conf_login = "http://4c1d1d85.ngrok.io/lws/Login.do";
        private static string conf_login_program = "http://" + Config.NPM_LNB + "/lws/LwsInitialize.do";
        //private static string conf_login_program = "http://4c1d1d85.ngrok.io/lws/LwsInitialize.do";
        private static string urladmindata = "userid=" + Config.NPM_LNB_USER + "&password=" + Config.NPM_LNB_PASS + "&operation=0&icuserid=";
        private static string url = "http://" + Config.NPM_LNB + "/lws/ReferPickupReportAction.do";
        //private static string url = "http://4c1d1d85.ngrok.io/lws/ReferPickupReportAction.do";
        private static string urlProgramName = "http://" + Config.NPM_LNB + "/lws/ReferSendingBoardGraphAction.do";
        //private static string urlProgramName = "http://4c1d1d85.ngrok.io/lws/ReferSendingBoardGraphAction.do";

        /*
         *  iniciar() 
         *  INICIA PROCESOS
         */
        public void iniciar()
        {
            Log.msg("[+] MODULO NPM");
            Log.msg("[+] Detectando session");

            if (session != "")
            {
                Log.msg("- Detectada");
                getInfo();
            }
            else
            {
                Log.msg("- No hay session");
                bool logueado = true;
                logueado = loguear();
                

                if (logueado)
                {
                    Log.msg("- Logueado");
                    getInfo();
                }
            }
            Log.msg("**** Procesos finalizados ****");
        }

        /*
         *  loguear() 
         *  CONECTA AL SERVIDOR HTTP Y DESCARGA LA INFORMACION XML
         */
        private bool loguear()
        {
            Log.msg("loguear()");

            bool done = false;
            var cookies = new CookieContainer();
            ServicePointManager.Expect100Continue = false;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(conf_login);
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                using (var requestStream = request.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(urladmindata);
                }
                using (var responseStream = request.GetResponse().GetResponseStream())
                //using (var responseStream = request.GetRequestStream())
                using (var reader = new StreamReader(responseStream))
                {
                    var result = reader.ReadToEnd();
                    Match match = Regex.Match(result, @"jsessionid=([A-Za-z0-9\-]{32})",
                    RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        session = ";jsessionid=" + match.Groups[1].Value;
                        done = true;
                    }
                    
                }
            } catch(Exception ex) {
                Log.msg("[Error] No se puede loguear a la ruta de NPM: "+ex.Message);
            }

            return done;
        }

        /*
         *  getInfo() 
         *  VERIFICA INFORMACION XML E INTENTA DECODIFICAR DATOS
         */
        private void getInfo()
        {
            //string xml = downloadPickReport(url);
            int pickinfo = 0;
            string xml = "";

            try
            {
                xml = downloadPickReport(url + session);
                //xml = downloadPickReport(url);
                pickinfo = xml.IndexOf("PickupInfo");
            }
            catch(Exception ex)
            {
                Log.msg("[Error] al descargar el archivo XML: "+ex.Message);
            }


            Log.msg("[+] PickupInfo: " + pickinfo);

            if (pickinfo > 0)
            {
                if (xml != null)
                {
                    filtrarErrores(xml);
                }
            }
            else
            {
                Log.msg("[ERROR] No se pudo acceder a la informacion - PickupInfo: " + pickinfo);
                Log.msg("[+] Reiniciando procesos...");
                session = "";

                iniciar();
            }
        }

        /*
         *  downloadPickReport() 
         *  DESCARGA DATOS XML DEL SERVIDOR HTTP
         */
        private string downloadPickReport(string lnv)
        {
            string xml = null;
            using (var webClient = new WebClient())
            {
                try
                {
                    xml = webClient.DownloadString(lnv);
                }
                catch (Exception e)
                {
                    Log.msg(e.ToString());
                }
            }
            return xml;
        }

        /*
         *  filtrarErrores() 
         *  ANALIZA ARCHIVO XML Y GUARDA DATOS EN DB
         */
        private void filtrarErrores(string xml)
        {
            // Analizo informacion
            DateTime now = DateTime.Now;
            Log.msg("[+] Analizando informacion XML");
            Log.msg("- "+now);

            XmlDocument motoXML = new XmlDocument();
            XmlNodeList xnList = null;
            try
            {
                motoXML.LoadXml(xml);
                xnList = motoXML.SelectNodes("/PAGE/PickupInfo/MissFeederTrend/Record");
            }
            catch(Exception ex)
            {
                Log.msg("Error al intentar acceder a los nodos del XML: "+ex.Message);
            }

            /*Obtengo nombre de programa por línea*/
            string program = getProgram();

            try
            {
                List<Pickinfo> missfeedertrend = new List<Pickinfo>();

                bool added = false;
                string op = Cogiscan.DB2.getPo();

                foreach (XmlNode x in xnList)
                {
                    string valores = x["Values"].InnerText;
                    string[] info = valores.Split(new char[] { ',' });

                    //int verMode = 1;

                    //int infoValues = int.Parse(info.Length.ToString());

                    //if (infoValues >= 16) {
                    //    verMode = 2;
                    //}

                    /*
                     * En la version 2, encontrada en el servidor LNB apartir de la version 08.01.03D, 
                     * se agregan 2 valores nuevos.
                     * es necesario separar las versiones ya que en versiones anteriores no posee estos valores.
                     * y no recopilaria datos.
                     */
                     
                    Pickinfo pick = new Pickinfo();
                    pick.historial = int.Parse(info[0]);
                    pick.npm = int.Parse(info[1]);
                    pick.lado = info[3];
                    pick.partnumber = info[5];
                    pick.programa = program;
                    pick.tabla = info[2];

                    pick.total_pickup = int.Parse(info[9]);
                    pick.total_error = int.Parse(info[10]);
                    pick.error_pickup = int.Parse(info[14]);
                    pick.error_recog = int.Parse(info[13]);

                    if (pick.historial.Equals(0))
                    {
                        #region Feeder side
                        string side = "";
                        switch (int.Parse(pick.lado))
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
                        #endregion

                        #region Table format
                        string formatTabla = "";
                        switch (pick.tabla[0])
                        {
                            case '1':
                                formatTabla = ((pick.npm * 2) - 1).ToString();
                                break;
                            case '2':
                                formatTabla = ((pick.npm * 2)).ToString();
                                break;
                        }
                        if (pick.tabla[1] != '0') { side = "-TRAY"; }
                        pick.feeder = int.Parse(pick.tabla.Substring(2)) + side;
                        #endregion

                        if (pick.total_error >= Config.config_limite_error)
                        {
                            Log.msg("[+] P: " + pick.programa + ", T: " + formatTabla + ", F: " + pick.feeder + ", P#: " + pick.partnumber + ", Errores: " + pick.total_error + ", Pick: " + pick.error_pickup + ", Recog: " + pick.error_recog);
                            added = true;

                            PickerDB pq = new PickerDB();
                            pq.linea = Config.config_linea.ToString();
                            pq.maquina = "NPM";
                            pq.modulo = pick.npm;
                            pq.tabla = formatTabla;
                            pq.feeder = pick.feeder;
                            pq.partNumber = pick.partnumber;
                            pq.programa = pick.programa;
                            pq.op = op;

                            pq.total_error = pick.total_error;
                            pq.total_pickup = pick.total_pickup;
                            pq.turno = Config.turno_actual;
                            pq.insert();
                        }
                    }
                }

                if (added)
                {
                    Log.msg("**** INFORMACION CARGADA CON EXITO ****");
                }
            }
            catch(Exception ex)
            {
                Log.msg("Error al intentar cargar la información de PickUp's: " + ex.Message);
            }
            
        }

        private string getProgram()
        {
            string programa = "";
            try
            {
                string xml = downloadPickReport(urlProgramName + session + "?Range=0");
                
                XmlDocument programXML = new XmlDocument();
                programXML.LoadXml(xml);
                XmlNodeList xnList = programXML.SelectNodes("/PAGE/BoardActualGraph/Shift/Lane");
                //XmlNode lane = xnList[xnList.Count - 1];
                foreach (XmlNode x in xnList)
                {
                    //string nroLinea = x.Attributes["No"].Value;
                    programa = x.SelectSingleNode("//Name").InnerText;
                }
            }
            catch(Exception ex)
            {
                Log.msg("Error al obtener el programa: " + ex.Message);
                programa = "DESCONOCIDO";
            }
            return programa;
        }
    }
}