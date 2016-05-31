using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;
using Model.General;

namespace DAL.IDAL.Management.Master
{
    public interface ICardUserMasterDA : IDataBaseCommandDA<CardUserMaster_cus_Info>, IMainDA<CardUserMaster_cus_Info>, IExtraDA
    {
        CardUserMaster_cus_Info CheckCardNum(CardUserMaster_cus_Info info);

        List<CardUserMaster_cus_Info> ClassSearch(CardUserMaster_cus_Info info);

        ReturnValueInfo UpdateMonitorItemGroup(string groupNum, int cardUserId);

        int GetPersonnelsCount(string Type);

        ReturnValueInfo UpdataCardUserPhoneNum(CardUserMaster_cus_Info info);

        List<CardUserMaster_cus_Info> ExportClassInfo(CardUserMaster_cus_Info info);

        /// <summary>
        /// 升级分班处理
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool UpdateCardUserClass(CardUserMaster_cus_Info info);

        /// <summary>
        /// 学生毕业处理
        /// </summary>
        /// <param name="studentList"></param>
        /// <returns></returns>
        bool HandelCardBind(List<CardUserMaster_cus_Info> studentList);
    }
}
