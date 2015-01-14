using MahApps.Metro.Controls;
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
    public partial class MainWindow : MetroWindow
    {
        private VMachine vMachine;

        public MainWindow()
        {
            InitializeComponent();
            VMMainPage = new MainPage(); 
            ProductsPage = new ProductsPage();
            VMFrame.Navigate(VMMainPage);
            VMMainPage.MainPanelFrame.Navigate(ProductsPage);
            vMachine = new VMachine(this);
        }

        public MainPage VMMainPage { get; private set; }
        public ProductsPage ProductsPage { get; private set; }
    }
}
