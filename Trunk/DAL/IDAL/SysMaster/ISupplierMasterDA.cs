using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface ISupplierMasterDA : IDataBaseCommandDA<Sys_SupplierMaster_slm_Info>, IExtraDA, IMainDA<Sys_SupplierMaster_slm_Info>
    {
    }
}
