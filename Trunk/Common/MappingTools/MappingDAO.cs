using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.MappingTools
{
    public class MappingDAO
    {
        public static List<T> FillCollection<T>(IDataReader dr)
        {
            List<T> list = new List<T>();

            while (dr.Read())
            {
                T obj = MappingHelper.CreateObject<T>(dr);
                list.Add(obj);
            }

            //dr.Close();

            return list;
        }

        public static T FillEntity<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T info = default(T);

            if (dr.Read())
            {
                info = MappingHelper.CreateObject<T>(dr);
            }

            return info;
        }
    }
}
