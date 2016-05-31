using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysMaster;
using model = Model.SysMaster;
using LinqToSQLModel;
using DAL.SqlDAL.LocalLayer;
using System.Data.Linq.SqlClient;
using Model.SysMaster;

namespace DAL.SqlDAL.SysMaster
{
    class Web_UserMasterPwdDA:Web_IUserMasterPwdDA
    {
        #region IMainDA<Web_Sys_UserMaster_usm_PWD> Members

        public bool InsertRecord(Web_Sys_UserMaster_usm_PWD infoObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(Web_Sys_UserMaster_usm_PWD infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public Web_Sys_UserMaster_usm_PWD DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<Web_Sys_UserMaster_usm_PWD> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
