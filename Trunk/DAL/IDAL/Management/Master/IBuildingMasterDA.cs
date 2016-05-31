using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace DAL.IDAL.Management.Master
{
    public interface IBuildingMasterDA : IDataBaseCommandDA<BuildingMaster_bdm_Info>, IMainDA<BuildingMaster_bdm_Info>, IExtraDA
    {
    }
}
