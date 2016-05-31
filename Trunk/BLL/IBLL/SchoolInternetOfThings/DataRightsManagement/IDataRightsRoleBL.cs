using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.DataRightsManagement;
using Model.General;
using Model.SysMaster;

namespace BLL.IBLL.Management.DataRightsManagement
{
    public interface IDataRightsRoleBL : IDataBaseCommandBL<DataRightsRole_drr_Info>, IExtraBL, IMainBL
    {
        /// <summary>
        /// 取得所有角色列表

        /// </summary>
        /// <returns></returns>
         List<DataRightsRole_drr_Info> GetAllRoleList();

        /// <summary>
        /// 取得所有角色列表

        /// </summary>
        /// <returns></returns>
         List<Sys_UserMaster_usm_Info> GetAllSysUserList();

         /// <summary>
         /// 取行角色中的所有用户
         /// </summary>
         /// <param name="roleNumber">角色编号</param>
         /// <returns></returns>
         List<Sys_UserMaster_usm_Info> GetRoleUsers(string roleNumber);

        /// <summary>
        /// 保存角色下所有用户记录

        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
         ReturnValueInfo SaveRoleUser(DataRightsRole_drr_Info Entity);

        /// <summary>
        /// 删除角色下所有用户记录

        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
         ReturnValueInfo DeleteRoleUser(DataRightsRole_drr_Info Entity);

         /// <summary>
         /// 保存多用户至多角色

         /// </summary>
         /// <param name="userList"></param>
         /// <param name="roleList"></param>
         /// <returns></returns>
         ReturnValueInfo SaveUserToRole(List<Sys_UserMaster_usm_Info> userList, List<DataRightsRole_drr_Info> roleList, bool iClearUserRole, bool iClearRoleUser);

         /// <summary>
         /// 保存用户角色
         /// </summary>
         /// <param name="userNumber">用户编号</param>
         /// <param name="roleNumList">角色编号列表</param>
         /// <returns></returns>
         ReturnValueInfo SaveUserRole(string userNumber, List<string> roleNumList);

         /// <summary>
         /// 删除用户角色
         /// </summary>
         /// <param name="userNumber">用户编号</param>
         /// <param name="roleNumList">角色列表</param>
         /// <returns></returns>
         ReturnValueInfo DeleteUserRole(string userNumber, List<string> roleNumList); 
        
        /// <summary>
         /// 取得帐户所属角色列表

         /// </summary>
         /// <param name="Entity"></param>
         /// <returns></returns>
         List<DataRightsRole_drr_Info> GetUserRoleList(Sys_UserMaster_usm_Info Entity);

         /// <summary>
         /// 取得帐户所属角色列表
         /// </summary>
         /// <param name="userLoginID">帐户ID</param>
         /// <returns></returns>
         List<DataRightsRole_drr_Info> GetUserRoles(string userLoginID);

         /// <summary>
         /// 取得角色下所有用户列表

         /// </summary>
         /// <param name="Entity"></param>
         /// <returns></returns>
         List<Sys_UserMaster_usm_Info> GetRoleUserList(DataRightsRole_drr_Info Entity);
    }
}
