using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class CardIDConverter
    {
        /// <summary>
        /// 转换查询机读卡器（黑色盒子）读取的卡ID为IC卡发卡器(YSD-36)读取的卡ID
        /// </summary>
        /// <param name="tagIDHex"></param>
        /// <returns></returns>
        public static string ConvertReaderCardID(string tagIDHex)
        {
            if (tagIDHex.Length != 10)
            {
                return string.Empty;
            }
            try
            {
                string res = string.Empty;
                string hexStr = Convert.ToString(Convert.ToInt64(tagIDHex), 16);
                string str = hexStr.Substring(2, 6);
                res = Convert.ToInt32(str.Substring(0, 2), 16).ToString();
                string tempStr = Convert.ToInt32(str.Substring(2, 4), 16).ToString();
                if (tempStr.Length<5)
                {
                    tempStr = "0" + tempStr;
                }
                if (tempStr.Length < 5)
                {
                    tempStr = "0" + tempStr;
                }
                if (tempStr.Length < 5)
                {
                    tempStr = "0" + tempStr;
                }
                res += tempStr;
                //res = res.PadLeft(8, '0');
                return res;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换手持机读取的卡ID为IC卡发卡器(YSD-36)读取的卡ID
        /// </summary>
        /// <param name="tagIDHex"></param>
        /// <returns></returns>
        public static string ConvertHandheldCardID(string tagIDHex)
        {
            if (tagIDHex.Length != 8)
            {
                return string.Empty;
            }
            string res = string.Empty;
            string[] temps = new string[4];
            temps[0] = tagIDHex.Substring(0, 2);
            temps[1] = tagIDHex.Substring(2, 2);
            temps[2] = tagIDHex.Substring(4, 2);
            temps[3] = tagIDHex.Substring(6, 2);
            //string hexStr = temps[3] + temps[2] + temps[1] + temps[0];
            //string str = hexStr.Substring(2, 6);    
            string str = temps[2] + temps[1] + temps[0];
            res = Convert.ToInt32(str.Substring(0, 2), 16).ToString();
            res += Convert.ToInt32(str.Substring(2, 4), 16).ToString();
            //res = res.PadLeft(8, '0');

            return res;
        }

        /// <summary>
        /// 把IC卡出厂ID（32位）转换为微耕（8位）ID
        /// </summary>
        /// <param name="p_strMacID"></param>
        /// <returns></returns>
        public static string ConvertICMacIDToWeiGengID(string p_strMacID)
        {
            string l_strReturn = "";

            string l_strPart1 = p_strMacID.Substring(0, 2);
            string l_strPart2 = p_strMacID.Substring(2, 2);
            string l_strPart3 = p_strMacID.Substring(4, 2);

            l_strReturn = Convert.ToInt32(l_strPart3, 16).ToString() + (100000 + Convert.ToInt32((l_strPart2 + l_strPart1), 16)).ToString().Substring(1, 5);

            string l_strPreFix = "00000000";

            l_strReturn = l_strPreFix.Substring(0, l_strPreFix.Length - l_strReturn.Length) + l_strReturn;            

            return l_strReturn;
        }
    }
}