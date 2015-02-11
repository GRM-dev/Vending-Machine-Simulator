using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VendingMachine.Core.Configuration;
using VendingMachine.Simulations;

namespace VendingMachine.XAML
{
    /// <summary>
    /// Interaction logic for SimulationFlyout.xaml
    /// </summary>
    public partial class SimulationFlyout : UserControl
    {
        private Boolean _running;

        /// <summary>
        /// Main Constructor for Flyout
        /// </summary>
        public SimulationFlyout()
        {
            InitializeComponent();
            SimulationRunning = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
           
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigProperties.instance.getProperty(ConfigPropertyType.WORKS).Value == "true")
            {
                return;
            }
            SimulationRunning = true;
            if (ClientGen == null)
            {
                ClientGen = new ClientGenerator(this);
            }
            ClientGen.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            SimulationRunning = false;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public Boolean SimulationRunning
        {
            get { return _running; }
            set { _running = value;
            StartButton.IsEnabled = !_running;
            StopButton.IsEnabled = _running;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ClientGenerator ClientGen
        {
            get;
            private set;
        }
    }
}
