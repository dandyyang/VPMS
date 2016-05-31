using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace BLL.IBLL.Management.Master
{
  public  interface IUserSkinBL
    {
        IModelObject GetUserSkin(IModelObject Entity);

        IModelObject SaveUserSkin(IModelObject Entity);
    }
}
