using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.Master;
using LinqToSQLModel;
using Model.Management.Master;

namespace DAL.SqlDAL.Management.Master
{
    class UserSkinDA:IUserSkinDA
    {
        #region IUserSkinDA Members

        public Model.IModel.IModelObject GetUserSkin(Model.IModel.IModelObject Entity)
        {
            UserSkin_urs_Info info = new UserSkin_urs_Info();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    info = Entity as UserSkin_urs_Info;
                    info.urs_cSkinName = "Default";
                    UserSkin_ur query = db.UserSkin_urs.SingleOrDefault(t => t.urs_cUserID == info.urs_cUserID);
                    if (query != null)
                    {
                        info.urs_cSkinName = query.urs_cSkinName;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Model.IModel.IModelObject SaveUserSkin(Model.IModel.IModelObject Entity)
        {
            UserSkin_urs_Info info = new UserSkin_urs_Info();
            
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    info = Entity as UserSkin_urs_Info;
                    UserSkin_ur query = db.UserSkin_urs.SingleOrDefault(t => t.urs_cUserID == info.urs_cUserID);
                    if (query != null)
                    {
                        query.urs_cSkinName = info.urs_cSkinName;
                    }
                    else
                    {
                        UserSkin_ur newTab = new UserSkin_ur();
                        newTab.urs_cUserID = info.urs_cUserID;
                        newTab.urs_cSkinName = info.urs_cSkinName;
                        db.UserSkin_urs.InsertOnSubmit(newTab);
                    }
                    db.SubmitChanges();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        #endregion
    }
}
