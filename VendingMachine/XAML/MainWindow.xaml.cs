using MahApps.Metro.Controls;
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

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Automat. Zawiera cala kontrole automate wraz widokiem MVC
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
        }

        private void Simulation_Button_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void Servis_Button_Click(object sender, RoutedEventArgs e)
        {
            VMDialogManager.ShowServiceDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            VMachine.closeProgram();
        }

        public MainPage VMMainPage { get; private set; }
        public Grid ProductsView { get; private set; }
    }
}
