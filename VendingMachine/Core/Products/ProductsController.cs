using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VendingMachine.Core.Configuration;

namespace VendingMachine.Core.Products
{
    class ProductsController
    {
        private static Dictionary<int, Product> _products = new Dictionary<int, Product>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public static void AddProductToList(Product product)
        {
            if (hasProduct(product.PData.Product_ID))
            {
                Products[product.PData.Product_ID] = product;
            }
            else
            {
                Products.Add(product.PData.Product_ID, product);
            }
            ParseProductsOnView();
        }

        /// <summary>
        /// Adds a product to Product Grid starting from collumn and row 1
        /// </summary>
        /// <param name="row">row number from 0</param>
        /// <param name="column">column number from 0</param>
        /// <param name="product">product to add</param>
        public static void AddProductToListAndView(int row, int column, Product product)
        {
            AddProductToList(product);
            AddProductToView(row, column, product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="product"></param>
        public static void AddProductToView(int row, int column, Product product)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            if (column > columns.Count || column < 1 || row > rows.Count || row < 1)
            {
                return;
            }
            ProductsView.Children.Add(product);
            Grid.SetColumn(product, --column);
            Grid.SetRow(product, --row);
        }

        /// <summary>
        /// gets product from main dictionary
        /// </summary>
        /// <param name="ID">product ID</param>
        /// <returns>Product corresponding to specified ID</returns>
        public static Product getProduct(int ID)
        {
            if (hasProduct(ID))
            {
                return Products[ID];
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodE"></param>
        /// <returns></returns>
        public static ProductData getProductData(ProductE prodE)
        {
            int ID = (int)prodE;
            return getProductData(ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ProductData getProductData(int ID)
        {
            if (hasProduct(ID))
            {
                return Products[ID].PData;
            }
            return null;
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
        /// <param name="ID"></param>
        public static void RemoveProduct(int ID)
        {
            if (hasProduct(ID))
            {
                Product product = Products[ID];
                Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
                ProductsView.Children.Remove(product);
                Products.Remove(ID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="count"></param>
        public static void RemoveProduct(string productName, int count)
        {
            var values = Enum.GetValues(typeof(ProductE));
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            int pCount = 0;
            foreach (ProductE pE in values)
            {
                if (pE.ToString().Equals(productName))
                {
                    int ID = (int)pE;
                    if (hasProduct(ID))
                    {
                        Product product = Products[ID];
                        if ((pCount = product.PData.Product_Count) > count)
                        {
                            product.PData.Product_Count -= count;
                        }
                        else
                        {
                            ProductsView.Children.Remove(product);
                            Products.Remove(ID);
                        }
                    }
                }
            }
            if (pCount < 3)
            {
                ConfigProperties.Update(ConfigPropertyType.CALL_FOR_REFILL, "true");
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ID"></param>
       /// <param name="count"></param>
        public static void RemoveProduct(int ID, int count)
        {
            var values = Enum.GetValues(typeof(ProductE));
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            foreach (ProductE pE in values)
            {
                if (((int)pE)==ID)
                {
                    if (hasProduct(ID))
                    {
                        Product product = Products[ID];
                        if (product.PData.Product_Count > count)
                        {
                            product.PData.Product_Count -= count;
                            product.Update();
                        }
                        else
                        {
                            ProductsView.Children.Remove(product);
                            Products.Remove(ID);
                        }
                    }
                }
            }
        }

        public static void RemoveProduct(string productName)
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            var values = Enum.GetValues(typeof(ProductE));
            foreach (ProductE pE in values)
            {
                if (pE.ToString().Equals(productName))
                {
                    int ID = (int)pE;
                    if (hasProduct(ID))
                    {
                        Product product = Products[ID];
                        ProductsView.Children.Remove(product);
                        Products.Remove(ID);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProductE getProductE(string name)
        {
            var values = Enum.GetValues(typeof(ProductE));
            foreach (ProductE pE in values)
            {
                if (pE.ToString().Equals(name))
                {
                    return pE;
                }
            }
            return ProductE.Null;
        }

        /// <summary>
        /// Checks if there is any product with specified number
        /// </summary>
        /// <param name="ID">product id number</param>
        /// <returns>true if exists already</returns>
        public static Boolean hasProduct(int ID)
        {
            if (Products.ContainsKey(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="newPrice"></param>
        public static void ChangeProductPrice(int ID, double newPrice)
        {
            ProductData pData = getProductData(ID);
            if (pData != null)
            {
                pData.Product_Price = newPrice;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newCount"></param>
        public static void ChangeProductCount(int id, int newCount)
        {
            ProductData pData = getProductData(id);
            if (pData != null)
            {
                pData.Product_Count = newCount;
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
            else if (slotsNmb <= 20)
            {
                columnsNmb = 4;
                rowsNmb = (slotsNmb % 4) == 0 ? (slotsNmb / 4) : ((slotsNmb + 1) % 4) == 0 ? ((slotsNmb + 1) / 4) : ((slotsNmb + 2) % 4) == 0 ? ((slotsNmb + 2) / 4) : ((slotsNmb + 3) / 4);
            }
            else
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
        public static void ParseProductsOnView()
        {
            Grid ProductsView = VMachine.instance.MWindow.VMMainPage.ProductsView;
            ColumnDefinitionCollection columns = ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            ProductsView.Children.Clear();
            int row = 1;
            int column = 1;
            foreach (KeyValuePair<int, Product> node in Products)
            {
                AddProductToView(row, column, node.Value);
                if (column == columns.Count)
                {
                    column = 1;
                    row++;
                }
                if (row == rows.Count)
                {
                    return;
                }
                node.Value.Update();
                column++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<int, Product> Products
        {
            get { return _products; }
            private set { _products = value; }
        }
    }
}
