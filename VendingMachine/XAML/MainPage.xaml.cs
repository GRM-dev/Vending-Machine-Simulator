using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
using VendingMachine.Core.Products;
using VendingMachine.VMDialogs;

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private double _restMoney = 0;
        /// <summary>
        /// Initialize components and product grid
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ProductsView.ShowGridLines = true; //TODO: just to test
        }

        private void wrzut_Monety_Click(object sender, RoutedEventArgs e)
        {
            Console_2.Text = "";
            VMDialogManager.ShowInputCoinDialog();
        }

        private void Nmb_1_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(1);
        }

        private void Nmb_2_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(2);
        }

        private void Nmb_3_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(3);
        }

        private void Nmb_4_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(4);
        }

        private void Nmb_5_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(5);
        }

        private void Nmb_6_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(6);
        }

        private void Nmb_7_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(7);
        }

        private void Nmb_8_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(8);
        }

        private void Nmb_9_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(9);
        }

        private void Nmb_0_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(0);
        }

        private void Nmb_C_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(100);
        }

        private void Nmb_H_Button_Click(object sender, RoutedEventArgs e)
        {
            Nmb_Button_Clicked(110);
        }

        private void Zwrot_Button_Click(object sender, RoutedEventArgs e)
        {
            RestMoney = CoinController.ReturnTempDepo();
            if (Console_1.Text.Contains("Stan konta"))
            {
                Console_1.Text = "Stan konta: 0";
            }
            Reszta.Content = RestMoney;
        }

        private void Reszta_Button_Click(object sender, RoutedEventArgs e)
        {
            RestMoney = 0; //TODO: zwracanie do klienta
            Reszta.Content = RestMoney;
        }

        /// <summary>
        /// Base input number buton method
        /// </summary>
        /// <param name="number"></param>
        private void Nmb_Button_Clicked(int number)
        {
            if (ConfigProperties.instance.getProperty(ConfigPropertyType.WORKS).Value == "true")
            {
                return;
            }
            if (!checkingIn)
            {
                Console_2.Text = "";
                checkingIn = true;
            }
            switch (number)
            {
                case 100:
                    Console_2.Text = "";
                    checkingIn = false;
                    break;
                case 110:
                    if (!Console_2.Text.Equals(""))
                    {
                        Console_2.Text = Console_2.Text.Substring(0, Console_2.Text.Length - 1);
                    }
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    Console_2.Text = Console_2.Text + number;
                    break;
                default: break;
            }
            checkForProduct();
        }

        private void checkForProduct()
        {
            try
            {
                int ID = Convert.ToInt32(Console_2.Text);
                if (ProductsController.hasProduct(ID))
                {
                    Product productO = ProductsController.getProduct(ID);
                    if (CoinController.hasEnoughMoney(productO.PData.Product_Price))
                    {
                        Product product = productO.clone();
                        CoinController.transferMoneyToMainDepo(productO.PData.Product_Price);
                        ProductsController.RemoveProduct(ID, 1);
                        VMDialogManager.ShowDeployProductMessage(product);
                        Console_1.Text = CoinController.TempCoinDepository.ToString();
                        Console_2.Text = "";
                    }
                }
            }
            catch (FormatException e)
            {
            }
        }

        /// <summary>
        /// Rest of money to take on by client
        /// </summary>
        public double RestMoney
        {
            get { return _restMoney; }
            set { _restMoney = value; }
        }

        /// <summary>
        /// is number of slot checking in?
        /// </summary>
        public Boolean checkingIn { get; set; }
    }
}