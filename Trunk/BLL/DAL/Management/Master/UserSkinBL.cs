using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.Management.Master;
using DAL.IDAL.Management.Master;
using DAL.Factory.Management;

namespace BLL.DAL.Management.Master
{
    public class UserSkinBL : IUserSkinBL
    {
        IUserSkinDA _userSkinDA;
        public UserSkinBL()
        {
            this._userSkinDA = MasterDAFactory.GetDAL<IUserSkinDA>(MasterDAFactory.UserSkin_urs);
        }

        #region IUserSkinBL Members

        public Model.IModel.IModelObject GetUserSkin(Model.IModel.IModelObject Entity)
        {
            try
            {
                return _userSkinDA.GetUserSkin(Entity);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Model.IModel.IModelObject SaveUserSkin(Model.IModel.IModelObject Entity)
        {
            try
            {
                return _userSkinDA.SaveUserSkin(Entity);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion
    }
}
