using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BLL.Factory.SysFunction
{
    public class SysBLLFactory
    {


        public static readonly string SysUserMaster = "BLL.DAL.SysFunction.SysUserMasterBL,BLL";

        public static readonly string LogDetail = "BLL.DAL.SysFunction.LogDetailBL,BLL";

        public static IBLL GetBLL<IBLL>(string fullName)
        {

            try
            {
                //動態創建實例類型 
                Type accessorType = Type.GetType(fullName, false);
                return (IBLL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //object[] orgs = new object[] { ConnString };
            //BllObject = (IAccessor)Activator.CreateInstance(accessorType, orgs);

            //反射機制
            //BllObject = (IAccessor)Assembly.Load("DAL").CreateInstance("AssetManagement.DAL.DALAMSInStock");




        }

    }
}
