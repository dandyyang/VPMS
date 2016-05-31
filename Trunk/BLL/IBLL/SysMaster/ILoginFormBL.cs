using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace BLL.IBLL.SysMaster
{
    public interface ILoginFormBL : IDataBaseCommandBL<Sys_UserMaster_usm_Info>, IExtraBL, IMainBL
    {
        Model.General.ReturnValueInfo Login(Model.IModel.IModelObject itemEntity);
    }
}
