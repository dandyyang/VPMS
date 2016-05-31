using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace BLL.IBLL.SysMaster
{
    public interface ISupplierMasterBL : IDataBaseCommandBL<Sys_SupplierMaster_slm_Info>, IExtraBL, IMainBL
    {
    }
}
