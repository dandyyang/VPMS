using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using Model.General;
using Model.IModel;
using DAL.IDAL.Base;
using DAL.Factory.Base;
using Common;

namespace BLL.Base
{
    public class CodeMaster
    {
        private ICodeMasterDA _codeMasterDA = null;
        public CodeMaster() 
        {
            this._codeMasterDA = CodeMasterDAFactory.Instance.GetCodeMasterDA();
        }

        public CodeMasterInfo[] FindRecord(CodeMasterFindInfo codeMasterFindInfo)
        {
            List<CodeMasterInfo> recordList;
            try
            {
                recordList = _codeMasterDA.FindRecord(codeMasterFindInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            CodeMasterInfo[] infos=null;

            infos = recordList.ToArray();

            return infos; 
        }

        public List<CodeMasterInfo> GetMasterList(CodeMasterFindInfo codeMasterFindInfo)
        {
    
            try
            {
               return   _codeMasterDA.FindRecord(codeMasterFindInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            
        }

        public CodeMasterInfo DisplayRecord(IModelObject KeyObject)
        {
            CodeMasterInfo info = null;
            try
            {
                info = _codeMasterDA.DisplayRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public ReturnValueInfo InsertRecord(CodeMasterInfo codeMasterInfo)
        {
            if (codeMasterInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();
            ValidateValue(codeMasterInfo, returnValue);
            if (!returnValue.boolValue)
            {
                return returnValue;
            }

            bool isExistRecord = false;
            try
            {
                isExistRecord = this._codeMasterDA.IsExistRecord(codeMasterInfo.CmtCKey1, codeMasterInfo.CmtCKey2, codeMasterInfo.CmtCValue);
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
                isSuccess = this._codeMasterDA.InsertRecord(codeMasterInfo);
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

        public ReturnValueInfo UpdateRecord(CodeMasterInfo codeMasterInfo)
        {
            if (codeMasterInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();

            bool isSuccess = false;
            try
            {
                isSuccess = this._codeMasterDA.UpdateRecord(codeMasterInfo);
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
                isSuccess = this._codeMasterDA.DeleteRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public CodeMasterInfo GetTableFieldLenght()
        {
            CodeMasterInfo info = null;

            try
            {
                info = this._codeMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public CodeMasterInfo[] GetKey1s()
        {
            List<CodeMasterInfo> recordList;
            try
            {
                recordList = this._codeMasterDA.GetKey1s();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            CodeMasterInfo[] infos = null;

            infos = recordList.ToArray();

            return infos;

        }

        public CodeMasterInfo[] GetKey2s(string key2)
        {
            List<CodeMasterInfo> recordList;
            try
            {
                recordList = this._codeMasterDA.GetKey2s(key2);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            CodeMasterInfo[] infos = null;

            infos = recordList.ToArray();

            return infos;

        }

        public bool IsExistRecord(string key1, string key2, string value)
        {
            bool isExist = false;

            try
            {
                isExist=this._codeMasterDA.IsExistRecord(key1, key2, value);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isExist;
        }

        private void ValidateValue(CodeMasterInfo codeMasterInfo, ReturnValueInfo returnValue)
        {
            string messageText = string.Empty;

            returnValue.boolValue = true;

            if (codeMasterInfo.CmtCKey1.Trim() == string.Empty)
            {
                returnValue.boolValue = false;
                returnValue.messageText = "\"主键一\"" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
            }
            if (codeMasterInfo.CmtCKey2.Trim() == string.Empty)
            {
                returnValue.boolValue = false;
                returnValue.messageText = "\"主键二\"" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
            }
        }

        
    }
}
