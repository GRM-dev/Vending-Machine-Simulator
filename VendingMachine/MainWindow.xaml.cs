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
using VendingMachine.Core;
using VendingMachine.XAML.Pages;

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Automat. Zawiera cala kontrole automate wraz widokiem MVC
    /// Klasa Main
    /// </summary>
    public partial class MainWindow : Window
    {
        private VMachine vMachine;

        public MainWindow()
        {
            InitializeComponent();
            VMMainPage = new MainPage();
            vMachine = new VMachine(this);
            MainPanelFrame.Navigate(VMMainPage);
        }

        public MainPage VMMainPage { get; private set; }
    }
}
