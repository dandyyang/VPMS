using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysFunction;
using Model.IModel;
using Model.SysFunction;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;
using DAL.SqlDAL.SysMaster;

namespace BLL.DAL.SysFunction
{
    public class SysUserMasterBL : ISysUserMasterBL
    {
        ISysUserMasterDA m_objMaster;

        #region IMainBL Members

        public SysUserMasterBL()
        {
            m_objMaster = MasterDAFactory.GetDAL<ISysUserMasterDA>(MasterDAFactory.SysUserMaster);
        }

        public IModelObject DisplayRecord(IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public List<IModelObject> SearchRecords(IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo Save(IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBaseCommandBL<Sys_UserMaster_usm> Members

        public Sys_UserMaster_usm GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        public Model.Management.Master.CardUserMaster_cus_Info GetCardInfo(string p_strWebUser)
        {
            return ((SysUserMasterDA)m_objMaster).GetCardInfo(p_strWebUser);
        }
        public Model.Management.Master.CardUserMaster_cus_Info GetUserInfo(string p_strWebUser)
        {
            return ((SysUserMasterDA)m_objMaster).GetUserInfo(p_strWebUser);
        }
    }
}
