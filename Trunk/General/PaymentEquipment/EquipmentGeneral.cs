using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentEquipment.Enum;
using System.Windows.Forms;
using System.Drawing;

namespace PaymentEquipment
{
    /// <summary>
    /// 消费设备通用函数
    /// </summary>
    public class EquipmentGeneral
    {
        /// <summary>
        /// 获取所有扇区及块号列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<byte, List<byte>> GetCardSectionPieceList()
        {
            Dictionary<byte, List<byte>> dicSP = new Dictionary<byte, List<byte>>();
            for (byte i = 0; i < 16; i++)
            {
                List<byte> listPiece = new List<byte>();
                listPiece.Add((byte)(i * 4));
                listPiece.Add((byte)(i * 4 + 1));
                listPiece.Add((byte)(i * 4 + 2));
                listPiece.Add((byte)(i * 4 + 3));
                dicSP.Add(i, listPiece);
            }

            return dicSP;
        }

        /// <summary>
        /// 二进制转16进制
        /// </summary>
        /// <param name="p_strBinaryCode"></param>
        /// <returns></returns>
        public static string BinToHex(string p_strBinaryCode)
        {
            string l_strReturn = Convert.ToString(Convert.ToInt32(p_strBinaryCode, 2), 16);
            l_strReturn = (l_strReturn.Length == 1 ? "0" : "") + l_strReturn;
            return l_strReturn.ToUpper();
        }

        /// <summary>
        /// 16进制转字符
        /// </summary>
        /// <param name="p_strTarget"></param>
        /// <returns></returns>
        public static string HexToStr(string p_strTarget)
        {
            string l_strReturn = "";

            byte[] l_arrByte = new byte[p_strTarget.Length / 2];

            for (int i = 0; i < l_arrByte.Length; i++)
            {
                l_arrByte[i] = Convert.ToByte(p_strTarget.Substring(i * 2, 2), 16);
            }

            l_strReturn = System.Text.Encoding.Default.GetString(l_arrByte);

            return l_strReturn;
        }

        /// <summary>
        /// 字符转16进制
        /// </summary>
        /// <param name="p_strTarget"></param>
        /// <returns></returns>
        public static string StrToHex(string p_strTarget)
        {
            byte[] ba = System.Text.ASCIIEncoding.Default.GetBytes(p_strTarget);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(b.ToString("x"));
            }

            string l_strReturn = sb.ToString();

            return l_strReturn.ToUpper();
        }

        /// <summary>
        /// 数字转16进制
        /// </summary>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        public static string IntToHex(int p_intNum)
        {
            string l_strTemp = Convert.ToString(p_intNum, 16);
            return ((l_strTemp.Length == 1 ? "0" : "") + l_strTemp).ToUpper();
        }

        /// <summary>
        /// 数字转16进制
        /// </summary>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        public static string IntToHex(int p_intNum, int p_intLength)
        {
            string l_strPreFix = "000000000000";
            string l_strTemp = Convert.ToString(p_intNum, 16);

            if (l_strTemp.Length < p_intLength)
                l_strTemp = l_strPreFix.Substring(0, p_intLength - l_strTemp.Length) + l_strTemp;

            return l_strTemp.ToUpper();
        }

        public static string IntToHexInSorting(int p_intNum, int p_intLength)
        {
            string l_strPreFix = "000000000000";
            string l_strTemp = Convert.ToString(p_intNum, 16);
            string l_strReturn = "";

            if (l_strTemp.Length < p_intLength)
                l_strTemp = l_strPreFix.Substring(0, p_intLength - l_strTemp.Length) + l_strTemp;

            for (int i = l_strTemp.Length; i > 0; i = i - 2)
            {
                if (i > 1)
                    l_strReturn = l_strReturn + l_strTemp.Substring(i - 2, 2);
                else
                    l_strReturn = l_strReturn + l_strTemp.Substring(0, 1);
            }

            return l_strReturn.ToUpper();
        }

        /// <summary>
        /// 把16进制的钱转为数字
        /// </summary>
        /// <param name="p_strCode"></param>
        /// <returns></returns>
        public static decimal HexMoneyToDouble(string p_strCode)
        {
            return (decimal)(HexToInt(p_strCode) / 100.0);
        }

        /// <summary>
        /// 把16进制转为数字
        /// </summary>
        /// <param name="p_strCode"></param>
        /// <returns></returns>
        public static int HexToInt(string p_strCode)
        {
            var l_strCode = "";
            for (int i = 0; i < p_strCode.Length; i = i + 2)
            {
                l_strCode = p_strCode.Substring(i, 2) + l_strCode;
            }
            return Convert.ToInt32(l_strCode, 16);
        }

        /// <summary>
        /// 检查优卡特消费功能
        /// </summary>
        /// <param name="p_enmType"></param>
        /// <returns></returns>
        public static bool CheckJocatFunction(PaymentEnumMaster.enmFunctionType p_enmType, string p_strFunctionCode)
        {
            //转换成二进制
            string l_strCode = Convert.ToString(Convert.ToInt32(p_strFunctionCode, 16), 2).PadLeft(16, '0');

            return l_strCode.Substring(l_strCode.Length - (int)(p_enmType) - 1, 1) == "1" ? true : false;
        }

        /// <summary>
        /// 获取优卡特消费功能CheckBox列表
        /// </summary>
        /// <param name="p_strFunctionCode">功能代码，可为二进制的16位数据，或十六进制的4位数据</param>
        /// <returns></returns>
        public static List<CheckBox> GetJocatFunctionCbx(string strFunctionCode)
        {
            int iLocationX = 10;
            int iLocationY = 10;
            List<CheckBox> listFunc = new List<CheckBox>();
            if (string.IsNullOrEmpty(strFunctionCode))
            {
                return null;
            }
            if (strFunctionCode.Length != 4 && strFunctionCode.Length != 16)
            {
                return null;
            }

            try
            {
                string l_strCode = strFunctionCode;
                if (l_strCode.Length == 16)
                {
                    l_strCode = EquipmentGeneral.BinToHex(l_strCode).PadLeft(4, '0');
                }

                foreach (PaymentEnumMaster.enmFunctionType item in System.Enum.GetValues(typeof(PaymentEnumMaster.enmFunctionType)))
                {
                    bool res = CheckJocatFunction(item, l_strCode);
                    CheckBox cbxItem = new CheckBox();
                    cbxItem.Name = "cbx" + item.ToString();
                    cbxItem.Text = PaymentEnumMaster.GetFunctionName(item);
                    cbxItem.Checked = res;
                    cbxItem.Tag = cbxItem.TabIndex = (int)item;
                    cbxItem.Location = new System.Drawing.Point(iLocationX, iLocationY);
                    cbxItem.Enabled = false;
                    cbxItem.AutoSize = true;
                    iLocationY += 30;
                    listFunc.Add(cbxItem);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listFunc;
        }

        /// <summary>
        /// 获得反码
        /// </summary>
        /// <param name="strSourceCode">原码</param>
        /// <returns></returns>
        public static string GetComplement(string strSourceCode)
        {
            string strComplement = string.Empty;

            char[] arrChar = strSourceCode.ToCharArray();
            foreach (char cItem in arrChar)
            {
                int iGet = 15 - Convert.ToInt32(cItem.ToString(), 16);
                strComplement += Convert.ToString(iGet, 16);
            }

            return strComplement;
        }
    }
}
