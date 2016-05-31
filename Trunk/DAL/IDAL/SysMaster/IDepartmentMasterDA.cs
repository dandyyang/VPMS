using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface IDepartmentMasterDA : IDataBaseCommandDA<DepartmentMaster_dpm_Info>, IExtraDA, IMainDA<DepartmentMaster_dpm_Info>
    {
    }
}
