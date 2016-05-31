using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace BLL.IBLL.Management.Master
{
    public interface ISchoolMasterBL:IDataBaseCommandBL<SchoolMaster_scm_Info>,IMainBL,IExtraBL
    {
    }
}
