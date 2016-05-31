using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using Model.Management.DeleteData;

namespace DAL.IDAL.Management.DeleteData
{
   public interface IDeleteDataLogicDA
    {
       ReturnValueInfo DeleteData(DeleteDataLogic_ddl_Info info);

       ReturnValueInfo CheckUserCard(DeleteDataLogic_ddl_Info info);

       ReturnValueInfo CheckCardUser(DeleteDataLogic_ddl_Info info);

    }
}
