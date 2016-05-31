using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Master;

namespace DAL.IDAL.Master
{
    public interface IDeptMasterDA : IMainDA<DeptMasterInfo>
    {
        List<DeptMasterInfo> GetAllRecord();
        DeptMasterInfo GetRecord(string deptNum);
        bool IsExistRecord(string deptNum);
        bool IsLockRecord(long recordID);
        DeptMasterInfo GetTableFieldLenght();
    }
}
