using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Configuration;

namespace VendingMachine.Core
{
    /// <summary>
    /// Główna klasa programu, Model z MVC.
    /// </summary>
    public class VMachine
    {
        private static MainWindow _mainWindow;
        private Configuration.Config _config;

        public VMachine(MainWindow mainWindow)
        {
            MWindow = mainWindow;
            ConfigManager = new Configuration.Config(this);
            ConfigManager.readConfigFromFile();
            ConfigManager.loadConfigToProgram();
        }

        public static MainWindow MWindow
        {
            get { return _mainWindow; }
            private set { _mainWindow = value; }
        }

        public Config ConfigManager
        {
            get { return _config; }
            private set { _config = value; }
        }
    }
}
