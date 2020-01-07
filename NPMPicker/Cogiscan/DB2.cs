using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using NPMPicker.Cogiscan;
using Microsoft.CSharp;
namespace NPMPicker.Cogiscan
{
    class DB2
    {
        public static string getPo()
        {
            int linea = Config.config_linea;
            string po = "";
            var json="";
            string url = "http://arushap34/iaserver/public/cogiscan/db2/opByComplexTool/" + linea;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
                json = json.Substring(1, (json.Length - 2));
            }
            try
            {
                if (json!="[]")
                {
                    dynamic objJson = JObject.Parse(json);
                    po = objJson.BATCH_ID;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return po;
        }
    }
}
