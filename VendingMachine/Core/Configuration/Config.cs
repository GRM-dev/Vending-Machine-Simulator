using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VendingMachine.Core.Misc;
using VendingMachine.Core.Products;
using VendingMachine.Properties;
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
        /// local resource dictionary VMDictionary
        /// </summary>
        public static ResourceDictionary VMRD;

        /// <summary>
        /// Main configuration handler creator
        /// </summary>
        public Config()
        {
            ConfigProperties.instance.initDefaultProperties();
            VMRD = new ResourceDictionary();
            VMRD.Source =
                new Uri("pack://application:,,,/VendingMachine;component/XAML/VMDictionary.xaml",
                        UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadAllConfigs()
        {
            readConfigFromFile();
            loadConfigToProgram();
            loadProductsToProgram(ConfigFileManager.getProductsFromFile());
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
                    ConfigProperties.LoadFromDictionary(ConfigFileManager.getOptionsFromFile());
                }
                printConfig();
            }
            catch (Exception e)
            {
                VMDialogManager.ShowExceptionMessage(e);
            }
        }

        private static void printConfig()
        {
            Console.WriteLine("-------- Config Start --------");
            foreach (KeyValuePair<int, ConfigProperty> entry in ConfigProperties.instance.Properties)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value.Name + " = " + entry.Value.Value);
            }
            Console.WriteLine("-------- Config End ----------");
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
        }

        /// <summary>
        /// Loads products to window page
        /// </summary>
        public void loadProductsToProgram(Dictionary<ProductE, ProductData> products)
        {
            foreach (KeyValuePair<ProductE, ProductData> node in products)
            {
                Product product = new Product(node.Value);
                ProductsController.Products.Add(node.Value.Product_Name,product);
            }
            ProductsController.ParseProductsOnView();
        }

        private static void testAddNewProducts() //TODO: delete after creating service panel
        {
            try
            {
                Product p1 = new Product(ProductE._3Bit);
                Product p2 = new Product(ProductE.Mars);
                Product p3 = new Product(ProductE.Cappy);
                ProductsController.AddProductToListAndView(1, 1, p1);
                ProductsController.AddProductToListAndView(1, 2, p2);
                ProductsController.AddProductToListAndView(2, 2, p3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                VMDialogManager.ShowExceptionMessage(e);
            }
        }
    }
}
