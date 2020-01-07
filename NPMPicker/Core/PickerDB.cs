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
    class PickerDB
    {
        public string id_stat = "";
        public int count = 0;
        public string linea ="";
        public string maquina = "";
        public string partNumber = "";
        public string programa = "";
        public string op = "";
        public int modulo = 0;
        public string tabla = "";
        public string feeder ="";
        public int total_error = 0;
        public int total_pickup = 0;
        public string turno = "";
        public string estado = "inestable";

        public int actual_total_error = 0;

        public static void ping(string linea, string maquina)
        {
            Mysql sql = new Mysql();
            string query = "";
           
            string ver = Utilidades.Version();
            string host = Dns.GetHostEntry(Dns.GetHostName()).HostName.ToString();
            
            query = @"select id,flag from `npmpicker`.`ping` where id_linea = '" + linea + "' and maquina = '" + maquina + "' limit 1";
            DataTable dt = sql.Select(query);
            if (dt.Rows.Count > 0)
            {
                query = @"update `npmpicker`.`ping` set  
                ping = NOW(),
                hostname = '" + host + @"',
                version = '" + ver + @"' 
                where id_linea = '" + linea + "'  and maquina = '" + maquina + "' limit 1";
                bool rs = sql.Ejecutar(query);

                Log.msg("[+] UPDATE PING");

                DataRow r = dt.Rows[0];
                string flag = r["flag"].ToString();
                string id = r["id"].ToString();
                if (flag.Equals("U"))
                {
                    sql.Ejecutar("Update `ping` set flag = null where id = '" + id + "' limit 1");
                    Log.msg("[+] EL administrador solicita actualizar el sistema!");
                    Utilidades.Actualizar_version();
                    Tray.restartAplicacion();
                }
            }
            else
            {
                query = @"INSERT INTO `npmpicker`.`ping` (`id_linea`, `maquina`, `ping`, `hostname`, `version`) VALUES (" + linea + ", '" + maquina + "', NOW(), '" + host + "', '" + ver+ "');";
                bool rs = sql.Ejecutar(query);

                Log.msg("[+] INSERT PING");
            }
        }

        public void insert()
        {
            // Verifico si es un feeder nuevo.
            getStatInfo();
            if (id_stat.Equals(""))
            {
                // Feeder nuevo, agrego nuevo STAT
                insertStat();
            }
            else
            {
                // El feeder ya contiene errores actualmente, actualizar count de STAT
                updateStat();
            }

            // Guardar nuevo DATA
            insertData();
        }

        private void updateStat()
        {
            Mysql sql = new Mysql();
            string count_sql = "";

            if (actual_total_error == total_error)
            {
                count_sql = " count = (count + 1) ";
                if ((count + 1) >= Config.config_limite_estable)
                {
                    estado = "estable";
                }
            }
            else
            {
                count_sql = " count = 1 "; 
            }

            string query = "update `npmpicker`.`stat` set estado = '"+estado+"',total_error = '" + total_error + "', total_pickup = '" + total_pickup + "', hora = CURTIME(),  " + count_sql + " where id = '" + id_stat + "' limit 1"; 
            sql.Ejecutar(query);
        }

        private void insertStat()
        {
            Mysql sql = new Mysql();
            string query = @"
                INSERT INTO  `npmpicker`.`stat` (
                `id` ,
                `id_linea` ,
                `maquina`, 
                `modulo` ,
                `tabla` ,
                `feeder` ,
                `partNumber`,
                `programa`,
                `op`,
                `total_error` ,
                `total_pickup` ,
                `fecha`,
                `hora`,
                `turno`,
                `count`
                )
                VALUES (
                NULL ,  '" + linea + "',  '" + maquina + "',  '" + modulo + "',  '" + tabla + "',  '" + feeder + "', '" + partNumber + "', '" + programa + "', '" + op + "', '" + total_error + "',  '" + total_pickup + "', CURDATE(), CURRENT_TIME( ) , '" + turno + "', '1'  );";

            bool rs = sql.Ejecutar(query);
            if (rs)
            {
                getStatInfo();
            }
        }

        public void insertData()
        {
            Mysql sql = new Mysql();

            string query = @"
                INSERT INTO  `npmpicker`.`data` (
                `id` ,
                `id_stat` ,
                `total_error` ,
                `total_pickup` ,
                `hora`,
                `inspeccion`,
                `ajuste`
                )
                VALUES (
                NULL ,  '" + id_stat+ "',  '" + total_error + "',  '" + total_pickup + "', CURRENT_TIME( ) ,  '0',  '0');";

            bool rs = sql.Ejecutar(query);
            if (rs)
            {
                // Insertado con exito
            }
        }

        public void getStatInfo()
        {
            Mysql sql = new Mysql();
            string query = @"
            SELECT 

            id,   
            count,
            total_error,
            total_pickup,
            turno

            FROM `npmpicker`.`stat`
        
            WHERE

            id_linea = '" + linea + @"' and            
            maquina = '" + maquina + @"' and            
            modulo = '" + modulo + @"' and            
            tabla = '" + tabla + @"' and            
            feeder = '" + feeder + @"' and
            partNumber = '" + partNumber + @"' and
            programa = '"+ programa + @"' and           
            op = '" + op + @"' and
            turno = '" + turno + @"' and            
            fecha = CURDATE()  

            limit 1
            ";

            DataTable dt = sql.Select(query);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                id_stat = r["id"].ToString();
                count = int.Parse(r["count"].ToString());
                actual_total_error = int.Parse(r["total_error"].ToString());
            }
        }

        public static string getTurno()
        {
            Mysql sql = new Mysql();
            string query = @"SELECT turno FROM `npmpicker`.`turnos` where curtime() between desde and hasta limit 1";
            //string query = @"SELECT IF(HOUR(CURTIME()) >= '00' and HOUR(CURTIME()) < '15','M','T') as turno";
            DataTable dt = sql.Select(query);

            string turno = "";
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                turno = r["turno"].ToString();
            }
            return turno;
        }

    }
}
