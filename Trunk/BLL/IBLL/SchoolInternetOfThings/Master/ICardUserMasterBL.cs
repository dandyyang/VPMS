using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;
using Model.General;

namespace BLL.IBLL.Management.Master
{
    public interface ICardUserMasterBL : IDataBaseCommandBL<CardUserMaster_cus_Info>, IMainBL, IExtraBL
    {
        CardUserMaster_cus_Info CheckCardNum(CardUserMaster_cus_Info info);

        List<CardUserMaster_cus_Info> ClassSearch(CardUserMaster_cus_Info info);

        ReturnValueInfo UpdateMonitorItemGroup(string groupNum, List<int> cardUserIds);

        int GetPersonnelsCount(string Type);

        ReturnValueInfo UpdataCardUserPhoneNum(CardUserMaster_cus_Info info);

        List<CardUserMaster_cus_Info> ExportClassInfo(CardUserMaster_cus_Info info);
    }
}
