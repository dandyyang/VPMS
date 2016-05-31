using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Model.General;
using Model.Management.Master;

namespace DAL.IDAL.General
{
    public interface IGeneralDA
    {
        List<ComboboxDataInfo> GetMasterDataInformations(DefineConstantValue.MasterType masterType);
        List<ComboboxDataInfo> GetMasterDataInformations(DefineConstantValue.MasterType masterType, object keyInfo);

        SendMessageInfo GetSendMessageProperty();

        /// <summary>
        /// 获取教师信息
        /// </summary>
        /// <param name="hfID">hf卡ID</param>
        /// <returns></returns>
        List<CardUserMaster_cus_Info> GetCardUserInfomation(string hfID);

        /// <summary>
        /// 获取卡用户信息
        /// </summary>
        /// <param name="hfID">hf卡ID</param>
        /// <returns></returns>
        CardUserMaster_cus_Info GetCardUserInfo(string hfID);

        /// <summary>
        /// 获取卡用户信息
        /// </summary>
        /// <param name="cardHFID">卡8位ID</param>
        /// <returns></returns>
        CardUserMaster_cus_Info GetCardUserInfoByCardID(string cardHFID);
    }
}
