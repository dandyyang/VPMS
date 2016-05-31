using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using Model.Management.DeleteData;

namespace BLL.IBLL.Management.DeleteData
{
   public interface IDeleteDataLogicBL
    {
       ReturnValueInfo DeleteData(DeleteDataLogic_ddl_Info info);
    }
}
