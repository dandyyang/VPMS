using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace BLL.IBLL.Management.Master
{
   public interface ICodeMasterBL : IDataBaseCommandBL<CodeMaster_cmt_Info>, IMainBL, IExtraBL
    {
       List<CodeMaster_cmt_Info> FindRecord(CodeMaster_cmt_Info info);
    }
}
