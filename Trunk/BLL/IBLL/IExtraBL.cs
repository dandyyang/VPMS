using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace BLL.IBLL
{
    public interface IExtraBL
    {

        bool IsExistRecord(object KeyObject);

        IModelObject LockRecord(object KeyObject);

        IModelObject UnLockRecord(object KeyObject);

        IModelObject GetTableFieldLenght();
    }
}
