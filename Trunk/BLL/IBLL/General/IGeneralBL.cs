using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;
using Common;
using Model.General;
using Model.Management.Master;

namespace BLL.IBLL.General
{
    public interface IGeneralBL
    {
        List<IModelObject> GetMasterDataInformations(DefineConstantValue.MasterType masterType);
        List<IModelObject> GetMasterDataInformations(DefineConstantValue.MasterType masterType, object keyInfo);

        SendMessageInfo GetSendMessageProperty();
    }
}
