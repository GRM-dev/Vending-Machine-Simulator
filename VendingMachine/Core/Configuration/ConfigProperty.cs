using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Configuration
{
    /// <summary>
    /// Property handling property name, value and type of ConfigPropertyType
    /// </summary>
    public class ConfigProperty
    {
        /// <summary>
        /// Creates property object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="propertyType"></param>
        public ConfigProperty(string name, string value, ConfigPropertyType propertyType)
        {
            this.ID = (int)propertyType;
            this.Name = name;
            this.Value = value;
            this.PropertyType = propertyType;
        }

        /// <summary>
        /// ID of property type
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Name of property
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Value of property
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// type of property
        /// </summary>
        public ConfigPropertyType PropertyType { get; private set; }
    }
}
