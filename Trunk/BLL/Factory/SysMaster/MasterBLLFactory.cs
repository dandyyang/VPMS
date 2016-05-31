using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Factory.SysMaster
{
    public class MasterBLLFactory
    {
        /// <summary>
        /// 科室主檔

        /// </summary>
        public static readonly string DepartmentMaster = "BLL.DAL.SysMaster.DepartmentMasterBL,BLL";

        public static readonly string SysUserMaster = "BLL.DAL.SysMaster.SysUserMasterBL,BLL";

        public static readonly string SysRoleMaster = "BLL.DAL.SysMaster.SysRoleMasterBL,BLL";

        public static readonly string SysFormMaster = "BLL.DAL.SysMaster.SysFormMasterBL,BLL";

        public static readonly string SysFunctionMaster = "BLL.DAL.SysMaster.SysFunctionMasterBL,BLL";

        public static readonly string UserPurview = "BLL.DAL.SysMaster.UserPurviewBL,BLL";

        public static readonly string LoginForm = "BLL.DAL.SysMaster.LoginFormBL,BLL";

        public static readonly string SupplierMaster = "BLL.DAL.SysMaster.SupplierMasterBL,BLL";

        public static readonly string ArticleTypeDefine = "BLL.DAL.SysMaster.ArticleTypeDefineBL,BLL";

        /// <summary>
        /// 根據對應的Accessor接口 獲得對應的DAL
        /// </summary>
        /// <typeparam name="IAccessor"></typeparam>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static IBLL GetBLL<IBLL>(string accessorFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (IBLL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
