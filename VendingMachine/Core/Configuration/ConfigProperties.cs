using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VendingMachine.Core.Configuration
{
    public class ConfigProperties
    {
        public static ConfigProperties instance = new ConfigProperties();
        private Dictionary<int, ConfigProperty> _properties;
        private ConfigProperty FullScreen = new ConfigProperty("FullScreen", "false", ConfigPropertyType.WINDOW_FULLSCREEN);
        private ConfigProperty WindowWidth = new ConfigProperty("Width", "600", ConfigPropertyType.WINDOWS_WIDTH);
        private ConfigProperty WindowHeight = new ConfigProperty("Height", "800", ConfigPropertyType.WINDOW_HEIGHT);

        private ConfigProperties()
        {
            Properties = new Dictionary<int, ConfigProperty>();
        }

        public void initDefaultProperties()
        {
            Add(FullScreen);
            Add(WindowWidth);
            Add(WindowHeight);
        }

        private void Add(ConfigProperty property)
        {
            Properties.Add(property.ID, property);
        }

        public static void Update(ConfigPropertyType property, string value)
        {
            instance.Properties[(int)property].Value = value;
        }

        public static void RestoreDefault(ConfigPropertyType property)
        {
            //TODO: Restoring default
        }

        internal static void LoadFromFile(Dictionary<XName, string> fileConfig)
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

        public static XName NameOf(ConfigPropertyType property)
        {
            int id = (int)property;
            return instance.Properties[id].Name;
        }

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

        public Dictionary<int, ConfigProperty> Properties
        {
            get { return _properties; }
            private set { _properties = value; }
        }
    }
}
