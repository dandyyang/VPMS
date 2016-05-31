using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Factory.Management
{
    public class MasterDAFactory
    {
        /// <summary>
        /// 院系部主檔
        /// </summary>
        public static readonly string SchoolMaster = "DAL.SqlDAL.Management.Master.SchoolMasterDA,DAL";
        public static readonly string BuildingMaster = "DAL.SqlDAL.Management.Master.BuildingMasterDA,DAL";
        public static readonly string SiteMaster = "DAL.SqlDAL.Management.Master.SiteMasterDA,DAL";
        public static readonly string CourseMaster = "DAL.SqlDAL.Management.Master.CourseMasterDA,DAL";
        public static readonly string SpecialtyMaster = "DAL.SqlDAL.Management.Master.SpecialtyMasterDA,DAL";
        public static readonly string CardUserMaster = "DAL.SqlDAL.Management.Master.CardUserMasterDA,DAL";
        public static readonly string HolidaysSetting = "DAL.SqlDAL.Management.Setting.HolidaysSettingDA,DAL";
        public static readonly string UserSkin_urs = "DAL.SqlDAL.Management.Master.UserSkinDA,DAL";
        public static readonly string CodeMaster_cmt = "DAL.SqlDAL.Management.Master.CodeMasterDA,DAL";
        public static readonly string DeleteDataLogic_ddl = "DAL.SqlDAL.Management.DeleteData.DeleteDataLogicDA,DAL";
        public static readonly string SchoolWeekSetting = "DAL.SqlDAL.Management.Setting.SchoolWeekSettingDA,DAL";
        public static readonly string MessageManagement = "DAL.SqlDAL.Management.MessageManagement.MessageManagementDA,DAL";
        public static readonly string DataRightsRole = "DAL.SqlDAL.Management.DataRightsManagement.DataRightsRoleDA,DAL";
        /// <summary>
        /// 後臺發送用戶定制信息
        /// </summary>
        public static readonly string AutoMessageMasterDA = "DAL.SqlDAL.Management.Master.AutoMessageMasterDA,DAL";
        public static readonly string AutoMessageRecipientDA = "DAL.SqlDAL.Management.Master.AutoMessageRecipientDA,DAL";
        public static readonly string AutoMessageOtherRecDA = "DAL.SqlDAL.Management.Master.AutoMessageOtherRecDA,DAL";


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
