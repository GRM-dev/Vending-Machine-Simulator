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
    public partial class Product : UserControl
    {
        private ImageSource _image;
        private ProductData _productData;

        /// <summary>
        /// Creates Product place holder
        /// </summary>
        private Product()
        {
            InitializeComponent();
            this.DataContext = this;
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productE"></param>
        public Product(ProductE productE)
            : this()
        {
            PData = new ProductData(productE);
            this.Name = PData.Product_Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productData"></param>
        public Product(ProductData productData)
            : this()
        {
            PData = productData;
            this.Name = PData.Product_Name;
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
            PData.Product_Price = price;
            PData.Product_Count = count;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (PData != null)
            {
                Product_Count_Lbl.Text = "Pozostało: " + PData.Product_Count;
                Product_Price_Lbl.Text = "Cena: " + PData.Product_Price+ " zł";
                Product_ID_Lbl.Text = "N: " + PData.Product_ID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Product clone()
        {
            Product product = new Product(this.PData.ProductEn);
            product.PData.Product_Price = this.PData.Product_Price;
            product.PData.Product_Count = this.PData.Product_Count;
            return product;
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
        public ProductData PData
        {
            get
            {
                return _productData;
            }
            set
            {
                _productData = value;
                ProductImageSource = _productData.ProductImageSource;
            }
        }
    }
}
