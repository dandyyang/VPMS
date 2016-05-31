using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ReaderEquipment
{
    /// <summary>
    /// 读写器通常程序集
    /// </summary>
    public static class ReaderGeneral
    {
        /// <summary>
        /// 将指令中每条C2.......C3数据抽取成独立记录
        /// </summary>
        public static List<string> HandleSourceDataToList(string data)
        {
            char[] cArrData = data.ToCharArray();
            List<string> listStrData = new List<string>();
            string tempStr = string.Empty;
            for (int i = 0; i < cArrData.Length; i++)
            {
                // 判断是否为数据头桢C2
                if (i > 1 && i != cArrData.Length - 1 && cArrData[i] == 'C' && cArrData[i + 1] == '2' && cArrData[i - 2] == 'C' && cArrData[i - 1] == '3')
                {
                    tempStr = string.Empty;
                    tempStr += cArrData[i].ToString();
                }
                // 判断是否为数据尾桢C3
                else if (i > 0 && i < cArrData.Length - 2 && cArrData[i] == '3' && cArrData[i - 1] == 'C' && cArrData[i + 2] == '2' && cArrData[i + 1] == 'C')
                {
                    tempStr += cArrData[i].ToString();
                    if (!listStrData.Contains(tempStr))
                    {
                        listStrData.Add(tempStr);
                    }
                }
                //判断是否为最后一条数据的尾桢C3
                else if (i == cArrData.Length - 1 && cArrData[i] == '3' && cArrData[i - 1] == 'C')
                {
                    tempStr += cArrData[i].ToString();
                    //if (tempStr.Length == 62)
                    if (!listStrData.Contains(tempStr))
                    {
                        listStrData.Add(tempStr);
                    }
                }
                else
                {
                    tempStr += cArrData[i].ToString();
                }
            }
            return listStrData;
        }

        public static List<string> HandleSourceDataToList(byte[] arrByte)
        {
            if (arrByte == null)
            {
                return null;
            }
            List<string> listRec = new List<string>();
            try
            {
                string strTmpRec = string.Empty;
                //string strZero = string.Empty;
                for (int i = 0; i < arrByte.Length; i++)
                {
                    string strPreItem = string.Empty;
                    string strCurItem = Convert.ToString(arrByte[i], 16).ToUpper().PadLeft(2, '0');
                    string strNextItem = string.Empty;

                    #region 解决长零问题

                    //if (strCurItem == string.Empty.PadLeft(2, '0'))
                    //{
                    //    strZero += strCurItem;
                    //    if (strZero.Length >= 10)
                    //    {
                    //        strZero = string.Empty;
                    //        if (strTmpRec.Length > 4 && strTmpRec.Substring(0, 2) == "C2" && strTmpRec.Substring(strTmpRec.Length - 2, 2) == "C3")
                    //        {
                    //            if (!listRec.Contains(strTmpRec))
                    //            {
                    //                listRec.Add(strTmpRec);
                    //            }
                    //            strTmpRec = string.Empty;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(strZero))
                    //    {
                    //        strTmpRec += strZero;
                    //        strZero = string.Empty;
                    //    }
                    //}
                    //if (strZero != string.Empty)
                    //{
                    //    continue;
                    //}

                    #endregion

                    if (i > 0)
                    {
                        strPreItem = Convert.ToString(arrByte[i - 1], 16).ToUpper().PadLeft(2, '0');
                    }
                    if (i < arrByte.Length - 1)
                    {
                        strNextItem = Convert.ToString(arrByte[i + 1], 16).ToUpper().PadLeft(2, '0');
                    }

                    if (i > 0 && strCurItem == "C2")//判断是否为桢头
                    {
                        if (strPreItem == "C3")
                        {
                            strTmpRec = string.Empty;
                            strTmpRec += strCurItem;
                        }
                        else
                        {
                            strTmpRec += strCurItem;
                        }
                    }
                    else if (strCurItem == "C3" && i < arrByte.Length - 1)//判断是否为桢尾
                    {
                        if (strTmpRec.Length == 60 || strTmpRec.Length == 12 || strTmpRec.Length == 26)
                        {
                            strTmpRec += strCurItem;
                            if (!listRec.Contains(strTmpRec))
                            {
                                listRec.Add(strTmpRec);
                            }
                            strTmpRec = string.Empty;
                        }
                        else
                        {
                            strTmpRec += strCurItem;
                        }
                    }
                    else if (strCurItem == "C3" && i == arrByte.Length - 1)
                    {
                        strTmpRec += strCurItem;
                        if (!listRec.Contains(strTmpRec))
                        {
                            listRec.Add(strTmpRec);
                        }
                    }
                    else
                    {
                        strTmpRec += strCurItem;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRec;
        }

        /// <summary>
        /// 将指令中每条C2.......C3数据抽取成独立记录
        /// </summary>
        /// <param name="data">COM口返回的原始数据源</param>
        /// <param name="sigleDataLength">处理后单条数据的长度</param>
        /// <returns></returns>
        public static List<string> HandleSourceDataToList(string data, int sigleDataLength)
        {
            char[] cArrData = data.ToCharArray();
            List<string> listStrData = new List<string>();
            string tempStr = string.Empty;
            for (int i = 0; i < cArrData.Length; i++)
            {
                // 判断是否为数据头桢C2
                if (i > 1 && i != cArrData.Length - 1 && cArrData[i] == 'C' && cArrData[i + 1] == '2' && cArrData[i - 2] == 'C' && cArrData[i - 1] == '3')
                {
                    if (tempStr.Length == sigleDataLength)
                    {
                        tempStr = string.Empty;
                    }

                    tempStr += cArrData[i].ToString();
                }
                // 判断是否为数据尾桢C3
                else if (i > 0 && i < cArrData.Length - 2 && cArrData[i] == '3' && cArrData[i - 1] == 'C' && cArrData[i + 2] == '2' && cArrData[i + 1] == 'C')
                {
                    tempStr += cArrData[i].ToString();
                    //if (tempStr.Length == 62)
                    if (tempStr.Length == sigleDataLength)
                    {
                        if (!listStrData.Contains(tempStr))
                        {
                            listStrData.Add(tempStr);
                        }
                    }
                }
                //判断是否为最后一条数据的尾桢C3
                else if (i == cArrData.Length - 1 && cArrData[i] == '3' && cArrData[i - 1] == 'C')
                {
                    tempStr += cArrData[i].ToString();
                    //if (tempStr.Length == 62)
                    if (tempStr.Length == sigleDataLength)
                    {
                        if (!listStrData.Contains(tempStr))
                        {
                            listStrData.Add(tempStr);
                        }
                    }
                }
                else
                {
                    tempStr += cArrData[i].ToString();
                }
            }
            return listStrData;
        }

        /// <summary>
        /// 计算校验位
        /// </summary>
        /// <param name="pData">要计算的数据</param>
        /// <param name="hiCRC">高位校验位</param>
        /// <param name="loCRC">低位校验位</param>
        public static void CRC16_CCITT(byte[] pData, out byte hiCRC, out byte loCRC)
        {
            try
            {
                ushort crc = 0xc78c;
                //减少一个结束位，两个校验位
                for (int i = 0; i < pData.Length - 3; i++)
                {
                    ushort dd = pData[i];
                    crc = (ushort)((crc << 8) ^ (ushort)Function.Functions.CRCTab[(crc >> 8) ^ dd]);
                }

                byte[] bycrc = BitConverter.GetBytes(crc);

                hiCRC = bycrc[0];
                loCRC = bycrc[1];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 将十六进制日期转为十进制日期
        /// </summary>
        /// <param name="strhex"></param>
        /// <returns></returns>
        public static string ChangetoDatebyHexstring(string strhex)
        {
            string Rtv = "";
            try
            {
                //年月日时分秒
                if (strhex.Trim().Length > 6)
                {
                    string year = "20" + Convert.ToInt64(strhex.Substring(0, 2), 16).ToString();
                    string month = Convert.ToInt64(strhex.Substring(2, 2), 16).ToString();
                    string day = Convert.ToInt64(strhex.Substring(4, 2), 16).ToString();
                    string hours = Convert.ToInt64(strhex.Substring(6, 2), 16).ToString();
                    string minute = Convert.ToInt64(strhex.Substring(8, 2), 16).ToString();
                    string second = Convert.ToInt64(strhex.Substring(10, 2), 16).ToString();

                    Rtv = year + "/" + month + "/" + day + " " + hours + ":" + minute + ":" + second;
                }
                else //时分秒
                {
                    string hours = Convert.ToInt64(strhex.Substring(0, 2), 16).ToString();
                    string minute = Convert.ToInt64(strhex.Substring(2, 2), 16).ToString();
                    string second = Convert.ToInt64(strhex.Substring(4, 2), 16).ToString();
                    Rtv = hours + ":" + minute + ":" + second;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return Rtv;
        }

        public static string ChangeHexArrayToString(byte[] arrDataSource)
        {
            if (arrDataSource == null)
            {
                return null;
            }
            if (arrDataSource.Length == 0)
            {
                return string.Empty;
            }

            string strReturn = string.Empty;
            string strZero = string.Empty;
            for (int i = 0; i < arrDataSource.Length - 1; i++)
            {
                if (arrDataSource[i] == 0)
                {
                    strZero += "00";
                }
                else
                {
                    if (!string.IsNullOrEmpty(strZero))
                    {
                        strReturn += strZero;
                        strZero = string.Empty;
                    }

                    strReturn += Convert.ToString(arrDataSource[i], 16).ToUpper().PadLeft(2, '0');
                }
            }
            return strReturn;
        }

        /// <summary>
        /// 显示命令行对话框的文本
        /// </summary>
        /// <param name="strContent"></param>
        public static void ConsoleWrite(string strContent)
        {
            string strIsUsed = ConfigurationSettings.AppSettings["IsShowCMD"];
            if (strIsUsed == "Y")
            {
                Console.WriteLine(strContent);
            }
        }
    }
}
