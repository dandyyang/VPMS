using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Master;
using Model.Base;

namespace DAL.IDAL.Master
{
    public interface IClientMasterDA : IMainDA<ClientMasterInfo>
    {
        List<ClientMasterInfo> SearchRecords(ClientMasterInfo clientMasterInfo);
        ClientMasterInfo GetRecord(string ClientNum);
        
        ClientMasterInfo GetRecord_Last();
        ClientMasterInfo GetRecord_First();
        ClientMasterInfo GetRecord_Previous(int currentRecordID);
        ClientMasterInfo GetRecord_Next(int currentRecordID);

        bool IsExistRecord(string clientNum);
        bool IsLockRecord(long recordID);
        ClientMasterInfo GetTableFieldLenght();
    }
}
