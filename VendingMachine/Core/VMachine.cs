using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Configuration;
using VendingMachine.VMDialogs;

namespace VendingMachine.Core
{
    /// <summary>
    /// Główna klasa programu, Model z MVC.
    /// </summary>
    public class VMachine
    {
        private static MainWindow _mainWindow;
        private Config _config;

        /// <summary>
        /// Constructs machine, load defaults, 
        /// override with values from file 
        /// and lods to machine
        /// </summary>
        /// <param name="mainWindow">The Main Window of program</param>
        public VMachine(MainWindow mainWindow)
        {
            instance = this;
            MWindow = mainWindow;
            Config = new Config();
            Config.readConfigFromFile();
            Config.loadConfigToProgram();
        }

        /// <summary>
        /// Correctly closes program.
        /// </summary>
        public static void closeProgram()
        {
            ConfigFileManager.saveConfig();
            VMDialogManager.ShowClosingDialog(instance.MWindow);
        }

        public MainWindow MWindow
        {
            get { return _mainWindow; }
            private set { _mainWindow = value; }
        }

        public Config Config
        {
            get { return _config; }
            private set { _config = value; }
        }

        public static VMachine instance
        { get; private set; }
    }
}
