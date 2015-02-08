using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Misc;

namespace VendingMachine.Core.Products
{
    /// <summary>
    /// Data of product
    /// </summary>
    public class ProductData
    {
        private int _productID;
        private int _productCount;
        private double _productPrice;
        private String _productName;
        private ImageSource _image;
        private ResourceDictionary vmRD;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productE"></param>
        public ProductData(ProductE productE)
        {
            ProductEn = productE;
            initProps();
        }

        /// <summary>
        /// 
        /// </summary>
        private void initProps()
        {
            vmRD = Config.VMRD;
            var image = vmRD["Default"];
            switch (ProductEn)
            {
                case ProductE.Mars: image = vmRD["Mars"]; break;
                case ProductE._3Bit: image = vmRD["_3Bit"]; break;
                case ProductE.Cappy: image = vmRD["Cappy"]; break;
                case ProductE.Rogal: image = vmRD["Rogal"]; break;
                default: Logger.Log("no image in dictionary or switch");
                    break;
            }
            this.ProductImageSource = (ImageSource)image;
            this.Product_Name = ProductEn.ToString();
            this.Product_ID = (int)ProductEn;
            this.Product_Price = 0;
            this.Product_Count = 0;
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
