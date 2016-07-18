using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.VegetableProduction.Master;

namespace BLL.DAL.VegetableProduction.Master
{
    class VegetableSpeciesMasterBL : IVegetableSpeciesMasterBL
    {
        #region IDataBaseCommandBL<VegetableSpeciesMaster_vsm> 成员

        public Model.VegetableProduction.Master.VegetableSpeciesMaster_vsm GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public Model.VegetableProduction.Master.VegetableSpeciesMaster_vsm GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public Model.VegetableProduction.Master.VegetableSpeciesMaster_vsm GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public Model.VegetableProduction.Master.VegetableSpeciesMaster_vsm GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainBL 成员

        public Model.IModel.IModelObject DisplayRecord(Model.IModel.IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExtraBL 成员

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject UnLockRecord(object KeyObject)
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
