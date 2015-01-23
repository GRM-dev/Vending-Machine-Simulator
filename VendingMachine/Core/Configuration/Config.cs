using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Misc;
using VendingMachine.VMDialogs;

namespace VendingMachine.Core.Configuration
{
    public class Config
    {
        public static String APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static String APP_PATH_MAIN = APP_DATA + @"\VMachine\";
        public static String LOG_FILE_NAME = "VMachine.log";
        public static String CONFIG_FILE_PATH = APP_PATH_MAIN + "VM-config.xml";

        public Config()
        {
            ConfigProperties.instance.initDefaultProperties();
        }

        /// <summary>
        /// Check if config file exists. (If not than create one).
        /// Loads configs from file
        /// </summary>
        public void readConfigFromFile()
        {
            try
            {
                if (!ConfigFileManager.configExists())
                {
                    Logger.Log("Not found config file.\nCreating one.");
                    ConfigFileManager.createNewConfig();
                }
                else
                {
                    ConfigProperties.LoadFromDictionary(ConfigFileManager.getConfigFileOptions());
                }
                Console.WriteLine("-------- Config Start --------");
                foreach (KeyValuePair<int, ConfigProperty> entry in ConfigProperties.instance.Properties)
                {
                    Console.WriteLine(entry.Key + ": " + entry.Value.Name + " = " + entry.Value.Value);
                }
                Console.WriteLine("-------- Config End ----------");
            }
            catch (Exception e)
            {
                 VMDialogManager.ShowExceptionMessage(e);
            }
        }

        /// <summary>
        /// Implements read parameters from file to program
        /// </summary>
        public void loadConfigToProgram()
        {
            foreach (ConfigPropertyType prop in Enum.GetValues(typeof(ConfigPropertyType)))
            {
                ConfigProperty property = ConfigProperties.instance.getProperty(prop);
                if (property == null)
                {
                    Logger.Log("There is no proprty " + prop.ToString() + " in properties.");
                    continue;
                }
                switch (prop)
                {
                    case ConfigPropertyType.WINDOW_HEIGHT: VMachine.instance.MWindow.Height = Convert.ToInt32(property.Value); break;
                    case ConfigPropertyType.WINDOWS_WIDTH: VMachine.instance.MWindow.Width = Convert.ToInt32(property.Value); break;
                    case ConfigPropertyType.MONEY_COLLECTED: break;
                    case ConfigPropertyType.SERVICE_NEEDED: break;
                    case ConfigPropertyType.SERVICE_PASSWD: break;
                    case ConfigPropertyType.SLOTS_COUNT: break;
                    default: Logger.Log("There is no " + property.PropertyType.ToString() + " property in configuration loader switch statement!"); break;
                }
            }
        }
}
}
