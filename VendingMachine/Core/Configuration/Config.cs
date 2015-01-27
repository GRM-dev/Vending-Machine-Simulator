using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VendingMachine.Core.Misc;
using VendingMachine.Core.Products;
using VendingMachine.VMDialogs;

namespace VendingMachine.Core.Configuration
{
    /// <summary>
    /// Main configuration handler
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Path to %appdata% folder
        /// </summary>
        public static String APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// Path to program config/log files
        /// </summary>
        public static String APP_PATH_MAIN = APP_DATA + @"\VMachine\";
        /// <summary>
        /// Name of log file
        /// </summary>
        public static String LOG_FILE_NAME = "VMachine.log";
        /// <summary>
        /// Name of config file
        /// </summary>
        public static String CONFIG_FILE_PATH = APP_PATH_MAIN + "VM-config.xml";

        /// <summary>
        /// Main configuration handler creator
        /// </summary>
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
                if (!ConfigFileManager.configFileExists())
                {
                    Logger.Log("Not found config file.\nCreating one.");
                    ConfigFileManager.createNewConfigFile();
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
                    case ConfigPropertyType.SLOTS_COUNT: ProductsController.setupSlots(property.Value); break;
                    default: Logger.Log("There is no " + property.PropertyType.ToString() + " property in configuration loader switch statement!"); break;
                }
            }
            Product p1 = new Product(1,2.3);
            BitmapImage im1 = new BitmapImage();
            /*im1.BeginInit();
            im1.UriSource = new Uri("../Resources/Header.png");
            im1.EndInit();
            p1.ProductImageSource = im1;*/
            Product p2 = new Product(2,3.0);
            ProductsController.AddProductTo(1, 1,p1);
            ProductsController.AddProductTo(1, 2, p2);
        }

        /// <summary>
        /// Loads products to window page
        /// </summary>
        public void loadProducts()
        {

        }
    }
}
