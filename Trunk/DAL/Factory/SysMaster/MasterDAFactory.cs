using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Factory.SysMaster
{
    public class MasterDAFactory
    {
        /// <summary>
        /// 科室主檔

        /// </summary>
        public static readonly string DepartmentMaster = "DAL.SqlDAL.SysMaster.DepartmentMasterDA,DAL";

        public static readonly string SysUserMaster = "DAL.SqlDAL.SysMaster.SysUserMasterDA,DAL";
        
        public static readonly string SysRoleMaster = "DAL.SqlDAL.SysMaster.SysRoleMasterDA,DAL";

        public static readonly string SysFormMaster = "DAL.SqlDAL.SysMaster.SysFormMasterDA,DAL";

        public static readonly string SysFunctionMaster = "DAL.SqlDAL.SysMaster.SysFunctionMasterDA,DAL";

        public static readonly string UserPurview = "DAL.SqlDAL.SysMaster.UserPurviewDA,DAL";

        public static readonly string LoginForm = "DAL.SqlDAL.SysMaster.LoginFormDA,DAL";

        public static readonly string SupplierMaster = "DAL.SqlDAL.SysMaster.SupplierMasterDA,DAL";

        public static readonly string ArticleTypeDefine = "DAL.SqlDAL.SysMaster.ArticleTypeDefineDA,DAL";
        
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

                throw ex;
            }

        }
    }
}
