using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;
using Model.IModel;

namespace DAL.IDAL.SysMaster
{
    public interface ISysFormMasterDA : IDataBaseCommandDA<Sys_FormMaster_fom_Info>, IExtraDA, IMainDA<Sys_FormMaster_fom_Info>
    {
        List<Sys_FormMaster_fom_Info> SearchRecords(IModelObject searchCondition, Sys_UserMaster_usm_Info info);
        List<Sys_FormMaster_fom_Info> SearchRecords(IModelObject searchCondition, string all);

        Sys_FormMaster_fom_Info GetWebTreeRoot();
        List<Sys_FormMaster_fom_Info> GetWebTreeNode(Sys_FormMaster_fom_Info info);

    }
}
