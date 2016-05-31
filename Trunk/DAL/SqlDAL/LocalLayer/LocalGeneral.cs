using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using LinqToSQLModel;
using Model.General;

namespace DAL.SqlDAL.LocalLayer
{
    class LocalGeneral
    {
        public LocalGeneral()
        {

        }

        /// <summary>
        /// 锁定记录
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">主键字符串</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static ReturnValueInfo LockRecord(string tableName, long recordID, string userID)
        {
            ReturnValueInfo retrunInfo = null;
            Sys_RecordLockRegister_rlr recordInfo = null;

            try
            {
                using (VPMSDBDataContext dbDataContext = new VPMSDBDataContext())
                {
                    recordInfo = dbDataContext.Sys_RecordLockRegister_rlrs.SingleOrDefault<Sys_RecordLockRegister_rlr>(t => t.rlr_cTableName == tableName && t.rlr_iByLockRecordID == recordID);

                    retrunInfo = new ReturnValueInfo();
                    if (recordInfo == null)
                    {
                        Sys_RecordLockRegister_rlr insertInfo = new Sys_RecordLockRegister_rlr();

                        insertInfo.rlr_cTableName = tableName;
                        insertInfo.rlr_cKeys = "";
                        insertInfo.rlr_iByLockRecordID = recordID;
                        insertInfo.rlr_dLockDateTime = DateTime.Now;
                        insertInfo.rlr_cLockedBy = userID;

                        dbDataContext.Sys_RecordLockRegister_rlrs.InsertOnSubmit(insertInfo);

                        dbDataContext.SubmitChanges();

                        retrunInfo.boolValue = true;

                    }
                    else
                    {
                        if (recordInfo.rlr_cLockedBy.Trim() == userID.Trim())
                        {
                            retrunInfo.boolValue = true;
                        }
                        else
                        {
                            TimeSpan ts = DateTime.Now.Subtract(recordInfo.rlr_dLockDateTime);

                            if (ts.TotalHours >= Common.DefineConstantValue.LockRecordMaxTime)
                            {
                                dbDataContext.Sys_RecordLockRegister_rlrs.DeleteOnSubmit(recordInfo);

                                Sys_RecordLockRegister_rlr insertInfo = new Sys_RecordLockRegister_rlr();

                                insertInfo.rlr_cTableName = tableName;
                                insertInfo.rlr_cKeys = "";
                                insertInfo.rlr_iByLockRecordID = recordID;
                                insertInfo.rlr_dLockDateTime = DateTime.Now;
                                insertInfo.rlr_cLockedBy = userID;

                                dbDataContext.Sys_RecordLockRegister_rlrs.InsertOnSubmit(insertInfo);

                                dbDataContext.SubmitChanges();
                                retrunInfo.boolValue = true;
                            }
                            else
                            {
                                retrunInfo.boolValue = false;
                                retrunInfo.messageText = "记录正被 " + recordInfo.rlr_cLockedBy + " 修改！";
                            }
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return retrunInfo;
        }

        /// <summary>
        /// 解记录锁
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">主键字符串</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static ReturnValueInfo UnLockRecord(VPMSDBDataContext dbDataContext, string tableName, long recordID, string userID)
        {
            if (dbDataContext == null)
            {
                throw new Exception("dbDataContext is null !");
            }

            ReturnValueInfo retrunInfo = null;
            Sys_RecordLockRegister_rlr recordInfo = null;

            try
            {
                recordInfo = dbDataContext.Sys_RecordLockRegister_rlrs.SingleOrDefault<Sys_RecordLockRegister_rlr>(t => t.rlr_cTableName == tableName && t.rlr_iByLockRecordID == recordID && t.rlr_cLockedBy == userID);

                retrunInfo = new ReturnValueInfo();
                if (recordInfo != null)
                {
                    dbDataContext.Sys_RecordLockRegister_rlrs.DeleteOnSubmit(recordInfo);
                    dbDataContext.SubmitChanges();
                }
                retrunInfo.boolValue = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return retrunInfo;
        }

        /// <summary>
        /// 检查记录是否为本用户所锁定
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">主键字符串</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static bool MyLockRecord(string tableName, long recordID, string userID)
        {
            Sys_RecordLockRegister_rlr recordInfo = null;
            bool isMyLock = false;

            try
            {
                using (VPMSDBDataContext dbDataContext = new VPMSDBDataContext())
                {
                    recordInfo = dbDataContext.Sys_RecordLockRegister_rlrs.SingleOrDefault<Sys_RecordLockRegister_rlr>(t => t.rlr_cTableName == tableName && t.rlr_iByLockRecordID == recordID && t.rlr_cLockedBy == userID);

                    if (recordInfo != null)
                    {
                        isMyLock = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isMyLock;
        }


        /// <summary>
        /// 獲得數據表各個欄位的長度
        /// </summary>
        /// <typeparam name="TModel">需要返回的類型</typeparam>
        /// <param name="tableName">數據表名稱</param>
        /// <returns></returns>
        public TModel GetTableFieldLenght<TModel>(string tableName)
        {
            List<TableFieldLenghtInfo> infoList = null;
            TModel info = default(TModel);
            string sqlString = "";

            sqlString = "SELECT (name+'_Length') AS FieldName,prec AS FieldLength FROM syscolumns WHERE id=object_id('" + tableName + "')";
            try
            {
                using (VPMSDBDataContext dbDataContext = new VPMSDBDataContext())
                {
                    IEnumerable<TableFieldLenghtInfo> tableFieldLenghtInfoList = null;
                    tableFieldLenghtInfoList = dbDataContext.ExecuteQuery<TableFieldLenghtInfo>(sqlString, new object[] { });

                    if (tableFieldLenghtInfoList != null)
                    {
                        infoList = tableFieldLenghtInfoList.ToList<TableFieldLenghtInfo>();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            info = GetTableFieldLenghtDataInfo<TModel>(infoList);

            return info;
        }

        /// <summary>
        /// 获得记录列表
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="dataBaseDictionary">实体类与数据库栏位对应字典</param>
        /// <returns>List</returns>
        private TResult GetTableFieldLenghtDataInfo<TResult>(List<TableFieldLenghtInfo> source)
        {
            TResult infoObject = default(TResult);

            if (source == null || source.Count() == 0)
            {
                return infoObject;
            }

            object propertyValueObject;
            string classPropertyName = string.Empty;
            string classPropertyValue = string.Empty;
            string dataBaseColumnName = string.Empty;
            Type classPropertyType;
            Type propertyType;

            infoObject = Activator.CreateInstance<TResult>();

            for (int j = 0; j < source.Count(); j++)
            {
                string fieldName = source[j].FieldName;
                Type pInfoType = infoObject.GetType();
                PropertyInfo[] pInfos = pInfoType.GetProperties();
                PropertyInfo pInfo;

                if (pInfos.Length == 0)
                {
                    break;
                }

                for (int i = 0; i < pInfos.Length; i++)
                {
                    propertyValueObject = null;
                    pInfo = pInfos[i];
                    classPropertyName = pInfo.Name;

                    if (fieldName.Trim() == classPropertyName.Trim())
                    {
                        classPropertyType = pInfo.PropertyType;
                        propertyType = Nullable.GetUnderlyingType(classPropertyType);

                        if (propertyType == null)
                        {
                            propertyType = classPropertyType;
                        }

                        propertyValueObject = Convert.ChangeType(source[j].FieldLength, propertyType);

                        pInfo.SetValue(infoObject, propertyValueObject, null);

                        break;
                    }
                }

            }

            return infoObject;
        }

    }

    [Serializable]
    class TableFieldLenghtInfo
    {
        public TableFieldLenghtInfo()
        {
            this.FieldName = string.Empty;
        }

        public string FieldName
        {
            set;
            get;
        }

        public object FieldLength
        {
            set;
            get;
        }
    }
}
