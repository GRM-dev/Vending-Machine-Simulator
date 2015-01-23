using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VendingMachine.Core.Configuration;

namespace VendingMachine.Core.Misc
{
    class Logger
    {
        public static void DumpLog()
        {
            using (StreamReader r = File.OpenText(Config.APP_PATH_MAIN + Config.LOG_FILE_NAME))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static void Log(string logMessage)
        {
            Console.WriteLine(logMessage);
            using (StreamWriter w = File.AppendText(Config.APP_PATH_MAIN + Config.LOG_FILE_NAME))
            {
                w.Write("\r\nInfo : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }

        public static void ExceptionLog(Exception e, String message)
        {
            Console.WriteLine(message);
            using (StreamWriter w = File.AppendText(Config.APP_PATH_MAIN + Config.LOG_FILE_NAME))
            {
                w.Write("\r\nException: ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :{0}", message);
                w.WriteLine(e.Message);
                w.WriteLine("-------------------------------");
            }
        }
    }
}
