using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Master;

namespace DAL.Factory.Master
{
    public class MasterDAFactory
    {
        public static readonly MasterDAFactory Instance = new MasterDAFactory();
        private DataBaseType _dataBaseType;

        public MasterDAFactory()
        {
            this._dataBaseType = FactoryLayerDefine.Instance.GetDataBaseType();
        }

        public IDeptMasterDA GetDeptMasterDA()
        {
            IDeptMasterDA iDeptMasterDA = null;

            if (this._dataBaseType == DataBaseType.Access)
            {
                iDeptMasterDA = new MD.DAL.AccessDAL.Master.DeptMasterDA();
            }
            if (this._dataBaseType == DataBaseType.SqlServer)
            {
                iDeptMasterDA = new DAL.SqlDAL.Master.DeptMasterDA();
            }

            return iDeptMasterDA;
        }

        public IClientMasterDA GetClientMasterDA()
        {
            IClientMasterDA iClientMasterDA = null;

            if (this._dataBaseType == DataBaseType.Access)
            {
                
            }
            if (this._dataBaseType == DataBaseType.SqlServer)
            {
                iClientMasterDA = new DAL.SqlDAL.Master.ClientMasterDA();
            }

            return iClientMasterDA;
        }

    }
}
