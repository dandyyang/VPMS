
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Common.MappingTools
{
    public static class MappingHelper
    {
        private static Dictionary<string, ConstructorInfo> _constructorCache = new Dictionary<string, ConstructorInfo>();
        private static Dictionary<string, PropertyInfo[]> _propertyInfoCache = new Dictionary<string, PropertyInfo[]>();
        private static Dictionary<string, Func<IDataReader, object>> _mappingCache = new Dictionary<string, Func<IDataReader, object>>();

        public static ConstructorInfo GetDefaultConstructor<T>()
        {
            Type type = typeof(T);

            if (!_constructorCache.ContainsKey(type.FullName))
            {
                _constructorCache.Add(type.FullName, type.GetConstructor(System.Type.EmptyTypes));
            }
            return _constructorCache[type.FullName];
        }

        private static T CreateInstance<T>()
        {
            Type type = typeof(T);

            ConstructorInfo constructor = GetDefaultConstructor<T>();
            return (T)constructor.Invoke(new object[0]);
        }

        static string GetMappingSignature(this IDataReader dr, Type type)
        {
            DataTable table = dr.GetSchemaTable();
            StringBuilder builder = new StringBuilder();

            foreach (DataColumn c in table.Columns)
            {
                builder.Append(c.ColumnName);
            }

            builder.Append(type.FullName);

            return builder.ToString();
        }

        static Action<IDataReader, object> CreateSetValueAction(PropertyInfo info, Type columnType, string columnName)
        {
            Type propType = info.PropertyType;

            Action<IDataReader, object> SetValueAction;

            if (propType.BaseType.Equals(typeof(System.Enum)))
            {
                SetValueAction = (IDataReader dr, object obj) =>
                {
                    if (MappingHelper.IsNumeric(dr[columnName]))
                    {
                        info.SetValue(obj, System.Enum.ToObject(propType, Convert.ToInt32(dr[columnName])), null);
                    }
                    else
                    {
                        info.SetValue(obj, System.Enum.ToObject(propType, dr[columnName]), null);
                    }
                };
            }
            else if (propType.Equals(columnType))
            {
                SetValueAction = (IDataReader dr, object obj) =>
                {
                    info.SetValue(obj, dr[columnName], null);
                };
            }
            else
            {
                //different types
                SetValueAction = (IDataReader dr, object obj) =>
                {
                    info.SetValue(obj, Convert.ChangeType(dr[columnName], propType), null);
                };
            }

            return (IDataReader dr, object obj) =>
            {

                if (Convert.IsDBNull(dr[columnName]))
                {
                    info.SetValue(obj, MappingNull.GetNull(info), null);
                }
                else
                {
                    SetValueAction.Invoke(dr, obj);
                }
            };
        }

        static List<Action<IDataReader, object>> CreatePropertyMapping<T>(IDataReader dr)
        {
            List<PropertyInfo> properties = MappingHelper.GetPropertyInfo<T>();
            List<Action<IDataReader, object>> mapping = new List<Action<IDataReader, object>>();

            int i;
            for (i = 0; i < dr.FieldCount; i++)
            {
                string columnName = dr.GetName(i);
                //now find matching property
                PropertyInfo propMatch = (from p in properties
                                          where p.Name.ToLower() == columnName.ToLower()
                                          select p).FirstOrDefault();
                if (propMatch != null)
                {
                    Type columnType = dr.GetFieldType(i);
                    Action<IDataReader, object> action = CreateSetValueAction(propMatch, columnType, columnName);
                    mapping.Add(action);
                }
            }
            return mapping;
        }
        static Func<IDataReader, object> CreateObjectMapping<T>(IDataReader dataReader)
        {
            List<Action<IDataReader, object>> mapping = CreatePropertyMapping<T>(dataReader);

            return (IDataReader dr) =>
            {
                object obj = CreateInstance<T>();

                for (int i = 0; i < mapping.Count; i++)
                {
                    Action<IDataReader, object> action = mapping[i];

                    action.Invoke(dr, obj);
                }

                return obj;
            };
        }

        public static T CreateObject<T>(IDataReader dr)
        {
            string mappingSignature = dr.GetMappingSignature(typeof(T));

            if (!_mappingCache.ContainsKey(mappingSignature))
            {
                _mappingCache.Add(mappingSignature, CreateObjectMapping<T>(dr));
            }

            return (T)_mappingCache[mappingSignature].Invoke(dr);
        }

        public static bool IsNumeric(object expression)
        {
            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static List<PropertyInfo> GetPropertyInfo<T>()
        {
            Type type = typeof(T);

            if (!_propertyInfoCache.ContainsKey(type.FullName))
            {
                _propertyInfoCache.Add(type.FullName, type.GetProperties());
            }
            return new List<PropertyInfo>(_propertyInfoCache[type.FullName]);
        }
        //public void RecursiveProperies(Type type)
        //{
        //    foreach (PropertyInfo info in type.GetProperties())
        //    {
        //        if (info.PropertyType.Namespace != "System")
        //        {
        //            _propertyInfoCache.Add(type.FullName, type.GetProperties());
        //        }
        //    }
        //}

        public static void FillParameters<T>(object obj, IDbCommand command)
        {
            List<PropertyInfo> properties = GetPropertyInfo<T>();

            foreach (IDbDataParameter param in command.Parameters)
            {
                //find the right property
                PropertyInfo property = properties.Find(delegate(PropertyInfo p)
                {
                    return p.Name.ToLower() == param.SourceColumn.ToLower();
                });

                object value = property.GetValue(obj, null);

                if (MappingNull.IsNull(value))
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = value;
                }
            }
        }
    }
}
