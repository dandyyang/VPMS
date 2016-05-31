using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using DAL.IDAL;

namespace DAL.SqlDAL.LocalLayer
{
    class DataBaseCommand<T>
    {
        #region IDataBaseCommand<T> Members

        /// <summary>
        /// 获得记录
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="dataBaseDictionary">实体类与数据库栏位对应字典</param>
        /// <returns>实体类</returns>
        public T GetRecord(string sqlString, Dictionary<string, string> dataBaseDictionary)
        {
            T record;
            List<T> recordList;

            //record = Activator.CreateInstance<T>();
            record = default(T);
            try
            {
                recordList = GetRecords(sqlString, dataBaseDictionary);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (recordList != null)
            {
                if (recordList.Count > 0)
                {
                    record = recordList[0];
                }
            }

            return record;
        }

        /// <summary>
        /// 获得记录列表
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="dataBaseDictionary">实体类与数据库栏位对应字典</param>
        /// <returns>List</returns>
        public List<T> GetRecordList(string sqlString, Dictionary<string, string> dataBaseDictionary)
        {
            List<T> recordList;

            try
            {
                recordList = GetRecords(sqlString, dataBaseDictionary);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return recordList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="returnValueType"></param>
        /// <returns></returns>
        public object GetValue(string sqlString, Type returnValueType)
        {
            string value = "";
            object ValueObject;
            SqlDataReader dr = null;
            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                dr = dal.GetDataReader(sqlString);
            }
            catch (Exception Ex)
            {
                if (dr != null)
                {
                    dr.Close();
                }
                throw Ex;
            }

            if (dr != null)
            {
                if (dr.Read())
                {
                    value = dr[0].ToString();
                }
                dr.Close();
            }

            try
            {
                ValueObject = Convert.ChangeType(value, returnValueType);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return ValueObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetTableFieldLenght(string tableName)
        {
            Dictionary<string, int> fieldDic = null;
            SqlDataReader dr = null;

            string sqlString = "";

            sqlString = "SELECT name,length FROM syscolumns WHERE id=object_id('" + tableName + "')";

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                dr = dal.GetDataReader(sqlString);
            }
            catch (Exception Ex)
            {
                if (dr != null)
                {
                    dr.Close();
                }
                throw Ex;
            }

            if (dr != null)
            {
                fieldDic = new Dictionary<string, int>();
                int lenght = 0;
                string value = "0";
                while (dr.Read())
                {
                    value = dr["length"].ToString();
                    lenght = Convert.ToInt32(value);
                    fieldDic.Add(dr["name"].ToString().Trim(), lenght);
                }
            }

            return fieldDic;
        }

        /// <summary>
        /// 获得记录列表
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="dataBaseDictionary">实体类与数据库栏位对应字典</param>
        /// <returns>List</returns>
        private List<T> GetRecords(string sqlString, Dictionary<string, string> dataBaseDictionary)
        {
            List<T> recordList = new List<T>();
            T infoObject;

            SqlDataReader dr = null;
            DataAccessLayer dal = new DataAccessLayer();

            try
            {
                dr = dal.GetDataReader(sqlString);
            }
            catch (Exception Ex)
            {
                if (dr != null)
                {
                    dr.Close();
                }
                throw Ex;
            }

            object propertyValueObject;
            string classPropertyName = string.Empty;
            string classPropertyValue = string.Empty;
            string dataBaseColumnName = string.Empty;
            Type classPropertyType;
            Type propertyType;

            while (dr.Read())
            {
                infoObject = Activator.CreateInstance<T>();

                Type pInfoType = infoObject.GetType();
                PropertyInfo[] pInfos = pInfoType.GetProperties();
                PropertyInfo pInfo;

                if (pInfos.Length == 0)
                {
                    dr.Close();
                    return recordList;
                }

                for (int i = 0; i < pInfos.Length; i++)
                {
                    propertyValueObject = null;
                    pInfo = pInfos[i];
                    classPropertyName = pInfo.Name;

                    if (dataBaseDictionary.ContainsKey(classPropertyName))
                    {
                        classPropertyType = pInfo.PropertyType;
                        propertyType = Nullable.GetUnderlyingType(classPropertyType);

                        if (propertyType == null)
                        {
                            propertyType = classPropertyType;
                        }

                        dataBaseColumnName = dataBaseDictionary[classPropertyName].ToString();
                        try
                        {
                            classPropertyValue = dr[dataBaseColumnName].ToString();
                            if (propertyType == typeof(DateTime))
                            {
                                if (classPropertyValue.Trim() != "")
                                {
                                    propertyValueObject = Convert.ChangeType(classPropertyValue, propertyType);
                                }
                            }
                            else
                            {
                                propertyValueObject = Convert.ChangeType(classPropertyValue, propertyType);
                            }

                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }

                        if (propertyValueObject != null)
                        {
                            pInfo.SetValue(infoObject, propertyValueObject, null);
                        }
                    }
                }

                recordList.Add(infoObject);
            }

            if (dr != null)
            {
                dr.Close();
            }

            return recordList;
        }

        #endregion
    }
}
