using System;
using System.Collections.Generic;
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
        public static bool configExists()
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
            XmlNodeList cHOElems = xmlDoc.SelectNodes("GRM/VendingMachine/option");
            foreach (XmlNode node in cHOElems)
            {
                configXml[node.Attributes["key"].Value] = node.InnerText;
            }
            return configXml;
        }

        /// <summary>
        /// Saves whole config
        /// </summary>
        public static void saveConfig()
        {
            XDocument xDoc = XDocument.Load(Config.CONFIG_FILE_PATH);
            XElement elList = xDoc.Element("GRM").Element("VendingMachine");
            elList.RemoveAll();
            foreach (var pair in ConfigProperties.instance.Properties)
            {
                XElement cElement = new XElement("option", pair.Value.Value);
                cElement.SetAttributeValue("key", pair.Value.Name);
                elList.Add(cElement);
            }
            xDoc.Save(Config.CONFIG_FILE_PATH);
        }

        /// <summary>
        /// Creates new config file
        /// </summary>
        public static void createNewConfig()
        {
            XDocument xDoc = new XDocument(new XElement("GRM", new XElement("VendingMachine")));
            XElement el = xDoc.Element("GRM").Element("VendingMachine");
            Dictionary<int, ConfigProperty> defConf = ConfigProperties.instance.Properties;
            foreach (var pair in defConf)
            {
                XElement cElement = new XElement("option", pair.Value.Value);
                cElement.SetAttributeValue("key", pair.Value.Name);
                el.Add(cElement);
            }
            xDoc.Save(Config.CONFIG_FILE_PATH);
        }
    }
}
