using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VendingMachine.Core.Misc;

namespace VendingMachine.Core.Configuration
{
    /// <summary>
    /// Config Properties handler having its own instance object
    /// </summary>
    public class ConfigProperties
    {
        /// <summary>
        /// Config Properties Instance
        /// </summary>
        public static ConfigProperties instance = new ConfigProperties();
        private Dictionary<int, ConfigProperty> _properties;
        private ConfigProperty FullScreen = new ConfigProperty("FullScreen", "false", ConfigPropertyType.WINDOW_FULLSCREEN);
        private ConfigProperty WindowWidth = new ConfigProperty("Width", "600", ConfigPropertyType.WINDOWS_WIDTH);
        private ConfigProperty WindowHeight = new ConfigProperty("Height", "800", ConfigPropertyType.WINDOW_HEIGHT);
        private ConfigProperty SlotsCount = new ConfigProperty("SlotsCount", "20", ConfigPropertyType.SLOTS_COUNT);
        private ConfigProperty SlotSize = new ConfigProperty("SlotSize", "12", ConfigPropertyType.SLOT_SIZE);
        private ConfigProperty MoneyCollected = new ConfigProperty("MoneyInMachine", "10", ConfigPropertyType.MONEY_COLLECTED);
        private ConfigProperty ServiceNeeded = new ConfigProperty("ServiceNeed", "0", ConfigPropertyType.SERVICE_NEEDED);
        private ConfigProperty ServicePasswd = new ConfigProperty("ServicePasswd", "654321", ConfigPropertyType.SERVICE_PASSWD);
        //private ConfigProperty  = new ConfigProperty("", "", ConfigPropertyType.);

        private ConfigProperties()
        {
            Properties = new Dictionary<int, ConfigProperty>();
        }

        /// <summary>
        /// Initialises basic properties from fields in this class
        /// </summary>
        public void initDefaultProperties()
        {
            Add(FullScreen);
            Add(WindowWidth);
            Add(WindowHeight);
            Add(SlotsCount);
            Add(SlotSize);
            Add(MoneyCollected);
            Add(ServiceNeeded);
            Add(ServicePasswd);
        }

        /// <summary>
        /// Adds a property to properties dictionary
        /// </summary>
        /// <param name="property"></param>
        private void Add(ConfigProperty property)
        {
            Properties.Add(property.ID, property);
        }

        /// <summary>
        /// Updates specified property with specified value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void Update(ConfigPropertyType property, string value)
        {
            instance.Properties[(int)property].Value = value;
        }

        /// <summary>
        /// Restores defaults
        /// </summary>
        /// <param name="property"></param>
        public static void RestoreDefault(ConfigPropertyType property)
        {
            //TODO: Restoring default
        }

        /// <summary>
        /// Loads config from defined dictionary
        /// </summary>
        /// <param name="fileConfig">dictionary to update its entries from file</param>
        internal static void LoadFromDictionary(Dictionary<XName, string> fileConfig)
        {
            foreach (ConfigPropertyType propertyType in Enum.GetValues(typeof(ConfigPropertyType)))
            {
                ConfigProperty property = instance.getProperty(propertyType);
                if (property != null)
                {
                    if (fileConfig.ContainsKey(property.Name))
                    {
                        Update(propertyType, fileConfig[property.Name]);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a name of specified property
        /// </summary>
        /// <param name="property"></param>
        /// <returns>XName of property type</returns>
        public static XName NameOf(ConfigPropertyType property)
        {
            int id = (int)property;
            return instance.Properties[id].Name;
        }

        /// <summary>
        /// Checks if dictionary contains specified XName
        /// </summary>
        /// <param name="xName"></param>
        /// <returns>true if contains</returns>
        public bool Contains(XName xName)
        {
            foreach (var property in Properties)
            {
                if (property.Value.Name == xName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propType"></param>
        /// <returns></returns>
        public bool Contains(ConfigPropertyType propType)
        {
            foreach (var property in Properties)
            {
                if (property.Key == (int)propType)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ConfigProperty getProperty(ConfigPropertyType property)
        {
            foreach (var entry in Properties)
            {
                if (entry.Value.PropertyType == property)
                {
                    return entry.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ConfigProperty getProperty(string name)
        {
            foreach (var entry in Properties)
            {
                if (entry.Value.Name == name)
                {
                    return entry.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConfigProperty getProperty(int id)
        {
            foreach (var entry in Properties)
            {
                if (entry.Value.ID == id)
                {
                    return entry.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Properties dictionary containings program properties
        /// </summary>
        public Dictionary<int, ConfigProperty> Properties
        {
            get { return _properties; }
            private set { _properties = value; }
        }
    }
}
