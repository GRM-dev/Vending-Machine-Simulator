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

namespace VendingMachine.Core.Products
{
    /// <summary>
    /// Interaction logic for IProduct.xaml
    /// </summary>
    public partial class IProduct : UserControl
    {

        /// <summary>
        /// Creates Product place holder
        /// </summary>
        private IProduct()
        {
            InitializeComponent();
        }

        public IProduct(string name, int left, float price)
            : this()
        {
            Product_Left.Content = left.ToString();
            Product_Price.Content = price.ToString();
            Name = name;
        }

        public IProduct(string name, int left, double price)
            : this()
        {
            Product_Left.Content = left.ToString();
            Product_Price.Content = price.ToString();
            Name = name;
        }
    }
}
