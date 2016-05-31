using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ReaderEquipment.Entity;
using Model.General;
using System.Net.Sockets;
using ReaderEquipment;

namespace PaymentEquipment.DLL
{
    /// <summary>
    /// 宇川相关设备API
    /// </summary>
    public class YuChuanDevice
    {
        #region 变量、字段

        /// <summary>
        /// 读取DLL文件的相对路径
        /// </summary>
        private const string strDLLPath = @"DLL\YCCARD.dll";

        /// <summary>
        /// 通讯设备标识符
        /// </summary>
        private static readonly int _iDevSign = 0;

        /// <summary>
        /// 设备句柄
        /// </summary>
        private static readonly IntPtr _ipDevSign = new IntPtr(0);

        /// <summary>
        /// 金额转换倍数，输出需除倍数，输入则乘以倍数方为正确数值。
        /// </summary>
        private static readonly decimal _dMultiple = 100.00M;

        /// <summary>
        /// 写入卡密码区的控制位模板
        /// </summary>
        public static readonly byte[] bCardContentTemplate = { 0xFF, 0x07, 0x80, 0x69, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        //public static readonly byte[] bCardContentTemplate = { 0xFF, 0x07, 0x80, 0x69 };

        #endregion

        #region 通用发卡函数

        #region 发卡API

        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="port">取值为0~3，代表COM1~COM4</param>
        /// <param name="baud">波特率</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_init")]
        private static extern int rf_init(int port, long baud);

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_exit")]
        private static extern int rf_exit(int iDevSign);

        /// <summary>
        /// 寻卡，能返回在工作区域内某张卡的序列号
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">寻卡模式，0为默认的中断模式，1为全模式</param>
        /// <param name="iCardNo">返回的卡序列号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_card")]
        private static extern int rf_card_Private(int iDevSign, byte bMode, ref ulong iCardNo);
        /// <summary>
        /// 寻卡，能返回在工作区域内某张卡的序列号
        /// </summary>
        /// <param name="bMode">寻卡模式，0为默认的中断模式，1为全模式</param>
        /// <param name="iCardNo">返回的卡序列号</param>
        /// <returns></returns>
        public static int rf_card(byte bMode, ref ulong iCardNo)
        {
            return rf_card_Private(_iDevSign, bMode, ref iCardNo);
        }

        /// <summary>
        /// 初始化系统的工作模式及卡与设备间的通讯速率
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">射频模块工作模式，M1卡的缺省值为0xC6,，有的模块需设为0xD6</param>
        /// <param name="bBand">卡与设备间的通讯速率，M1卡的缺省值为0x0E</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_config")]
        private static extern int rf_config(int iDevSign, byte bMode, byte bBand);

        /// <summary>
        /// 寻卡请求
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">寻卡模式</param>
        /// <param name="iTagType">卡类型，0x0004为M1卡，0x0010为ML卡</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_request")]
        private static extern int rf_request_private(int iDevSign, byte bMode, ref uint iTagType);
        /// <summary>
        /// 寻卡请求
        /// </summary>
        /// <param name="bMode">寻卡模式</param>
        /// <param name="eTagType">卡类型</param>
        /// <returns></returns>
        public static int rf_request(byte bMode, ref YC_Enum_TagType eTagType)
        {
            uint iTagType = 0;

            int iRes = rf_request_private(_iDevSign, bMode, ref iTagType);
            if (iTagType == (uint)YC_Enum_TagType.M1)
            {
                eTagType = YC_Enum_TagType.M1;
            }
            else if (iTagType == (uint)YC_Enum_TagType.ML)
            {
                eTagType = YC_Enum_TagType.ML;
            }

            return iRes;
        }

        /// <summary>
        /// 返回卡的序列号，防止卡冲突。（request指令之后应立即调用anticoll，除非卡的序列号已知）
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bLength">预选卡所用的位数，标准值为0（不考虑系列号）</param>
        /// <param name="iCardSn">返回的卡片物理ID</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_anticoll")]
        private static extern int rf_anticoll_Private(int iDevSign, byte bLength, ref ulong iCardSn);
        /// <summary>
        /// 返回卡的序列号，防止卡冲突。（request指令之后应立即调用anticoll，除非卡的序列号已知）
        /// </summary>
        /// <param name="bLength">预选卡所用的位数，标准值为0（不考虑系列号）</param>
        /// <param name="iCardSn">返回的卡片物理ID</param>
        /// <returns></returns>
        public static int rf_anticoll(byte bLength, ref ulong iCardSn)
        {
            return rf_anticoll_Private(_iDevSign, bLength, ref iCardSn);
        }

        /// <summary>
        /// 从多个卡中选取一个给定序列号的卡
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="iCardSn">卡序列号</param>
        /// <param name="bCardSize">指向返回的卡容量的数据</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_select")]
        private static extern int rf_select_Private(int iDevSign, ulong iCardSn, ref byte[] arrByteCardSize);
        /// <summary>
        /// 从多个卡中选取一个给定序列号的卡
        /// </summary>
        /// <param name="iCardSn">卡序列号</param>
        /// <param name="arrByteCardSize">指向返回的卡容量的数据</param>
        /// <returns></returns>
        public static int rf_select(ulong iCardSn, ref byte[] arrByteCardSize)
        {
            return rf_select_Private(_iDevSign, iCardSn, ref arrByteCardSize);
        }

        /// <summary>
        /// 将密码装入读写模块RAM中
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">装入密码模式，同密码验证模式，0为验证A密码，1为验证B密码</param>
        /// <param name="bSectionNum">扇区号</param>
        /// <param name="arrByteCardPwd">写入读写器中的卡密码</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_load_key")]
        private static extern int rf_load_key_Private(int iDevSign, byte bMode, byte bSectionNum, byte[] arrByteCardPwd);
        /// <summary>
        /// 将密码装入读写模块RAM中
        /// </summary>
        /// <param name="bMode">装入密码模式，同密码验证模式，0为验证A密码，1为验证B密码</param>
        /// <param name="bSectionNum">扇区号</param>
        /// <param name="arrByteCardPwd">写入读写器中的卡密码</param>
        /// <returns></returns>
        public static int rf_load_key(byte bMode, byte bSectionNum, byte[] arrByteCardPwd)
        {
            return rf_load_key_Private(_iDevSign, bMode, bSectionNum, arrByteCardPwd);
        }

        /// <summary>
        /// 向读写器中装入十六进制密码
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">寻卡模式</param>
        /// <param name="bSectionNum">扇区号</param>
        /// <param name="strCardPwd">写入读写器中的卡密码</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_load_key_hex")]
        private static extern int rf_load_key_hex_Private(int iDevSign, byte bMode, byte bSectionNum, string strCardPwd);
        /// <summary>
        /// 向读写器中装入十六进制密码
        /// </summary>
        /// <param name="bMode">寻卡模式</param>
        /// <param name="bSectionNum">扇区号</param>
        /// <param name="strCardPwd">写入读写器中的卡密码</param>
        /// <returns></returns>
        public static int rf_load_key_hex(byte bMode, byte bSectionNum, string strCardPwd)
        {
            return rf_load_key_hex_Private(_iDevSign, bMode, bSectionNum, strCardPwd);
        }

        /// <summary>
        /// 验证某一扇区密码
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">密码验证模式</param>
        /// <param name="bSectionNum">要验证密码的扇区号（0~15）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_authentication")]
        private static extern int rf_authentication_Private(int iDevSign, byte bMode, byte bSectionNum);
        /// <summary>
        /// 验证某一扇区密码
        /// </summary>
        /// <param name="bMode">密码验证模式</param>
        /// <param name="bSectionNum">要验证密码的扇区号（0~15）</param>
        /// <returns></returns>
        public static int rf_authentication(byte bMode, byte bSectionNum)
        {
            return rf_authentication_Private(_iDevSign, bMode, bSectionNum);
        }

        /// <summary>
        /// 中止卡操作
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_halt")]
        private static extern int rf_halt_Private(int iDevSign);
        /// <summary>
        /// 中止卡操作
        /// </summary>
        /// <returns></returns>
        public static int rf_halt()
        {
            return rf_halt_Private(_iDevSign);
        }

        /// <summary>
        /// 读取卡中数据
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bAddr">卡块地址</param>
        /// <param name="arrData">读出数据</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_read")]
        private static extern int rf_read_Private(int iDevSign, byte bAddr, byte[] arrData);
        /// <summary>
        /// 读取卡中数据
        /// </summary>
        /// <param name="bAddr">卡块地址</param>
        /// <param name="arrData">读出数据</param>
        /// <returns></returns>
        public static int rf_read(byte bAddr, byte[] arrData)
        {
            return rf_read_Private(_iDevSign, bAddr, arrData);
        }

        /// <summary>
        /// 向卡中写入数据
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bAddr">卡块地址</param>
        /// <param name="arrData">要写入的数据</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_write")]
        private static extern int rf_write_Private(int iDevSign, byte bAddr, byte[] arrData);
        //private static extern int rf_write_Private(int iDevSign, byte bAddr, string strData);
        /// <summary>
        /// 向卡中写入数据
        /// </summary>
        /// <param name="bAddr">卡块地址</param>
        /// <param name="arrData">要写入的数据</param>
        /// <returns></returns>
        public static int rf_write(byte bAddr, byte[] arrData)
        //public static int rf_write(byte bAddr, string strData)
        {
            return rf_write_Private(_iDevSign, bAddr, arrData);
        }

        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="iBeepTime">蜂鸣时间的长短，单位是0.1毫秒</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_beep")]
        private static extern int rf_beep_Private(int iDevSign, ushort iBeepTime);
        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="iBeepTime">蜂鸣时间的长短，单位是0.1毫秒</param>
        /// <returns></returns>
        public static int rf_beep(ushort iBeepTime)
        {
            return rf_beep_Private(_iDevSign, iBeepTime);
        }

        /// <summary>
        /// 使读写器的数码管显示数字
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="iLength">显示位数，其值为1-8，表示设置他总共使用几位数码管</param>
        /// <param name="bDigiData">显示数据（BCD码，最高位控制小数点）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_disp8")]
        private static extern int rf_disp8_Private(int iDevSign, int iLength, ref byte bDigiData);
        /// <summary>
        /// 使读写器的数码管显示数字
        /// </summary>
        /// <param name="iLength">显示位数，其值为1-8，表示设置他总共使用几位数码管</param>
        /// <param name="bDigiData">显示数据（BCD码，最高位控制小数点）</param>
        /// <returns></returns>
        public static int rf_disp8(int iLength, ref byte bDigiData)
        {
            return rf_disp8_Private(_iDevSign, iLength, ref bDigiData);
        }

        /// <summary>
        /// 设置读写器显示时间还是显示日期（读写器控制，仅使用新版读卡器）
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="bMode">显示内容的类型，0表示日期，1表示显示时间</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_disp_m")]
        private static extern int rf_disp_m_Private(int iDevSign, byte bMode);
        /// <summary>
        /// 设置读写器显示时间还是显示日期（读写器控制，仅使用新版读卡器）
        /// </summary>
        /// <param name="bMode">显示内容的类型，0表示日期，1表示显示时间</param>
        /// <returns></returns>
        public static int rf_disp_m(byte bMode)
        {
            return rf_disp_m_Private(_iDevSign, bMode);
        }

        /// <summary>
        /// 设置读写器时间
        /// </summary>
        /// <param name="iDevSign">通讯设备标识符</param>
        /// <param name="arrByteTime">时间数组（7个字节，年、月、日、时、分、秒、周），与文档中不一样</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "rf_settime")]
        private static extern int rf_settime_Private(int iDevSign, byte[] arrByteTime);
        /// <summary>
        /// 设置读写器时间
        /// </summary>
        /// <param name="dtMac">要设置的时间值</param>
        /// <returns></returns>
        public static int rf_Settime(DateTime dtMac)
        {
            List<byte> listTimeVal = new List<byte>();
            listTimeVal.Add((byte)(dtMac.Year - 2000));
            listTimeVal.Add((byte)dtMac.Month);
            listTimeVal.Add((byte)dtMac.Day);
            listTimeVal.Add((byte)dtMac.Hour);
            listTimeVal.Add((byte)dtMac.Minute);
            listTimeVal.Add((byte)dtMac.Second);
            listTimeVal.Add((byte)dtMac.DayOfWeek);
            return rf_settime_Private(_iDevSign, listTimeVal.ToArray());
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="iCommPort">串口标志（取值如下：0-Com1, 1-Com2最大到COM20）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "OpenComm")]
        private static extern IntPtr OpenComm(int iCommPort);

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "CloseComm")]
        private static extern int CloseComm(IntPtr iDevSign);

        /// <summary>
        /// 测试发卡器
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Check_Reader")]
        private static extern int Check_Reader_Private(IntPtr iDevSign);
        /// <summary>
        /// 测试发卡器
        /// </summary>
        /// <returns></returns>
        public static int Check_Reader()
        {
            return Check_Reader_Private(_ipDevSign);
        }

        /// <summary>
        /// 读卡片物理ID
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="bTagType">卡标签类型：04为MF1卡</param>
        /// <param name="lSnID">卡片的物理ID</param>
        /// <returns>成功则返回0</returns>
        [DllImport(strDLLPath, EntryPoint = "ReadCard_ID")]
        private static extern int ReadCard_ID_Private(IntPtr iDevSign, ref byte bTagType, ref ulong iSnID);
        /// <summary>
        /// 读卡片物理ID
        /// </summary>
        /// <param name="eTagType">卡标签类型</param>
        /// <param name="iSnID">卡片物理ID</param>
        /// <returns></returns>
        public static int ReaCard_ID(ref YC_Enum_TagType eTagType, ref ulong iSnID)
        {
            byte bTagType = 0x00;
            int iRes = ReadCard_ID_Private(_ipDevSign, ref bTagType, ref iSnID);
            if (bTagType == (byte)YC_Enum_TagType.M1)
            {
                eTagType = YC_Enum_TagType.M1;
            }
            else if (bTagType == (byte)YC_Enum_TagType.ML)
            {
                eTagType = YC_Enum_TagType.ML;
            }
            return iRes;
        }

        /// <summary>
        /// 更新系统卡（存在其他系统的情况下，添加其他系统的信息）
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSysType">系统类型，0为系统或工具卡，1为门禁，2为消费，4为节水，如果现实为两种以上的系统卡，显示12,123,23,13</param>
        /// <param name="iCardType">卡片的类型，0为用户卡、2为系统卡、1为操作员卡、3为初始化卡、4为白卡、5为节水设置卡、6为采集卡、7为加密卡、8为查询卡、9为机号设置卡、10为时间设置卡</param>
        /// <param name="iCardSerno">卡片的物理ID</param>
        /// <param name="iOPT_Num">营业员卡编号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Query_Card_Type")]
        private static extern int Query_Card_Type_Private(IntPtr iDevSign, int iSysType, int iCardType, ulong iCardSerno, uint iOPT_Num, int iWaitTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 更新系统卡（存在其他系统的情况下，添加其他系统的信息）
        /// </summary>
        /// <param name="eSysType">系统类型</param>
        /// <param name="eCardType">卡片的类型</param>
        /// <param name="iCardSerno">卡片的物理</param>
        /// <param name="iOPT_Num">营业员卡编号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Query_Card_Type(YC_Enum_SysType eSysType, YC_Enum_CardType eCardType, ulong iCardSerno, uint iOPT_Num, int iWaitTime, byte[] arrByteUserCodeNew)
        {
            return Query_Card_Type_Private(_ipDevSign, (int)eSysType, (int)eSysType, iCardSerno, iOPT_Num, iWaitTime, arrByteUserCodeNew);
        }

        /// <summary>
        /// 初始化系统卡（无其他系统的情况下）
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="strUserPwd">用户密码（8位ASCII字符，此密码用户必须保存数据库中）</param>
        /// <param name="iSysType">系统类别，1为门禁，2为消费</param>
        /// <param name="iUserSector">用户卡使用扇区</param>
        /// <param name="strCommPwd">返回此系统的通讯密码</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_SysCard12_NewCreat")]
        private static extern int Init_SysCard12_NewCreat_Private(IntPtr iDevSign, string strUserPwd, int iSysType, int iUserSector, string strCommPwd);
        /// <summary>
        /// 初始化系统卡（无其他系统的情况下）
        /// </summary>
        /// <param name="strUserPwd">用户密码（8位ASCII字符，此密码用户必须保存数据库中）</param>
        /// <param name="eSysType">系统类别</param>
        /// <param name="iUserSector">用户卡使用扇区</param>
        /// <param name="strCommPwd">返回此系统的通讯密码</param>
        /// <returns></returns>
        public static int Init_SysCard12_NewCreat(string strUserPwd, YC_Enum_SysType eSysType, int iUserSector, string strCommPwd)
        {
            return Init_SysCard12_NewCreat_Private(_ipDevSign, strUserPwd, (int)eSysType, iUserSector, strCommPwd);
        }

        /// <summary>
        /// 更新系统卡（存在其他系统的情况下，添加其他系统的信息）
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSysType">系统类别，1为门禁、2为消费</param>
        /// <param name="iUserSector">用户卡使用扇区</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_SysCard12_NewReWrite")]
        private static extern int Init_SysCard12_NewReWrite_Private(IntPtr iDevSign, int iSysType, int iUserSector);
        /// <summary>
        /// 更新系统卡（存在其他系统的情况下，添加其他系统的信息）
        /// </summary>
        /// <param name="eSysType">系统类别</param>
        /// <param name="iUserSector">用户卡使用扇区</param>
        /// <returns></returns>
        public static int Init_SysCard12_NewReWrite(YC_Enum_SysType eSysType, int iUserSector)
        {
            return Init_SysCard12_NewReWrite_Private(_ipDevSign, (int)eSysType, iUserSector);
        }

        /// <summary>
        /// 修改用户有效期
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSysType">系统类型，1为门禁、2为消费、3为节水</param>
        /// <param name="iUserTerm">卡片有效期（十进制，例如2009年1月1号 20090101）</param>
        /// <param name="iCardSerno">卡片物理ID</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Write_UserCard_Term")]
        private static extern int WRT_UserCard_Team_Private(IntPtr iDevSign, int iSysType, ulong iUserTerm, uint iCardSerno, int iWaitTime);
        /// <summary>
        /// 修改用户有效期
        /// </summary>
        /// <param name="eSysType">系统类型</param>
        /// <param name="dUserTerm">卡片有效期</param>
        /// <param name="iCardSerno">卡片物理ID</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <returns></returns>
        public static int WRT_UserCard_Team(YC_Enum_SysType eSysType, DateTime dUserTerm, uint iCardSerno, int iWaitTime)
        {
            ulong iUserTerm = ulong.Parse(dUserTerm.ToString("yyyyMMdd"));
            return WRT_UserCard_Team_Private(_ipDevSign, (int)eSysType, iUserTerm, iCardSerno, iWaitTime);
        }

        /// <summary>
        /// 恢复用户卡密码（默认为0000）
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSysType">系统类型，1为门禁、2为消费、3为节水</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Make_UserPassword_NEW")]
        private static extern int Make_UserPassword_NEW_Private(IntPtr iDevSign, int iSysType);
        /// <summary>
        /// 恢复用户卡密码（默认为0000）
        /// </summary>
        /// <param name="eSysType">系统类型</param>
        /// <returns></returns>
        public static int Make_UserPassword_NEW(YC_Enum_SysType eSysType)
        {
            return Make_UserPassword_NEW_Private(_ipDevSign, (int)eSysType);
        }

        /// <summary>
        /// 修改用户卡类
        /// </summary>
        /// <param name="iDevSign">设备标识</param>
        /// <param name="iOldUserType">原用户卡卡类（1-8类）</param>
        /// <param name="iUserType">待修改卡类（1-8类）</param>
        /// <param name="iSysType">系统类型，1为门禁、2为消费、3为节水</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Change_Pos_UserType12_NEW")]
        private static extern int Change_Pos_UserType12_NEW_Private(IntPtr iDevSign, ref int iOldUserType, int iUserType, int iSysType);
        /// <summary>
        /// 修改用户卡类
        /// </summary>
        /// <param name="eOldUserType">原用户卡卡类</param>
        /// <param name="eUserType">待修改卡类</param>
        /// <param name="eSysType">系统类型</param>
        /// <returns></returns>
        public static int Change_Pos_UserType12_NEW(YC_Enum_UserType eOldUserType, YC_Enum_UserType eUserType, YC_Enum_SysType eSysType)
        {
            int iOldUserType = (int)eOldUserType;
            return Change_Pos_UserType12_NEW_Private(_ipDevSign, ref iOldUserType, (int)eUserType, (int)eSysType);
        }

        #endregion

        #region 读写卡块相关

        /// <summary>
        /// 读写卡前准备
        /// </summary>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo ReadWritePrepare()
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();

            ulong iCard_ulong = 0;

            //寻卡       
            int iResCard = rf_card(0, ref iCard_ulong);
            if (iResCard != 0)
            {
                rvInfo.isError = true;
                rvInfo.ValueObject = iCard_ulong;
                rvInfo.messageText = "寻卡失败";
                return rvInfo;
            }

            //寻卡请求
            YC_Enum_TagType eRagType = YC_Enum_TagType.M1;
            int iResReq = rf_request(0, ref eRagType);
            if (iResReq != 0)
            {
                rvInfo.isError = true;
                rvInfo.ValueObject = iCard_ulong;
                rvInfo.messageText = "寻卡请求失败";
                return rvInfo;
            }

            //防止卡冲突
            iCard_ulong = 0;
            int iResAnt = rf_anticoll(0, ref iCard_ulong);
            if (iResAnt != 0)
            {
                rvInfo.isError = true;
                rvInfo.ValueObject = iCard_ulong;
                rvInfo.messageText = "防止卡冲突失败";
                return rvInfo;
            }


            //从多个卡中选取一个给定序列号的卡
            byte[] arrByteCardSize = new byte[0];
            int iResSel = rf_select(iCard_ulong, ref arrByteCardSize);
            if (iResSel != 0)
            {
                rvInfo.isError = true;
                rvInfo.ValueObject = iCard_ulong;
                rvInfo.messageText = "选取给定序列号卡失败";
                return rvInfo;
            }

            rvInfo.ValueObject = iCard_ulong;
            return rvInfo;
        }

        /// <summary>
        /// 将十六进制字符串转换成字节数组(规则为相邻两个字符组成一个字节，不足偶数位则在最后补F)
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static byte[] ChangeStrToHex(string strContent)
        {
            List<byte> listByte = new List<byte>();

            if (strContent.Length % 2 != 0)
            {
                strContent += "F";
            }
            for (int i = 0; i < strContent.Length; i += 2)
            {
                string strSub = strContent.Substring(i, 2);
                int iItem = Convert.ToInt32(strSub, 16);
                listByte.Add((byte)iItem);
            }

            return listByte.ToArray();
        }

        /// <summary>
        /// 将字节数组转换成字符串
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        public static string ChangeHexToString(byte[] arrData)
        {
            string strData = string.Empty;

            try
            {
                foreach (byte bItem in arrData)
                {
                    string strItem = Convert.ToString(bItem, 16);
                    strItem = strItem.PadLeft(2, '0');
                    strData += strItem;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return strData;
        }

        public static Model.General.ReturnValueInfo PwdVerify(byte[] arrPwd, YC_Enum_PwdVerifyMode pvMode, byte bSection)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();

            try
            {
                int iResLoad = rf_load_key((byte)pvMode, bSection, arrPwd);
                if (iResLoad != 0)
                {
                    rvInfo.isError = true;
                    rvInfo.messageText = "载入密码时发生错误。";
                    return rvInfo;
                }

                int iResAut = rf_authentication((byte)pvMode, bSection);
                if (iResAut != 0)
                {
                    rvInfo.isError = true;
                    rvInfo.messageText = "验证密码时发生错误。";
                    return rvInfo;
                }
            }
            catch (Exception ex)
            {
                rvInfo.isError = true;
                rvInfo.messageText = ex.Message;
            }

            return rvInfo;
        }

        #endregion

        #endregion

        #region  消费发卡专用函数

        /// <summary>
        /// 初始化消费用户卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSerNo">用户卡流水号，不可重复（1-100000）</param>
        /// <param name="strCardNo">用户卡号（五位ASCII字符）</param>
        /// <param name="eUserType">用户卡类型（1-8类）</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回用户卡固有ID</param>
        /// <param name="iUseTerm">卡使用期限</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_Pos_UserCard12")]
        private static extern int Init_Pos_UserCard12_Private(IntPtr iDevSign, int iSerNo, string strCardNo, int iUserType, int iWaitTime, ref ulong iCardSerNo, int iUseTerm, byte[] arrByteUserCodeNew);
        /// <summary>
        ///  初始化消费用户卡
        /// </summary>
        /// <param name="iSerNo">用户卡流水号，不可重复（1-100000）</param>
        /// <param name="strCardNo">用户卡号（五位ASCII字符）</param>
        /// <param name="eUserType">用户卡类型</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回用户卡固有ID</param>
        /// <param name="iUseTerm">卡使用期限</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Init_Pos_UserCard12(int iSerNo, string strCardNo, YC_Enum_UserType eUserType, int iWaitTime, ref ulong iCardSerNo, int iUseTerm, byte[] arrByteUserCodeNew)
        {
            int iRes = 0;
            try
            {
                iRes = Init_Pos_UserCard12_Private(_ipDevSign, iSerNo, strCardNo, (int)eUserType, iWaitTime, ref iCardSerNo, iUseTerm, arrByteUserCodeNew);
            }
            catch (Exception)
            {

            }
            return iRes;
        }

        /// <summary>
        /// 消费机用户卡块查询
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iCardType">卡片的类型，0为用户卡、2为系统卡、1为操作员卡、3为初始化卡、4为白卡、5为节水设置卡、6为采集卡、7为加密卡、8为查询卡、9为机号设置卡、10为时间设置卡</param>
        /// <param name="iOptNum">营业员编号</param>
        /// <param name="iSerNo">用户卡流水编号</param>
        /// <param name="strCardNo">用户卡卡编号</param>
        /// <param name="iUserType">用户类型</param>
        /// <param name="iCardSerNo">用户固有ID号</param>
        /// <param name="iChkSuml">用户卡使用块的校验状态，0为正确、1为错误</param>
        /// <param name="iValue1">用户卡使用块的金额，为实际金额*100</param>
        /// <param name="iLastPay1">用户卡使用块的余额，为实际余额*100</param>
        /// <param name="iCount1">用户卡使用块的消费次数</param>
        /// <param name="iConsumeAdd1">用户卡使用块的消费累加额，为实际余额*100</param>
        /// <param name="iValue2">用户卡备份块的金额，为实际金额*100</param>
        /// <param name="iLastPay2">用户卡备份块的余额，为实际余额*100</param>
        /// <param name="iCount2">用户卡备份块的消费次数</param>
        /// <param name="iConsumeAdd2">用户卡备用块的消费累加额，为实际余额*100</param>
        /// <param name="iUseTerm">用户卡的使用期限</param>
        /// <param name="iAddCount">充值次数增加1</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Query_Pos_UserCard12")]
        private static extern int Query_Pos_UserCard12_Private(IntPtr iDevSign, int iCardType, int iOptNum, ref int iSerNo, ref string strCardNo, ref  int iUserType, ref ulong iCardSerNo, ref int iChkSuml, ref int iValue1, ref int iLastPay1, ref int iCount1, ref int iConsumeAdd1, ref int iValue2, ref int iLastPay2, ref int iCount2, ref int iConsumeAdd2, ref int iUseTerm, ref int iAddCount, int iWaitTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 消费机用户卡块查询
        /// </summary>
        /// <param name="eCardType">卡片的类型</param>
        /// <param name="iOptNum">营业员编号</param>
        /// <param name="iSerNo">用户卡流水编号</param>
        /// <param name="strCardNo">用户卡卡编号</param>
        /// <param name="eUserType">用户类型</param>
        /// <param name="iCardSerNo">用户固有ID号</param>
        /// <param name="lChkSuml">用户卡使用块的校验状态，0为正确、1为错误</param>
        /// <param name="dValue1">用户卡使用块的金额</param>
        /// <param name="dLastPay1">用户卡使用块的余额</param>
        /// <param name="iCount1">用户卡使用块的消费次数</param>
        /// <param name="dConsumeAdd1">用户卡使用块的消费累加额</param>
        /// <param name="dValue2">用户卡备份块的金额</param>
        /// <param name="dLastPay2">用户卡备份块的余额</param>
        /// <param name="iCount2">用户卡备份块的消费次数</param>
        /// <param name="dConsumeAdd2">用户卡备用块的消费累加额</param>
        /// <param name="iUseTerm">用户卡的使用期限</param>
        /// <param name="iAddCount">充值次数增加</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Query_Pos_UserCard12(YC_Enum_CardType eCardType, int iOptNum, ref PosCardRecord posCardRecord, int iWaitTime, byte[] arrByteUserCodeNew)
        {
            #region 预初始化参数

            int iSerNo = 0;
            string strCardNo = string.Empty;
            int iRes = 0;
            int iUserType = 0;
            ulong iCardSerNo = 0;
            int iChkSuml = 0;
            int iValue1 = 0;
            int iLastPay1 = 0;
            int iCount1 = 0;
            int iConsumeAdd1 = 0;
            int iValue2 = 0;
            int iLastPay2 = 0;
            int iCount2 = 0;
            int iConsumeAdd2 = 0;
            int iUseTerm = 0;
            int iAddCount = 0;

            #endregion

            try
            {
                iRes = Query_Pos_UserCard12_Private(_ipDevSign, (int)eCardType, iOptNum, ref iSerNo, ref strCardNo, ref iUserType, ref iCardSerNo, ref iChkSuml, ref iValue1, ref iLastPay1, ref iCount1, ref iConsumeAdd1, ref iValue2, ref iLastPay2, ref iCount2, ref iConsumeAdd2, ref iUseTerm, ref iAddCount, iWaitTime, arrByteUserCodeNew);
            }
            catch (Exception)
            {
                posCardRecord.enumCardType = eCardType;
                posCardRecord.iOptNum = iOptNum;
                posCardRecord.iSerNo = iSerNo;
                posCardRecord.strCardNo = strCardNo;
                posCardRecord.bChkSuml = iChkSuml == 0 ? true : false;

                #region 转换用户卡类型

                switch (iUserType)
                {
                    case ((int)YC_Enum_UserType.Type_1):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_1;
                        break;
                    case ((int)YC_Enum_UserType.Type_2):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_2;
                        break;
                    case ((int)YC_Enum_UserType.Type_3):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_3;
                        break;
                    case ((int)YC_Enum_UserType.Type_4):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_4;
                        break;
                    case ((int)YC_Enum_UserType.Type_5):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_5;
                        break;
                    case ((int)YC_Enum_UserType.Type_6):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_6;
                        break;
                    case ((int)YC_Enum_UserType.Type_7):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_7;
                        break;
                    case ((int)YC_Enum_UserType.Type_8):
                        posCardRecord.enumUserType = YC_Enum_UserType.Type_8;
                        break;
                    default:
                        break;
                }

                #endregion

                posCardRecord.iCusId = iCardSerNo;

                posCardRecord.dValue1 = (decimal)iValue1 / _dMultiple;
                posCardRecord.dLastPay1 = (decimal)iLastPay1 / _dMultiple;
                posCardRecord.dConsAdd1 = (decimal)iConsumeAdd1 / _dMultiple;
                posCardRecord.iCount1 = iCount1;
                posCardRecord.dValue2 = (decimal)iValue2 / _dMultiple;
                posCardRecord.dLastPay2 = (decimal)iLastPay2 / _dMultiple;
                posCardRecord.dConsAdd2 = (decimal)iConsumeAdd2 / _dMultiple;
                posCardRecord.iCount2 = iCount2;

                posCardRecord.iUseTerm = iUseTerm;
                posCardRecord.iAddCount = iAddCount;
                posCardRecord.iWaitTime = iWaitTime;

            }


            return iRes;
        }

        /// <summary>
        /// 用户卡写值（充值次数增加1）
        /// </summary>
        /// <param name="iDevSign">设备句柄 </param>
        /// <param name="iValue">需要写入的金额，为实际金额*100</param>
        /// <param name="iCardSerNo">需要写值的用户卡固有ID号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "WRT_Pos_UserCard_AddCount12")]
        private static extern int WRT_Pos_UserCard_AddCount12_Private(IntPtr iDevSign, int iValue, uint iCardSerNo, int iWaitTime);
        /// <summary>
        /// 用户卡写值（充值次数增加1）
        /// </summary>
        /// <param name="dValue">需要写入的金额</param>
        /// <param name="iCardSerNo">需要写值的用户卡固有ID号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <returns></returns>
        public static int WRT_Pos_UserCard_AddCount12(decimal dValue, uint iCardSerNo, int iWaitTime)
        {
            int iValue = (int)(dValue * _dMultiple);
            return WRT_Pos_UserCard_AddCount12_Private(_ipDevSign, iValue, iCardSerNo, iWaitTime);
        }

        /// <summary>
        /// 回收消费机用户卡
        /// </summary>
        /// <param name="iDEvSign">设备句柄</param>
        /// <param name="iCardSerNo">要回收的用户卡固有ID号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "RST_Pos_UserCard12")]
        private static extern int RST_Pos_UserCard12_Private(IntPtr iDEvSign, uint iCardSerNo, int iWaitTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 回收消费机用户卡
        /// </summary>
        /// <param name="iCardSerNo">要回收的用户卡固有ID号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int RST_Pos_UserCard12(uint iCardSerNo, int iWaitTime, byte[] arrByteUserCodeNew)
        {
            return RST_Pos_UserCard12_Private(_ipDevSign, iCardSerNo, iWaitTime, arrByteUserCodeNew);
        }

        /// <summary>
        /// 初始化消费机操作员卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iOPT_Num">操作员编号，（1-255）</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回操作员卡固有ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_Pos_OPTCard12")]
        private static extern int Init_Pos_OPTCard12_Private(IntPtr iDevSign, int iOPT_Num, int iWaitTime, ref ulong iCardSerNo, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 初始化消费机操作员卡
        /// </summary>
        /// <param name="iOPT_Num">操作员编号，（1-255）</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回操作员卡固有ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Init_Pos_OPTCard12(int iOPT_Num, int iWaitTime, ref ulong iCardSerNo, byte[] arrByteUserCodeNew)
        {
            if (iOPT_Num < 1 || iOPT_Num > 255)
            {
                return 1;
            }
            return Init_Pos_OPTCard12_Private(_ipDevSign, iOPT_Num, iWaitTime, ref iCardSerNo, arrByteUserCodeNew);
        }

        /// <summary>
        /// 回收消费机操作员卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iCardSerNo">要回收的操作员卡固有ID号</param>
        /// <param name="iWaitTime">等卡时间，单位ms</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "RST_Pos_OPTCard12")]
        private static extern int RST_Pos_OPTCard12_Private(IntPtr iDevSign, uint iCardSerNo, int iWaitTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCardSerNo"></param>
        /// <param name="iWaitTime"></param>
        /// <param name="arrByteUserCodeNew"></param>
        /// <returns></returns>
        public static int RST_Pos_OPTCard12(uint iCardSerNo, int iWaitTime, byte[] arrByteUserCodeNew)
        {
            return RST_Pos_OPTCard12_Private(_ipDevSign, iCardSerNo, iWaitTime, arrByteUserCodeNew);
        }

        #endregion

        #region 节能发卡专用函数

        /// <summary>
        /// 读节能用户卡
        /// </summary>
        /// <param name="iDevSign">设备标识</param>
        /// <param name="iCardType">卡类型（0-用户卡，1-操作员卡，2-系统卡，4-白卡）</param>
        /// <param name="iOPT_Num">操作员编号</param>
        /// <param name="iSerNo">用户卡流水号</param>
        /// <param name="strCardNo">用户卡编号</param>
        /// <param name="iCardSerNo">用户卡物理ID号</param>
        /// <param name="iValue">用户卡余额</param>
        /// <param name="iCount">用户卡使用次数</param>
        /// <param name="iUserType">用户卡卡类（1-8类）</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "QueryJsCard")]
        private static extern int QueryJsCard_Private(IntPtr iDevSign, ref  int iCardType, ref int iOPT_Num, ref int iSerNo, ref string strCardNo, ref ulong iCardSerNo, ref int iValue, ref int iCount, ref int iUserType, int iWaitTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 读节能用户卡
        /// </summary>
        /// <param name="eCardType">卡类型</param>
        /// <param name="iOPT_Num">操作员编号</param>
        /// <param name="iSerNo">用户卡流水号</param>
        /// <param name="strCardNo">用户卡编号</param>
        /// <param name="iCardSerNo">用户卡物理ID号</param>
        /// <param name="dValue">用户卡余额</param>
        /// <param name="iCount">用户卡使用次数</param>
        /// <param name="eUserType">用户卡卡类</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="arrByteUserCodeNew">配置数组</param>
        /// <returns></returns>
        public static int QueryJsCard(ref JsCardRecoed jsCardRecord, int iWaitTime, byte[] arrByteUserCodeNew)
        {
            int iCardType = -1;
            int iOpt = 0;
            int iSerNo = 0;
            string strCardNo = string.Empty;
            ulong iCardSerNo = 0;
            int iValue = 0;
            int iCount = 0;
            int iUserType = -1;
            int iRes = 0;

            try
            {
                iRes = QueryJsCard_Private(_ipDevSign, ref iCardType, ref iOpt, ref iSerNo, ref strCardNo, ref iCardSerNo, ref iValue, ref iCount, ref iUserType, jsCardRecord.iWaitTime, arrByteUserCodeNew);
            }
            catch (Exception)
            {
                jsCardRecord.eumCardType = YuChuanDevice.GetEnumCardType(iCardType);
                jsCardRecord.dValue = (decimal)iValue / _dMultiple;
                jsCardRecord.enumUserType = YuChuanDevice.GetEnumUserType(iUserType);
                jsCardRecord.iCardSerNo = iCardSerNo;
                jsCardRecord.iCount = iCount;
                jsCardRecord.iOpt = iOpt;
                jsCardRecord.iSerNo = iSerNo;
                jsCardRecord.strCardNo = strCardNo;
                jsCardRecord.iWaitTime = iWaitTime;
                jsCardRecord.arrConfigPara = arrByteUserCodeNew.ToList();
            }

            return iRes;
        }

        /// <summary>
        /// 发节能用户卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iSerNo">用户卡流水号</param>
        /// <param name="strCardNo">用户卡编号</param>
        /// <param name="iUserType">用户卡卡类（1-8类）</param>
        /// <param name="iCardSerNo">用户卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_Js_UserCard")]
        private static extern int Init_Js_UserCard_Private(IntPtr iDevSign, int iSerNo, string strCardNo, int iUserType, ref ulong iCardSerNo, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 发节能用户卡
        /// </summary>
        /// <param name="iSerNo">用户卡流水号</param>
        /// <param name="strCardNo">用户卡编号</param>
        /// <param name="eUserType">用户卡卡类</param>
        /// <param name="iCardSerNo">用户卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Init_Js_UserCard(int iSerNo, string strCardNo, YC_Enum_UserType eUserType, ref ulong iCardSerNo, byte[] arrByteUserCodeNew)
        {
            return Init_Js_UserCard_Private(_ipDevSign, iSerNo, strCardNo, (int)eUserType, ref iCardSerNo, arrByteUserCodeNew);
        }

        /// <summary>
        /// 回收节能用户卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iCardSerNo">要回收的用户卡固有ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "RSTJsUserCard")]
        private static extern int RSTJsUserCard_Private(IntPtr iDevSign, uint iCardSerNo, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 回收节能用户卡
        /// </summary>
        /// <param name="iCardSerNo">要回收的用户卡固有ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int RSTJsUserCard(uint iCardSerNo, byte[] arrByteUserCodeNew)
        {
            return RSTJsUserCard_Private(_ipDevSign, iCardSerNo, arrByteUserCodeNew);
        }

        /// <summary>
        /// 修改节水系统用户卡上得金额并将充值次数加1
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iChargeVal">卡金额</param>
        /// <param name="strChargeDateTime">充值时间YYMMDDHHMMSS格式</param>
        /// <param name="iCardSerNo">卡物理ID号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "WRT_Js_UserCard_AddCount")]
        private static extern int WRT_Js_UserCard_AddCount_Private(IntPtr iDevSign, int iChargeVal, string strChargeDateTime, ulong iCardSerNo);
        /// <summary>
        /// 修改节水系统用户卡上得金额并将充值次数加1
        /// </summary>
        /// <param name="dChargeVal">卡金额</param>
        /// <param name="dtChargeDateTime">充值时间</param>
        /// <param name="iCardSerNo">卡物理ID号</param>
        /// <returns></returns>
        public static int WRT_Js_UserCard_AddCount(decimal dChargeVal, DateTime dtChargeDateTime, ulong iCardSerNo)
        {
            int iChargeVal = (int)(dChargeVal * _dMultiple);
            string strChargeDateTime = dtChargeDateTime.ToString("yyMMddHHmmss");
            return WRT_Js_UserCard_AddCount_Private(_ipDevSign, iChargeVal, strChargeDateTime, iCardSerNo);
        }

        /// <summary>
        /// 制一体化节水时间设置卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="strJsDate">设置的日期YYMMDD格式，即只存放年月日</param>
        /// <param name="strJsTime">设置的时间HHMMSS格式，即只存放时分秒</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Set_Js_DateTimeCard")]
        private static extern int Set_Js_DateTimeCard_Private(IntPtr iDevSign, string strJsDate, string strJsTime, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 制一体化节水时间设置卡
        /// </summary>
        /// <param name="dtJsDate">需要设置的时间</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Set_Js_DateTimeCard(DateTime dtJsDate, byte[] arrByteUserCodeNew)
        {
            string strJsDate = dtJsDate.ToString("yyMMdd");
            string strJsTime = dtJsDate.ToString("HHmmss");
            return Set_Js_DateTimeCard_Private(_ipDevSign, strJsDate, strJsTime, arrByteUserCodeNew);
        }

        /// <summary>
        /// 制一体化节水机号设置卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iAddr">设置机号的起始地址</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Set_Js_AddreCard")]
        private static extern int Set_Js_AddreCard_Private(IntPtr iDevSign, int iAddr, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 制一体化节水机号设置卡
        /// </summary>
        /// <param name="iAddr">设置机号的起始地址</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Set_Js_AddreCard(int iAddr, byte[] arrByteUserCodeNew)
        {
            return Set_Js_AddreCard_Private(_ipDevSign, iAddr, arrByteUserCodeNew);
        }

        /// <summary>
        /// 初始化节能营业员卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iOPT_Num">营业员编号，（1-254）</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回操作员卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "Init_JS_OPTCard12")]
        private static extern int Init_JS_OPTCard12_Private(IntPtr iDevSign, int iOPT_Num, int iWaitTime, ref ulong iCardSerNo, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 初始化节能营业员卡
        /// </summary>
        /// <param name="iOPT_Num">营业员编号</param>
        /// <param name="iWaitTime">等卡时间，单位为ms</param>
        /// <param name="iCardSerNo">返回操作员卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int Init_JS_OPTCard12(int iOPT_Num, int iWaitTime, ref ulong iCardSerNo, byte[] arrByteUserCodeNew)
        {
            if (iOPT_Num < 1 || iOPT_Num > 255)
            {
                return 1;
            }
            return Init_JS_OPTCard12_Private(_ipDevSign, iOPT_Num, iWaitTime, ref iCardSerNo, arrByteUserCodeNew);
        }

        /// <summary>
        /// 发节能参数设置卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="jsFeeRate">节能费率参数</param>
        /// <param name="jsAdvanPara">节能高级参数</param>
        /// <param name="iAddr">设备机号设置卡起始机号（供节能设备设置机号用）</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "MakeJSSubSysCard12")]
        private static extern int MakeJSSubSysCard12_Private(IntPtr iDevSign, ref  JS_FEE_RATE jsFeeRate, ref JS_AdvanPara jsAdvanPara, int iAddr, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 发节能参数设置卡
        /// </summary>
        /// <param name="jsFeeRate">节能费率参数</param>
        /// <param name="jsAdvanPara">节能高级参数</param>
        /// <param name="iAddr">设备机号设置卡起始机号（供节能设备设置机号用）</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int MakeJSSubSysCard12(ref  JS_FEE_RATE jsFeeRate, ref JS_AdvanPara jsAdvanPara, int iAddr, byte[] arrByteUserCodeNew)
        {
            return MakeJSSubSysCard12_Private(_ipDevSign, ref jsFeeRate, ref jsAdvanPara, iAddr, arrByteUserCodeNew);
        }

        /// <summary>
        /// 回收节能参数设置卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "RSTJSSubSysCard")]
        private static extern int RSTJSSubSysCard_Private(IntPtr iDevSign, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 回收节能参数设置卡
        /// </summary>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int RSTJSSubSysCard(byte[] arrByteUserCodeNew)
        {
            return RSTJSSubSysCard_Private(_ipDevSign, arrByteUserCodeNew);
        }


        /// <summary>
        /// 发节能数据采集卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iOperatorNo">采集卡操作员编号</param>
        /// <param name="iMode">采集卡的模式，1为明细采集卡，0为总额采集卡</param>
        /// <param name="iCardSerNo">返回采集卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "InitCollectCard")]
        private static extern int InitCollectCard_Private(IntPtr iDevSign, int iOperatorNo, int iMode, ref ulong iCardSerNo, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 发节能数据采集卡
        /// </summary>
        /// <param name="iOperatorNo">采集卡操作员编号</param>
        /// <param name="iMode">采集卡的模式</param>
        /// <param name="iCardSerNo">返回采集卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int InitCollectCard(int iOperatorNo, YC_Enum_CollectionMode eMode, ref ulong iCardSerNo, byte[] arrByteUserCodeNew)
        {
            return InitCollectCard_Private(_ipDevSign, iOperatorNo, (int)eMode, ref iCardSerNo, arrByteUserCodeNew);
        }

        /// <summary>
        /// 回收节能采集卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iCardSerno">采集卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "InitCollectCard")]
        private static extern int RSTCollectCard_Private(IntPtr iDevSign, uint iCardSerno, byte[] arrByteUserCodeNew);
        /// <summary>
        /// 回收节能采集卡
        /// </summary>
        /// <param name="iCardSerno">采集卡物理ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        public static int RSTCollectCard(uint iCardSerno, byte[] arrByteUserCodeNew)
        {
            return RSTCollectCard_Private(_ipDevSign, iCardSerno, arrByteUserCodeNew);
        }

        /// <summary>
        /// 读采集卡基本信息
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iMode">返回采集卡的模式，1为明细采集卡，0未总额采集卡</param>
        /// <param name="iCardSerNo">返回采集卡卡ID号</param>
        /// <param name="iRecNum">返回已采集记录条数，如果为明细采集卡则为0</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "ReadRecNum_NEW")]
        private static extern int ReadRecNum_NEW_Private(IntPtr iDevSign, ref uint iMode, ref ulong iCardSerNo, ref uint iRecNum);
        /// <summary>
        /// 读采集卡基本信息
        /// </summary>
        /// <param name="eMode">返回采集卡的模式</param>
        /// <param name="iCardSerNo">返回采集卡卡ID号</param>
        /// <param name="iRecNum">返回已采集记录条数，如果为明细采集卡则为0</param>
        /// <returns></returns>
        public static int ReadRecNum_NEW(ref YC_Enum_CollectionMode eMode, ref ulong iCardSerNo, ref uint iRecNum)
        {
            uint iMode = 0;
            int iRes = ReadRecNum_NEW_Private(_ipDevSign, ref iMode, ref iCardSerNo, ref iRecNum);

            switch (iMode)
            {
                case ((int)YC_Enum_CollectionMode.DetailCollecttionMode):
                    {
                        eMode = YC_Enum_CollectionMode.DetailCollecttionMode;
                        break;
                    }
                case ((int)YC_Enum_CollectionMode.TotalCollecttionMode):
                    {
                        eMode = YC_Enum_CollectionMode.TotalCollecttionMode;
                        break;
                    }
                default:
                    break;
            }

            return iRes;
        }

        /// <summary>
        /// 如果ReadRecNum_NEW返回采集卡模式为0，可采用此函数采集各设备总额
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="recordAllData">返回每台设备总额情况（注意结构体定义）</param>
        /// <param name="iCardSerNo">采集卡卡ID号</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "ReadAllRec_NEW")]
        private static extern int ReadAllRec_NEW_Private(IntPtr iDevSign, RecordAllData[] recordAllData, ulong iCardSerNo);
        /// <summary>
        /// 如果ReadRecNum_NEW返回采集卡模式为0，可采用此函数采集各设备总额
        /// </summary>
        /// <param name="recordAllData">返回每台设备总额情况</param>
        /// <param name="iCardSerNo">采集卡卡ID号</param>
        /// <returns></returns>
        public static int ReadAllRec_NEW(ref RecordAllData[] recordAllData, ulong iCardSerNo)
        {
            int iRes = 0;
            try
            {
                iRes = ReadAllRec_NEW_Private(_ipDevSign, recordAllData, iCardSerNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        /// <summary>
        /// 如果ReadRecNum_NEW返回采集卡模式为1，可采用此函数采集设备明细
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="recDetailedData">返回每台设备消费明细</param>
        /// <param name="iCardSerNo">采集卡卡ID号</param>
        /// <param name="iAllMoney">返回此设备消费总金额</param>
        /// <param name="iAllTime">返回此设备消费总使用次数</param>
        /// <param name="iAllAmount">返回此设备消费总使用量（计时单位为S，计量单位为L）</param>
        /// <param name="iAddress">返回设备地址</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "ReadDetailedRec_NEW")]
        private static extern int ReadDetailedRec_NEW_Private(IntPtr iDevSign, ref RecordDetailedData recDetailedData, ulong iCardSerNo, ref ulong iAllMoney, ref ulong iAllTime, ref ulong iAllAmount, ref ulong iAddress);
        /// <summary>
        /// 如果ReadRecNum_NEW返回采集卡模式为1，可采用此函数采集设备明细
        /// </summary>
        /// <param name="recDetailedData">返回每台设备消费明细</param>
        /// <param name="iCardSerNo">采集卡卡ID号</param>
        /// <param name="dAllMoney">返回此设备消费总金额</param>
        /// <param name="iAllTime">返回此设备消费总使用次数</param>
        /// <param name="iAllAmount">返回此设备消费总使用量（计时单位为S，计量单位为L）</param>
        /// <param name="iAddress">返回设备地址</param>
        /// <returns></returns>
        public static int ReadDetailedRec_NEW(ref RecordDetailedData recDetailedData, ulong iCardSerNo, ref decimal dAllMoney, ref ulong iAllTime, ref ulong iAllAmount, ref ulong iAddress)
        {
            ulong iAllMoney = 0;
            int iRes = 0;
            try
            {
                iRes = ReadDetailedRec_NEW_Private(_ipDevSign, ref  recDetailedData, iCardSerNo, ref  iAllMoney, ref  iAllTime, ref  iAllAmount, ref  iAddress);
            }
            catch (Exception)
            {
                dAllMoney = (decimal)iAllMoney * _dMultiple;
            }

            return iRes;
        }

        /// <summary>
        /// 初始化黑名单卡
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="bBlock">黑名单块号（每块设置4096个黑名单成员）</param>
        /// <param name="iCardSerNo">返回黑名单卡卡ID号</param>
        /// <param name="arrByteUserCodeNew">配置参数组</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "InitBlackCard")]
        public static extern int InitBlackCard(IntPtr iDevSign, byte bBlock, ref ulong iCardSerNo, byte[] arrByteUserCodeNew);

        /// <summary>
        /// 下黑名单数据
        /// </summary>
        /// <param name="iDevSign">设备句柄</param>
        /// <param name="iCardSerNo">核对黑名单卡ID号</param>
        /// <param name="arrBlackData">黑名单数据组（每张卡设置512个字节，如果大于4096用户可以用多张黑名单卡在初始化黑名单卡时候配置他的块号来实现）</param>
        /// <returns></returns>
        [DllImport(strDLLPath, EntryPoint = "WriteBlackCard")]
        public static extern int WriteBlackCard(IntPtr iDevSign, ulong iCardSerNo, byte[] arrBlackData);

        #endregion

        #region 消费机专用函数

        /// <summary>
        /// 使用Socket与设备进行通讯，并返回消息
        /// </summary>
        /// <param name="arrIp">被访问设备的IP</param>
        /// <param name="iPort">端口号</param>
        /// <param name="arrSend">发送的指令</param>
        /// <param name="strReturnValue">返回的消息</param>
        ///  <param name="recType">需要返回的信息格式</param>
        /// <returns></returns>
        private static Model.General.ReturnValueInfo GetReturnValue(byte[] arrIp, int iPort, byte[] arrSend, out object objReturnValue, Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType recType)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;
            objReturnValue = new object();

            if (recType == PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String)
            {
                objReturnValue = string.Empty;
            }
            else if (recType == PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray)
            {
                objReturnValue = new List<byte>();
            }
            else if (recType == PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_Null)
            {
                objReturnValue = null;
            }

            try
            {
                System.Net.IPAddress ipHost = new System.Net.IPAddress(arrIp);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipHost, iPort);

                byte bLow = 0;
                byte bHigh = 0;
                ReaderGeneral.CRC16_CCITT(arrSend, out bHigh, out bLow);
                arrSend[arrSend.Length - 1] = bHigh;
                arrSend[arrSend.Length - 2] = bLow;

                socket.Send(arrSend);
                byte[] arrRec = new byte[1024];

                while (true && objReturnValue != null)
                {
                    int resRec = socket.Receive(arrRec, arrRec.Length, 0);
                    if (resRec <= 0)
                    {
                        break;
                    }

                    if (recType == PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String)
                    {
                        objReturnValue += Encoding.ASCII.GetString(arrRec, 0, resRec);
                    }
                    else if (recType == PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray)
                    {
                        int indexCut = 0;
                        for (int i = arrRec.Length - 1; i > 0; i--)
                        {
                            if (arrRec[i] != 0)
                            {
                                indexCut = i + 1;
                                break;
                            }
                        }
                        List<byte> listTmp = arrRec.ToList();
                        listTmp.RemoveRange(indexCut, listTmp.Count - indexCut);
                        ((List<byte>)objReturnValue).AddRange(listTmp);
                    }
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                rvInfo.isError = false;
            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 设置消费机当前时间
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetMacTime(byte[] arrIp, int iPort)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x01, 0x53, 0x45, 0x54, 0x5F, 0x54, 0x69, 0x6D, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                arrSend[9] = (byte)int.Parse(DateTime.Now.Year.ToString().Substring(2, 2));
                arrSend[10] = (byte)DateTime.Now.Month;
                arrSend[11] = (byte)DateTime.Now.Day;
                arrSend[12] = (byte)DateTime.Now.Hour;
                arrSend[13] = (byte)DateTime.Now.Minute;
                arrSend[14] = (byte)DateTime.Now.Second;
                arrSend[15] = 01;

                object objRec;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);

                if (objRec.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "设置时间成功。";
                }
                else
                {
                    rvInfo.messageText = "设置时间失败。失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 设置消费机为特定时间
        /// </summary>
        /// <param name="arrIp"></param>
        /// <param name="iPort"></param>
        /// <param name="dtSpecial"></param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetSpecialMacTime(byte[] arrIp, int iPort, DateTime dtSpecial)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x01, 0x53, 0x45, 0x54, 0x5F, 0x54, 0x69, 0x6D, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                arrSend[9] = (byte)int.Parse(dtSpecial.Year.ToString().Substring(2, 2));
                arrSend[10] = (byte)dtSpecial.Month;
                arrSend[11] = (byte)dtSpecial.Day;
                arrSend[12] = (byte)dtSpecial.Hour;
                arrSend[13] = (byte)dtSpecial.Minute;
                arrSend[14] = (byte)dtSpecial.Second;
                arrSend[15] = 01;

                object objRec;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);

                if (objRec.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "设置时间成功。";
                }
                else
                {
                    rvInfo.messageText = "设置时间失败。失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 获取消费机当前时间
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static DateTime YC_Pos_GetMacTime(byte[] arrIp, int iPort)
        {
            DateTime dtMac = DateTime.MinValue;

            try
            {
                byte[] arrSend = { 0x02, 0x52, 0x44, 0x5F, 0x54, 0x69, 0x6D, 0x65, 0x00, 0x00 };
                object objRec;
                Model.General.ReturnValueInfo rvInfoRec = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray);
                List<byte> listRec = (List<byte>)objRec;
                dtMac = new DateTime(listRec[1] + 2000, listRec[2], listRec[3], listRec[4], listRec[5], listRec[6], 0);
            }
            catch (Exception Ex)
            {
                dtMac = DateTime.MaxValue;
            }

            return dtMac;
        }

        /// <summary>
        /// 设置消费机系统参数
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetSysParams(byte[] arrIp, int iPort, YC_Pos_SysParams para)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;
            if (para == null)
            {
                rvInfo.messageText = "参数类未被初始化。";
                return rvInfo;
            }

            object objRec;
            try
            {
                byte[] arrSend = { 0x03, 0x53, 0x45, 0x54, 0x5F, 0x53, 0x59, 0x53, 0x54, 0x45, 0x4D, 
#region 数据区
                                     /*允许使用的卡类，本系统共有32类卡，每类卡对应最低字节的每位*/
                                     0x00, 0x00, 0x00, 0x00,  //11~14
                                     /*定值模式下定值金额。（4字节，不足4位，高位补0）*/
                                     0x00, 0x00, 0x00, 0x00,  //15~18
                                     /*最大卡金额。*/
                                     0x00, 0x00, 0x00, 0x00,  //19~22
                                     /*最大消费金额*/
                                     0x00, 0x00, 0x00, 0x00,  //23~27
                                     /*营业人员开机：0为不需要营业员开机，1为需要营业员卡开机。*/
                                     0x00, 0x00, 0x00, 0x00,  //28~31
                                     /*工作模式：0：编号，1：定值，2：单价*/
                                     0x00, 0x00, 0x00, 0x00,  //32~35
                                     /*每餐最大消费次数（高级参数：设置为0时，为不限制。本系统未启用。）*/
                                     0x00, 0x00, 0x00, 0x00,  //36~39
                                     /*保留参数（高级参数：设置为0时，为不限制。本系统未启用。）*/
                                     0x00, 0x00, 0x00, 0x00, //40~43
                                     /*两次消费间隔分钟数（高级参数：设置为0时，为不限制。）*/
                                     0x00, 0x00, 0x00, 0x00,  //44~47
                                     /*每天最大消费额度。）*/
                                     0x00, 0x00, 0x00, 0x00, //48~51
                                     /*每天消费次数。）*/
                                     0x00, 0x00, 0x00, 0x00, //52~55
                                     /*编号模式的0-9编号值（每种编号对应四个字节）。）*/
                                     0x00, 0x00, 0x00, 0x00, //56~59
                                     0x00, 0x00, 0x00, 0x00, //60~63
                                     0x00, 0x00, 0x00, 0x00, //64~67
                                     0x00, 0x00, 0x00, 0x00, //68~71
                                     0x00, 0x00, 0x00, 0x00, //72~75
                                     0x00, 0x00, 0x00, 0x00, //76~79
                                     0x00, 0x00, 0x00, 0x00, //80~83
                                     0x00, 0x00, 0x00, 0x00, //84~87
                                     0x00, 0x00, 0x00, 0x00, //88~91
                                     0x00, 0x00, 0x00, 0x00, //92~95
                                     /*时间段模式的1-4个时间段开始时间、结束时间、定值金额（每个时间段对应12个字节）。）*/
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //96~107
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //108~119
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //120~131
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //132~143
                                     /*设置是否启用有效期。（一字节））*/
                                     0x00, //144
                                     /*定是重新启动。（4段，两个字节））*/
                                     0x00, 0x00,//145~146
                                     0x00, 0x00,//147~148
                                     0x00, 0x00,//149~150
                                     0x00, 0x00,//151~152
                                     /*自动屏幕保护时间（四个字节 0xffffffff为不启用屏保 2000表示1分钟）。）*/
                                     0x00, 0x00, 0x00, 0x00, //153~156
                                     /*通讯超时时间。）*/
                                     0x00, 0x00, 0x00, 0x00, //157~160
#endregion
                                     0x00,0x00
                                 };

                arrSend = GetByteArrRes(arrSend, 11, 14, para.iCardType);
                arrSend = GetByteArrRes(arrSend, 15, 18, (long)(para.dSetValue * _dMultiple));
                arrSend = GetByteArrRes(arrSend, 19, 22, (long)(para.dMaxCardVal * _dMultiple));
                arrSend = GetByteArrRes(arrSend, 23, 26, (long)(para.dMaxPosVal * _dMultiple));
                int iOpt = 0;
                if (para.lIsNeedOpt)
                    iOpt = 1;
                arrSend = GetByteArrRes(arrSend, 27, 30, iOpt);
                arrSend = GetByteArrRes(arrSend, 31, 34, para.bWorkMode);
                arrSend = GetByteArrRes(arrSend, 35, 38, para.iMaxCountPerMeal);
                arrSend = GetByteArrRes(arrSend, 39, 42, para.iHoldParams);
                arrSend = GetByteArrRes(arrSend, 43, 46, para.iPosInterval);
                arrSend = GetByteArrRes(arrSend, 47, 50, (int)(para.dMaxPosValPerDay * _dMultiple));
                arrSend = GetByteArrRes(arrSend, 51, 54, para.iMaxPosCountPerDay);

                #region 编号消费

                arrSend = GetByteArrRes(arrSend, 55, 58, (int)(para.dNumPos_0 * 100));
                arrSend = GetByteArrRes(arrSend, 59, 62, (int)(para.dNumPos_1 * 100));
                arrSend = GetByteArrRes(arrSend, 63, 66, (int)(para.dNumPos_2 * 100));
                arrSend = GetByteArrRes(arrSend, 67, 70, (int)(para.dNumPos_3 * 100));
                arrSend = GetByteArrRes(arrSend, 71, 74, (int)(para.dNumPos_4 * 100));
                arrSend = GetByteArrRes(arrSend, 75, 78, (int)(para.dNumPos_5 * 100));
                arrSend = GetByteArrRes(arrSend, 79, 82, (int)(para.dNumPos_6 * 100));
                arrSend = GetByteArrRes(arrSend, 83, 86, (int)(para.dNumPos_7 * 100));
                arrSend = GetByteArrRes(arrSend, 87, 90, (int)(para.dNumPos_8 * 100));
                arrSend = GetByteArrRes(arrSend, 91, 94, (int)(para.dNumPos_9 * 100));

                #endregion

                #region 时段模式消费

                #region 时段一

                int iTimeSpan = int.Parse(para.timeModeValInfo_1.tsStart.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_1.tsStart.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 95, 98, iTimeSpan);
                iTimeSpan = int.Parse(para.timeModeValInfo_1.tsEnd.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_1.tsEnd.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 99, 102, iTimeSpan);
                arrSend = GetByteArrRes(arrSend, 103, 106, (int)(para.timeModeValInfo_1.dHoldVal * 100));

                #endregion

                #region 时段二

                iTimeSpan = int.Parse(para.timeModeValInfo_2.tsStart.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_2.tsStart.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 107, 110, iTimeSpan);
                iTimeSpan = int.Parse(para.timeModeValInfo_2.tsEnd.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_2.tsEnd.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 111, 114, iTimeSpan);
                arrSend = GetByteArrRes(arrSend, 115, 118, (int)(para.timeModeValInfo_2.dHoldVal * 100));

                #endregion

                #region 时段三

                iTimeSpan = int.Parse(para.timeModeValInfo_3.tsStart.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_3.tsStart.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 119, 122, iTimeSpan);
                iTimeSpan = int.Parse(para.timeModeValInfo_3.tsEnd.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_3.tsEnd.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 123, 126, iTimeSpan);
                arrSend = GetByteArrRes(arrSend, 127, 130, (int)(para.timeModeValInfo_3.dHoldVal * 100));

                #endregion

                #region 时段四

                iTimeSpan = int.Parse(para.timeModeValInfo_4.tsStart.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_4.tsStart.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 131, 134, iTimeSpan);
                iTimeSpan = int.Parse(para.timeModeValInfo_4.tsEnd.Hours.ToString().PadLeft(2, '0') + para.timeModeValInfo_4.tsEnd.Minutes.ToString().PadLeft(2, '0'));
                arrSend = GetByteArrRes(arrSend, 135, 138, iTimeSpan);
                arrSend = GetByteArrRes(arrSend, 139, 142, (int)(para.timeModeValInfo_4.dHoldVal * 100));

                #endregion

                #endregion

                int iIsUsedValidDate = para.lIsUsedValidDate ? 1 : 0;
                arrSend = GetByteArrRes(arrSend, 143, 143, iIsUsedValidDate);

                #region 定时重新启动四个时段值

                arrSend[144] = (byte)para.tsReset1.Value.Hours;
                arrSend[145] = (byte)para.tsReset1.Value.Minutes;
                arrSend[146] = (byte)para.tsReset2.Value.Hours;
                arrSend[147] = (byte)para.tsReset2.Value.Minutes;
                arrSend[148] = (byte)para.tsReset3.Value.Hours;
                arrSend[149] = (byte)para.tsReset3.Value.Minutes;
                arrSend[150] = (byte)para.tsReset4.Value.Hours;
                arrSend[151] = (byte)para.tsReset4.Value.Minutes;

                #endregion

                arrSend = GetByteArrRes(arrSend, 152, 155, para.iScreenProtectTime);
                arrSend = GetByteArrRes(arrSend, 156, 159, para.iTimeCommOver);

                Model.General.ReturnValueInfo rvInfoRec = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objRec.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.messageText = "设置消费机系统参数成功。";
                    rvInfo.isError = false;
                }
                else
                {
                    rvInfo.messageText = "设置消费机系统参数失败，失败信息：" + rvInfoRec.messageText;
                }

            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 读取消费机系统参数
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static YC_Pos_SysParams YC_Pos_GetSysParams(byte[] arrIp, int iPort)
        {
            YC_Pos_SysParams para = new YC_Pos_SysParams();

            try
            {
                object objRec;
                byte[] arrSend = { 0x04, 0x52, 0x44, 0x5F, 0x53, 0x59, 0x53, 0x54, 0x45, 0x4D, 0x00, 0x00 };
                Model.General.ReturnValueInfo rvInfoRec = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray);

                List<byte> listRec = (List<byte>)objRec;
                para.iCardType = (int)GetIntRes(listRec.ToArray(), 1, 4);
                para.dSetValue = (decimal)(GetIntRes(listRec.ToArray(), 5, 8) / _dMultiple); //OK
                para.dMaxCardVal = (decimal)(GetIntRes(listRec.ToArray(), 9, 12) / _dMultiple); //OK
                para.dMaxPosVal = (decimal)(GetIntRes(listRec.ToArray(), 13, 16) / _dMultiple);
                para.lIsNeedOpt = Convert.ToBoolean(GetIntRes(listRec.ToArray(), 17, 20));//true ? false : (int)GetIntRes(listRec.ToArray(), 16, 19) == 1;
                para.bWorkMode = (byte)GetIntRes(listRec.ToArray(), 21, 24); //OK
                para.iMaxCountPerMeal = (int)GetIntRes(listRec.ToArray(), 25, 28);
                para.iHoldParams = (int)GetIntRes(listRec.ToArray(), 29, 32);
                para.iPosInterval = (int)GetIntRes(listRec.ToArray(), 33, 36);
                para.dMaxPosValPerDay = (decimal)(GetIntRes(listRec.ToArray(), 37, 40) / _dMultiple);    //OK
                para.iMaxPosCountPerDay = (int)GetIntRes(listRec.ToArray(), 41, 44);

                #region 编号模式值

                para.dNumPos_0 = (decimal)GetIntRes(listRec.ToArray(), 45, 48) / _dMultiple;
                para.dNumPos_1 = (decimal)GetIntRes(listRec.ToArray(), 49, 52) / _dMultiple;
                para.dNumPos_2 = (decimal)GetIntRes(listRec.ToArray(), 53, 56) / _dMultiple;
                para.dNumPos_3 = (decimal)GetIntRes(listRec.ToArray(), 57, 60) / _dMultiple;
                para.dNumPos_4 = (decimal)GetIntRes(listRec.ToArray(), 61, 64) / _dMultiple;
                para.dNumPos_5 = (decimal)GetIntRes(listRec.ToArray(), 65, 68) / _dMultiple;
                para.dNumPos_6 = (decimal)GetIntRes(listRec.ToArray(), 69, 72) / _dMultiple;
                para.dNumPos_7 = (decimal)GetIntRes(listRec.ToArray(), 73, 76) / _dMultiple;
                para.dNumPos_8 = (decimal)GetIntRes(listRec.ToArray(), 77, 80) / _dMultiple;
                para.dNumPos_9 = (decimal)GetIntRes(listRec.ToArray(), 81, 84) / _dMultiple;

                #endregion

                #region 时段模式四个时段值

                string strTimeSpan = GetIntRes(listRec.ToArray(), 85, 88).ToString().PadLeft(4, '0');
                para.timeModeValInfo_1.tsStart = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                strTimeSpan = GetIntRes(listRec.ToArray(), 89, 92).ToString().PadLeft(4, '0');
                para.timeModeValInfo_1.tsEnd = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                para.timeModeValInfo_1.dHoldVal = (decimal)GetIntRes(listRec.ToArray(), 93, 96) / _dMultiple;

                strTimeSpan = GetIntRes(listRec.ToArray(), 97, 100).ToString().PadLeft(4, '0');
                para.timeModeValInfo_2.tsStart = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                strTimeSpan = GetIntRes(listRec.ToArray(), 101, 104).ToString().PadLeft(4, '0');
                para.timeModeValInfo_2.tsEnd = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                para.timeModeValInfo_2.dHoldVal = (decimal)GetIntRes(listRec.ToArray(), 105, 108) / _dMultiple;

                strTimeSpan = GetIntRes(listRec.ToArray(), 109, 112).ToString().PadLeft(4, '0');
                para.timeModeValInfo_3.tsStart = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                strTimeSpan = GetIntRes(listRec.ToArray(), 113, 116).ToString().PadLeft(4, '0');
                para.timeModeValInfo_3.tsEnd = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                para.timeModeValInfo_3.dHoldVal = (decimal)GetIntRes(listRec.ToArray(), 117, 120) / _dMultiple;

                strTimeSpan = GetIntRes(listRec.ToArray(), 121, 124).ToString().PadLeft(4, '0');
                para.timeModeValInfo_4.tsStart = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                strTimeSpan = GetIntRes(listRec.ToArray(), 125, 128).ToString().PadLeft(4, '0');
                para.timeModeValInfo_4.tsEnd = TimeSpan.Parse(strTimeSpan.Substring(0, 2) + ":" + strTimeSpan.Substring(2, 2));
                para.timeModeValInfo_4.dHoldVal = (decimal)GetIntRes(listRec.ToArray(), 129, 132) / _dMultiple;

                #endregion

                para.lIsUsedValidDate = Convert.ToBoolean(GetIntRes(listRec.ToArray(), 133, 133));

                #region 定时重启时段

                para.tsReset1 = new TimeSpan(listRec[134], listRec[135], 0);
                para.tsReset2 = new TimeSpan(listRec[136], listRec[137], 0);
                para.tsReset3 = new TimeSpan(listRec[138], listRec[139], 0);
                para.tsReset4 = new TimeSpan(listRec[140], listRec[141], 0);

                #endregion

                para.iScreenProtectTime = (int)GetIntRes(listRec.ToArray(), 142, 145);

                para.iTimeCommOver = (int)GetIntRes(listRec.ToArray(), 146, 149);

            }
            catch (Exception Ex)
            {
                return null;
            }

            return para;
        }

        /// <summary>
        /// 采集消费数据
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static List<PosRecord> YC_Pos_CollectPosRecords(byte[] arrIp, int iPort)
        {
            List<PosRecord> listPosRecord = new List<PosRecord>();

            try
            {
                List<byte> listRecMotherBoard = new List<byte>();
                object objRec;
                byte bRecordIndex = 0x01;
                byte iCount = 30;
                //指令模板
                byte[] arrSend = { 0x06, 0x52, 0x44, 0x5F, 0x52, 0x45, 0x43, 0x4F, 0x52, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                //消费机每次返回最多990个字节，每条合法指令的长度为33字节，第一个字节为0x01时，此指令方为可用数据。
                while (true)
                {
                    arrSend[10] = bRecordIndex;
                    Model.General.ReturnValueInfo rvInfoRec = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray);
                    bRecordIndex += iCount;
                    List<byte> listRec = (List<byte>)objRec;

                    if (listRec != null && listRec.Count > 0)
                    {
                        List<PosRecord> listPos = GetPosRecordEntityList(listRec);
                        listPosRecord.AddRange(listPos);
                        if (listRec[0] != 0x01)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return listPosRecord;
        }

        /// <summary>
        /// 设置主动上传对应PC的参数
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">消费机端口号</param>
        /// <param name="arrIpPC">服务器IP地址</param>
        /// <param name="iPortPC">服务器端口号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetServerAddr(byte[] arrIp, int iPort, byte[] arrIpPC, int iPortPC)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x07, 0x53, 0x45, 0x54, 0x5F, 0x49, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                arrSend[7] = arrIpPC[3];
                arrSend[8] = arrIpPC[2];
                arrSend[9] = arrIpPC[1];
                arrSend[10] = arrIpPC[0];
                GetByteArrRes(arrSend, 11, 14, iPortPC);

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_IP_OK
                    || objGet.ToString().Substring(0, 6) == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "设置服务器信息成功。";
                }
                else
                {
                    rvInfo.messageText = "设置服务器信息失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {

                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 清除设备数据
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <param name="isKeepBlacklist">是否保留黑名单</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_ClearRecords(byte[] arrIp, int iPort, bool isKeepBlacklist)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x08, 0x11, 0x22, 0x33, 0x44, 0x55, 0x00, 0x00 };
                byte[] arrSend_NotKeep = { 0x08, 0x11, 0x22, 0x33, 0x44, 0x55, 0x88, 0x00, 0x00 };
                if (!isKeepBlacklist)
                {
                    arrSend = arrSend_NotKeep;
                }

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "清除数据成功。";
                }
                else
                {
                    rvInfo.messageText = "清除数据失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {

                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 设置消费机IP地址及子网掩码
        /// </summary>
        /// <param name="arrIpOld">消费机旧IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <param name="arrIpNew">消费机新IP地址</param>
        /// <param name="arrSubnetMask">消费机新子网掩码</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetIpAddr(byte[] arrIpOld, int iPort, byte[] arrIpNew, byte[] arrSubnetMask)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x09, 0x53, 0x45, 0x54, 0x5F, 0x4E,0x45, 0x54, 0x5F, 0x49, 0x50, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                     0x00, 0x00 };

                byte[] arrIpNewAscii = GetASCII_IPArr(arrIpNew);
                byte[] arrSubnetMaskAscii = GetASCII_IPArr(arrSubnetMask);

                int iStartIp = 11;
                for (int i = 0; i < arrIpNewAscii.Length; i++)
                {
                    arrSend[i + iStartIp] = arrIpNewAscii[i];
                }

                int iStartSubnet = 31;
                for (int i = 0; i < arrSubnetMaskAscii.Length; i++)
                {
                    arrSend[i + iStartSubnet] = arrSubnetMaskAscii[i];
                }

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIpOld, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);

                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "设置消费机IP地址和子网掩码成功，更新为：" + BitConverter.ToString(arrIpNew) + " " + BitConverter.ToString(arrSubnetMask);
                }
                else
                {
                    rvInfo.messageText = "设置消费机IP地址和子网掩码失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 设置消费机网关
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <param name="arrGateway">新网关地址</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetGateway(byte[] arrIp, int iPort, byte[] arrGateway)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x0B, 0x53, 0x45, 0x54, 0x5F, 0x4E, 0x45, 0x54, 0x5F, 0x47, 0x57, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                     0x00,0x00 };

                byte[] arrGatewayAscii = GetASCII_IPArr(arrGateway);

                int iStart = 11;
                for (int i = 0; i < arrGatewayAscii.Length; i++)
                {
                    arrSend[i + iStart] = arrGatewayAscii[i];
                }

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_NET_GW_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "设置网关地址成功。";
                }
                else
                {
                    rvInfo.messageText = "设置网关地址失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {

                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 补采集采集消费数据
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static List<PosRecord> YC_Pos_ReCollectPosRecords(byte[] arrIp, int iPort)
        {
            List<PosRecord> listPosRecord = new List<PosRecord>();

            try
            {
                List<byte> listRecMotherBoard = new List<byte>();
                object objRec;
                byte bRecordIndex = 0x01;
                byte iCount = 30;
                //指令模板
                byte[] arrSend = { 0x0D, 0x52, 0x44, 0x5F, 0x52, 0x44, 0x5F, 0x41, 0x4C, 0x4C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                //消费机每次返回最多990个字节，每条合法指令的长度为33字节，第一个字节为0x01时，此指令方为可用数据。
                while (true)
                {
                    arrSend[10] = bRecordIndex;
                    Model.General.ReturnValueInfo rvInfoRec = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_ByteArray);
                    bRecordIndex += iCount;
                    List<byte> listRec = (List<byte>)objRec;

                    if (listRec != null && listRec.Count > 0)
                    {
                        List<PosRecord> listPos = GetPosRecordEntityList(listRec);
                        listPosRecord.AddRange(listPos);
                        if (listPos == null)
                        {
                            break;
                        }
                        if (listPos.Count == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return listPosRecord;
        }

        /// <summary>
        /// 远程重启消费机
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_RemoteRebootSys(byte[] arrIp, int iPort)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;

            try
            {
                byte[] arrSend = { 0x0E, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x00, 0x00 };

                object objRec;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objRec, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_Null);
                if (rvInfoGet.isError)
                {
                    rvInfo.messageText = rvInfoGet.messageText;
                }
                else
                {
                    rvInfo.isError = false;
                }
            }
            catch (Exception Ex)
            {
                rvInfo.messageText = Ex.Message;
            }
            return rvInfo;
        }

        /// <summary>
        /// 挂失单条黑名单
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <param name="iIndex">要挂失的黑名单流水号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_SetSingleBalcklist(byte[] arrIp, int iPort, long iIndex)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;
            long num = 255;
            long nums = num * num * num * num - 1;
            if (iIndex > nums)
            {
                rvInfo.messageText = "流水号超出界限。";
                return rvInfo;
            }

            try
            {
                byte[] arrSend = { 0x14, 0x53, 0x45, 0x54, 0x5F, 0x4F, 0x4E, 0x45, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                byte[] arrChange = BitConverter.GetBytes(iIndex);
                arrSend[8] = arrChange[0];
                arrSend[9] = arrChange[1];
                arrSend[10] = arrChange[2];
                arrSend[11] = arrChange[3];

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "挂失单条黑名单成功。";
                }
                else
                {
                    rvInfo.messageText = "挂失单条黑名单失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {

                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 解挂单条黑名单
        /// </summary>
        /// <param name="arrIp">消费机IP地址</param>
        /// <param name="iPort">端口号</param>
        /// <param name="iIndex">要解挂的黑名单流水号</param>
        /// <returns></returns>
        public static Model.General.ReturnValueInfo YC_Pos_RemoveSingleBlacklist(byte[] arrIp, int iPort, long iIndex)
        {
            Model.General.ReturnValueInfo rvInfo = new Model.General.ReturnValueInfo();
            rvInfo.isError = true;
            long num = 255;
            long nums = num * num * num * num - 1;
            if (iIndex > nums)
            {
                rvInfo.messageText = "流水号超出界限。";
                return rvInfo;
            }

            try
            {
                byte[] arrSend = { 0x15, 0x53, 0x45, 0x54, 0x5F, 0x4F, 0x4E, 0x45, 
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                byte[] arrChange = BitConverter.GetBytes(iIndex);
                arrSend[8] = arrChange[0];
                arrSend[9] = arrChange[1];
                arrSend[10] = arrChange[2];
                arrSend[11] = arrChange[3];

                object objGet;
                Model.General.ReturnValueInfo rvInfoGet = GetReturnValue(arrIp, iPort, arrSend, out objGet, PaymentEquipment.Enum.PaymentEnumMaster.YCPOS_EnumSocketRecType.Type_String);
                if (objGet.ToString() == Enum.PaymentEnumMaster.YC_POS_SET_OK)
                {
                    rvInfo.isError = false;
                    rvInfo.messageText = "挂失单条黑名单成功。";
                }
                else
                {
                    rvInfo.messageText = "挂失单条黑名单失败，失败信息：" + rvInfoGet.messageText;
                }
            }
            catch (Exception Ex)
            {

                rvInfo.messageText = Ex.Message;
            }

            return rvInfo;
        }

        #region 内部使用转换相关函数

        /// <summary>
        /// 获取消费数据实体组
        /// </summary>
        /// <param name="listRec">备分析的原始数据</param>
        /// <returns></returns>
        private static List<PosRecord> GetPosRecordEntityList(List<byte> listRec)
        {
            List<PosRecord> listPosRecord = new List<PosRecord>();
            int iCount = 33;

            try
            {
                //将原始数据分组
                List<byte> listTmp = new List<byte>();
                for (int i = 0; i < listRec.Count; i++)
                {
                    if ((i + 1) % iCount == 0)
                    {
                        listTmp.Add(listRec[i]);
                        PosRecord pos = GetPosRecordEntity(listTmp.ToArray());
                        if (pos != null && pos.iEmp_2 == 0x66)
                        {
                            listPosRecord.Add(pos);
                        }
                        continue;
                    }
                    if (i != 0 && i % iCount == 0)
                    {
                        listTmp = new List<byte>();
                    }
                    listTmp.Add(listRec[i]);
                }
            }
            catch (Exception)
            {

                return null;
            }

            return listPosRecord;
        }

        /// <summary>
        /// 将一组原始数据转换成一个消费数据实体
        /// </summary>
        /// <param name="arrPerPos"></param>
        /// <returns></returns>
        private static PosRecord GetPosRecordEntity(byte[] arrPerPos)
        {
            PosRecord pos = new PosRecord();
            try
            {
                if (arrPerPos.Length == 33)
                {
                    //记录流水号                    
                    pos.iRecordIndex = (int)GetIntRes(arrPerPos, 1, 4);
                    //用户流水号
                    pos.iUserIndex = (int)GetIntRes(arrPerPos, 5, 7);
                    //用户卡余额
                    pos.dCardBalance = (Decimal)(GetIntRes(arrPerPos, 8, 10) / 100.00);
                    //用户消费额
                    pos.dCardConsum = (Decimal)(GetIntRes(arrPerPos, 11, 13) / 100.00);
                    //消费时间
                    int iYear = arrPerPos[14] + 2000;
                    int iMonth = arrPerPos[15];
                    int iDay = arrPerPos[16];
                    int iHour = arrPerPos[17];
                    int iMinute = arrPerPos[18];
                    int iSecond = arrPerPos[19];
                    pos.dtRecordTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, DateTimeKind.Local);
                    //用户使用次数
                    pos.iCount = (int)GetIntRes(arrPerPos, 20, 21);
                    //保留一
                    pos.iEmp_1 = (int)GetIntRes(arrPerPos, 22, 22);
                    //保留二
                    pos.iEmp_2 = (int)GetIntRes(arrPerPos, 23, 23);
                    //保留三
                    pos.iEmp_3 = (int)GetIntRes(arrPerPos, 24, 28);
                    //设备地址
                    pos.iMacAddr = (int)GetIntRes(arrPerPos, 29, 30);
                    //保留四
                    pos.iEmp_4 = (int)GetIntRes(arrPerPos, 31, 32);
                }
            }
            catch (Exception)
            {

                pos = null;
            }
            return pos;
        }

        /// <summary>
        /// 获取由多位16进制字节值组成的整型值
        /// </summary>
        /// <param name="arrSource">源数据</param>
        /// <param name="iStart">起始位置</param>
        /// <param name="iEnd">结束位置</param>
        /// <returns></returns>
        private static long GetIntRes(byte[] arrSource, int iStart, int iEnd)
        {
            long iRes = 0;
            string strRes = string.Empty;

            try
            {
                if (iEnd >= iStart && arrSource.Length > iEnd)
                {
                    for (int i = iEnd; i >= iStart; i--)
                    {
                        string strLow = Convert.ToString(arrSource[i], 16);
                        strRes += strLow.PadLeft(2, '0');
                    }
                    iRes = Convert.ToInt64(strRes, 16);
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return iRes;
        }

        /// <summary>
        /// 将整型IP地址转换成ASCII码数组串
        /// </summary>
        /// <param name="arrIntIP"></param>
        /// <returns></returns>
        private static byte[] GetASCII_IPArr(byte[] arrIntIP)
        {
            List<byte> listByteIP = new List<byte>();

            try
            {
                List<char> listRecChar = new List<char>();
                foreach (byte bItem in arrIntIP)
                {
                    string strItem = bItem.ToString();
                    listRecChar.AddRange(strItem.ToCharArray());
                    listRecChar.Add('.');
                }
                listRecChar.RemoveAt(listRecChar.Count - 1);

                foreach (char cItem in listRecChar)
                {
                    listByteIP.Add((byte)cItem);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return listByteIP.ToArray();
        }

        /// <summary>
        /// 将整型数值转换成十六进制值并赋入源数组对应位置中
        /// </summary>
        /// <param name="arrSource">源数组</param>
        /// <param name="iStartIndex">起始位置</param>
        /// <param name="iEndIndex">结束位置</param>
        /// <param name="iTarget">目标整型值</param>
        /// <returns></returns>
        private static byte[] GetByteArrRes(byte[] arrSource, int iStartIndex, int iEndIndex, long iTarget)
        {
            if (iStartIndex > iEndIndex)
            {
                return null;
            }
            byte[] arrExchage = BitConverter.GetBytes(iTarget);
            for (int i = 0; i < iEndIndex - iStartIndex; i++)
            {
                arrSource[iStartIndex + i] = arrExchage[i];
            }
            if (iStartIndex == iEndIndex)
            {
                arrSource[iStartIndex] = arrExchage[0];
            }
            return arrSource;
        }

        #endregion

        #endregion

        #region 获取相关枚举值

        /// <summary>
        /// 用户类型转换为枚举值
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_UserType GetEnumUserType(int iVal)
        {
            YC_Enum_UserType eUserType = YC_Enum_UserType.Type_Null;

            switch (iVal)
            {
                case ((int)YC_Enum_UserType.Type_1):
                    {
                        eUserType = YC_Enum_UserType.Type_1;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_2):
                    {
                        eUserType = YC_Enum_UserType.Type_2;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_3):
                    {
                        eUserType = YC_Enum_UserType.Type_3;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_4):
                    {
                        eUserType = YC_Enum_UserType.Type_4;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_5):
                    {
                        eUserType = YC_Enum_UserType.Type_5;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_6):
                    {
                        eUserType = YC_Enum_UserType.Type_6;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_7):
                    {
                        eUserType = YC_Enum_UserType.Type_7;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_8):
                    {
                        eUserType = YC_Enum_UserType.Type_8;
                        break;
                    }
                case ((int)YC_Enum_UserType.Type_Null):
                    {
                        eUserType = YC_Enum_UserType.Type_Null;
                        break;
                    }
                default:
                    break;
            }

            return eUserType;
        }

        /// <summary>
        /// 系统卡类型转换成枚举值
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_SysType GetEnumSysType(int iVal)
        {
            YC_Enum_SysType eSysType = YC_Enum_SysType.EmptyOrNull;

            switch (iVal)
            {
                case ((int)YC_Enum_SysType.SystemOrTools):
                    {
                        eSysType = YC_Enum_SysType.SystemOrTools;
                        break;
                    }
                case ((int)YC_Enum_SysType.Doorcontrol):
                    {
                        eSysType = YC_Enum_SysType.Doorcontrol;
                        break;
                    }
                case ((int)YC_Enum_SysType.Consumption):
                    {
                        eSysType = YC_Enum_SysType.Consumption;
                        break;
                    }
                case ((int)YC_Enum_SysType.WaterSaving):
                    {
                        eSysType = YC_Enum_SysType.WaterSaving;
                        break;
                    }
                default:
                    break;
            }

            return eSysType;
        }

        /// <summary>
        /// 卡HF标签类型转换成枚举值
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_TagType GetEnumTagType(int iVal)
        {
            YC_Enum_TagType eTagType = YC_Enum_TagType.NULL;

            switch (iVal)
            {
                case ((int)YC_Enum_TagType.M1):
                    {
                        eTagType = YC_Enum_TagType.M1;
                        break;
                    }
                case ((int)YC_Enum_TagType.ML):
                    {
                        eTagType = YC_Enum_TagType.ML;
                        break;
                    }
                default:
                    break;
            }

            return eTagType;
        }

        /// <summary>
        /// 卡片类型转换成枚举值
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_CardType GetEnumCardType(int iVal)
        {
            YC_Enum_CardType eCardType = YC_Enum_CardType.Type_Null;

            switch (iVal)
            {
                case ((int)YC_Enum_CardType.Type_Collection):
                    {
                        eCardType = YC_Enum_CardType.Type_Collection;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_Empty):
                    {
                        eCardType = YC_Enum_CardType.Type_Empty;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_Encryption):
                    {
                        eCardType = YC_Enum_CardType.Type_Encryption;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_Initialization):
                    {
                        eCardType = YC_Enum_CardType.Type_Initialization;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_MacIDSetting):
                    {
                        eCardType = YC_Enum_CardType.Type_MacIDSetting;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_Operator):
                    {
                        eCardType = YC_Enum_CardType.Type_Operator;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_Query):
                    {
                        eCardType = YC_Enum_CardType.Type_Query;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_System):
                    {
                        eCardType = YC_Enum_CardType.Type_System;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_TimeSetting):
                    {
                        eCardType = YC_Enum_CardType.Type_TimeSetting;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_User):
                    {
                        eCardType = YC_Enum_CardType.Type_User;
                        break;
                    }
                case ((int)YC_Enum_CardType.Type_WaterSavingSetting):
                    {
                        eCardType = YC_Enum_CardType.Type_WaterSavingSetting;
                        break;
                    }
                default:
                    break;
            }

            return eCardType;
        }

        /// <summary>
        /// 节能水控数据采集模式转换成枚举值
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_CollectionMode GetEnumCollectionMode(int iVal)
        {
            YC_Enum_CollectionMode eCollectionMode = YC_Enum_CollectionMode.EmptyOrNull;

            switch (iVal)
            {
                case ((int)YC_Enum_CollectionMode.DetailCollecttionMode):
                    {
                        eCollectionMode = YC_Enum_CollectionMode.DetailCollecttionMode;
                        break;
                    }
                case ((int)YC_Enum_CollectionMode.TotalCollecttionMode):
                    {
                        eCollectionMode = YC_Enum_CollectionMode.TotalCollecttionMode;
                        break;
                    }
                default:
                    break;
            }

            return eCollectionMode;
        }

        /// <summary>
        /// 获取节能水控计费模式特定字符串对应的枚举值
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static YC_Enum_ConsumeMode GetEnumConsumeMode(string strVal)
        {
            YC_Enum_ConsumeMode eConsumeMode = YC_Enum_ConsumeMode.Null;
            foreach (YC_Enum_ConsumeMode consItem in System.Enum.GetValues(typeof(YC_Enum_ConsumeMode)))
            {
                if (strVal == consItem.ToString())
                {
                    eConsumeMode = consItem;
                    break;
                }
            }
            return eConsumeMode;
        }

        /// <summary>
        /// 获取函数返回值枚举
        /// </summary>
        /// <param name="iVal"></param>
        /// <returns></returns>
        public static YC_Enum_ReturnCode GetEnumReturnCode(int iVal)
        {
            YC_Enum_ReturnCode enumReturnCode = YC_Enum_ReturnCode.Correct;
            foreach (YC_Enum_ReturnCode rcItem in System.Enum.GetValues(typeof(YC_Enum_ReturnCode)))
            {
                if (iVal == (int)rcItem)
                {
                    enumReturnCode = rcItem;
                    break;
                }
            }
            return enumReturnCode;
        }

        /// <summary>
        /// 获取密码验证模式枚举
        /// </summary>
        /// <param name="iVerifyMode"></param>
        /// <returns></returns>
        public static YC_Enum_PwdVerifyMode GetEnumPwdVerifyMode(int iVerifyMode)
        {
            YC_Enum_PwdVerifyMode enumVerifyMode = YC_Enum_PwdVerifyMode.NullOrException;
            foreach (YC_Enum_PwdVerifyMode pvItem in System.Enum.GetValues(typeof(YC_Enum_PwdVerifyMode)))
            {
                if (iVerifyMode == (int)pvItem)
                {
                    enumVerifyMode = pvItem;
                    break;
                }
            }
            return enumVerifyMode;
        }

        #endregion
    }

    #region 宇川发卡参数结构体

    /// <summary>
    /// 节能费率参数，每个单位消费的金额=最小计量单位*0.2L*费率
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct JS_FEE_RATE
    {
        /// <summary>
        /// 消费方式，0x00:计时模式;0x11:计量模式;0x33:预扣费计时模式;0x44:预扣费计时模式;0x55:精准计时模式
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte ConsumeMode;

        /// <summary>
        /// 最小计费单位（每个单位表示6S或者1单位脉冲）
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte MinFeeUnit;

        /// <summary>
        /// 费率1
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate1;

        /// <summary>
        /// 费率2
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate2;

        /// <summary>
        /// 费率3
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate3;

        /// <summary>
        /// 费率4
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate4;

        /// <summary>
        /// 费率5
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate5;

        /// <summary>
        /// 费率6
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate6;

        /// <summary>
        /// 费率7
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate7;

        /// <summary>
        /// 费率8
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Rate8;
    }

    /// <summary>
    /// 节能高级参数
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct JS_AdvanPara
    {
        /// <summary>
        /// 一阶限制比率
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Grade_Rate1;

        /// <summary>
        /// 二阶限制比率
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Grade_Rate2;

        /// <summary>
        /// 三阶限制比率
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Grade_Rate3;

        /// <summary>
        /// 一阶限制值
        /// </summary>
        //[MarshalAs(UnmanagedType.U4)]
        public uint One_Grade;

        /// <summary>
        /// 二阶限制值
        /// </summary>
        //[MarshalAs(UnmanagedType.U4)]
        public uint Two_Grade;

        /// <summary>
        /// 当天最大消费次数
        /// </summary>
        //[MarshalAs(UnmanagedType.U4)]
        public uint Max_Consume_Times;

        /// <summary>
        /// 当天最大消费金额（传入值需为需求值的两倍）
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Max_Consume_Money;

        /// <summary>
        /// 当天免费时间（时，分，秒）
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Free_Time;

        /// <summary>
        /// 消费的间隔时间2字节（秒）
        /// </summary>
        //[MarshalAs(UnmanagedType.U4)]
        public uint Consume_Interval;

        /// <summary>
        /// 为单次的最大消费额（传入值需为需求值的两倍）
        /// </summary>
        //[MarshalAs(UnmanagedType.U4)]
        public uint Once_Max_Consume_Money;

        /// <summary>
        /// 允许使用的卡类（传入时，需先转换成该卡类的2的N次幂值）
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte En_Card_Type;

        /// <summary>
        /// 启用一机限卡模式(为0表示不启用)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Used_OneCard_Mode;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte En_Auto_Close;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte User_Card_Flag;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Auto_Write_Record_Flag;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Auto_Lock_Used_Card;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Used_Card_Overdraft_Flag;

        /// <summary>
        /// 保留(缺省为0x00)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Used_Card_Overdraft_Flag2;

        /// <summary>
        /// 保留(缺省为0x01)
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public byte Gk_Cala_Key;
    }

    /// <summary>
    /// 总额采集结构体
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]    
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RecordAllData
    {
        /// <summary>
        /// 设备总消费额
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Materiel_All_Money;

        /// <summary>
        ///  设备总使用次数
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Materiel_All_Times;

        /// <summary>
        /// 设备总流量S/L
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Materiel_All_Amount;

        /// <summary>
        /// 设备地址
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_Address;
    }

    /// <summary>
    /// 明细采集结构体
    /// </summary>
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct RecordDetailedData
    {
        /// <summary>
        /// 卡号
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_Number;

        /// <summary>
        /// 卡余额
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_RestMoney;

        /// <summary>
        /// 消费额
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_UsedMoney;

        /// <summary>
        /// 使用次数
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_UsedTimes;

        /// <summary>
        /// 设备地址
        /// </summary>
        //[MarshalAs(UnmanagedType.U8)]
        public uint Card_Address;

        /// <summary>
        /// 消费时间
        /// </summary>
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 12)]
        public string Card_Times;
    }

    #endregion

    #region 宇川消费机相关实体

    /// <summary>
    /// 宇川消费机系统参数
    /// </summary>
    public class YC_Pos_SysParams
    {
        public YC_Pos_SysParams()
        {
            this.iCardType = 0;
            this.dSetValue = 0;
            this.dMaxCardVal = 0;
            this.dMaxPosVal = 0;
            this.lIsNeedOpt = false;
            this.bWorkMode = 0;
            this.iMaxCountPerMeal = 0;
            this.iHoldParams = 0;
            this.iPosInterval = 0;
            this.dMaxPosValPerDay = 0;
            this.iMaxPosCountPerDay = 0;
            this.dNumPos_0 = 0;
            this.dNumPos_1 = 0;
            this.dNumPos_2 = 0;
            this.dNumPos_3 = 0;
            this.dNumPos_4 = 0;
            this.dNumPos_5 = 0;
            this.dNumPos_6 = 0;
            this.dNumPos_7 = 0;
            this.dNumPos_8 = 0;
            this.dNumPos_9 = 0;
            this.timeModeValInfo_1 = new TimeModeValueInfo();
            this.timeModeValInfo_2 = new TimeModeValueInfo();
            this.timeModeValInfo_3 = new TimeModeValueInfo();
            this.timeModeValInfo_4 = new TimeModeValueInfo();
            this.lIsUsedValidDate = false;
            this.tsReset1 = null;
            this.tsReset2 = null;
            this.tsReset3 = null;
            this.tsReset4 = null;
        }

        /// <summary>
        /// 允许使用的卡类，本系统共有32类卡
        /// </summary>
        public int iCardType { get; set; }
        /// <summary>
        /// 定值模式下定值金额。
        /// </summary>
        public decimal dSetValue { get; set; }
        /// <summary>
        /// 最大卡金额
        /// </summary>
        public decimal dMaxCardVal { get; set; }
        /// <summary>
        /// 最大消费金额
        /// </summary>
        public decimal dMaxPosVal { get; set; }
        /// <summary>
        /// 是否需要营业员开机
        /// </summary>
        public bool lIsNeedOpt { get; set; }
        /// <summary>
        /// 工作模式
        /// </summary>
        public byte bWorkMode { get; set; }
        /// <summary>
        /// 每餐最大消费次数（高级参数，设置为0时，为不限制）
        /// </summary>
        public int iMaxCountPerMeal { get; set; }
        /// <summary>
        /// 保留参数（高级参数：设置为0时，为不限制。）
        /// </summary>
        public int iHoldParams { get; set; }
        /// <summary>
        /// 两次消费间隔分钟数（高级参数：设置为0时，为不限制）
        /// </summary>
        public int iPosInterval { get; set; }
        /// <summary>
        /// 每天最大消费额度
        /// </summary>
        public decimal dMaxPosValPerDay { get; set; }
        /// <summary>
        /// 每天最大消费次数
        /// </summary>
        public int iMaxPosCountPerDay { get; set; }

        #region 编号模式消费的0-9对应消费值

        /// <summary>
        /// 编号模式的0编号值
        /// </summary>
        public decimal dNumPos_0 { get; set; }
        /// <summary>
        /// 编号模式的1编号值
        /// </summary>
        public decimal dNumPos_1 { get; set; }
        /// <summary>
        /// 编号模式的2编号值
        /// </summary>
        public decimal dNumPos_2 { get; set; }
        /// <summary>
        /// 编号模式的3编号值
        /// </summary>
        public decimal dNumPos_3 { get; set; }
        /// <summary>
        /// 编号模式的4编号值
        /// </summary>
        public decimal dNumPos_4 { get; set; }
        /// <summary>
        /// 编号模式的5编号值
        /// </summary>
        public decimal dNumPos_5 { get; set; }
        /// <summary>
        /// 编号模式的6编号值
        /// </summary>
        public decimal dNumPos_6 { get; set; }
        /// <summary>
        /// 编号模式的7编号值
        /// </summary>
        public decimal dNumPos_7 { get; set; }
        /// <summary>
        /// 编号模式的8编号值
        /// </summary>
        public decimal dNumPos_8 { get; set; }
        /// <summary>
        /// 编号模式的9编号值
        /// </summary>
        public decimal dNumPos_9 { get; set; }

        #endregion

        #region 时间段模式的4个时间段值

        /// <summary>
        /// 时段一（时间值只需要设置到时和分）
        /// </summary>
        public TimeModeValueInfo timeModeValInfo_1 { get; set; }

        /// <summary>
        /// 时段二（时间值只需要设置到时和分）
        /// </summary>
        public TimeModeValueInfo timeModeValInfo_2 { get; set; }

        /// <summary>
        /// 时段三（时间值只需要设置到时和分）
        /// </summary>
        public TimeModeValueInfo timeModeValInfo_3 { get; set; }

        /// <summary>
        /// 时段四（时间值只需要设置到时和分）
        /// </summary>
        public TimeModeValueInfo timeModeValInfo_4 { get; set; }

        #endregion

        /// <summary>
        /// 是否启用有效期
        /// </summary>
        public bool lIsUsedValidDate { get; set; }

        #region 定时重新启动，4段

        /// <summary>
        /// 定时重启时段一
        /// </summary>
        public TimeSpan? tsReset1 { set; get; }
        /// <summary>
        /// 定时重启时段二
        /// </summary>
        public TimeSpan? tsReset2 { set; get; }
        /// <summary>
        /// 定时重启时段三
        /// </summary>
        public TimeSpan? tsReset3 { set; get; }
        /// <summary>
        /// 定时重启时段四
        /// </summary>
        public TimeSpan? tsReset4 { set; get; }

        #endregion

        /// <summary>
        /// 自动屏幕保护时间（四个字节0xffffffff 为不启用屏保，2000表示1分钟）
        /// </summary>
        public int iScreenProtectTime { get; set; }

        /// <summary>
        /// 通讯超时时间
        /// </summary>
        public int iTimeCommOver { get; set; }
    }

    /// <summary>
    /// 时间段模式实体
    /// </summary>
    public class TimeModeValueInfo
    {
        public TimeModeValueInfo()
        {
            this.tsStart = TimeSpan.MaxValue;
            this.tsEnd = TimeSpan.MaxValue;
            this.dHoldVal = 0;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan tsStart { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan tsEnd { get; set; }
        /// <summary>
        /// 定值金额
        /// </summary>
        public decimal dHoldVal { get; set; }
    }

    /// <summary>
    /// 消费机数据采集实体
    /// </summary>
    public class PosRecord
    {
        public PosRecord()
        {
            this.iRecordIndex = 0;
            this.iUserIndex = 0;
            this.dCardBalance = 0;
            this.dCardConsum = 0;
            this.dtRecordTime = DateTime.MinValue;
            this.iCount = 0;
            this.iEmp_1 = 0;
            this.iEmp_2 = 0;
            this.iEmp_3 = 0;
            this.iMacAddr = 0;
            this.iEmp_4 = 0;
        }

        /// <summary>
        /// 记录流水号
        /// </summary>
        public int iRecordIndex { get; set; }
        /// <summary>
        /// 用户流水号
        /// </summary>
        public int iUserIndex { get; set; }
        /// <summary>
        /// 用户卡余额
        /// </summary>
        public decimal dCardBalance { get; set; }
        /// <summary>
        /// 用户消费额
        /// </summary>
        public decimal dCardConsum { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        public DateTime dtRecordTime { get; set; }
        /// <summary>
        /// 用户使用次数
        /// </summary>
        public int iCount { get; set; }
        /// <summary>
        /// 保留字段1，一字节
        /// </summary>
        public int iEmp_1 { get; set; }
        /// <summary>
        /// 保留字段2，一字节（消费记录此字节必须为0x66,如果不是请记录为错误记录）
        /// </summary>
        public int iEmp_2 { get; set; }
        /// <summary>
        /// 保留字段3，5字节
        /// </summary>
        public int iEmp_3 { get; set; }
        /// <summary>
        /// 设备机号
        /// </summary>
        public int iMacAddr { get; set; }
        /// <summary>
        /// 保留字段4，2字节
        /// </summary>
        public int iEmp_4 { get; set; }
    }

    /// <summary>
    /// 消费卡卡数据实体
    /// </summary>
    public class PosCardRecord
    {
        public PosCardRecord()
        {
            this.enumCardType = YC_Enum_CardType.Type_Null;
            this.iOptNum = 0;
            this.iSerNo = 0;
            this.strCardNo = string.Empty;
            this.enumUserType = YC_Enum_UserType.Type_Null;
            this.iCusId = 0;
            this.bChkSuml = false;
            this.dValue1 = 0.00M;
            this.dLastPay1 = 0.00M;
            this.iCount1 = 0;
            this.dConsAdd1 = 0.00M;
            this.dValue2 = 0.00M;
            this.dLastPay2 = 0.00M;
            this.iCount2 = 0;
            this.dConsAdd2 = 0.00M;
            this.iUseTerm = 0;
            this.iAddCount = 0;
            this.iWaitTime = 0;
            this.arrConfigPara = new List<byte>();
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        public YC_Enum_CardType enumCardType { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int iOptNum { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public int iSerNo { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string strCardNo { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public YC_Enum_UserType enumUserType { get; set; }
        /// <summary>
        /// 卡片固有ID号
        /// </summary>
        public ulong iCusId { get; set; }
        /// <summary>
        /// 用户卡使用块的校验状态
        /// </summary>
        public bool bChkSuml { get; set; }
        /// <summary>
        /// 卡金额
        /// </summary>
        public decimal dValue1 { get; set; }
        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal dLastPay1 { get; set; }
        /// <summary>
        /// 卡使用次数
        /// </summary>
        public int iCount1 { get; set; }
        /// <summary>
        /// 消费累加额
        /// </summary>
        public decimal dConsAdd1 { get; set; }
        /// <summary>
        /// 备用卡金额
        /// </summary>
        public decimal dValue2 { get; set; }
        /// <summary>
        /// 备用卡余额
        /// </summary>
        public decimal dLastPay2 { get; set; }
        /// <summary>
        /// 备用卡使用次数
        /// </summary>
        public int iCount2 { get; set; }
        /// <summary>
        /// 备用消费累加额
        /// </summary>
        public decimal dConsAdd2 { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public int iUseTerm { get; set; }
        /// <summary>
        /// 充值累计次数
        /// </summary>
        public int iAddCount { get; set; }
        /// <summary>
        /// 等卡时间
        /// </summary>
        public int iWaitTime { get; set; }
        /// <summary>
        /// 配置参数组
        /// </summary>
        public List<byte> arrConfigPara { get; set; }
    }

    /// <summary>
    /// 节能卡卡数据实体
    /// </summary>
    public class JsCardRecoed
    {
        public JsCardRecoed()
        {
            this.eumCardType = YC_Enum_CardType.Type_Null;
            this.iOpt = 0;
            this.iSerNo = 0;
            this.iCardSerNo = 0;
            this.strCardNo = string.Empty;
            this.dValue = 0.00M;
            this.iCount = 0;
            this.enumUserType = YC_Enum_UserType.Type_Null;
            this.iWaitTime = 0;
            this.arrConfigPara = new List<byte>();
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        public YC_Enum_CardType eumCardType { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int iOpt { get; set; }
        /// <summary>
        /// 用户卡流水号
        /// </summary>
        public int iSerNo { get; set; }
        /// <summary>
        /// 用户卡编号
        /// </summary>
        public string strCardNo { get; set; }
        /// <summary>
        /// 用户卡物理ID号
        /// </summary>
        public ulong iCardSerNo { get; set; }
        /// <summary>
        /// 用户卡余额
        /// </summary>
        public decimal dValue { get; set; }
        /// <summary>
        /// 用户卡使用次数
        /// </summary>
        public int iCount { get; set; }
        /// <summary>
        /// 用户卡卡类型
        /// </summary>
        public YC_Enum_UserType enumUserType { get; set; }
        /// <summary>
        /// 等卡时间，单位ms
        /// </summary>
        public int iWaitTime { get; set; }
        /// <summary>
        /// 配置参数组
        /// </summary>
        public List<byte> arrConfigPara { get; set; }
    }

    #endregion

    #region 枚举

    /// <summary>
    /// 用户卡类型
    /// </summary>
    public enum YC_Enum_UserType
    {
        Type_Null = -1,
        Type_1 = 1,
        Type_2 = 2,
        Type_3 = 3,
        Type_4 = 4,
        Type_5 = 5,
        Type_6 = 6,
        Type_7 = 7,
        Type_8 = 8
    }

    /// <summary>
    /// 卡系统类型
    /// </summary>
    public enum YC_Enum_SysType
    {
        /// <summary>
        /// 空值或异常值
        /// </summary>
        EmptyOrNull = -1,
        /// <summary>
        /// 系统或工具卡--0
        /// </summary>
        SystemOrTools = 0,
        /// <summary>
        /// 门禁卡--1
        /// </summary>
        Doorcontrol = 1,
        /// <summary>
        /// 消费卡--2
        /// </summary>
        Consumption = 2,
        /// <summary>
        /// 节水卡--3
        /// </summary>
        WaterSaving = 3
    }

    /// <summary>
    /// 卡片HF标签类型
    /// </summary>
    public enum YC_Enum_TagType
    {
        /// <summary>
        /// 空值或异常值
        /// </summary>
        NULL = -0x0001,
        /// <summary>
        ///    M1--0x0004
        /// </summary>
        M1 = 0x0004,
        /// <summary>
        /// ML--  0X0010
        /// </summary>
        ML = 0x0010
    }

    /// <summary>
    /// 卡片类型，0为用户卡、2为系统卡、1为操作员卡、3为初始化卡、4为白卡、5为节水设置卡、6为采集卡、7为加密卡、8为查询卡、9为机号设置卡、10为时间设置卡
    /// </summary>
    public enum YC_Enum_CardType
    {
        /// <summary>
        /// 空值或异常值
        /// </summary>
        Type_Null = -1,
        /// <summary>
        /// 用户卡--0
        /// </summary>
        Type_User = 0,
        /// <summary>
        /// 系统卡--1
        /// </summary>
        Type_System = 1,
        /// <summary>
        /// 操作员卡--2
        /// </summary>
        Type_Operator = 2,
        /// <summary>
        /// 初始化卡--3
        /// </summary>
        Type_Initialization = 3,
        /// <summary>
        /// 白卡--4
        /// </summary>
        Type_Empty = 4,
        /// <summary>
        /// 节水设置卡--5
        /// </summary>
        Type_WaterSavingSetting = 5,
        /// <summary>
        /// 采集卡--6
        /// </summary>
        Type_Collection = 6,
        /// <summary>
        /// 加密卡--7
        /// </summary>
        Type_Encryption = 7,
        /// <summary>
        /// 查询卡--8
        /// </summary>
        Type_Query = 8,
        /// <summary>
        /// 机号设置卡--9
        /// </summary>
        Type_MacIDSetting = 9,
        /// <summary>
        /// 时间设置卡--10
        /// </summary>
        Type_TimeSetting = 10
    }

    /// <summary>
    /// 采集卡的模式，1为明细采集卡，0为总额采集卡
    /// </summary>
    public enum YC_Enum_CollectionMode
    {
        /// <summary>
        /// 空值或异常值
        /// </summary>
        EmptyOrNull = -1,
        /// <summary>
        /// 明细采集卡--1
        /// </summary>
        DetailCollecttionMode = 1,
        /// <summary>
        /// 总额采集卡--0
        /// </summary>
        TotalCollecttionMode = 0
    }

    /// <summary>
    /// 节能水控计费模式,0x00:计时模式;0x11:计量模式;0x33:预扣费计时模式;0x44:预扣费计量模式;0x55:精准计时模式
    /// </summary>
    public enum YC_Enum_ConsumeMode
    {
        /// <summary>
        /// 错误状态
        /// </summary>
        Null = -1,
        /// <summary>
        /// 计时模式
        /// </summary>
        TimeMode = 0x00,
        /// <summary>
        /// 计量模式
        /// </summary>
        MeasureMode = 0x11,
        /// <summary>
        /// 预扣费计时模式
        /// </summary>
        WithholdingTimeMode = 0x33,
        /// <summary>
        /// 预扣费计量模式
        /// </summary>
        WithholdingMeasureMode = 0x44,
        /// <summary>
        /// 精准计时模式
        /// </summary>
        PreciseTimeMode = 0x55
    }

    /// <summary>
    /// 函数返回代码含义
    /// </summary>
    public enum YC_Enum_ReturnCode
    {
        /// <summary>
        /// 正确
        /// </summary>
        Correct = 0,
        /// <summary>
        /// 串口错
        /// </summary>
        COMFalse = -1,
        /// <summary>
        /// 读写器通讯错
        /// </summary>
        CommunicationFail = -2,
        /// <summary>
        /// 没有消费系统授权信息
        /// </summary>
        NonePosSysAuthorizeInfos = -3,
        /// <summary>
        /// 没有门禁系统授权信息
        /// </summary>
        NoneDoorSysAuthorizeInfos = -4,
        /// <summary>
        /// 参数错
        /// </summary>
        ParamsFalse = -5,
        /// <summary>
        /// 不能再初始化系统卡
        /// </summary>
        CanNotInitSysCard = -6,
        /// <summary>
        /// 找不到卡
        /// </summary>
        CardMissed = -7,
        /// <summary>
        /// 找不到门禁授权卡
        /// </summary>
        DoorAuthorizeCardMissed = -8,
        /// <summary>
        /// 找不到消费授权卡
        /// </summary>
        PosAuthorizeCardMissed = -9,
        /// <summary>
        /// 系统卡错误
        /// </summary>
        SysCardFail = -10,
        /// <summary>
        /// 用户卡错误
        /// </summary>
        UserCardFail = -11,
        /// <summary>
        /// 读卡错
        /// </summary>
        ReadCardFail = -12,
        /// <summary>
        /// 写卡错
        /// </summary>
        WriteCardFail = -13,
        /// <summary>
        /// 建立授权文档错误
        /// </summary>
        BuildAuthorizeFileFail = -14,
        /// <summary>
        /// 不可识别的卡
        /// </summary>
        UnrecognizedCrad = -15
    }

    /// <summary>
    /// 密码验证模式
    /// </summary>
    public enum YC_Enum_PwdVerifyMode
    {
        /// <summary>
        /// 验证A密码
        /// </summary>
        A_Mode = 0,
        /// <summary>
        /// 验证B密码
        /// </summary>
        B_Mode = 1,
        /// <summary>
        /// 异常值
        /// </summary>
        NullOrException = -1
    }

    #endregion
}
