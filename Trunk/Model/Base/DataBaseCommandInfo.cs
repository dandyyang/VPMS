using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Base
{
    /// <summary>
    /// 数据库指令信息
    /// </summary>
    public class DataBaseCommandInfo
    {
        public DataBaseCommandInfo() 
        {

            this.KeyInfoList = new List<DataBaseCommandKeyInfo>();
        }

        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// KEY信息列表
        /// </summary>
        public List<DataBaseCommandKeyInfo> KeyInfoList
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库指令类型
        /// </summary>
        public DataBaseCommandType CommandType
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 数据库KEY信息
    /// </summary>
    public class DataBaseCommandKeyInfo
    {
        public DataBaseCommandKeyInfo() { }

        /// <summary>
        /// 数据表KEY
        /// </summary>
        public string Key
        {
            get;
            set;
        }
        /// <summary>
        /// KEY值
        /// </summary>
        public string KeyValue
        {
            get;
            set;
        }
        /// <summary>
        /// KEY值类型

        /// </summary>
        public TypeCode KeyValueType
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 数据库指令类型
    /// </summary>
    public enum DataBaseCommandType
    {
        First=0,
        Next,
        Previous,
        Last,
        MoveTo
    }

}
