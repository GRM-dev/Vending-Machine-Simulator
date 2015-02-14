using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VendingMachine.Core;
using VendingMachine.Core.Configuration;
using VendingMachine.VMDialogs;
using VendingMachine.XAML;

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Automat. Contains main view page of program
    /// Klasa Main
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private VMachine vMachine;

        /// <summary>
        /// Main Window of program containing frame for pages
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            VMMainPage = new MainPage();
            ProductsView = VMMainPage.ProductsView;
            VMFrame.Navigate(VMMainPage);
            vMachine = new VMachine(this);
            ServisFrame.Navigate(ServiceFlyoutP = new ServiceFlyout());
            SimulationFrame.Navigate(SimulationFlyoutP = new SimulationFlyout());
        }

        private void Simulation_Button_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            var flyoutE = this.Flyouts.Items[1] as Flyout;
            SimulationFlyoutP.Update();
            flyoutE.IsOpen = false;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private async void Servis_Button_Click(object sender, RoutedEventArgs e)
        {
            string passwd = await DialogManager.ShowInputAsync(this, "Passwd", "passwd:");
            string passwdO = ConfigProperties.instance.getProperty(ConfigPropertyType.SERVICE_PASSWD).Value;
            if (passwd != null && passwd.Equals(passwdO))
            {
                var flyoutE = this.Flyouts.Items[0] as Flyout;
                var flyout = this.Flyouts.Items[1] as Flyout;
                ServiceFlyoutP.Update();
                flyoutE.IsOpen = false;
                flyout.IsOpen = !flyout.IsOpen;
            }
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            VMDialogManager.ShowInfoMessage("Info", "Aplikację wykonali:\nA. Gulik,\n\tM. Tycner,\n\tK. Imburski,\n\tŁ. Lewandowski.\n\nW ramach projektu naukowego");
        }

        /// <summary>
        /// When app closing is requested
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            VMachine.closeProgram();
        }

        /// <summary>
        /// Main page
        /// </summary>
        public MainPage VMMainPage { get; private set; }
        /// <summary>
        /// Grid of viewed products
        /// </summary>
        public Grid ProductsView { get; private set; }
        /// <summary>
        /// Service card
        /// </summary>
        public ServiceFlyout ServiceFlyoutP { get; private set; }
        /// <summary>
        /// Simulation card
        /// </summary>
        public SimulationFlyout SimulationFlyoutP { get; private set; }
    }
}
