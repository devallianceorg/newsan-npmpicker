using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M77;
using System.Windows.Forms;

namespace NPMPicker
{
    class Config
    {
        public static string turno_actual = "";

        public static string config_folder = @"C:\M77\NPMPicker\";

        // Configuracion por defecto
        public static string NPM_LNB = @"192.168.10.1:8080";
        public static string NPM_LNB_USER = "administrator";
        public static string NPM_LNB_PASS = "";
        public static string CM_pt200 = @"E:\kme\pt200\ProductReport\"; //@""; // 

        public static string server = "10.30.10.22";
        public static string db = "npmpicker";
        public static string user = "npmpicker";
        public static string pass = "npmpicker";
        public static string adminPass = "admin";

        public static int camx = 23;
        public static int config_linea = 16;
        public static int config_limite_error = 5;
        public static int config_limite_estable = 4;
        public static string config_modulo = "";  

        public static string config_file = "";
        public static INIFile ini;

        public static void start()
        {
            SetIniPath(Config.config_folder + "config.ini");

            // Si el archivo de configuracion existe
                if(Convert.ToBoolean(AppConfig.Read("NPMPICKER","legacy")))
                {
                    LoadOldConfigFile();
                    AppConfigSave();                    
                }
                AppConfigLoad();

//            } catch(Exception ex) 
//            {
//                MessageBox.Show("ERROR: " + ex.Message);
////                Application.Exit();
//            }
        }

        public static void SetIniPath(string ini_path)
        {
            config_file = ini_path;
            ini = new INIFile(config_file);
        }

        public static void AppConfigSave()
        {
            try
            {
                // Se genera nuevo archivo INI con la configuracion por defecto.
                AppConfig.Save("NPMPICKER", "admin_password", Utilidades.Encrypt_byte(Config.adminPass));

                AppConfig.Save("NPMPICKER", "npm_lnb", Config.NPM_LNB.ToString());
                AppConfig.Save("NPMPICKER", "cm_pt200", Config.CM_pt200.ToString());

                AppConfig.Save("NPMPICKER", "modulo", Config.config_modulo.ToString());
                AppConfig.Save("NPMPICKER", "linea", Config.config_linea.ToString());
                AppConfig.Save("NPMPICKER", "limite_error", Config.config_limite_error.ToString());
                AppConfig.Save("NPMPICKER", "limite_estable", Config.config_limite_estable.ToString());
                AppConfig.Save("NPMPICKER", "camx", Config.camx.ToString());

                AppConfig.Save("MYSQL", "server", Config.server);
                AppConfig.Save("MYSQL", "database", Utilidades.Encrypt_byte(Config.db));
                AppConfig.Save("MYSQL", "user", Utilidades.Encrypt_byte(Config.user));
                AppConfig.Save("MYSQL", "pass", Utilidades.Encrypt_byte(Config.pass));

                AppConfig.Save("NPMPICKER", "legacy", "false");
            }
            catch(Exception ex)
            {
                Log.msg("Error al guardar la configuración de la aplicación: "+ex.Message);
            }
            
        }

        public static void AppConfigLoad()
        {
            try
            {
                // Cargo configuracion
                Config.server = AppConfig.Read("MYSQL", "server");
                Config.db = Utilidades.Decrypt_byte(AppConfig.Read("MYSQL", "database"));
                Config.user = Utilidades.Decrypt_byte(AppConfig.Read("MYSQL", "user"));
                Config.pass = Utilidades.Decrypt_byte(AppConfig.Read("MYSQL", "pass"));

                Config.adminPass = Utilidades.Decrypt_byte(AppConfig.Read("NPMPICKER", "admin_password"));

                if (Config.adminPass.Equals("")) { Config.adminPass = "admin"; }

                Config.config_modulo = AppConfig.Read("NPMPICKER", "modulo");
                Config.config_linea = int.Parse(AppConfig.Read("NPMPICKER", "linea"));
                Config.config_limite_error = int.Parse(AppConfig.Read("NPMPICKER", "limite_error"));
                Config.config_limite_estable = int.Parse(AppConfig.Read("NPMPICKER", "limite_estable"));
                Config.camx = int.Parse(AppConfig.Read("NPMPICKER", "camx"));

                Config.NPM_LNB = AppConfig.Read("NPMPICKER", "npm_lnb");
                Config.NPM_LNB_USER = AppConfig.Read("NPMPICKER", "npm_lnb_user");
                Config.NPM_LNB_PASS = AppConfig.Read("NPMPICKER", "npm_lnb_pass");
                Config.CM_pt200 = AppConfig.Read("NPMPICKER", "cm_pt200");

                // Asigno a mysql
                Mysql.server = Config.server;
                Mysql.database = Config.db;
                Mysql.user = Config.user;
                Mysql.password = Config.pass;
            }
            catch(Exception ex)
            {
                Log.msg("Error al cargar la configuración en la aplicación: "+ex.Message);
            }
            
        }

        public static void LoadOldConfigFile()
        {
            try
            {
                // Cargo configuracion de archivo INI
                Config.server = ini.Read("servidor", "server");
                Config.db = Utilidades.Decrypt_byte(ini.Read("servidor", "database"));
                Config.user = Utilidades.Decrypt_byte(ini.Read("servidor", "user"));
                Config.pass = Utilidades.Decrypt_byte(ini.Read("servidor", "pass"));

                Config.adminPass = Utilidades.Decrypt_byte(ini.Read("servidor", "admin"));

                if (Config.adminPass.Equals("")) { Config.adminPass = "admin"; }

                Config.config_linea = int.Parse(ini.Read("servidor", "linea"));
                Config.config_limite_error = int.Parse(ini.Read("servidor", "limite_error"));
                Config.config_modulo = ini.Read("servidor", "modulo");

                Config.NPM_LNB = ini.Read("servidor", "npm_lnb");
                Config.CM_pt200 = ini.Read("servidor", "cm_pt200");

                // Asigno a mysql
                Mysql.server = Config.server;
                Mysql.database = Config.db;
                Mysql.user = Config.user;
                Mysql.password = Config.pass;
            }
            catch (Exception ex)
            {
                Log.msg("Error al cargar el archivo de configuración ANTERIOR: "+ex.Message);
            }
            
        }
    }
}
