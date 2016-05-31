
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.MappingTools
{
    public sealed class MappingNull
    {
        private MappingNull()
        {
        }
        //  define application encoded null values 
        public static short NullShort
        {
            get
            {
                //return -1;
                return 0;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullInteger
        public static int NullInteger
        {
            get
            {
                //return -1;
                return 0;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullSingle
        public static float NullSingle
        {
            get
            {
                //return float.MinValue;
                return 0;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullDouble
        public static double NullDouble
        {
            get
            {
                //return double.MinValue;
                return 0;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullDecimal
        public static decimal NullDecimal
        {
            get
            {
                //return decimal.MinValue;
                return 0;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullDate
        public static DateTime NullDate
        {
            get
            {
                return DateTime.MinValue;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullString
        public static string NullString
        {
            get
            {
                return "";
            }
        }
        // TRANSMISSINGCOMMENT: Property NullBoolean
        public static bool NullBoolean
        {
            get
            {
                return false;
            }
        }
        // TRANSMISSINGCOMMENT: Property NullGuid
        public static Guid NullGuid
        {
            get
            {
                return Guid.Empty;
            }
        }


        //  sets a field to an application encoded null value ( used in BLL layer )
        public static object GetNull(PropertyInfo objPropertyInfo)
        {
            if (objPropertyInfo == null)
            {
                return null;
            }

            string type = objPropertyInfo.PropertyType.ToString();
            if (type == "System.Int16")
            {
                return NullShort;
            }
            else if (type == "System.Int32")
            {
                return NullInteger;
            }
            else if (type == "System.Int64")
            {
                return NullInteger;
            }
            else if (type == "System.Single")
            {
                return NullSingle;
            }
            else if (type == "System.Double")
            {
                return NullDouble;
            }
            else if (type == "System.Decimal")
            {
                return NullDecimal;
            }
            else if (type == "System.DateTime")
            {
                return NullDate;
            }
            else if (type == "System.String")
            {
                return NullString;
            }
            else if (type == "System.Char")
            {
                return NullString;
            }
            else if (type == "System.Boolean")
            {
                return NullBoolean;
            }
            else if (type == "System.Guid")
            {
                return NullGuid;
            }
            else
            {

                //  Enumerations default to the first entry
                Type pType = objPropertyInfo.PropertyType;
                if (pType.BaseType.Equals(typeof(System.Enum)))
                {
                    System.Array objEnumValues = System.Enum.GetValues(pType);
                    Array.Sort(objEnumValues);
                    return System.Enum.ToObject(pType, objEnumValues.GetValue(0));
                }
                else
                { //  complex object

                    return null;
                }
            }

        }

        //  checks if a field contains an application encoded null value
        public static bool IsNull(object objField)
        {
            if (!(objField == null))
            {
                if (objField is int)
                {
                    return objField.Equals(NullInteger);
                }
                else if (objField is float)
                {
                    return objField.Equals(NullSingle);
                }
                else if (objField is double)
                {
                    return objField.Equals(NullDouble);
                }
                else if (objField is decimal)
                {
                    return objField.Equals(NullDecimal);
                }
                else if (objField is DateTime)
                {
                    DateTime objDate = ((DateTime)(objField));
                    return objDate.Date.Equals(NullDate.Date);
                }
                else if (objField is string)
                {
                    return objField.Equals(NullString);
                }
                else if (objField is bool)
                {
                    return objField.Equals(NullBoolean);
                }
                else if (objField is Guid)
                {
                    return objField.Equals(NullGuid);
                }
                else
                { //  complex object

                    return false;
                }
            }
            else
            {
                return true;
            }
        }


    }
}
