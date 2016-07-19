using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.VegetableProduction.Master;
using Model.VegetableProduction.Master;
using LinqToSQLModel;

namespace DAL.SqlDAL.VegetableProduction.Master
{
    class VegetableSpeciesMasterDA : IVegetableSpeciesMasterDA
    {
        #region IDataBaseCommandDA<VegetableSpeciesMaster_vsm> 成员

        public VegetableSpeciesMaster_vsm_Info GetRecord_First()
        {
            VegetableSpeciesMaster_vsm_Info supplierMasterInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    IEnumerable<VegetableSpeciesMaster_vsm> query = db.VegetableSpeciesMaster_vsm.OrderBy(t => t.vsm_iSeq);

                    if (query != null && query.Count() > 0)
                    {
                        VegetableSpeciesMaster_vsm vegetableSpeciesMaster = query.First();

                        if (vegetableSpeciesMaster != null)
                        {
                            supplierMasterInfo = Common.General.CopyObjectValue<VegetableSpeciesMaster_vsm, VegetableSpeciesMaster_vsm_Info>(vegetableSpeciesMaster);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return supplierMasterInfo;
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<VegetableSpeciesMaster_vsm> 成员

        public bool InsertRecord(VegetableSpeciesMaster_vsm_Info infoObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(VegetableSpeciesMaster_vsm_Info infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public VegetableSpeciesMaster_vsm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<VegetableSpeciesMaster_vsm_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
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
