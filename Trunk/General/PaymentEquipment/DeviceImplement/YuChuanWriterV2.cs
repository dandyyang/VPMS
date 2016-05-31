using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.IDevice;
using Model.General;
using PaymentEquipment.DLL;
using PaymentEquipment.Entity;
using PaymentEquipment.Funtion;

namespace PaymentEquipment.DeviceImplement
{
    public class YuChuanWriterV2 : AbstractICWriter
    {
        #region 字段、属性

        /// <summary>
        /// 蜂鸣时长
        /// </summary>
        private const ushort _iBeepInterval = 30;
        /// <summary>
        /// 扇区密码
        /// </summary>
        private string _strSectionPwd;
        /// <summary>
        /// 消费金额固定校验码
        /// </summary>
        private readonly string _strPosValCRC = "10EF10EF";
        /// <summary>
        /// 消费备份金额固定校验码
        /// </summary>
        private readonly string _strPosValBakCRC = "15EA15EA";
        /// <summary>
        /// <summary>
        /// 水控金额固定校验码
        /// </summary>
        private readonly string _strSubValCRC = "08F708F7";
        /// <summary>
        /// 水控备份金额固定校验码
        /// </summary>
        private readonly string _strSubValBakCRC = "09F609F6";

        #endregion

        public override bool Conn(int iCom, int iBaud, byte bSection, string strPwd)
        {
            try
            {
                this._strSectionPwd = string.Empty.PadLeft(12, 'F');

                if (!string.IsNullOrEmpty(strPwd))
                {
                    this._strSectionPwd = strPwd;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得卡片的固有ID号
        /// </summary>
        /// <returns></returns>
        public override string GetMacID()
        {
            string strMacID = string.Empty;
            //string strPwd = this._strSectionPwd;
            try
            {
                //this._strSectionPwd = string.Empty.PadLeft(12, 'F');
                if (!ReadCardsPrepare(0))
                {
                    return string.Empty;
                }

                YuChuanDevice.rf_beep(_iBeepInterval);

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(0, arrData);
                strMacID = YuChuanDevice.ChangeHexToString(arrData).ToUpper();
                //this._strSectionPwd = strPwd;
            }
            catch (Exception ex)
            {
                //this._strSectionPwd = strPwd;
                throw ex;
            }
            return strMacID;
        }

        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <returns></returns>
        public override int GetCardNo()
        {
            int iCardNo = 0;
            byte bSection = 0;
            Dictionary<byte, List<byte>> dicSetcion = EquipmentGeneral.GetCardSectionPieceList();
            try
            {
                if (!ReadCardsPrepare(bSection))
                {
                    return 0;
                }

                YuChuanDevice.rf_beep(_iBeepInterval);
                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSetcion[bSection][1], arrData);
                string strPiece = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece.Length == 32)
                {
                    string strHexCardNo = strPiece.Substring(2, 1) + strPiece.Substring(3, 1) + strPiece.Substring(0, 1) + strPiece.Substring(1, 1);
                    iCardNo = Convert.ToInt32(strHexCardNo, 16);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return iCardNo;
        }

        public override bool WriteCard(PaymentCard p_objCard)
        {
            bool res = true;

            try
            {
                res = WriteInitUserBasicInfo(p_objCard);
                res = WriteInitPosVal(p_objCard.Money);
                res = WriteInitSubzVal(p_objCard.Subsidize);
                res = WriteInitContactPhone(p_objCard.FamilyPhoneInfo, p_objCard.FamilyPhoneNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public override PaymentCard ReadCard()
        {
            PaymentCard payCard = GetUserBasicInfos();
            if (payCard == null)
            {
                return null;
            }
            payCard.Money = GetPosVal();
            payCard.Subsidize = GetSubzVal();
            //payCard.FamilyPhoneNum = GetPhoneNumList();
            //payCard.FamilyPhoneInfo = payCard.FamilyPhoneNum[0];
            //payCard.FamilyPhoneNum.RemoveAt(0);

            return payCard;
        }

        /// <summary>
        /// 设置指定卡扇区密码
        /// </summary>
        /// <param name="p_intSector">扇区号</param>
        /// <param name="p_strPassword">密码</param>
        /// <returns></returns>
        public override bool SetCardPassword(int p_intSector, string p_strPassword)
        {
            bool res = false;
            YuChuanDevice.rf_beep(_iBeepInterval);

            try
            {
                if (string.IsNullOrEmpty(p_strPassword))
                {
                    return res;
                }
                if (p_strPassword.Length != 12)
                {
                    return res;
                }

                byte[] arrNewPwd = YuChuanDevice.ChangeStrToHex(p_strPassword);

                if (!ReadCardsPrepare((byte)p_intSector))
                {
                    return false;
                }

                List<byte> listNewPwd = new List<byte>();
                listNewPwd.AddRange(arrNewPwd);
                listNewPwd.AddRange(YuChuanDevice.bCardContentTemplate);

                Dictionary<byte, List<byte>> dicSP = EquipmentGeneral.GetCardSectionPieceList();
                int iPwdPieceNum = dicSP[(byte)p_intSector][3];
                int iResWrite = YuChuanDevice.rf_write((byte)iPwdPieceNum, listNewPwd.ToArray());
                if (iResWrite == 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            YuChuanDevice.rf_beep(_iBeepInterval);
            return res;
        }

        public override bool SetWriterPassword(string p_strPassword)
        {
            try
            {
                if (p_strPassword.Length != 12)
                {
                    return false;
                }
                try
                {
                    Convert.ToInt64(p_strPassword, 16);
                }
                catch (Exception)
                {
                    return false;
                }

                this._strSectionPwd = p_strPassword;
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }

        public override bool SetWriterPassword(int port, string p_strPassword)
        {
            return SetWriterPassword(p_strPassword);
        }

        public override bool RechargeMoney(decimal p_dMoney)
        {
            try
            {
                byte bSection = 1;
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                #region 读第1块

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][0], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return false;
                }
                string strPiece4 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece4.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return false;
                }
                decimal dMoney = EquipmentGeneral.HexMoneyToDouble(strPiece4.Substring(0, 8));

                #endregion

                dMoney = dMoney + p_dMoney;

                #region 写入第1块数据

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                //卡内金额
                string strCardVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strCardVal = strCardVal.Substring(6, 2) + strCardVal.Substring(4, 2) + strCardVal.Substring(2, 2) + strCardVal.Substring(0, 2);

                //卡内金额反码
                string strComplement = EquipmentGeneral.GetComplement(strCardVal);

                string strPieceData = strCardVal + strComplement + strCardVal + this._strPosValCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                int iRes = YuChuanDevice.rf_write(dicSection[bSection][0], arrData);
                if (iRes == 0)
                {
                    YuChuanDevice.rf_beep(_iBeepInterval);
                }
                else
                {
                    return false;
                }

                #endregion

                #region 读第2块

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                arrData = new byte[16];
                iResRead = YuChuanDevice.rf_read(dicSection[bSection][1], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return false;
                }
                string strPiece5 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece5.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return false;
                }
                dMoney = EquipmentGeneral.HexMoneyToDouble(strPiece5.Substring(0, 8));

                #endregion

                dMoney = dMoney + p_dMoney;

                #region 写入第2块数据

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                //备份金额
                string strCardBakVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strCardBakVal = strCardBakVal.Substring(6, 2) + strCardBakVal.Substring(4, 2) + strCardBakVal.Substring(2, 2) + strCardBakVal.Substring(0, 2);

                //卡内金额反码
                string strBakComplement = EquipmentGeneral.GetComplement(strCardBakVal);

                strPieceData = strCardBakVal + strBakComplement + strCardBakVal + this._strPosValBakCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                iRes = YuChuanDevice.rf_write(dicSection[bSection][1], arrData);
                if (iRes == 0)
                {
                    YuChuanDevice.rf_beep(_iBeepInterval);
                }
                else
                {
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public override bool RechargeSubsidize(decimal p_dMoney)
        {
            try
            {
                byte bSection = 2;
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                #region 读第1块

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][0], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return false;
                }
                string strPiece8 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece8.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return false;
                }
                decimal dMoney = EquipmentGeneral.HexMoneyToDouble(strPiece8.Substring(0, 8));

                #endregion

                dMoney = dMoney + p_dMoney;

                #region 写入第1块数据

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                //水控金额
                string strCardVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strCardVal = strCardVal.Substring(6, 2) + strCardVal.Substring(4, 2) + strCardVal.Substring(2, 2) + strCardVal.Substring(0, 2);

                //水控金额反码
                string strComplement = EquipmentGeneral.GetComplement(strCardVal);

                string strPieceData = strCardVal + strComplement + strCardVal + this._strSubValCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                int iRes = YuChuanDevice.rf_write(dicSection[bSection][0], arrData);
                if (iRes == 0)
                {
                    YuChuanDevice.rf_beep(_iBeepInterval);
                }
                else
                {
                    return false;
                }

                #endregion

                #region 读第2块

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                arrData = new byte[16];
                iResRead = YuChuanDevice.rf_read(dicSection[bSection][1], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return false;
                }
                string strPiece9 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece9.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return false;
                }
                dMoney = EquipmentGeneral.HexMoneyToDouble(strPiece9.Substring(0, 8));

                #endregion

                dMoney = dMoney + p_dMoney;

                #region 写入第2块数据

                if (!ReadCardsPrepare((bSection)))
                {
                    return false;
                }

                //水控备份金额
                string strCardBakVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strCardBakVal = strCardBakVal.Substring(6, 2) + strCardBakVal.Substring(4, 2) + strCardBakVal.Substring(2, 2) + strCardBakVal.Substring(0, 2);

                //水控备份金额反码
                string strBakComplement = EquipmentGeneral.GetComplement(strCardBakVal);

                strPieceData = strCardBakVal + strBakComplement + strCardBakVal + this._strSubValBakCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                iRes = YuChuanDevice.rf_write(dicSection[bSection][1], arrData);
                if (iRes == 0)
                {
                    YuChuanDevice.rf_beep(_iBeepInterval);
                }
                else
                {
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// 解读优卡特消费机水表可读数据格式
        /// </summary>
        /// <param name="dicPieceData"></param>
        /// <param name="payCard"></param>
        /// <returns></returns>
        private ReturnValueInfo DecodeJocatUserInfo(Dictionary<int, string> dicPieceData, ref PaymentCard payCard)
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();

            try
            {
                #region 解释第一块数据

                string strPieceData = dicPieceData[1];

                //出厂ID
                payCard.MACID = GetMacID();

                //卡号
                string strHexCardNo = strPieceData.Substring(2, 1) + strPieceData.Substring(3, 1) + strPieceData.Substring(0, 1) + strPieceData.Substring(1, 1);
                payCard.No = Convert.ToInt32(strHexCardNo, 16);

                //状态
                payCard.Valid = strPieceData.Substring(4, 2) == "80" ? true : false;

                //注册时间
                payCard.ExpiryDate = DateTime.ParseExact(strPieceData.Substring(6, 6), "yyMMdd", null);

                //系统功能
                payCard.FunctionCode = Convert.ToString(Convert.ToInt32(strPieceData.Substring(12, 4), 16), 2).PadLeft(16, '0');

                //卡片所有人姓名
                payCard.Name = EquipmentGeneral.HexToStr(strPieceData.Substring(16, 16));

                #endregion

                #region 解释第二块数据

                strPieceData = dicPieceData[2];

                //最低余额
                payCard.LimitRemainder = (decimal)(Convert.ToInt32(strPieceData.Substring(0, 8), 16) / 100.00M);

                //消费密码
                payCard.PaymentPassword = strPieceData.Substring(8, 6);

                //每天最大消费额
                payCard.DailyMaxPayment = (decimal)(Convert.ToInt32(strPieceData.Substring(14, 8), 16) / 100.00M);

                //时段消费次数
                payCard.Span1PaymentCount = Convert.ToInt32(strPieceData.Substring(22, 2), 16);
                payCard.Span2PaymentCount = Convert.ToInt32(strPieceData.Substring(24, 2), 16);
                payCard.Span3PaymentCount = Convert.ToInt32(strPieceData.Substring(26, 2), 16);
                payCard.Span4PaymentCount = Convert.ToInt32(strPieceData.Substring(28, 2), 16);

                //卡类型
                payCard.Type = ((char)Convert.ToByte(strPieceData.Substring(30, 2), 16)).ToString();
                #endregion

                #region 解释第四块数据

                strPieceData = dicPieceData[4];

                payCard.Money = EquipmentGeneral.HexMoneyToDouble(strPieceData.Substring(0, 8));

                #endregion

                #region 解释第五块数据

                strPieceData = dicPieceData[5];

                payCard.MoneyBackup = EquipmentGeneral.HexMoneyToDouble(strPieceData.Substring(0, 8));

                #endregion

                #region 解释第六块数据

                payCard.MachineList += dicPieceData[6];

                #endregion

                #region 解释第八块数据

                strPieceData = dicPieceData[8];

                payCard.Subsidize = EquipmentGeneral.HexMoneyToDouble(strPieceData.Substring(0, 8));

                #endregion

                #region 解释第九块数据

                strPieceData = dicPieceData[9];

                payCard.SubsidizeBackup = EquipmentGeneral.HexMoneyToDouble(strPieceData.Substring(0, 8));

                #endregion

                #region 解释第十块数据

                payCard.MachineList += dicPieceData[10];

                #endregion
            }
            catch (Exception ex)
            {
                rvInfo.isError = true;
                rvInfo.messageText = ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 读卡准备
        /// </summary>
        /// <param name="bSection">需进行读写操作的的扇区号</param>
        /// <returns></returns>
        private bool ReadCardsPrepare(byte bSection)
        {
            ReturnValueInfo rvInfo = YuChuanDevice.ReadWritePrepare();
            if (rvInfo.isError)
            {
                return false;
            }

            byte[] arrPwd = YuChuanDevice.ChangeStrToHex(this._strSectionPwd);
            ReturnValueInfo rvVerify = YuChuanDevice.PwdVerify(arrPwd, YC_Enum_PwdVerifyMode.A_Mode, bSection);
            if (rvVerify.isError)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得消费金额
        /// </summary>
        /// <returns></returns>
        public override decimal GetPosVal()
        {
            decimal dPosVal = 0.00M;
            byte bSection = 1;

            try
            {
                if (!ReadCardsPrepare(bSection))
                {
                    return 0;
                }

                YuChuanDevice.rf_beep(_iBeepInterval);

                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][0], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return 0;
                }
                string strPiece = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return 0;
                }

                dPosVal = EquipmentGeneral.HexMoneyToDouble(strPiece.Substring(0, 8));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dPosVal;
        }

        /// <summary>
        /// 获得水表金额
        /// </summary>
        /// <returns></returns>
        public override decimal GetSubzVal()
        {
            decimal dSubzVal = 0.00M;
            byte bSection = 2;

            try
            {
                if (!ReadCardsPrepare(bSection))
                {
                    return 0;
                }

                YuChuanDevice.rf_beep(_iBeepInterval);

                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][0], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return 0;
                }
                string strPiece = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return 0;
                }

                dSubzVal = EquipmentGeneral.HexMoneyToDouble(strPiece.Substring(0, 8));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dSubzVal;
        }

        /// <summary>
        /// 获得亲情号码列表
        /// </summary>
        /// <returns></returns>
        public override List<string> GetPhoneNumList()
        {
            List<string> listPhones = new List<string>();
            byte bSection = 10;

            try
            {
                YuChuanDevice.rf_beep(_iBeepInterval);
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                if (!ReadCardsPrepare(bSection))
                {
                    return null;
                }

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][0], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return null;
                }
                string strPiece = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return null;
                }
                strPiece = strPiece.ToUpper().TrimEnd('F');
                strPiece = Convert.ToChar(Convert.ToInt32(strPiece.Substring(0, 2), 16)).ToString() + strPiece.Substring(2, strPiece.Length - 2);
                listPhones.Add(strPiece);

                string strEmpty = string.Empty;
                strEmpty = strEmpty.PadRight(16, '0');
                for (int i = 0; i < 2; i++)
                {
                    if (!ReadCardsPrepare(bSection))
                    {
                        return null;
                    }

                    arrData = new byte[16];
                    iResRead = YuChuanDevice.rf_read(dicSection[bSection][i + 1], arrData);
                    if (iResRead != 0)
                    {
                        //读数失败
                        return null;
                    }
                    strPiece = YuChuanDevice.ChangeHexToString(arrData);
                    if (strPiece.Length != 32)
                    {
                        //读取块数据不完整，不足32位
                        return null;
                    }

                    string strPhoneOneTmp = strPiece.Substring(0, 16);
                    string strPhoneTwoTmp = strPiece.Substring(16, 16);
                    if (strPhoneOneTmp != strEmpty)
                    {
                        string strPhoneOne = GetPhoneNum(strPhoneOneTmp);
                        if (!string.IsNullOrEmpty(strPhoneOne) && strPhoneOne.Substring(0, 1) != "1" && strPhoneOne.Length != 7 && strPhoneOne.Length != 8)
                        {
                            strPhoneOne = "0" + strPhoneOne;
                        }
                        listPhones.Add(strPhoneOne);
                    }
                    if (strPhoneTwoTmp != strEmpty)
                    {
                        string strPhoneTwo = GetPhoneNum(strPhoneTwoTmp);
                        if (!string.IsNullOrEmpty(strPhoneTwo) && strPhoneTwo.Substring(0, 1) != "1" && strPhoneTwo.Length != 7 && strPhoneTwo.Length != 8)
                        {
                            strPhoneTwo = "0" + strPhoneTwo;
                        }
                        listPhones.Add(strPhoneTwo);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listPhones;
        }

        /// <summary>
        /// 获得用户卡基本信息
        /// </summary>
        /// <returns></returns>
        public override PaymentCard GetUserBasicInfos()
        {
            PaymentCard payCard = new PaymentCard();
            byte bSection = 0;

            try
            {
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                string strPwdTemp = this._strSectionPwd;
                this._strSectionPwd = string.Empty.PadRight(12, 'F');
                payCard.MACID = GetMacID();
                this._strSectionPwd = strPwdTemp;

                #region 读第1块

                if (!ReadCardsPrepare(bSection))
                {
                    return null;
                }

                byte[] arrData = new byte[16];
                int iResRead = YuChuanDevice.rf_read(dicSection[bSection][1], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return null;
                }
                string strPiece1 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece1.Length != 32)
                {
                    //读取到的块数据不完整，不足32位
                    return null;
                }

                #endregion

                #region 读第2块

                if (!ReadCardsPrepare(bSection))
                {
                    return null;
                }

                arrData = new byte[16];
                iResRead = YuChuanDevice.rf_read(dicSection[bSection][2], arrData);
                if (iResRead != 0)
                {
                    //读数失败
                    return null;
                }
                string strPiece2 = YuChuanDevice.ChangeHexToString(arrData);
                if (strPiece2.Length != 32)
                {
                    //读取块数据不完整，不足32位
                    return null;
                }

                #endregion

                #region 解释第一块数据

                string strPieceData = strPiece1;

                //出厂ID
                payCard.MACID = GetMacID();

                //卡号
                string strHexCardNo = strPieceData.Substring(2, 1) + strPieceData.Substring(3, 1) + strPieceData.Substring(0, 1) + strPieceData.Substring(1, 1);
                payCard.No = Convert.ToInt32(strHexCardNo, 16);

                //状态
                payCard.Valid = strPieceData.Substring(4, 2) == "80" ? true : false;

                //注册时间
                try
                {
                    payCard.ExpiryDate = DateTime.ParseExact(strPieceData.Substring(6, 6), "yyMMdd", null);
                }
                catch (Exception EX)
                {
                    payCard.ExpiryDate = null;
                }

                //系统功能
                payCard.FunctionCode = Convert.ToString(Convert.ToInt32(strPieceData.Substring(12, 4), 16), 2).PadLeft(16, '0');

                //卡片所有人姓名
                payCard.Name = EquipmentGeneral.HexToStr(strPieceData.Substring(16, 16));

                #endregion

                #region 解释第二块数据

                strPieceData = strPiece2;

                //最低余额
                payCard.LimitRemainder = (decimal)(Convert.ToInt32(strPieceData.Substring(0, 8), 16) / 100.00M);

                //消费密码
                payCard.PaymentPassword = strPieceData.Substring(8, 6);

                //每天最大消费额
                payCard.DailyMaxPayment = (decimal)(Convert.ToInt32(strPieceData.Substring(14, 8), 16) / 100.00M);

                //时段消费次数
                payCard.Span1PaymentCount = Convert.ToInt32(strPieceData.Substring(22, 2), 16);
                payCard.Span2PaymentCount = Convert.ToInt32(strPieceData.Substring(24, 2), 16);
                payCard.Span3PaymentCount = Convert.ToInt32(strPieceData.Substring(26, 2), 16);
                payCard.Span4PaymentCount = Convert.ToInt32(strPieceData.Substring(28, 2), 16);

                //卡类型
                payCard.Type = ((char)Convert.ToByte(strPieceData.Substring(30, 2), 16)).ToString();

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return payCard;
        }

        /// <summary>
        /// 抽取指定格式的电话号码
        /// </summary>
        /// <param name="strSourceData">源数据</param>
        /// <returns></returns>
        private string GetPhoneNum(string strSourceData)
        {
            string strPhone = string.Empty;

            try
            {
                strPhone = strSourceData.ToUpper();
                strPhone = strPhone.TrimEnd('F');
                strPhone = strPhone.TrimStart('0');
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return strPhone;
        }

        public override bool WriteInitPosVal(decimal dMoney)
        {
            YuChuanDevice.rf_beep(_iBeepInterval);
            byte bSection;

            try
            {
                bSection = 1;
                Dictionary<byte, List<byte>> dicSect = EquipmentGeneral.GetCardSectionPieceList();

                #region 写入初始消费金额数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                //卡内金额
                string strCardVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strCardVal = strCardVal.Substring(6, 2) + strCardVal.Substring(4, 2) + strCardVal.Substring(2, 2) + strCardVal.Substring(0, 2);

                //卡内金额反码
                string strComplement = EquipmentGeneral.GetComplement(strCardVal);

                string strPieceData = strCardVal + strComplement + strCardVal + this._strPosValCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                byte[] arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                int iRes = YuChuanDevice.rf_write(dicSect[bSection][0], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                #region 写入备份消费金额数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                strPieceData = strCardVal + strComplement + strCardVal + this._strPosValCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                iRes = YuChuanDevice.rf_write(dicSect[bSection][1], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                if (1 == 2)
                {
                    #region 不用到

                    bSection = 5;

                    #region 写入初始消费金额数据（备份扇区）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    strPieceData = strCardVal + strComplement + strCardVal + this._strPosValBakCRC;
                    if (strPieceData.Length != 32)
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][0], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #region 写入备份消费金额数据（备份扇区）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    strPieceData = strCardVal + strComplement + strCardVal + this._strPosValBakCRC;
                    if (strPieceData.Length != 32)
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][1], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    bSection = 6;

                    #region 写入消费记录初始数据（0号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    strPieceData = DateTime.Now.AddDays(-1).ToString("yyMMdd01");
                    strPieceData = strPieceData.PadLeft(24, '0').PadRight(32, '0');
                    if (strPieceData.Length != 32)
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][0], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #region 写入消费记录初始数据（1号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][1], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #region 写入消费记录初始数据（2号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    string strPieceDataTmp = string.Empty.PadLeft(30, '0') + "AA";
                    arrData = YuChuanDevice.ChangeStrToHex(strPieceDataTmp);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][2], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion


                    bSection = 7;

                    #region 写入备份消费记录初始数据（0号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    if (strPieceData.Length != 32)
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][0], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #region 写入备份消费记录初始数据（1号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][1], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #region 写入备份消费记录初始数据（2号位置）

                    if (!ReadCardsPrepare(bSection))
                    {
                        return false;
                    }

                    arrData = YuChuanDevice.ChangeStrToHex(strPieceDataTmp);
                    iRes = YuChuanDevice.rf_write(dicSect[bSection][2], arrData);
                    if (iRes != 0)
                    {
                        return false;
                    }

                    #endregion

                    #endregion
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            YuChuanDevice.rf_beep(_iBeepInterval);
            return true;
        }

        public override bool WriteInitSubzVal(decimal dMoney)
        {
            Dictionary<byte, List<byte>> dicSect = EquipmentGeneral.GetCardSectionPieceList();
            byte bSection = 2;
            YuChuanDevice.rf_beep(_iBeepInterval);

            try
            {
                #region 写入初始水控金额数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                //水控金额
                string strSubVal = EquipmentGeneral.IntToHex((int)(dMoney * 100), 8).PadRight(8, '0');
                strSubVal = strSubVal.Substring(6, 2) + strSubVal.Substring(4, 2) + strSubVal.Substring(2, 2) + strSubVal.Substring(0, 2);

                //水控金额反码
                string strSubComplement = EquipmentGeneral.GetComplement(strSubVal);

                string strPieceData = strSubVal + strSubComplement + strSubVal + this._strSubValCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                byte[] arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                int iRes = YuChuanDevice.rf_write(dicSect[bSection][0], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                #region 写入备份水控金额数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                strPieceData = strSubVal + strSubComplement + strSubVal + this._strSubValBakCRC;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                iRes = YuChuanDevice.rf_write(dicSect[bSection][1], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            YuChuanDevice.rf_beep(_iBeepInterval);
            return true;
        }

        public override bool WriteInitContactPhone(string strContactInfo, List<string> listPhoneNums)
        {
            YuChuanDevice.rf_beep(_iBeepInterval);
            byte bSection;

            try
            {
                bSection = 10;
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

                //strContactInfo = Convert.ToString(Convert.ToInt32(char.Parse(strContactInfo.Substring(0, 1))), 16) + strContactInfo.Substring(1, strContactInfo.Length - 1);
                if (string.IsNullOrEmpty(strContactInfo))
                {
                    return false;
                }

                string strPhoneInfo;
                if (strContactInfo.Length == 9)
                {
                    strContactInfo = Convert.ToString(Convert.ToInt32(char.Parse(strContactInfo.Substring(0, 1))), 16) + strContactInfo.Substring(1, strContactInfo.Length - 1);
                }
                strPhoneInfo = strContactInfo.PadRight(32, 'F');


                #region 初始化卡显示信息

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                byte[] arrData = YuChuanDevice.ChangeStrToHex(strPhoneInfo);
                int iRes = YuChuanDevice.rf_write(dicSection[bSection][0], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                string strPhoneSub = string.Empty.PadLeft(12, 'F');
                List<string> listPhoneWait = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    listPhoneWait.Add(strPhoneSub);
                }
                for (int i = 0; i < listPhoneNums.Count; i++)
                {
                    listPhoneWait[i] = listPhoneNums[i].PadLeft(12, '0').PadRight(16, 'F');
                }
                string strPhoneListOne = listPhoneWait[0] + listPhoneWait[1];
                string strPhoneListTwo = listPhoneWait[2] + listPhoneWait[3];

                #region 初始化第一批电话列表

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPhoneListOne);
                iRes = YuChuanDevice.rf_write(dicSection[bSection][1], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                #region 初始化第二批电话列表

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPhoneListTwo);
                iRes = YuChuanDevice.rf_write(dicSection[bSection][2], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }

            YuChuanDevice.rf_beep(_iBeepInterval);
            return true;
        }

        public override bool WriteInitUserBasicInfo(PaymentCard payCard)
        {
            YuChuanDevice.rf_beep(_iBeepInterval);
            byte bSection = 0;
            try
            {
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
                string strPieceData = string.Empty;

                #region 初始化第1块信息数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                //卡号
                string strCardNo = EquipmentGeneral.IntToHex(payCard.No, 4);
                strCardNo = strCardNo.Substring(2, 2) + strCardNo.Substring(0, 2);
                //状态值
                string strState = payCard.Valid ? "80" : "00";
                //注销时间
                string strRegDate = payCard.ExpiryDate.Value.ToString("yyMMdd");
                //功能
                string strFunc = string.Empty;
                if (payCard.FunctionCode.Length == 16)
                {
                    strFunc = EquipmentGeneral.BinToHex(payCard.FunctionCode).PadLeft(4, '0');
                }
                else if (payCard.FunctionCode.Length == 4)
                {
                    strFunc = payCard.FunctionCode;
                }
                else
                {
                    strFunc = string.Empty.PadRight(4, '0');
                }

                //姓名
                string strName = EquipmentGeneral.StrToHex(payCard.Name).PadRight(16, '0');

                strPieceData = strCardNo + strState + strRegDate + strFunc + strName;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                byte[] arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                int iRes = YuChuanDevice.rf_write(dicSection[bSection][1], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion

                #region 初始化第2块信息数据

                if (!ReadCardsPrepare(bSection))
                {
                    return false;
                }

                //卡内最低余额
                string strMinVal = EquipmentGeneral.IntToHex((int)(payCard.LimitRemainder * 100), 8);
                //每日最大消费额
                string strMaxPosVal = EquipmentGeneral.IntToHex((int)(payCard.DailyMaxPayment * 100), 8);
                //时段限次消费次数
                string strIntervalCount = EquipmentGeneral.IntToHex(payCard.Span1PaymentCount, 2) + EquipmentGeneral.IntToHex(payCard.Span2PaymentCount, 2) + EquipmentGeneral.IntToHex(payCard.Span3PaymentCount, 2) + EquipmentGeneral.IntToHex(payCard.Span4PaymentCount, 2);
                //卡片类型
                string strType = EquipmentGeneral.IntToHex(Convert.ToInt32(char.Parse(payCard.Type)), 2);

                strPieceData = strMinVal + payCard.PaymentPassword + strMaxPosVal + strIntervalCount + strType;
                if (strPieceData.Length != 32)
                {
                    return false;
                }

                arrData = YuChuanDevice.ChangeStrToHex(strPieceData);
                iRes = YuChuanDevice.rf_write(dicSection[bSection][2], arrData);
                if (iRes != 0)
                {
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            YuChuanDevice.rf_beep(_iBeepInterval);
            return true;
        }

        public override bool SetWatermeterAdminCard(int port, string paymentPassword, int eachTimeMaxinumNumber, int dailyMaxinumNumber, decimal unitPrice)
        {
            byte bSection = 2;

            byte bPieceOne = EquipmentGeneral.GetCardSectionPieceList()[bSection][1];
            byte bPieceTwo = EquipmentGeneral.GetCardSectionPieceList()[bSection][2];

            string l_strEachAmount = EquipmentGeneral.IntToHexInSorting(eachTimeMaxinumNumber, 4);
            string l_strMaxAmount = EquipmentGeneral.IntToHexInSorting(dailyMaxinumNumber, 4);

            decimal fUPrice = unitPrice * 100;
            int uPrice = Convert.ToInt32(fUPrice);
            string l_strUnitPrice = EquipmentGeneral.IntToHexInSorting(uPrice, 4);

            string l_strWaterInfo = paymentPassword + l_strEachAmount + l_strMaxAmount + l_strUnitPrice + "FFFF";

            l_strWaterInfo = l_strWaterInfo + DataConvert.GetCRC16Code(l_strWaterInfo);

            if (l_strWaterInfo.Length != 32)
            {
                return false;
            }

            if (!ReadCardsPrepare(bSection))
            {
                return false;
            }
            byte[] arrData = YuChuanDevice.ChangeStrToHex(l_strWaterInfo);
            int iRes = YuChuanDevice.rf_write(bPieceOne, arrData);

            if (!ReadCardsPrepare(bSection))
            {
                return false;
            }
            iRes = YuChuanDevice.rf_write(bPieceTwo, arrData);

            return true;
        }

        public override bool InitConsumptionSectionPwd(string strPwd)
        {
            bool res = false;

            try
            {

                res = SetCardPassword(1, strPwd);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public override bool InitWaterSectionPwd(string strPwd)
        {
            bool res = false;

            try
            {
                res = SetCardPassword(2, strPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public override bool InitPhoneSectionPwd(string strPwd)
        {
            bool res = false;

            try
            {
                res = SetCardPassword(10, strPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public override bool InitMacSectionPwd(string strPwd)
        {
            bool res = false;

            try
            {
                res = SetCardPassword(0, strPwd);
                //for (int i = 0; i < 16; i++)
                //{
                //    res = SetCardPassword(i, strPwd);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
