using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface ILoginFormDA : IDataBaseCommandDA<Sys_UserMaster_usm_Info>, IExtraDA, IMainDA<Sys_UserMaster_usm_Info>
    {
        Sys_UserMaster_usm_Info FindAllPermission(Sys_UserMaster_usm_Info usm);
        Sys_UserMaster_usm_Info FindPermission(Model.IModel.IModelObject KeyObject);
    }
}
