using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Configuration
{
    public class ConfigProperty
    {
        public ConfigProperty(string name, string value, ConfigPropertyType propertyType)
        {
            this.ID = (int)propertyType;
            this.Name = name;
            this.Value = value;
            this.PropertyType = propertyType;
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Value { get; set; }
        public ConfigPropertyType PropertyType { get; private set; }
    }
}
