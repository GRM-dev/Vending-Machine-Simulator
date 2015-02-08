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
        private ImageSource _image;
        private ProductData _productData;
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates Product place holder
        /// </summary>
        private Product()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productE"></param>
        public Product(ProductE productE)
            : this()
        {
            ProductDatas = new ProductData(productE);
            this.Name = ProductDatas.Product_Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productData"></param>
        public Product(ProductData productData)
            : this()
        {
            ProductDatas = productData;
            this.Name = ProductDatas.Product_Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productE"></param>
        /// <param name="price"></param>
        /// <param name="count"></param>
        public Product(ProductE productE, double price, int count)
            : this(productE)
        {
            ProductDatas.Product_Price = price;
            ProductDatas.Product_Count = count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

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

        /// <summary>
        /// 
        /// </summary>
        public ProductData ProductDatas
        {
            get { return _productData; }
            set { _productData = value;
            ProductImageSource = _productData.ProductImageSource;
            NotifyPropertyChanged("ProductDatas");
            }
        }
    }
}
