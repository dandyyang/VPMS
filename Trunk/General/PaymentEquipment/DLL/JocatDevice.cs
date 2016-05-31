using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using PaymentEquipment.Entity;
using PaymentEquipment.Funtion;
using Common;

namespace PaymentEquipment.DLL
{
    public class JocatDevice
    {
        /// <summary>
        /// 读取DLL文件的相对路径
        /// </summary>
        private const string strDLLPath = @"DLL\JocatDeviceLab.dll";

        #region API
        /// <summary>
        /// 读多块
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kh">块号</param>
        /// <param name="ks">块数</param>
        /// <returns></returns>        
        //[DllImport(strDLLPath, EntryPoint = "newreadcard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [DllImport(strDLLPath, EntryPoint = "newreadcard")]
        public static extern string ReadCard(int port, string kh, int ks);

        /// <summary>
        /// 写多块
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kh">块号</param>
        /// <param name="ks">块数</param>
        /// <param name="data">数据（16个字节）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "newwritecard")]
        public static extern bool WriteCard(int port, string kh, int ks, string data);

        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="mm">密码</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downmmre")]
        public static extern bool SetReaderPassWord(int port, string mm);

        /// <summary>
        /// 设置卡片密码（加密0－4,5个扇区）
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="mm">密码</param>
        /// <returns></returns>       
        [DllImport(strDLLPath, EntryPoint = "allcardpassword")]
        public static extern bool SetCardPassword(int port, string mm);

        /// <summary>
        /// 设置卡片指定5个块的密码
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="qh">开始区号</param>
        /// <param name="mm">密码</param>
        /// <returns></returns>        
        [DllImport(strDLLPath, EntryPoint = "cardpassword1")]
        public static extern bool SetCardPassword(int port, String qh, String mm);

