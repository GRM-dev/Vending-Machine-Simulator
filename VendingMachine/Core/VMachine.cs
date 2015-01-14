using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core
{
    /// <summary>
    /// Główna klasa programu, Model z MVC.
    /// </summary>
    class VMachine
    {
        private static MainWindow _mainWindow;

        public VMachine(MainWindow mainWindow)
        {
            MWindow = mainWindow;
            ConfigManager.initSim();
        }

        public static MainWindow MWindow
        {
            get { return _mainWindow; }
            private set { _mainWindow = value; }
        }
    }
}
