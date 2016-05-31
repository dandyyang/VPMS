using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace DAL.IDAL.Management.Master
{
  public  interface IUserSkinDA
    {
      IModelObject GetUserSkin(IModelObject Entity);

      IModelObject SaveUserSkin(IModelObject Entity);
    }
}