        /// <summary>
        /// 设置卡片密码（加密0－15，16个扇区）
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="mm">密码</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "allcardpassword1")]
        public static extern bool SetCardAllBlockPassword(int port, string mm);

        /// <summary>
        /// 写卡号
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kkh">卡块号</param>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "writekh")]
        public static extern bool WriteCardNo(int port, int kkh, int kh);

        /// <summary>
        /// 读取卡片金额
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kh">块号（4块是个人金额，8块是补贴金额）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "dqnewreadje")]
        public static extern string GetCardMoney(int port, int kh);

        /// <summary>
        /// 余额清零
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kh">块号（4块是个人金额，8块是补贴金额）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "dqmoney")]
        public static extern bool CleanCardMoney(int port, int kh);

        /// <summary>
        /// 写基本信息
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kkh">卡块号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <param name="mm">消费密码（3个字节）</param>
        /// <param name="zxrq">注销日期</param>
        /// <param name="gn">功能</param>
        /// <param name="zdye">最低余额</param>
        /// <param name="daymaxje">最大消费限额</param>
        /// <param name="xc1">一时段限次</param>
        /// <param name="xc2">二时段限次</param>
        /// <param name="xc3">三时段限次</param>
        /// <param name="xc4">四时段限次</param>
        /// <param name="lx"></param>
        /// <returns></returns>        
        [DllImport(strDLLPath, EntryPoint = "newcard")]
        public static extern bool WriteCardNormalInfo(int port, int kkh, int kh, String xm, String mm, String zxrq, String gn, decimal zdye, decimal daymaxje, int xc1, int xc2, int xc3, int xc4, String lx);

        /// <summary>
        /// 加款
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="kh">块号</param>
        /// <param name="je">金额</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "dqaddje")]
        public static extern bool AddCardMoney(int port, int kh, int je);

        /// <summary>
        /// 取得机械时间
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "gettime")]
        public static extern string GetMachineTime(int port, int btl, int jh);

        /// <summary>
        /// 设置机号
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="oldjh">原机号</param>
        /// <param name="newjh">新机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setjh")]
        public static extern bool SetMachineNo(int port, int btl, int oldjh, int newjh);

        /// <summary>
        /// 验证时间
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "settime")]
        public static extern bool SetMachineTime(int port, int btl, int jh);

        /// <summary>
        /// 挂失卡号
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="kh"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "gskh")]
        public static extern bool AddLossCard(int port, int btl, int jh, int kh);

        /// <summary>
        /// 解除挂失卡片
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "jgkh")]
        public static extern bool RemoveLossCard(int port, int btl, int jh, int kh);

        /// <summary>
        /// 设置管理员卡
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setglk")]
        public static extern bool SetAdminCard(int port, int btl, int jh, int kh);

        /// <summary>
        /// 取得记录数
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "getnum")]
        public static extern int GetRecordNum(int port, int btl, int jh);

        /// <summary>
        /// 取得机械记录总金额
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "getje")]
        public static extern int GetMacPaymentMoney(int port, int btl, int jh);

        /// <summary>
        /// 取得机械最大记录总数
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "getmaxnum")]
        public static extern int GetMachineMaxNum(int port, int btl, int jh);

        /// <summary>
        /// 设置消费时间间隔
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="sj"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downsjjg")]
        public static extern bool SetPaymentTimeSpan(int port, int btl, int jh, int sj);

        /// <summary>
        /// 下传单笔最大消费金额
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="je"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downmaxje")]
        public static extern bool SetPaymentMaxAmount(int port, int btl, int jh, int je);

        /// <summary>
        /// 下传消费机密码
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="jmm"></param>
        /// <param name="xmm"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downpassword")]
        public static extern bool SetMachinePassword(int port, int btl, int jh, String jmm, String xmm);

        /// <summary>
        /// 清除全部挂失卡
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "clearallhmd")]
        public static extern bool CleanLossCard(int port, int btl, int jh);

        /// <summary>
        /// 下传菜单
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="jh">机号</param>
        /// <param name="bh">菜单编号</param>
        /// <param name="je">金额</param>
        /// <param name="cm">菜名</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downcd")]
        public static extern bool SetMenu(int port, int jh, int bh, double je, String cm);

        /// <summary>
        /// 设置消费模式
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="zt">消费模式（00--固定消费，01--手动消费，02--菜单消费，03--PC下转）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setxfms")]
        public static extern bool SetPaymentMode(int port, int btl, int jh, String zt);

        /// <summary>
        /// 设置卡块类型折扣
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="zt">卡块类型</param>
        /// <param name="je">折扣（0－1000分比）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setzk")]
        public static extern bool SetBuckling(int port, int btl, int jh, string zt, int je);

        /// <summary>
        /// 下传二次消费金额
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="bz">类型（早餐A：0X41，B：0X42；中餐C：0X43，D：0X44；晚餐E：0X45，F：0X46；夜宵G：0X47，H：0X48）</param>
        /// <param name="je">金额</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downsdje")]
        public static extern bool SetTwiceAmount(int port, int btl, int jh, string bz, double je);

        /// <summary>
        /// 下传二次消费时间
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="bz">类型（不跨天00，跨天01。早餐 A：0X4100；中餐 B：0X4200；晚餐 C：0X4300；夜宵 D：0X4400）</param>
        /// <param name="sj">时间</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downsdsjre")]
        public static extern bool SetTwiceTime(int port, int btl, int jh, string bz, string sj);

        /// <summary>
        /// 下传数据
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="bh">编号（开始为0，数据一次返回20条，编号加20，不足20条有几条返回几条）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downlssjregrbtcd")]
        public static extern string GetPaymentInfo(int port, int btl, int jh, int bh);

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "cleardata")]
        public static extern bool CleanPaymentInfo(int port, int btl, int jh);

        /// <summary>
        /// 语言切换
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="yy"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "updateyy")]
        public static extern bool SetLanguage(int port, int btl, int jh, string yy);

        /// <summary>
        /// 下传打印标题
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="yy"></param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downlogin")]
        public static extern bool SetTitle(int port, int btl, int jh, string yy);

        /// <summary>
        /// 下传扇区
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="sq">开始盲区编号（0－12）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setsq")]
        public static extern bool SetStartBlock(int port, int btl, int jh, int sq);

        /// <summary>
        /// 下传波特率
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="lbtl"></param>
        /// <param name="jh">机号</param>
        /// <param name="xbtl">新波特率（9600 xbtl:=0；19200 xbtl:=1；38400 xbtl:=2；57600 xbtl:=3；115200 xbtl:=4；）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setbtl")]
        public static extern bool SetBaud(int port, int lbtl, int jh, int xbtl);

        /// <summary>
        /// 下传充值机号
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="zm">组名（1－99）</param>
        /// <param name="bh">编号</param>
        /// <param name="kh">卡号</param>
        /// <param name="je">金额</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downczj")]
        public static extern bool SetRechargeInfo(int port, int btl, int jh, int zm, int bh, int kh, int je);

        /// <summary>
        /// 下传补贴机器清零日（充值机器没有此命令）
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <param name="rq">清零日期</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "downqlr")]
        public static extern bool SetExpiryDate(int port, int btl, int jh, string rq);

        /// <summary>
        /// 清除机器所有下传充值人员记录（和已刷卡充值记录无关）
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="btl">波特率</param>
        /// <param name="jh">机号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "clearallczj")]
        public static extern bool CleanRechargeInfo(int port, int btl, int jh);

        /// <summary>
        /// 下传补贴机补贴金额
        /// </summary>
        /// <param name="iPort">串口号</param>
        /// <param name="iBaud">波特率</param>
        /// <param name="iMacID">机号</param>
        /// <param name="isUsedWhiteList">是否启用白名单，01启用，00不启用</param>
        /// <param name="fMoney">补贴金额</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "setbtgn")]
        public static extern bool SetSubzMoney(int iPort, int iBaud, int iMacID, string isUsedWhiteList, double fMoney);

        #endregion

        /// <summary>
        /// 取得用户卡编号
        /// </summary>
        /// <param name="p_intPort"></param>
        /// <param name="p_intSector"></param>
        /// <returns></returns>
        public static int ReadCardNo(int p_intPort, int p_intSector)
        {
            string l_strData = ReadCard(p_intPort, p_intSector.ToString(), 3);

            //取得块1数据
            string l_strTemp = l_strData.Substring(32, 32);
            var l_arrTemp = l_strTemp.ToCharArray();

            //取得卡号
            return Convert.ToInt32(l_arrTemp[5].ToString() + l_arrTemp[2].ToString() + l_arrTemp[3].ToString() + l_arrTemp[0].ToString() + l_arrTemp[1].ToString(), 16);
        }

        public static PaymentCard ReadCard(int p_intPort, int p_intSector, string strPwd)
        {
            try
            {
                var l_strReturn = ReadCard(p_intPort, p_intSector.ToString(), 3);
                if (l_strReturn == "")
                {
                    throw new Exception("读卡失败!");
                }

                if (l_strReturn.Substring(32, 32) == "00000000000000000000000000000000")
                {
                    throw new Exception("该卡为新卡。");
                }

                PaymentCard l_objCardInfo = new PaymentCard();

                //取得出厂卡号
                //JocatDevice.SetReaderPassWord(p_intPort, "FFFFFFFFFFFF");
                //l_objCardInfo.MACID = JocatDevice.ReadCard(p_intPort, "0", 1);
                l_objCardInfo.MACID = l_strReturn.Substring(0, 32);

                //JocatDevice.SetReaderPassWord(p_intPort, strPwd);

                //解读第一块信息
                EncodeBlock1(l_strReturn.Substring(32, 32), l_objCardInfo);

                //解读第二块信息
                EncodeBlock2(l_strReturn.Substring(64, 32), l_objCardInfo);

                //读取消费扇区信息
                byte bPosSection = 1;
                l_strReturn = ReadCard(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bPosSection][0].ToString(), 3);
                EncodeDivision1(l_strReturn, l_objCardInfo);

                //读取水费扇区信息
                byte bSubzSection = 2;
                l_strReturn = ReadCard(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSubzSection][0].ToString(), 3);
                EncodeDivision2(l_strReturn, l_objCardInfo);

                return l_objCardInfo;
            }
            catch (Exception Ex)
            {

                throw new Exception("读卡失败!"); ;
            }
        }

        /// <summary>
        /// 写入卡片基本信息
        /// </summary>
        /// <param name="p_intPort"></param>
        /// <param name="p_objCard"></param>
        private static bool WriteCard(int p_intPort, int p_intSector, PaymentCard p_objCard)
        {
            WriteCardNormalInfo(p_intPort, (p_intSector * 4 + 1), p_objCard.No, p_objCard.Name, p_objCard.PaymentPassword, p_objCard.ExpiryDate.Value.ToString("yyMMdd"), p_objCard.FunctionCode, (decimal)p_objCard.LimitRemainder, (decimal)p_objCard.DailyMaxPayment, p_objCard.Span1PaymentCount, p_objCard.Span2PaymentCount, p_objCard.Span3PaymentCount, p_objCard.Span4PaymentCount, EquipmentGeneral.StrToHex(p_objCard.Type));
            CleanCardMoney(p_intPort, (p_intSector * 4 + 1));
            CleanCardMoney(p_intPort, (p_intSector * 4 + 8));
            AddCardMoney(p_intPort, (p_intSector * 4 + 4), int.Parse(p_objCard.Money.ToString()) * 100);
            AddCardMoney(p_intPort, (p_intSector * 4 + 8), int.Parse(p_objCard.Subsidize.ToString()) * 100);
            return true;
        }

        /// <summary>
        /// 写卡片基本信息
        /// </summary>
        /// <param name="p_intPort"></param>
        /// <param name="p_objCard"></param>
        /// <returns></returns>
        public static bool WriteCardInfo(int p_intPort, PaymentCard p_objCard)
        {
            bool lUserValid = p_objCard.Valid;
            string l_strCarNo = CardNoToHex(p_objCard.No, lUserValid);
            string l_strDate = p_objCard.ExpiryDate.Value.ToString("yyMMdd");
            string l_strName = GetNameCode(p_objCard.Name);

            string l_strData = l_strCarNo + l_strDate + p_objCard.FunctionCode + l_strName;

            //string l_strMoneyLimit = EquipmentGeneral.IntToHex(int.Parse((p_objCard.LimitRemainder * 100).ToString()), 8);
            //string l_strMaxPayment = EquipmentGeneral.IntToHex(int.Parse((p_objCard.DailyMaxPayment * 100).ToString()), 8);
            string l_strMoneyLimit = EquipmentGeneral.IntToHex((int)(p_objCard.LimitRemainder * 100), 8);
            string l_strMaxPayment = EquipmentGeneral.IntToHex((int)(p_objCard.DailyMaxPayment * 100), 8);
            string l_strPaymentCount = EquipmentGeneral.IntToHex(p_objCard.Span1PaymentCount) + EquipmentGeneral.IntToHex(p_objCard.Span2PaymentCount) + EquipmentGeneral.IntToHex(p_objCard.Span3PaymentCount) + EquipmentGeneral.IntToHex(p_objCard.Span4PaymentCount);
            string l_strType = EquipmentGeneral.IntToHex(Convert.ToInt32(char.Parse(p_objCard.Type)));

            l_strData = l_strData + l_strMoneyLimit + p_objCard.PaymentPassword + l_strMaxPayment + l_strPaymentCount + l_strType;

            byte bSection = 0;
            WriteCard(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][1].ToString(), 2, l_strData);
            bSection = 1;
            CleanCardMoney(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0]);
            AddCardMoney(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0], (int)(p_objCard.Money * 100));

            bSection = 2;
            CleanCardMoney(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0]);
            AddCardMoney(p_intPort, EquipmentGeneral.GetCardSectionPieceList()[bSection][0], (int)(p_objCard.Subsidize * 100));

            return true;
        }

        /// <summary>
        /// 写入卡片基本信息
        /// </summary>
        /// <param name="iCom"></param>
        /// <param name="iSection"></param>
        /// <param name="payCard"></param>
        /// <returns></returns>
        public static bool WriteCardBasicInfo(int iCom, byte bSection, PaymentCard payCard)
        {
            bool res = false;
            try
            {
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
                bool bValid = payCard.Valid;
                string l_strCarNo = CardNoToHex(payCard.No, bValid);
                string l_strDate = payCard.ExpiryDate.Value.ToString("yyMMdd");
                string l_strName = GetNameCode(payCard.Name);

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

                string l_strData = l_strCarNo + l_strDate + strFunc + l_strName;

                string l_strMoneyLimit = EquipmentGeneral.IntToHex(int.Parse(((int)(payCard.LimitRemainder * 100)).ToString()), 8);
                string l_strMaxPayment = EquipmentGeneral.IntToHex(int.Parse(((int)(payCard.DailyMaxPayment * 100)).ToString()), 8);
                string l_strPaymentCount = EquipmentGeneral.IntToHex(payCard.Span1PaymentCount) + EquipmentGeneral.IntToHex(payCard.Span2PaymentCount) + EquipmentGeneral.IntToHex(payCard.Span3PaymentCount) + EquipmentGeneral.IntToHex(payCard.Span4PaymentCount);
                string l_strType = EquipmentGeneral.IntToHex(Convert.ToInt32(char.Parse(payCard.Type)));

                l_strData = l_strData + l_strMoneyLimit + payCard.PaymentPassword + l_strMaxPayment + l_strPaymentCount + l_strType;

                res = WriteCard(iCom, dicSection[bSection][1].ToString(), 2, l_strData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public static string CardNoToHex(int p_intCarNo, bool p_blnActive)
        {
            string l_strReturn = "";

            l_strReturn = EquipmentGeneral.IntToHexInSorting(p_intCarNo, 5);

            l_strReturn = l_strReturn.Insert(4, (p_blnActive) ? "8" : "0");

            return l_strReturn;
        }

        /// <summary>
        /// 取得名称的16进制编码
        /// </summary>
        /// <param name="p_strName"></param>
        /// <returns></returns>
        private static string GetNameCode(string p_strName)
        {
            string l_strReturn = EquipmentGeneral.StrToHex(p_strName);
            l_strReturn = l_strReturn + "0000000000000000";

            return l_strReturn.Substring(0, 16);
        }

        /// <summary>
        /// 解读第一块信息
        /// </summary>
        /// <param name="p_strCode">16进制信息</param>
        /// <param name="p_objCardInfo">卡</param>
        private static void EncodeBlock1(string p_strCode, PaymentCard p_objCardInfo)
        {
            //取得块1数据
            string l_strTemp = p_strCode.Substring(0, 6);
            var l_arrTemp = l_strTemp.ToCharArray();

            //取得卡号
            p_objCardInfo.No = Convert.ToInt32(l_arrTemp[5].ToString() + l_arrTemp[2].ToString() + l_arrTemp[3].ToString() + l_arrTemp[0].ToString() + l_arrTemp[1].ToString(), 16);

            //取得状态
            p_objCardInfo.Valid = l_arrTemp[4] == '8' ? true : false;

            //取得有效时间
            l_strTemp = p_strCode.Substring(6, 6);
            p_objCardInfo.ExpiryDate = DateTime.ParseExact("20" + l_strTemp, "yyyyMMdd", null);

            //取得系统功能
            //l_strTemp = Convert.ToString(Convert.ToInt32(p_strCode.Substring(12, 4), 16), 2);
            //p_objCardInfo.FunctionCode = ("0000000000000000").Substring(0, 16 - l_strTemp.Length) + l_strTemp;
            //p_objCardInfo.FunctionCode = p_strCode.Substring(12, 4);
            p_objCardInfo.FunctionCode = Convert.ToString(Convert.ToInt32(p_strCode.Substring(12, 4), 16), 2).PadLeft(16, '0');

            //取得持卡人姓名
            l_strTemp = p_strCode.Substring(16, 16);
            p_objCardInfo.Name = EquipmentGeneral.HexToStr(l_strTemp);
        }

        /// <summary>
        /// 解读第二块信息
        /// </summary>
        /// <param name="p_strCode">16进制信息</param>
        /// <param name="p_objCardInfo">卡</param>
        private static void EncodeBlock2(string p_strCode, PaymentCard p_objCardInfo)
        {
            //前8位为最低余额

            //后两们为小数位，所以要除100
            p_objCardInfo.LimitRemainder = (decimal)(Convert.ToInt32(p_strCode.Substring(0, 8), 16) / 100.00M);

            //8~14为消费密码
            p_objCardInfo.PaymentPassword = p_strCode.Substring(8, 6);

            //14~22为每天最大消费额            
            p_objCardInfo.DailyMaxPayment = (decimal)(Convert.ToInt32(p_strCode.Substring(14, 8), 16) / 100.00M);

            //不同时段的消费次数
            p_objCardInfo.Span1PaymentCount = Convert.ToInt32(p_strCode.Substring(22, 2), 16);
            p_objCardInfo.Span2PaymentCount = Convert.ToInt32(p_strCode.Substring(24, 2), 16);
            p_objCardInfo.Span3PaymentCount = Convert.ToInt32(p_strCode.Substring(26, 2), 16);
            p_objCardInfo.Span4PaymentCount = Convert.ToInt32(p_strCode.Substring(28, 2), 16);

            p_objCardInfo.Type = ((char)Convert.ToByte(p_strCode.Substring(30, 2), 16)).ToString();
        }

        /// <summary>
        /// 解读第1区信息
        /// </summary>
        /// <param name="p_strCode">16进制信息</param>
        /// <param name="p_objCardInfo">卡</param>
        private static void EncodeDivision1(string p_strCode, PaymentCard p_objCardInfo)
        {
            //前8位为最低余额

            //后两们为小数位，所以要除100
            p_objCardInfo.Money = EquipmentGeneral.HexMoneyToDouble(p_strCode.Substring(0, 8));
            p_objCardInfo.MoneyBackup = EquipmentGeneral.HexMoneyToDouble(p_strCode.Substring(32, 8));
            p_objCardInfo.MachineList = p_strCode.Substring(64, 32);
        }

        /// <summary>
        /// 解读第2区信息
        /// </summary>
        /// <param name="p_strCode">16进制信息</param>
        /// <param name="p_objCardInfo">卡</param>
        private static void EncodeDivision2(string p_strCode, PaymentCard p_objCardInfo)
        {
            //前8位为最低余额

            //后两们为小数位，所以要除100
            p_objCardInfo.Subsidize = EquipmentGeneral.HexMoneyToDouble(p_strCode.Substring(0, 8));
            p_objCardInfo.SubsidizeBackup = EquipmentGeneral.HexMoneyToDouble(p_strCode.Substring(32, 8));
            p_objCardInfo.MachineList = p_strCode.Substring(64, 32);
        }

        /// <summary>
        /// 获取消费金额
        /// </summary>
        /// <param name="iPort">COM口号</param>
        /// <param name="iSection">扇区号</param>
        /// <returns></returns>
        public static decimal GetPosVal(int iPort, byte bSection)
        {
            decimal dMoney = 0.00M;
            try
            {
                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
                string strReturn = ReadCard(iPort, dicSection[bSection][0].ToString(), 1);
                if (strReturn.Length != 32)
                {
                    return 0.00M;
                }
                dMoney = EquipmentGeneral.HexMoneyToDouble(strReturn.Substring(0, 8));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dMoney;
        }

        /// <summary>
        /// 获取亲情号码列表及显示信息
        /// </summary>
        /// <param name="iPort">COM口号</param>
        /// <param name="bSection">扇区号</param>
        /// <returns></returns>
        public static List<string> GetPhoneNumList(int iPort, byte bSection)
        {
            Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
            List<string> listPhone = new List<string>();
            try
            {
                string strReturn = ReadCard(iPort, dicSection[bSection][0].ToString(), 3);
                if (strReturn.Length != 96)
                {
                    return null;
                }
                strReturn = strReturn.ToUpper();
                string strSub = strReturn.Substring(0, 32);

                strSub = Convert.ToChar(Convert.ToInt32(strSub.Substring(0, 2), 16)).ToString() + strSub.Substring(2, 8);

                listPhone.Add(strSub);

                for (int i = 0; i < 4; i++)
                {
                    strSub = strReturn.Substring(32 + i * 16, 16).TrimStart('0').TrimEnd('F');
                    listPhone.Add(strSub);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listPhone;
        }

        /// <summary>
        /// 获取电话卡用户基本信息
        /// </summary>
        /// <param name="iPort">COM口号</param>
        /// <param name="bSection">扇区号</param>
        /// <returns></returns>
        public static PaymentCard GetUserBasicInfos(int iPort, byte bSection)
        {
            PaymentCard payCard = new PaymentCard();
            Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();

            try
            {
                string strReturn = ReadCard(iPort, dicSection[bSection][1].ToString(), 2);
                if (strReturn.Length != 64)
                {
                    if (strReturn.Length > 64)
                    {
                        strReturn = strReturn.Substring(0, 64);
                    }
                    else
                    {
                        return null;
                    }
                }

                //PaymentCard l_objCardInfo = new PaymentCard();

                //解读第一块信息
                EncodeBlock1(strReturn.Substring(0, 32), payCard);

                //解读第二块信息
                EncodeBlock2(strReturn.Substring(32, 32), payCard);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return payCard;
        }

        /// <summary>
        /// 写入电话用户信息及电话号码
        /// </summary>
        /// <param name="iPort">COM口号</param>
        /// <param name="bSection">扇区号</param>
        /// <param name="strCardInfo">电话卡用户信息</param>
        /// <param name="listPhoneNum">电话号码列表</param>
        /// <returns></returns>
        public static bool WritePhoneList(int iPort, byte bSection, string strCardInfo, List<string> listPhoneNum)
        {
            try
            {
                if (listPhoneNum.Count > 4)
                {
                    return false;
                }

                Dictionary<byte, List<byte>> dicSection = EquipmentGeneral.GetCardSectionPieceList();
                strCardInfo = strCardInfo.ToUpper();
                //string strSub = (Convert.ToString(Convert.ToInt32(char.Parse(strCardInfo.Substring(0, 1))), 16) + strCardInfo.Substring(1, strCardInfo.Length - 1)).PadRight(32, 'F');

                string strSub;
                if (strCardInfo.Length == 9)
                {
                    strSub = Convert.ToString(Convert.ToInt32(char.Parse(strCardInfo.Substring(0, 1))), 16) + strCardInfo.Substring(1, strCardInfo.Length - 1);
                }
                strSub = strCardInfo.PadRight(32, 'F');

                string strPhoneSub = "FFFFFFFFFFFFFFFF";
                List<string> listPhoneWait = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    listPhoneWait.Add(strPhoneSub);
                }
                for (int i = 0; i < listPhoneNum.Count; i++)
                {
                    listPhoneWait[i] = listPhoneNum[i].PadLeft(12, '0').PadRight(16, 'F');
                }
                strSub += listPhoneWait[0] + listPhoneWait[1] + listPhoneWait[2] + listPhoneWait[3];
                bool res = JocatDevice.WriteCard(iPort, dicSection[bSection][0].ToString(), 3, strSub);
                if (!res)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
