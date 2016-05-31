using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace DAL.IDAL.Management.Master
{
    public interface ISchoolMasterDA : IDataBaseCommandDA<SchoolMaster_scm_Info>, IMainDA<SchoolMaster_scm_Info>, IExtraDA
    {
    }
}
