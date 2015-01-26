using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Core
{
    /// <summary>
    /// Keeps value with its type
    /// </summary>
    public class ValueHandler
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

        internal ValueHandler(ValueType value)
        {
            try
            {
                if (IsNumeric(value))
                {
                    if (IsFloat(value))
                    {
                        double dValue;
                        if (IsInteger(value))
                        {
                            PropertyType = ValueTypes.INT;
                        }
                        else if (double.TryParse(value+"", out dValue) && dValue > 3.4 * Math.Pow(10, 38))
                        {
                            PropertyType = ValueTypes.DOUBLE;
                        }
                        else
                        {
                            PropertyType = ValueTypes.FLOAT;
                        }
                    }
                    else
                    {
                        if (value is Double)
                        {
                            PropertyType = ValueTypes.DOUBLE;
                        }
                    }
                }
                else
                {
                    PropertyType = ValueTypes.STRING;
                }
                this.Value = value;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Creates handle to specified value.
        /// </summary>
        /// <param name="value">Value to be handled</param>
        public ValueHandler(String value)
        {
            double dValue;
            if (double.TryParse(value, out dValue))
            {
                PropertyType = ValueTypes.DOUBLE;
                this.Value = dValue;
            }
            else
            {
                this.Value = value;
                PropertyType = ValueTypes.STRING;
            }
        }

        /// <summary>
        /// Checks if value is is numeric
        /// </summary>
        /// <param name="value">param to check</param>
        /// <returns>true if numeric</returns>
        public static bool IsNumeric(ValueType value)
        {
            if (!(value is Byte ||
                    value is Int16 ||
                    value is Int32 ||
                    value is Int64 ||
                    value is SByte ||
                    value is UInt16 ||
                    value is UInt32 ||
                    value is UInt64 ||
                    value is Decimal ||
                    value is Double ||
                    value is Single))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if value is is numeric integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(ValueType value)
        {
            return (value is SByte || value is Int16 || value is Int32
                    || value is Int64 || value is Byte || value is UInt16
                    || value is UInt32 || value is UInt64);
        }

        /// <summary>
        /// Checks if value is is numeric float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloat(ValueType value)
        {
            return (value is float | value is double | value is Decimal);
        }

        /// <summary>
        /// Handled value
        /// </summary>
        public Object Value { get; private set; }

        /// <summary>
        /// Type of handled property
        /// </summary>
        public ValueTypes PropertyType { get; private set; }
    }
}
