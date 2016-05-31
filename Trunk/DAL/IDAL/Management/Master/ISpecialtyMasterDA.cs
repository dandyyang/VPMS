using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace DAL.IDAL.Management.Master
{
    public interface ISpecialtyMasterDA:IDataBaseCommandDA<SpecialtyMaster_spm_Info>,IExtraDA,IMainDA<SpecialtyMaster_spm_Info>
    {
    }
}
