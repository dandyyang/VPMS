using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BLL.Factory
{
    class FactoryLayerDefine
    {
        public static readonly FactoryLayerDefine Instance = new FactoryLayerDefine();
        public FactoryLayerDefine() { }

        /// <summary>
        /// 获得数据來源类型
        /// </summary>
        /// <returns>DataSourceType</returns>
        public DataSourceType GetDataSourceType()
        {
            DataSourceType dataSourceType;
            string dataSourceTypeName = string.Empty;

            dataSourceType = DataSourceType.DAL;

            dataSourceTypeName = ConfigurationSettings.AppSettings["DataSourceType"].Trim();

            if (dataSourceTypeName.Trim() == "DAL")
            {
                dataSourceType = DataSourceType.DAL;
            }
            else if (dataSourceTypeName.Trim() == "WCF")
            {
                dataSourceType = DataSourceType.WCF;
            }
            else if (dataSourceTypeName.Trim() == "WEBSERVICE")
            {
                dataSourceType = DataSourceType.WebService;
            }

            return dataSourceType;
        }
    }
}
