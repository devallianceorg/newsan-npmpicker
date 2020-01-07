using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Management;
using System.Diagnostics;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using NPMPicker;

namespace CogiscanManager.Controles
{
    class RestartCAMX
    {
        public static int deleteHour = 23;
        public static bool deleteMode = false;

        public static bool isDeleteMprMode()
        {
            DateTime now = DateTime.Now;
            if (now.Hour == RestartCAMX.deleteHour)
            {
                RestartCAMX.deleteMode = true;
            } else
            {
                RestartCAMX.deleteMode = false;
            }

            return RestartCAMX.deleteMode;
        }

        public static string DoTheJob(String path)
        {
            Log.cogiscan("+ DoTheJob:" + path);

            try
            {                
                string results = "";

                /*
                 * Sniper que reinicia CAMX Collection point
                 */
                #region SniperCAMX
                string ptm = partyTimeMethod();
                #endregion

				 /*
				 * Sniper que borra los MPR y vacia las carpetas de Maquinas
				 */
                #region SniperMPR
                string nc = nightCall(path);
                #endregion

                Log.cogiscan(ptm);               

                return results;
            }
            catch (Exception snipperException)
            {
                Log.cogiscan(snipperException.Message);
                
                return snipperException.Message;
            }
        }

        static string partyTimeMethod()
        {
            try
            {
                ServiceController sc = new ServiceController("Cogiscan CAMX Collection Point");

                Console.WriteLine("\n ");
                Console.WriteLine("Snipper en: " + sc.ServiceName);

                if (sc.Status.Equals(ServiceControllerStatus.Running))
                { sc.Stop(); sc.Refresh(); }
                System.Threading.Thread.Sleep(5000);

                Console.WriteLine("Proceso en Stop...\n ");
                Console.WriteLine("Estado del proceso: {0}.", sc.Status.ToString());

                while ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
                {
                    if (!sc.Status.Equals(ServiceControllerStatus.Running))
                    { sc.Start(); sc.Refresh(); }
                    System.Threading.Thread.Sleep(5000);
                }

                Console.WriteLine("Proceso en Start...\n ");
                Console.WriteLine("Estado del proceso: {0}.", sc.Status.ToString());
                string returnedStatus = "Estado del proceso: "+sc.Status.ToString();
                return returnedStatus;
            }
            catch (Exception snipperException)
            {
                return snipperException.Message;
            }
        }

        static string nightCall(string path)
        {
            try
            {
                string ga = GrantAccess(path);
                string ds = DoSomething(path);
                return ga + " - " + ds;
            }
            catch (System.IO.IOException e) { Console.WriteLine(e.Message); return e.Message.ToString(); }
        }

        static string GrantAccess(string fullPath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(fullPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                dSecurity.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.SetAccessControl(fullPath, dSecurity);

                dInfo.SetAccessControl(dSecurity);
                return "access granted";
            }
            catch (System.IO.IOException e) { Console.WriteLine(e.Message); return e.Message.ToString(); }
        }       

        static string DoSomething(string path)
        {
            try
            {
                Console.WriteLine("Deleted Files: ");
                foreach (var pathToList in Directory.GetFiles(path, "*.MPR", SearchOption.AllDirectories))
                {
                    File.Delete(pathToList);
                    Console.WriteLine(pathToList.ToString());
                }
                return "Files Deleted";
            }
            catch (System.IO.IOException e) { Console.WriteLine(e.Message); return e.Message.ToString(); }
        }
    }
}
