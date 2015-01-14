using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VendingMachine.Core
{
    /// <summary>
    /// Wczytuje, zapisuje dane z/do pliku xml.
    /// Kazda zmiana w maszynie powoduje zmiane w pliku w ramach synchronizacji.
    /// </summary>
    class ConfigManager
    {
        private static Dictionary<String, Property> _config;

        public static void initSim()
        {
            Config = new Dictionary<string, Property>();
            Grid ProductsView = VMachine.MWindow.VMMainPage.ProductsView;
            if (!configFileExists())
            {
                createNewConfig();
            }
            getConfigFromFile();
            //  productsGrid.Children.Cast<IProduct>().FirstOrDefault(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);
        }

        private static bool configFileExists()
        {
            //TODO: checking of file existance
            return false;
        }

        private static void createNewConfig()
        {
            //TODO: creating of file
        }

        private static void getConfigFromFile()
        {
            //TODO: reading file
        }

        public static Dictionary<String, Property> Config
        {
            private set { _config = value; }
            get { return _config; }
        }
    }
}
