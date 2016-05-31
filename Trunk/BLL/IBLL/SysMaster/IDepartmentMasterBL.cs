using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace BLL.IBLL.SysMaster
{
    public interface IDepartmentMasterBL : IDataBaseCommandBL<DepartmentMaster_dpm_Info>, IMainBL, IExtraBL
    {
    }
}
