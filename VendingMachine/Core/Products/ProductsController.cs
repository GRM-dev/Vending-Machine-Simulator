using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VendingMachine.Core.Products
{
    class ProductsController
    {
        private static Dictionary<String, Product> _products = new Dictionary<String, Product>();

        /// <summary>
        /// Adds a product to Product Grid starting from collumn and row 1
        /// </summary>
        /// <param name="row">row number from 0</param>
        /// <param name="column">column number from 0</param>
        /// <param name="product">product to add</param>
        public static void AddProduct(int row, int column, Product product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            if (column > columns.Count || column < 1 || row > rows.Count || row < 1)
            {
                return;
            } Console.Out.WriteLine("Adding " + product.Name + "  Total: " + Products.Count);
            if (Products.ContainsKey(product.Name))
            {
                Products[product.Name] = product;
            }
            else
            {
                Products.Add(product.Name, product);
            }
            ProductsView.Children.Add(product);
            Grid.SetColumn(product, --column);
            Grid.SetRow(product, --row);
        }

        /// <summary>
        /// Removes a product from Grid
        /// </summary>
        /// <param name="product"></param>
        public static void RemoveProductFromView(Product product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ProductsView.Children.Remove(product);
        }

        /// <summary>
        /// Removes specified Product
        /// </summary>
        /// <param name="productNumber"></param>
        public static void RemoveProduct(String productNumber)
        {
            if (Products.ContainsKey(productNumber))
            {
                Product product = Products[productNumber];
                Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
                ProductsView.Children.Remove(product);
            }
        }

        /// <summary>
        /// Checks if there is any product with specified number
        /// </summary>
        /// <param name="number">product number</param>
        /// <returns>true if exists already</returns>
        public Boolean hasProductWithNumber(String number)
        {
            if (Products.ContainsKey(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Setups slots for machine simulator operations
        /// </summary>
        /// <param name="slots"></param>
        public static void setupSlots(String slots)
        {
            int slotsNmb = Convert.ToInt32(slots);
            int rowsNmb = 1;
            int columnsNmb = 1;
            if (slotsNmb < 4)
            {
                columnsNmb = slotsNmb;
            }
            else if (slotsNmb <= 20){
                columnsNmb = 4;
                rowsNmb = (slotsNmb % 4) == 0 ? (slotsNmb / 4) : ((slotsNmb + 1) % 4) == 0 ? ((slotsNmb + 1) / 4) : ((slotsNmb + 2) % 4) == 0 ? ((slotsNmb + 2) / 4) : ((slotsNmb + 3) / 4);
            } else
            {
                rowsNmb = 5;
                columnsNmb = 5;
            }
           
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            while (columns.Count < columnsNmb)
            {
                columns.Add(new ColumnDefinition());
            }
            while (rows.Count < rowsNmb)
            {
                rows.Add(new RowDefinition());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<String, Product> Products
        {
            get { return _products; }
            private set { _products = value; }
        }
    }
}
