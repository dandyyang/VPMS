using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.Entity;

namespace PaymentEquipment.IDevice
{
    public abstract class AbstractICWriter
    {
        protected AbstractICWriter()
        { }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="iCom">COM口</param>
        /// <param name="iBaud">波特率</param>
        /// <param name="bSection">需读取扇区号</param>
        /// <param name="strPwd">读卡器密码</param>
        /// <returns></returns>
        public abstract bool Conn(int iCom, int iBaud, byte bSection, string strPwd);

        /// <summary>
        /// 取得卡片MAC地址
        /// </summary>
        /// <returns></returns>
        public abstract string GetMacID();

        /// <summary>
        /// 取得卡用户编号
        /// </summary>
        /// <returns></returns>
        public abstract int GetCardNo();

        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="p_objCard">卡信息</param>
        /// <returns></returns>
        public abstract bool WriteCard(PaymentCard p_objCard);

        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns>卡信息</returns>
        public abstract PaymentCard ReadCard();

        /// <summary>
        /// 设置卡片密码
        /// </summary>
        /// <param name="p_intSector">当参数值为“99”时，设置所有扇区的密码</param>
        /// <param name="p_strPassword">密码</param>
        /// <returns></returns>
        public abstract bool SetCardPassword(int p_intSector, string p_strPassword);

        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="p_strPassword"></param>
        /// <returns></returns>
        public abstract bool SetWriterPassword(string p_strPassword);

        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="p_strPassword"></param>
        /// <returns></returns>
        public abstract bool SetWriterPassword(int port, string p_strPassword);

        /// <summary>
        /// 充值金额
        /// </summary>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public abstract bool RechargeMoney(decimal dMoney);

        /// <summary>
        /// 充值补贴
        /// </summary>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public abstract bool RechargeSubsidize(decimal dMoney);

        /// <summary>
        /// COM口号
        /// </summary>
        public int ComPort
        {
            set;
            get;
        }

        /// <summary>
        /// 获取Pos机消费余额
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetPosVal();

        /// <summary>
        /// 获取水费余额
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetSubzVal();

        /// <summary>
        /// 获取亲情号码列表，第0位存放卡信息，后4位存放电话号码
        /// </summary>
        /// <returns></returns>
        public abstract List<string> GetPhoneNumList();

        /// <summary>
        /// 获取用户基本信息，包括用户名称，功能列表，最低余额，每天最大消费额，卡号等
        /// </summary>
        /// <returns></returns>
        public abstract PaymentCard GetUserBasicInfos();

        /// <summary>
        /// 初始化Pos消费金额
        /// </summary>
        /// <param name="dMoney">初始金额</param>
        /// <returns></returns>
        public abstract bool WriteInitPosVal(decimal dMoney);

        /// <summary>
        /// 初始化水控金额
        /// </summary>
        /// <param name="dMoney">初始金额</param>
        /// <returns></returns>
        public abstract bool WriteInitSubzVal(decimal dMoney);

        /// <summary>
        /// 初始化亲情号码列表
        /// </summary>
        /// <param name="strContactInfo">电话机显示信息，C\G对应的的两位ASCII16进制数值 + 四位届别 + 两位班别 + 两位学号</param>
        /// <param name="listPhoneNums">联系电话号码列表</param>
        /// <returns></returns>
        public abstract bool WriteInitContactPhone(string strContactInfo, List<string> listPhoneNums);

        /// <summary>
        /// 初始化用户基本信息
        /// </summary>
        /// <param name="payCard">卡信息</param>
        /// <returns></returns>
        public abstract bool WriteInitUserBasicInfo(PaymentCard payCard);

        /// <summary>
        /// 设置水表管理卡
        /// </summary>
        /// <param name="watermeterPassword">水表消费密码</param>
        /// <param name="amountInEach">单次用水限额</param>
        /// <param name="amountInDay">日用水限额</param>
        /// <param name="unitPrice">每升价格</param>
        /// <returns></returns>
        public abstract bool SetWatermeterAdminCard(int port, string paymentPassword, int eachTimeMaxinumNumber, int dailyMaxinumNumber, decimal unitPrice);

        /// <summary>
        /// 初始化出厂ID所在扇区密码
        /// </summary>
        /// <param name="strPwd">扇区密码</param>
        /// <returns></returns>
        public abstract bool InitMacSectionPwd(string strPwd);

        /// <summary>
        /// 初始化卡片消费扇区密码
        /// </summary>
        /// <param name="strPwd">扇区密码</param>
        /// <returns></returns>
        public abstract bool InitConsumptionSectionPwd(string strPwd);

        /// <summary>
        /// 初始化卡片水费扇区密码
        /// </summary>
        /// <param name="strPwd">扇区密码</param>
        /// <returns></returns>
        public abstract bool InitWaterSectionPwd(string strPwd);

        /// <summary>
        /// 初始化卡片电话扇区密码
        /// </summary>
        /// <param name="strPwd">扇区密码</param>
        /// <returns></returns>
        public abstract bool InitPhoneSectionPwd(string strPwd);
    }
}

