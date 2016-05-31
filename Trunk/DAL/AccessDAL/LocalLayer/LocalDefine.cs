using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;

namespace DAL.AccessDAL.LocalLayer
{
    class LocalDefine
    {
        public static readonly LocalDefine Instance = new LocalDefine();

        public LocalDefine()
        {
        }

        private enum CommandType
        {
            Next=0,
            Previous
        }

        /// <summary>
        /// 获得第一条记录的SQL语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>string</returns>
        public string GetCommandSQL_First(string tableName)
        {
            string sqlString = string.Empty;

            sqlString = "SELECT TOP 1 * FROM " + tableName.Trim();
            sqlString += " ORDER BY 1";

            return sqlString;
        }

        /// <summary>
        /// 获得最后一条记录的SQL语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>string</returns>
        public string GetCommandSQL_Last(string tableName)
        {
            string sqlString = string.Empty;

            sqlString = "SELECT TOP 1 * FROM " + tableName.Trim();
            sqlString += " ORDER BY 1  DESC";

            return sqlString;
        }

        /// <summary>
        /// 获得前一条记录的SQL语句
        /// </summary>
        /// <param name="dataBaseCommandKeyInfoList">数据表KEY信息列表</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns>string</returns>
        public string GetCommandSQL_Previous(List<DataBaseCommandKeyInfo> dataBaseCommandKeyInfoList, string tableName)
        {
            string sqlString = string.Empty;

            sqlString = GetCommandSQL(dataBaseCommandKeyInfoList, tableName, CommandType.Previous);

            return sqlString;
            
        }

        /// <summary>
        /// 获得下一条记录的SQL语句
        /// </summary>
        /// <param name="dataBaseCommandKeyInfoList">数据表KEY信息列表</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns>string</returns>
        public string GetCommandSQL_Next(List<DataBaseCommandKeyInfo> dataBaseCommandKeyInfoList, string tableName)
        {
            string sqlString = string.Empty;

            sqlString = GetCommandSQL(dataBaseCommandKeyInfoList, tableName, CommandType.Next);

            return sqlString;
        }

        /// <summary>
        /// 获得SQL语句
        /// </summary>
        /// <param name="dataBaseCommandKeyInfo">数据表KEY信息列表</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>string</returns>
        private string GetCommandSQL(List<DataBaseCommandKeyInfo> dataBaseCommandKeyInfoList, string tableName, CommandType commandType)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;
            string andString = string.Empty;
            string commandSymbol = string.Empty;

            if (dataBaseCommandKeyInfoList == null)
            {
                return sqlString;
            }

            if (commandType == CommandType.Next)
            {
                commandSymbol = ">";
            }
            else
            {
                commandSymbol = "<";
            }

            DataBaseCommandKeyInfo dataBaseCommandKeyInfo;
            if (dataBaseCommandKeyInfoList.Count > 0)
            {
                for (int i = dataBaseCommandKeyInfoList.Count - 1; i >= 0; i--)
                {
                    whereString = whereString.Trim() == "" ? "" : " OR " + whereString;
                    andString = "";
                    for (int j = i; j >= 0; j--)
                    {
                        dataBaseCommandKeyInfo = dataBaseCommandKeyInfoList[j];
                        andString = andString.Trim() == "" ? "" : " AND " + andString;
                        if (j == i)
                        {
                            if (dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int16 || dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int32 || dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int64)
                            {
                                andString = dataBaseCommandKeyInfo.Key + commandSymbol + dataBaseCommandKeyInfo.KeyValue.Trim() + andString;
                            }
                            else
                            {
                                andString = dataBaseCommandKeyInfo.Key + commandSymbol + "'" + dataBaseCommandKeyInfo.KeyValue.Trim().Replace("'", "''") + "'" + andString;
                            }
                        }
                        else
                        {
                            if (dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int16 || dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int32 || dataBaseCommandKeyInfo.KeyValueType == TypeCode.Int64)
                            {
                                andString = dataBaseCommandKeyInfo.Key + "=" + dataBaseCommandKeyInfo.KeyValue.Trim() + andString;
                            }
                            else
                            {
                                andString = dataBaseCommandKeyInfo.Key + "='" + dataBaseCommandKeyInfo.KeyValue.Trim().Replace("'", "''") + "'" + andString;
                            }
                        }

                    }
                    whereString = "(" + andString + ")" + whereString;
                }

            }

            whereString = whereString.Trim() == "" ? "" : " WHERE " + whereString;
            sqlString = "SELECT TOP 1 * FROM " + tableName + whereString;
            sqlString += " ORDER BY 1 DESC";

            return sqlString;
        }

        /// <summary>
        /// 获得填充ComboBox的ＳＱＬ语句
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public string GetInitComboBoxSQL(string strTableName, string strDisplayMember, string strValueMember)
        {
            string l_strSQL = string.Empty;
            strValueMember = strValueMember.Trim() == "" ? "" : "," + strValueMember;
            l_strSQL = "SELECT DISTINCT " + strDisplayMember.Trim() + strValueMember + " FROM " + strTableName.Trim();

            return l_strSQL;

        }

    }
}
