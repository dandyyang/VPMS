using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;

namespace DAL.IDAL.Base
{
    public interface IUserInformationDA:IMainDA<UserInformationInfo>
    {
        List<UserInformationInfo> FindRecord(UserInformationFindInfo codeMasterFindInfo);
        bool IsExistRecord(string userID);
        bool IsLockRecord(long recordID);
        UserInformationInfo GetTableFieldLenght();
    }
}
