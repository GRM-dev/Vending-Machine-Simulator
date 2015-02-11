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
        private byte _simState;
        /// <summary>
        /// 
        /// </summary>
        public const byte STOPPED = 0;
        /// <summary>
        /// 
        /// </summary>
        public const byte RUNNING = 1;
        /// <summary>
        /// 
        /// </summary>
        public const byte PAUSED = 2;

        /// <summary>
        /// Main Constructor for Flyout
        /// </summary>
        public SimulationFlyout()
        {
            InitializeComponent();
            SimulationState = STOPPED;
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
            if (ClientGen == null || SimulationState == STOPPED)
            {
                ClientGen = new ClientGenerator(this);
            }
            SimulationState = RUNNING;
            ClientGen.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            SimulationState = PAUSED;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            SimulationState = STOPPED;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateSimulationButtons()
        {
            Boolean start=false;
            Boolean pause=false;
            Boolean stop=false;
            switch (SimulationState)
            {
                case STOPPED: start = true; stop = false; pause = false; break;
                case RUNNING: start = false; stop = true; pause = true; break;
                case PAUSED: start = true; stop = true; pause = false; break;
            }
            StartButton.IsEnabled=start;
            PauseButton.IsEnabled=pause;
            StopButton.IsEnabled=stop;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte SimulationState
        {
            get { return _simState; }
            set
            {
                _simState = value;
                UpdateSimulationButtons();
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
