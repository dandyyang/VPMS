using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.DeleteData;
using Model.Management.DeleteData;
using Model.General;
using LinqToSQLModel;


namespace DAL.SqlDAL.Management.DeleteData
{
    class DeleteDataLogicDA : IDeleteDataLogicDA
    {
        #region IDeleteDataLogicDA Members

        public ReturnValueInfo DeleteData(DeleteDataLogic_ddl_Info info)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            returnValue.boolValue = false;

            return returnValue;
        }

        #endregion

        #region IDeleteDataLogicDA 成员


        public ReturnValueInfo CheckUserCard(DeleteDataLogic_ddl_Info info)
        {
            throw new NotImplementedException();
        }

        public ReturnValueInfo CheckCardUser(DeleteDataLogic_ddl_Info info)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
