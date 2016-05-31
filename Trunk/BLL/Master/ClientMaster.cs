using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Master;
using Model.General;
using Model.Base;
using DAL.IDAL.Master;
using DAL.Factory.Master;
using Model.IModel;
using Common;

namespace BLL.Master
{
    public class ClientMaster
    {
        private IClientMasterDA _clientMasterDA = null;

        public ClientMaster() 
        {
            this._clientMasterDA = MasterDAFactory.Instance.GetClientMasterDA();
        }

        public ClientMasterInfo GetRecord_Last()
        {
            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMasterInfo;
        }

        public ClientMasterInfo GetRecord_First()
        {
            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMasterInfo;
        }

        public ClientMasterInfo GetRecord_Previous(int currentRecordID)
        {
            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterDA.GetRecord_Previous(currentRecordID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMasterInfo;
        }

        public ClientMasterInfo GetRecord_Next(int currentRecordID)
        {
            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterDA.GetRecord_Next(currentRecordID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMasterInfo;
        }

        public ClientMasterInfo GetRecord(IModelObject KeyObject)
        {
            ClientMasterInfo info = null;
            try
            {
                info = this._clientMasterDA.DisplayRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public ClientMasterInfo[] SearchRecords(ClientMasterInfo clientMasterInfo)
        {
            ClientMasterInfo[] infos = null;
            List<ClientMasterInfo> deptMasterInfoList = null;
            try
            {
                deptMasterInfoList = this._clientMasterDA.SearchRecords(clientMasterInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (deptMasterInfoList != null)
            {
                infos = deptMasterInfoList.ToArray();
            }

            return infos;
        }

        public ClientMasterInfo GetRecord(string clientNum)
        {
            ClientMasterInfo info = null;
            try
            {
                info = this._clientMasterDA.GetRecord(clientNum);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public ReturnValueInfo InsertRecord(ClientMasterInfo clientInfo)
        {
            if (clientInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();
            ValidateValue(clientInfo, returnValue);
            if (!returnValue.boolValue)
            {
                return returnValue;
            }

            bool isExistRecord = false;
            try
            {
                isExistRecord = this._clientMasterDA.IsExistRecord(clientInfo.ClmCClientNum);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isExistRecord)
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_RecordIsExist;

                return returnValue;
            }

            bool isSuccess = false;

            try
            {
                isSuccess = this._clientMasterDA.InsertRecord(clientInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isSuccess)
            {
                returnValue.boolValue = true;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
            }
            else
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_AddFail;
            }

            return returnValue;
        }

        public ReturnValueInfo UpdateRecord(ClientMasterInfo clientInfo)
        {
            if (clientInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();

            bool isSuccess = false;
            try
            {
                isSuccess = this._clientMasterDA.UpdateRecord(clientInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isSuccess)
            {
                returnValue.boolValue = true;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
            }
            else
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
            }

            return returnValue;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            bool isSuccess = false;
            try
            {
                isSuccess = this._clientMasterDA.DeleteRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public ClientMasterInfo GetTableFieldLenght()
        {
            ClientMasterInfo info = null;

            try
            {
                info = this._clientMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public bool IsExistRecord(string clientNumber)
        {
            bool isExist = false;

            try
            {
                isExist = this._clientMasterDA.IsExistRecord(clientNumber);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isExist;
        }

        private void ValidateValue(ClientMasterInfo clientInfo, ReturnValueInfo returnValue)
        {
            string messageText = string.Empty;

            returnValue.boolValue = true;

            if (clientInfo.ClmCClientNum.Trim() == string.Empty)
            {
                returnValue.boolValue = false;
                returnValue.messageText = "\"客户编号\"" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
            }
        }
    }
}
