using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.Funtion;
using PaymentEquipment.IDevice;
using PaymentEquipment.Entity;
using PaymentEquipment.DLL;

namespace PaymentEquipment.DeviceImplement
{
    public class LeoJocatWriter : AbstractICWriter
    {
        private int m_intComPort;
        private int m_intSectorNo;
        private string _strWriterPwd;
        //private const string m_strPhonePassword = "54484C696E6B";
        //private const string m_strPhoneSector = "40";
        private const string m_strWaterCollectCardPW = "4C454F205448";
        private const string m_strAdminPassword = "4C454F2D5448";
        private const string m_strCollectionNormal = "00000000000000000000FFFFFFFF5F2E";

        //public LeoJocatWriter()
        //{
        //}

        public override bool Conn(int iCom, int iBaud, byte bSection, string strPwd)
        {
            try
            {
                this.m_intComPort = iCom;
                base.ComPort = iCom;
                this.m_intSectorNo = bSection;
                this._strWriterPwd = strPwd;
            }
            catch
            {
                return false;
            }
            return true;
            //this.m_intSectorNo = p_intSectorNo;
        }

        /// <summary>
        /// 取得MAC地址
        /// </summary>
        /// <returns></returns>
        public override string GetMacID()
        {
            try
            {
                //string strPwd = this._strWriterPwd;

                //SetWriterPassword(string.Empty.PadLeft(12, 'F'));

                string strMacID = JocatDevice.ReadCard(m_intComPort, "0", 1);

                //SetWriterPassword(strPwd);

                return strMacID;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 取得卡用户编号
        /// </summary>
        /// <returns></returns>
        public override int GetCardNo()
        {
            try
            {
                byte bSection = 0;
                return JocatDevice.ReadCardNo(m_intComPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0]);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 写卡资料
        /// </summary>
        /// <param name="p_objCard"></param>
        /// <returns></returns>
        public override bool WriteCard(PaymentCard p_objCard)
        {
            return JocatDevice.WriteCardInfo(m_intComPort, p_objCard);
        }

        /// <summary>
        /// 设置亲情号码资料
        /// </summary>
        /// <param name="p_strPhoneInfo">亲情号码卡信息</param>
        /// <param name="p_lisPhones">亲情号码</param>
        /// <returns></returns>
        public bool WriteFamilyPhones(string p_strPhoneInfo, List<string> p_lisPhones)
        {
            byte bSection = 10;
            Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
            string l_strPhoneInfo = p_strPhoneInfo + "FFFFFFFFFFFFFFFFFFFFFF";

            string l_strPhoneNo1 = FormatPhoneNo(p_lisPhones[0], p_lisPhones[1]);
            string l_strPhoneNo2 = FormatPhoneNo(p_lisPhones[2], p_lisPhones[3]);

            if (l_strPhoneInfo.Length != 32)
            {
                throw new Exception("亲情号码卡信息格式不正确。");
            }

            if ((l_strPhoneNo1 + l_strPhoneNo2).Length != 64)
            {
                throw new Exception("亲情号码格式不正确。");
            }

            return JocatDevice.WriteCard(m_intComPort, dicSection[bSection][0].ToString(), 3, l_strPhoneInfo + l_strPhoneNo1 + l_strPhoneNo2);
        }

        /// <summary>
        /// 读卡资料
        /// </summary>
        /// <returns></returns>
        public override PaymentCard ReadCard()
        {
            try
            {
                byte bSection = 0;
                var l_objCardInfo = JocatDevice.ReadCard(m_intComPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0], this._strWriterPwd);

                //List<string> l_lisPhones = new List<string>();
                //string l_strPhoneInfo = "";

                //ReadFamilyPhones(out l_strPhoneInfo, out l_lisPhones);

                //l_objCardInfo.FamilyPhoneInfo = l_strPhoneInfo;
                //l_objCardInfo.FamilyPhoneNum = l_lisPhones;

                return l_objCardInfo;
            }
            catch (Exception Ex)
            {
                throw Ex;

            }
        }

        /// <summary>
        /// 读取亲情电话信息
        /// </summary>
        /// <param name="p_strPhineInfo"></param>
        /// <param name="p_lisPhones"></param>
        private void ReadFamilyPhones(out string p_strPhineInfo, out List<string> p_lisPhones)
        {
            p_strPhineInfo = "";
            p_lisPhones = new List<string>();
            byte bSection = 10;
            Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

            try
            {
                string l_strPhoneInfo = JocatDevice.ReadCard(m_intComPort, dicSection[bSection][0].ToString(), 3);

                p_strPhineInfo = ((char)Convert.ToByte(l_strPhoneInfo.Substring(0, 2), 16)).ToString() + l_strPhoneInfo.Substring(2, 8);

                p_lisPhones.Add(CollapsePhoneNo(l_strPhoneInfo.Substring(32, 16).Replace("F", "")));
                p_lisPhones.Add(CollapsePhoneNo(l_strPhoneInfo.Substring(48, 16).Replace("F", "")));
                p_lisPhones.Add(CollapsePhoneNo(l_strPhoneInfo.Substring(64, 16).Replace("F", "")));
                p_lisPhones.Add(CollapsePhoneNo(l_strPhoneInfo.Substring(80, 16).Replace("F", "")));
            }
            catch
            {
                throw new Exception("读取亲情号码信息失败。");
            }
        }

        /// <summary>
        /// 设置卡片密码
        /// </summary>
        /// <param name="p_intSector">当参数值为“99”时，设置所有扇区的密码</param>
        /// <param name="p_strPassword">密码</param>
        /// <returns></returns>
        public override bool SetCardPassword(int p_intSector, string p_strPassword)
        {
            bool l_blnReturn = false;

            if (p_intSector == 0)
            {
                l_blnReturn = JocatDevice.SetCardPassword(this.ComPort, p_strPassword);
            }
            else if (p_intSector <= 16)
            {
                l_blnReturn = JocatDevice.SetCardPassword(this.ComPort, p_intSector.ToString(), p_strPassword);
            }
            else
            {
                l_blnReturn = JocatDevice.SetCardAllBlockPassword(this.ComPort, p_strPassword);
            }

            return l_blnReturn;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="p_dboMoney"></param>
        /// <returns></returns>
        public override bool RechargeMoney(decimal p_dboMoney)
        {
            int l_intBlock = m_intSectorNo * 4 + 4;
            int l_intMoney = (int)(p_dboMoney * 100.00M);
            return JocatDevice.AddCardMoney(m_intComPort, l_intBlock, l_intMoney);
        }

        /// <summary>
        /// 充值补贴
        /// </summary>
        /// <param name="p_intMoney"></param>
        /// <returns></returns>
        public override bool RechargeSubsidize(decimal p_dboMoney)
        {
            int l_intBlock = m_intSectorNo * 4 + 8;
            int l_intMoney = (int)(p_dboMoney * 100.00M);
            return JocatDevice.AddCardMoney(m_intComPort, l_intBlock, l_intMoney);
        }

        /// <summary>
        /// 格式化电话号码
        /// </summary>
        /// <param name="p_strPhoneNo1"></param>
        /// <param name="p_strPhoneNo2"></param>
        /// <returns></returns>
        private string FormatPhoneNo(string p_strPhoneNo1, string p_strPhoneNo2)
        {
            string l_strPreFix = "000000000000";

            string l_strReturn = "";

            l_strReturn = l_strPreFix.Substring(0, 12 - p_strPhoneNo1.Length) + p_strPhoneNo1 + "FFFF";
            l_strReturn = l_strReturn + l_strPreFix.Substring(0, 12 - p_strPhoneNo2.Length) + p_strPhoneNo2 + "FFFF";

            return l_strReturn;
        }

        /// <summary>
        /// 格式化电话号码，去掉多余的0
        /// </summary>
        /// <param name="p_strPhoneNo"></param>
        /// <returns></returns>
        private string CollapsePhoneNo(string p_strPhoneNo)
        {
            string l_strPhone = "";

            for (int i = 0; i < p_strPhoneNo.Length; i++)
            {
                if (p_strPhoneNo.Substring(i, 1) != "0")
                {
                    l_strPhone = p_strPhoneNo.Substring(i, p_strPhoneNo.Length - i);
                    break;
                }
            }

            return l_strPhone;
        }

        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="p_strPassword"></param>
        /// <returns></returns>
        public override bool SetWriterPassword(string p_strPassword)
        {
            this._strWriterPwd = p_strPassword;
            return JocatDevice.SetReaderPassWord(m_intComPort, p_strPassword);
        }

        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="port"></param>
        /// <param name="p_strPassword"></param>
        /// <returns></returns>
        public override bool SetWriterPassword(int port, string p_strPassword)
        {
            this._strWriterPwd = p_strPassword;
            return JocatDevice.SetReaderPassWord(port, p_strPassword);
        }

        /// <summary>
        /// 设置电话区密码
        /// </summary>
        //public void InitPhoneSector()
        //{
        //    JocatDevice.SetCardPassword(m_intComPort, "10", m_strPhonePassword);
        //}

        /// <summary>
        /// 设置用水收集卡
        /// </summary>
        public void ResetWaterCollectCard()
        {
            //设置读卡器密码
            JocatDevice.SetReaderPassWord(m_intComPort, m_strWaterCollectCardPW);

            //写入资料
            JocatDevice.WriteCard(m_intComPort, "9", 2, m_strCollectionNormal + m_strCollectionNormal);
        }

        /// <summary>
        /// 设置热水管理卡
        /// </summary>
        public void SetWaterAdminCard(string p_strPassword, int p_intPreUse, int p_intDailyUse, decimal p_decPrice)
        {
            //设置读卡器密码
            JocatDevice.SetReaderPassWord(m_intComPort, m_strAdminPassword);

            string l_strCRC = "FFFF9E10";

            string l_strPreUse = EquipmentGeneral.IntToHexInSorting(p_intPreUse, 4);
            string l_strDailyUse = EquipmentGeneral.IntToHexInSorting(p_intDailyUse, 4);
            string l_strPrice = EquipmentGeneral.IntToHexInSorting(Convert.ToInt32(p_decPrice * 100), 4);

            string l_strAdmin = p_strPassword + l_strPreUse + l_strDailyUse + l_strPrice + l_strCRC;

            //写入数据
            JocatDevice.WriteCard(m_intComPort, "9", 2, l_strAdmin + l_strAdmin);
        }

        /// <summary>
        /// 读取采集卡数据
        /// </summary>
        /// <param name="p_decWater"></param>
        /// <param name="p_decMachine"></param>
        public void ReadCollectCardInfo(out decimal p_decWater, out decimal p_decMachine)
        {
            p_decWater = 0;
            p_decMachine = 0;
            string l_strData = JocatDevice.ReadCard(m_intComPort, "9", 1);

            string l_strWater = l_strData.Substring(0, 8);
            string l_strMachine = l_strData.Substring(16, 2);

            p_decWater = EquipmentGeneral.HexMoneyToDouble(l_strData.Substring(0, 8));
            p_decMachine = EquipmentGeneral.HexMoneyToDouble(l_strData.Substring(16, 2));
        }

        public override decimal GetPosVal()
        {
            byte bSection = 1;
            return JocatDevice.GetPosVal(this.m_intComPort, bSection);
        }

        public override decimal GetSubzVal()
        {
            byte bSection = 2;
            return JocatDevice.GetPosVal(this.m_intComPort, bSection);
        }

        public override List<string> GetPhoneNumList()
        {
            byte bSection = 10;
            return JocatDevice.GetPhoneNumList(this.m_intComPort, bSection);
        }

        public override PaymentCard GetUserBasicInfos()
        {
            byte bSection = 0;
            PaymentCard cardInfo = JocatDevice.GetUserBasicInfos(this.m_intComPort, bSection);
            if (cardInfo != null)
            {
                //SetWriterPassword(string.Empty.PadLeft(12, 'F'));
                cardInfo.MACID = GetMacID();
                //SetWriterPassword(this._strWriterPwd);
            }

            return cardInfo;
        }

        public override bool WriteInitPosVal(decimal dMoney)
        {
            byte bSection = 1;
            int iCardPieceNo = EquipmentGeneral.GetCardSectionPieceList()[bSection][0];
            JocatDevice.CleanCardMoney(1, iCardPieceNo);
            return JocatDevice.AddCardMoney(1, iCardPieceNo, (int)(dMoney * 100));
        }

        public override bool WriteInitSubzVal(decimal dMoney)
        {
            byte bSection = 2;
            int iCardPieceNo = EquipmentGeneral.GetCardSectionPieceList()[bSection][0];
            JocatDevice.CleanCardMoney(1, iCardPieceNo);
            return JocatDevice.AddCardMoney(1, iCardPieceNo, (int)(dMoney * 100));
        }

        public override bool WriteInitContactPhone(string strContactInfo, List<string> listPhoneNums)
        {
            byte bSection = 10;
            return JocatDevice.WritePhoneList(this.ComPort, bSection, strContactInfo, listPhoneNums);
        }

        public override bool WriteInitUserBasicInfo(PaymentCard payCard)
        {
            byte bSection = 0;
            return JocatDevice.WriteCardBasicInfo(this.ComPort, bSection, payCard);
        }

        /// <summary>
        /// 设置水表管理卡
        /// </summary>
        /// <param name="watermeterPassword">水表消费密码</param>
        /// <param name="amountInEach">单次用水限额</param>
        /// <param name="amountInDay">日用水限额</param>
        /// <param name="unitPrice">每升价格</param>
        /// <returns></returns>
        public override bool SetWatermeterAdminCard(int port, string paymentPassword, int eachTimeMaxinumNumber, int dailyMaxinumNumber, decimal unitPrice)
        {
            byte bSection = 2;
            bool isSccess = false;
            string strPiece = EquipmentGeneral.GetCardSectionPieceList()[bSection][1].ToString();

            string l_strEachAmount = EquipmentGeneral.IntToHexInSorting(eachTimeMaxinumNumber, 4);
            string l_strMaxAmount = EquipmentGeneral.IntToHexInSorting(dailyMaxinumNumber, 4);

            decimal fUPrice = unitPrice * 100;
            int uPrice = Convert.ToInt32(fUPrice);
            string l_strUnitPrice = EquipmentGeneral.IntToHexInSorting(uPrice, 4);

            string l_strWaterInfo = paymentPassword + l_strEachAmount + l_strMaxAmount + l_strUnitPrice + "FFFF";

            l_strWaterInfo = l_strWaterInfo + DataConvert.GetCRC16Code(l_strWaterInfo);

            isSccess = JocatDevice.WriteCard(port, strPiece, 2, l_strWaterInfo + l_strWaterInfo);

            return isSccess;
        }

        public override bool InitConsumptionSectionPwd(string strPwd)
        {
            return true;
        }

        public override bool InitWaterSectionPwd(string strPwd)
        {
            return true;
        }

        public override bool InitPhoneSectionPwd(string strPwd)
        {
            return true;
        }

        public override bool InitMacSectionPwd(string strPwd)
        {
            return true;
        }
    }
}