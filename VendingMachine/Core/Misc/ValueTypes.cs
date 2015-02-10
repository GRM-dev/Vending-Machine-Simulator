using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Misc
{
    /// <summary>
    /// Type of values
    /// </summary>
    public enum ValueTypes
    {
        /// <summary>
        /// Integer
        /// </summary>
        INT,
        /// <summary>
        /// Float
        /// </summary>
        FLOAT,
        /// <summary>
        /// Double
        /// </summary>
        DOUBLE,
        /// <summary>
        /// String. It's setted when the value is not Numeric
        /// </summary>
        STRING,
        /// <summary>
        /// Empty Value
        /// </summary>
        NULL
    }
}
