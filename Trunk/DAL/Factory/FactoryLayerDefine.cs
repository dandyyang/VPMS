using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DAL.Factory
{
    class FactoryLayerDefine
    {
        public static readonly FactoryLayerDefine Instance = new FactoryLayerDefine();
        public FactoryLayerDefine() { }

        /// <summary>
        /// 获得数据库类型
        /// </summary>
        /// <returns>DataBaseType</returns>
        public DataBaseType GetDataBaseType()
        {
            DataBaseType dataBaseType;
            string dataBaseTypeName = string.Empty;

            dataBaseType = dataBaseType = DataBaseType.Access;

            dataBaseTypeName = ConfigurationSettings.AppSettings["DataBaseType"].Trim();

            if (dataBaseTypeName.Trim() == "ACCESS")
            {
                dataBaseType = DataBaseType.Access;
            }
            else if (dataBaseTypeName.Trim() == "SQLSERVER")
            {
                dataBaseType = DataBaseType.SqlServer;
            }

            return dataBaseType;
        }
    }

}
