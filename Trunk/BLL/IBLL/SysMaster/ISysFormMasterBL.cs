using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace BLL.IBLL.SysMaster
{
    public interface ISysFormMasterBL : IDataBaseCommandBL<Sys_FormMaster_fom_Info>, IExtraBL, IMainBL
    {
        Sys_FormMaster_fom_Info GetWebTreeRoot();

        List<Sys_FormMaster_fom_Info> GetWebTreeNode(Sys_FormMaster_fom_Info info);
    }
}
