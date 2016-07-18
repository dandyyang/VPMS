using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.VegetableProduction.Master;
using Model.VegetableProduction.Master;

namespace DAL.SqlDAL.VegetableProduction.Master
{
    class VegetableSpeciesMasterDA : IVegetableSpeciesMasterDA
    {
        #region IDataBaseCommandDA<VegetableSpeciesMaster_vsm> 成员

        public VegetableSpeciesMaster_vsm GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<VegetableSpeciesMaster_vsm> 成员

        public bool InsertRecord(VegetableSpeciesMaster_vsm infoObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(VegetableSpeciesMaster_vsm infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<VegetableSpeciesMaster_vsm> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExtraDA 成员

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
