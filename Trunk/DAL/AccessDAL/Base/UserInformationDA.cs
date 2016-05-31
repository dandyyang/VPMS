using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using DAL.LocalDefine;
//using DAL.LocalDefine.Base;
using DAL.AccessDAL.LocalLayer;
using DAL.IDAL.Base;

namespace DAL.AccessDAL.Base
{
    class UserInformationDA : IUserInformationDA
    {
        private Dictionary<string, string> _dataBaseDic;
        private string _newLineSybm = GeneralDefine.Instance.NewLineSymbol;

        public UserInformationDA() 
        {
            //this._dataBaseDic = DataBaseDictionary.Instance.GetUserInformationDBD();
        }

        #region IUserInformationDA Members

        public List<UserInformationInfo> FindRecord(UserInformationFindInfo userInformationFindInfo)
        {
            string sqlString = string.Empty;
            string sqlWhere = string.Empty;

            if (userInformationFindInfo == null)
            {
                return null;
            }

            sqlWhere = " WHERE 1=1 ";
            if (userInformationFindInfo.UsmCUserID.Trim() != "")
            {
                sqlWhere += " AND " + this._dataBaseDic[UserInformationInfoEnum.UsmCUserID.ToString()] + "='" + userInformationFindInfo.UsmCUserID.Replace("'", "''") + "'";
            }
            if (userInformationFindInfo.UsmCEngName.Trim() != "")
            {
                string symbol = "=";
                if (userInformationFindInfo.UsmCEngNameIsLike)
                {
                    symbol = "LIKE";
                }
                sqlWhere += " AND " + this._dataBaseDic[UserInformationInfoEnum.UsmCEngName.ToString()] + symbol + " '" + userInformationFindInfo.UsmCEngName.Replace("'", "''") + "'";
            }

            if (userInformationFindInfo.UsmCChaName.Trim() != "")
            {
                string symbol = "=";
                if (userInformationFindInfo.UsmCChaNameIsLike)
                {
                    symbol = "LIKE";
                }
                sqlWhere += " AND " + this._dataBaseDic[UserInformationInfoEnum.UsmCChaName.ToString()] + symbol + " '" + userInformationFindInfo.UsmCChaName.Replace("'", "''") + "'";
            }

            sqlString = "SELECT * FROM " + UserInformationInfoEnum.TableName.ToString() + sqlWhere;

            DataBaseCommand<UserInformationInfo> dbCommand = new DataBaseCommand<UserInformationInfo>();
            List<UserInformationInfo> userInformationInfoList=null;

            try
            {
                //userInformationInfoList = dbCommand.GetRecordList(sqlString, DataBaseDictionary.Instance.GetUserInformationDBD());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return userInformationInfoList;
        }

        public bool IsExistRecord(string userID)
        {
            bool isExist = false;
            string sqlString = string.Empty;
            object valueObject = null;

            sqlString = "SELECT COUNT(1) FROM " + this._dataBaseDic[UserInformationInfoEnum.TableName.ToString()] + this._newLineSybm;
            sqlString += " WHERE " + this._dataBaseDic[UserInformationInfoEnum.UsmCUserID.ToString()] + "='" + userID.Replace("'", "''").Trim() + "'";

            DataBaseCommand<UserInformationInfo> dataBaseCommand = new DataBaseCommand<UserInformationInfo>();

            try
            {
                valueObject = dataBaseCommand.GetValue(sqlString, typeof(int));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            int count = (int)valueObject;
            if (count > 0)
            {
                isExist = true;
            }

            return isExist;
        }

        public bool IsLockRecord(long recordID)
        {
            throw new NotImplementedException();
        }

        public UserInformationInfo GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IMainDA<UserInformationInfo> Members

        public bool InsertRecord(UserInformationInfo infoObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(UserInformationInfo infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public UserInformationInfo DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<UserInformationInfo> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
