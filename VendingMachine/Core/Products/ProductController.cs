using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VendingMachine.Core.Products
{
    class ProductController
    {
        private static Dictionary<String, IProduct> products = new Dictionary<string, IProduct>();

        /// <summary>
        /// Adds a product to Product Grid starting from collumn and row 1
        /// </summary>
        /// <param name="row">row number from 0</param>
        /// <param name="column">column number from 0</param>
        /// <param name="product">product to add</param>
        public static void AddProductTo(int row, int column, IProduct product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            if (column > columns.Count || column < 1 || row > rows.Count || row < 1)
            {
                return;
            }
            products[product.Name] = product;
            ProductsView.Children.Add(product);
            Grid.SetColumn(product, --column);
            Grid.SetRow(product, --row);
        }

        /// <summary>
        /// Removes a product from Grid
        /// </summary>
        /// <param name="product"></param>
        public static void RemoveProduct(IProduct product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ProductsView.Children.Remove(product);
        }

        public static void RemoveProduct(String productName)
        {
            IProduct product = null;
            if (products.ContainsKey(productName))
            {
                product = products[productName]; Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
                ProductsView.Children.Remove(product);
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
            if (slotsNmb > 20)
            {
                rowsNmb = 4;
                columnsNmb = 5;
            }
            else if (slotsNmb < 3)
            {
                rowsNmb = slotsNmb;
            }
            else
            {
                rowsNmb = 4;
                while (columnsNmb < slotsNmb - 4)
                {
                    columnsNmb += 4;
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
    }
}
