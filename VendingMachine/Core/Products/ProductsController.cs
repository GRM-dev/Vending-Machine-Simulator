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
        private static Dictionary<String, Product> activeProducts = new Dictionary<String, Product>();
        private static Dictionary<String, ProductData> allProducts = new Dictionary<String, ProductData>();

        /// <summary>
        /// Adds a product to Product Grid starting from collumn and row 1
        /// </summary>
        /// <param name="row">row number from 0</param>
        /// <param name="column">column number from 0</param>
        /// <param name="product">product to add</param>
        public static void AddProductTo(int row, int column, Product product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            if (column > columns.Count || column < 1 || row > rows.Count || row < 1)
            {
                return;
            }
            activeProducts[product.Name] = product;
            ProductsView.Children.Add(product);
            Grid.SetColumn(product, --column);
            Grid.SetRow(product, --row);
        }

        /// <summary>
        /// Removes a product from Grid
        /// </summary>
        /// <param name="product"></param>
        public static void RemoveProduct(Product product)
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
            Product product = null;
            if (activeProducts.ContainsKey(productNumber))
            {
                product = activeProducts[productNumber]; 
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
            if (activeProducts.ContainsKey(number))
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
            if (slotsNmb >= 20)
            {
                rowsNmb = 4;
                columnsNmb = 5;
            }
            else if (slotsNmb < 4)
            {
                columnsNmb = slotsNmb;
            }
            else
            {
                columnsNmb = 4;
                while (rowsNmb < slotsNmb - 4)
                {
                    rowsNmb += 4;
                }
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

        private static void initProductsDictionary(){
           // allProducts.Add("",)
        }

    }
}
