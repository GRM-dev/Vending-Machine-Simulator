using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using VendingMachine.Core.Configuration;

namespace VendingMachine.Core.Products
{
    /// <summary>
    /// Data of product
    /// </summary>
    public class ProductData
    {
        private int _productID;
        private String _productName;
        private double _productPrice;

        private ImageSource _image;
        private int _productCount;
        private ResourceDictionary vmRD;

        public ProductData(ProductE productE)
        {
            ProductEn = productE;
            initProps();
        }

        private void initProps()
        {
            vmRD = Config.vmRD;
            var image = vmRD["Default"];
            switch (ProductEn)
            {
                case ProductE.Mars: image = vmRD["Mars"]; break;
                case ProductE._3Bit: image = vmRD["_3Bit"]; break;
                default: break;
            }
            this.ProductImageSource = (ImageSource)image;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductE ProductEn { get; set; }
        /// <summary>
        /// Product ID
        /// </summary>
        public int Product_ID { get { return _productID; } private set { _productID = value; } }
        /// <summary>
        /// Name of product
        /// </summary>
        public String Product_Name { get { return _productName; } set { _productName = value; } }
        /// <summary>
        /// Product Price
        /// </summary>
        public double Product_Price { get { return _productPrice; } set { _productPrice = value; } }
        /// <summary>
        /// 
        /// </summary>
        public int Product_Count { get { return _productCount; } set { _productCount = value; } }
        /// <summary>
        /// 
        /// </summary>
        public ImageSource ProductImageSource { get { return _image; } set { _image = value; } }
    }
}
