using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;
using Model.General;

namespace DAL.IDAL
{
    public interface IExtraDA
    {
        bool IsExistRecord(object KeyObject);
        ReturnValueInfo LockRecord(object KeyObject);
        ReturnValueInfo UnLockRecord(object KeyObject);
        bool IsMyLockedRecord(object KeyObject);

        IModelObject GetTableFieldLenght();
    }
}
