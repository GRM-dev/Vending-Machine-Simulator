using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using VendingMachine.Core.Misc;
using VendingMachine.Core.Products;
using VendingMachine.VMDialogs;

namespace VendingMachine.Core.Configuration
{
    /// <summary>
    /// Wczytuje, zapisuje dane z/do pliku xml.
    /// Kazda zmiana w maszynie powoduje zmiane w pliku w ramach synchronizacji.
    /// </summary>
    class ConfigFileManager
    {
        /// <summary>
        /// Checks config file
        /// </summary>
        /// <returns>true if exists</returns>
        public static bool configFileExists()
        {
            if (!Directory.Exists(Config.APP_PATH_MAIN))
            {
                Directory.CreateDirectory(Config.APP_PATH_MAIN);
            }
            return File.Exists(Config.CONFIG_FILE_PATH);
        }

        /// <summary>
        /// Read config nodes from file and saves hem to dictionary
        /// </summary>
        /// <returns>dictionary of options nodes</returns>
        public static Dictionary<XName, String> getOptionsFromFile()
        {
            Dictionary<XName, String> configXml = new Dictionary<XName, String>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.CONFIG_FILE_PATH);
            XmlNodeList cHOElems = xmlDoc.SelectNodes("GRM/VendingMachine/Options/option");
            foreach (XmlNode node in cHOElems)
            {
                configXml[node.Attributes["name"].Value] = node.InnerText;
            }
            return configXml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<ProductE, ProductData> getProductsFromFile()
        {
            Dictionary<ProductE, ProductData> products = new Dictionary<ProductE, ProductData>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.CONFIG_FILE_PATH);
            XmlNodeList cHOSlotElems = xmlDoc.SelectNodes("GRM/VendingMachine/Products/Product");
            foreach (XmlNode node in cHOSlotElems)
            {
                int ID = Convert.ToInt32(node["ID"].InnerText);
                ProductE productE = (ProductE)Enum.ToObject(typeof(ProductE),ID);
                ProductData productData = new ProductData(productE);
                productData.Product_Count = Convert.ToInt32(node["Count"].InnerText);
                productData.Product_Price = Convert.ToDouble(node["Price"].InnerText);
                products.Add(productE,productData);
            }
            return products;
        }

        /// <summary>
        /// Saves whole config
        /// </summary>
        public static void saveConfigsToFile()
        {
            XDocument xDoc;
            XElement grmElem;
            XElement vmElem;
            XElement elList;
            try
            {
                xDoc = XDocument.Load(Config.CONFIG_FILE_PATH);
                if (!xDoc.Elements("GRM").Any() || xDoc.Root.Name != "GRM")
                {
                    xDoc.Root.Elements().Remove();
                    grmElem = new XElement("GRM");
                    xDoc.Root.AddFirst(grmElem);
                }
                else
                {
                    grmElem = xDoc.Element("GRM");
                }
                if (!grmElem.Elements("VendingMachine").Any())
                {
                    vmElem = new XElement("VendingMachine");
                    grmElem.Add(vmElem);
                }
                else
                {
                    vmElem = grmElem.Element("VendingMachine");
                }
                if (!vmElem.Elements("Options").Any())
                {
                    elList = new XElement("Options");
                    vmElem.Add(elList);
                }
                else
                {
                    elList = vmElem.Element("Options");
                }
                if (!elList.IsEmpty)
                {
                    elList.RemoveAll();
                }
                foreach (var pair in ConfigProperties.instance.Properties)
                {
                    XElement cElement = new XElement("option", pair.Value.Value);
                    cElement.SetAttributeValue("name", pair.Value.Name);
                    elList.Add(cElement);
                }
                xDoc.Save(Config.CONFIG_FILE_PATH);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.Out.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void saveProductsToFile()
        {
            XDocument xDoc;
            XElement grmElem;
            XElement vmElem;
            XElement elList;
            try
            {
                xDoc = XDocument.Load(Config.CONFIG_FILE_PATH);
                if (!xDoc.Elements("GRM").Any() || xDoc.Root.Name != "GRM")
                {
                    xDoc.Root.Elements().Remove();
                    grmElem = new XElement("GRM");
                    xDoc.Root.AddFirst(grmElem);
                }
                else
                {
                    grmElem = xDoc.Element("GRM");
                }
                if (!grmElem.Elements("VendingMachine").Any())
                {
                    vmElem = new XElement("VendingMachine");
                    grmElem.Add(vmElem);
                }
                else
                {
                    vmElem = grmElem.Element("VendingMachine");
                }
                if (!vmElem.Elements("Products").Any())
                {
                    elList = new XElement("Products");
                    vmElem.Add(elList);
                }
                else
                {
                    elList = vmElem.Element("Products");
                }
                if (!elList.IsEmpty)
                {
                    elList.RemoveAll();
                }
                Console.Out.WriteLine("Products Count: " + ProductsController.Products.Count);
                foreach (var pair in ProductsController.Products)
                {
                    Product product = pair.Value;
                    ProductData productData = product.ProductDatas;
                   Console.Out.WriteLine("Product: "+productData.Product_Name);
                    XElement productElem = new XElement("Product");
                    elList.Add(productElem);
                    productElem.SetAttributeValue("name", productData.Product_Name);
                    productElem.Add(new XElement("ID", productData.Product_ID));
                    productElem.Add(new XElement("Price", productData.Product_Price));
                    productElem.Add(new XElement("Count", productData.Product_Count));
                }
                xDoc.Save(Config.CONFIG_FILE_PATH);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.Out.WriteLine(e.ToString());
                Console.Out.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^");
                Logger.ExceptionLog(e,"Error while saving");
            }
        }

        /// <summary>
        /// Creates new config file
        /// </summary>
        public static void createNewConfigFile()
        {
            XDocument xDoc = new XDocument(new XElement("GRM", new XElement("VendingMachine")));
            xDoc.Save(Config.CONFIG_FILE_PATH);
        }
    }
}
