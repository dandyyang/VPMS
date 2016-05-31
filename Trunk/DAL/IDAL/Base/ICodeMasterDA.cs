using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using DAL.LocalDefine;

namespace DAL.IDAL.Base
{
    public interface ICodeMasterDA : IMainDA<CodeMasterInfo>
    {
        List<CodeMasterInfo> FindRecord(CodeMasterFindInfo codeMasterFindInfo);
        bool IsExistRecord(string key1, string key2, string value);
        bool IsLockRecord(long recordID);
        CodeMasterInfo GetTableFieldLenght();
        List<CodeMasterInfo> GetKey1s();
        List<CodeMasterInfo> GetKey2s(string key2);
        
    }
}
