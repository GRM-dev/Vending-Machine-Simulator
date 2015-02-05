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
        public static Dictionary<XName, String> getConfigFileOptions()
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
        public static Dictionary<XName, String> getProductsFromFile()
        {
            Dictionary<XName, String> configXml = new Dictionary<XName, String>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.CONFIG_FILE_PATH);
            XmlNodeList cHOSlotElems = xmlDoc.SelectNodes("GRM/VendingMachine/Slots/slot");
            foreach (XmlNode node in cHOSlotElems)
            {
                configXml[node.Attributes["number"].Value] = node.InnerText;
            }
            return configXml;
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
                grmElem = xDoc.Element("GRM");
                if (grmElem == null)
                {
                    grmElem = new XElement("GRM");
                    xDoc.Root.Add(grmElem);
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

        public static void saveProductsToFile()
        {
            try
            {
                XDocument xDoc = XDocument.Load(Config.CONFIG_FILE_PATH);
                XElement grmElem = xDoc.Element("GRM");
                if (grmElem == null)
                {
                    grmElem = new XElement("GRM");
                    xDoc.Add(grmElem);
                }
                XElement vmElem = grmElem.Element("VendingMachine");
                if (vmElem == null)
                {
                    vmElem = new XElement("VendingMachine");
                    grmElem.Add(vmElem);
                }
                XElement elList = vmElem.Element("Slots");
                if (elList == null)
                {
                    elList = new XElement("Slots");
                    vmElem.Add(elList);
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
        /// Creates new config file
        /// </summary>
        public static void createNewConfigFile()
        {
            XDocument xDoc = new XDocument(new XElement("GRM", new XElement("VendingMachine")));
            XElement el = xDoc.Element("GRM").Element("VendingMachine").Element("Options");
            Dictionary<int, ConfigProperty> defConf = ConfigProperties.instance.Properties;
            foreach (var pair in defConf)
            {
                XElement cElement = new XElement("option", pair.Value.Value);
                cElement.SetAttributeValue("name", pair.Value.Name);
                el.Add(cElement);
            }
            xDoc.Save(Config.CONFIG_FILE_PATH);
        }
    }
}
