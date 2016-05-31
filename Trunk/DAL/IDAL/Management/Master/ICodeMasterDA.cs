using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;
using Model.Base;

namespace DAL.IDAL.Management.Master
{
    public interface ICodeMasterDA : IDataBaseCommandDA<CodeMaster_cmt_Info>, IMainDA<CodeMaster_cmt_Info>, IExtraDA
    {
        List<CodeMaster_cmt_Info> FindRecord(CodeMaster_cmt_Info info);
                
    }
}
