using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.VegetableProduction.Master;

namespace DAL.IDAL.VegetableProduction.Master
{
    public interface IVegetableSpeciesMasterDA: IDataBaseCommandDA<VegetableSpeciesMaster_vsm>, IMainDA<VegetableSpeciesMaster_vsm>, IExtraDA
    {
    }
}
