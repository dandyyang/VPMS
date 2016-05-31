using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using DAL.Factory;


namespace DAL.Factory.SysFunction
{
    public class SysDALFactory
    {
        #region MyRegion

        public static readonly string UserMaster = "DAL.SqlDAL.SysFunction.SysUserMasterDA,DAL";

        public static readonly string LogDetail = "DAL.SqlDAL.SysFunction.LogDetailDA,DAL";

        /// <summary>
        /// 根據對應的Accessor接口 獲得對應的DAL
        /// </summary>
        /// <typeparam name="IAccessor"></typeparam>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static IDAL GetDAL<IDAL>(string accessorFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (IDAL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {
                
                throw ex ;
            }
      

            //object[] orgs = new object[] { ConnString };
            //dataAccessor = (IAccessor)Activator.CreateInstance(accessorType, orgs);

            //反射機制
            //dataAccessor = (IAccessor)Assembly.Load("DAL").CreateInstance("MD.DAL.AccessDAL.TH24G.TH24GCardMasterInformationDA");



        }

        #endregion

    }
}
