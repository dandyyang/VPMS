using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace DAL.IDAL.Management.Master
{
    public interface ISiteMasterDA : IDataBaseCommandDA<SiteMaster_stm_Info>, IExtraDA, IMainDA<SiteMaster_stm_Info>
    {
    }
}
