using System;
using System.Collections;
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
                ArrayList products = new ArrayList();
                foreach (Product product in ProductsController.Products.Values)
                {
                    products.Add(product.Name);
                }
                PMRemoveProduct.ItemsSource = products;
                PMChangeProductCount.ItemsSource = products;
                PMChangeProductPrice.ItemsSource = products;
                ProductsController.ParseProductsOnView();
            }
            else
            {
                RemoveProductButton.IsEnabled = false;
                PMRemoveProduct.ItemsSource = null;
            }
            Array pEs = Enum.GetValues(typeof(ProductE));
            ArrayList aL = new ArrayList();
            aL.AddRange(pEs);
            aL.Remove(ProductE.Null);
            PMAddProduct.ItemsSource = aL;
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
                    ProductsController.AddProductToList(new Product((ProductE)PMAddProduct.SelectedItem));
                }
            }
            Update();
        }

        private void RemoveProduct_Button_Click(object sender, RoutedEventArgs e)
        {
            string txt = PMRemoveCount.Text;
            ValueHandler vh = new ValueHandler(txt);
            if (PMRemoveProduct.SelectedItem != null && vh.PropertyType != ValueTypes.STRING && ((double)vh.Value) > 0)
            {
                var item = PMRemoveProduct.SelectedItem as string;

                ProductsController.RemoveProduct(item);
            }
            Update();
        }
        private void ChangeProductCount_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PMChangeProductCount.SelectedItem != null)
            {
                string txt = PMChangeCountNmb.Text;
                ValueHandler vh = new ValueHandler(txt);
                if (vh.PropertyType != ValueTypes.STRING)
                {
                    int intVal = Convert.ToInt32(vh.Value);
                    if (intVal > 0 &&
                    intVal <= Convert.ToInt32(ConfigProperties.instance.getProperty(ConfigPropertyType.SLOT_SIZE).Value))
                    {
                        var pName = PMChangeProductCount.SelectedItem as string;
                        ProductE pE = ProductsController.getProductE(pName);
                        if (pE != ProductE.Null)
                        {
                            ProductsController.ChangeProductCount((int)pE, intVal);
                        }
                    }
                }
            }
            Update();
        }

        private void ChangeProductPrice_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PMChangeProductPrice.SelectedItem != null)
            {
                string txt = PMChangePriceNmb.Text;
                ValueHandler vh = new ValueHandler(txt);
                double dbVal = 0;
                if (vh.PropertyType != ValueTypes.STRING && (dbVal=(double)vh.Value) > 0)
                {
                    var pName = PMChangeProductPrice.SelectedItem as string;
                    ProductE pE = ProductsController.getProductE(pName);
                    if (pE != ProductE.Null)
                    {
                        ProductsController.ChangeProductPrice((int)pE, dbVal);
                    }
                }
            }
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
