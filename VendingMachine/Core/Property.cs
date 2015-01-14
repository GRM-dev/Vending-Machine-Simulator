using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Core
{
    class Property
    {
        public enum ValueTypes
        {
            INT, FLOAT, DOUBLE, STRING, NULL
        }

        internal Property(ValueType value)
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

        internal Property(String value)
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

        public static bool IsInteger(ValueType value)
        {
            return (value is SByte || value is Int16 || value is Int32
                    || value is Int64 || value is Byte || value is UInt16
                    || value is UInt32 || value is UInt64);
        }

        public static bool IsFloat(ValueType value)
        {
            return (value is float | value is double | value is Decimal);
        }

        public Object Value { get; set; }

        public ValueTypes PropertyType { get; set; }
    }
}
