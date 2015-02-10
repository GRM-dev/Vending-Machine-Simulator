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
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Misc;
using VendingMachine.Core.Products;

namespace VendingMachine.XAML
{
    /// <summary>
    /// Interaction logic for ServiceFlyout.xaml
    /// </summary>
    public partial class ServiceFlyout : Page
    {
        /// <summary>
        /// Main Constructor
        /// </summary>
        public ServiceFlyout()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (ConfigProperties.instance.Contains(ConfigPropertyType.SERVICE_NEEDED) && ConfigProperties.instance.getProperty(ConfigPropertyType.SERVICE_NEEDED).Value.Equals("true"))
            {
                RepairButton.IsEnabled = true;
            }
            else
            {
                RepairButton.IsEnabled = false;
            }
            if (ProductsController.Products.Count != 0)
            {
                RemoveProductButton.IsEnabled = true;
                PMRemoveProduct.ItemsSource = ProductsController.Products.Keys;
            }
            else
            {
                RemoveProductButton.IsEnabled = false;
                PMRemoveProduct.ItemsSource = null;
            }
            PMAddProduct.ItemsSource = Enum.GetValues(typeof(ProductE));
        }

        private void Repair_Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigProperties.instance.getProperty(ConfigPropertyType.SERVICE_NEEDED).Value = "true";
            Update();
        }

        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PMAddProduct.SelectedItem is ProductE)
            {
                string txt = PMAddCount.Text;
                ValueHandler vh = new ValueHandler(txt);
                if (vh.PropertyType != ValueTypes.STRING && ((double)vh.Value) > 0 && 
                    ((double)vh.Value) <= (double)new ValueHandler(ConfigProperties.instance.getProperty(ConfigPropertyType.SLOT_SIZE).Value).Value)
                {
                    Console.Out.WriteLine("aaa");
                    ProductsController.AddProductToList(new Product((ProductE)PMAddProduct.SelectedItem));
                }
            }
            Update();
        }

        private void RemoveProduct_Button_Click(object sender, RoutedEventArgs e)
        {

            Update();
        }

        private void ShowStats_Button_Click(object sender, RoutedEventArgs e)
        {

            Update();
        }

        private void ChangeAccount_Button_Click(object sender, RoutedEventArgs e)
        {

            Update();
        }
    }
}
