using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    public partial class Product : UserControl, INotifyPropertyChanged
    {
        private String _product_Name;
        private int _product_ID;
        private double _product_Price;
        private ImageSource _image;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates Product place holder
        /// </summary>
        private Product()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public Product(int ID, double price)
            : this()
        {
            // Name = name;
            Product_ID = ID;
            Product_Price = price;
        }

        public Product(int ID, double price, int count)
            : this(ID, price)
        {

        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        /// <summary>
        /// Name of product
        /// </summary>
        public String Product_Name { get { return _product_Name; } private set { _product_Name = value; NotifyPropertyChanged("Product_Name"); } }
        /// <summary>
        /// Product ID
        /// </summary>
        public int Product_ID { get { return _product_ID; } private set { _product_ID = value; NotifyPropertyChanged("Product_ID"); } }
        /// <summary>
        /// Product Price
        /// </summary>
        public double Product_Price { get { return _product_Price; } private set { _product_Price = value; NotifyPropertyChanged("Product_Price"); } }
        /// <summary>
        /// 
        /// </summary>
        public int Product_Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImageSource ProductImageSource
        {
            get { return _image; }
            set
            {
                _image = value;
                Product_Image.Source = _image;
            }
        }
    }
}
