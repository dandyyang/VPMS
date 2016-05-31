using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using Common.DataTypeVerify;
using Common.Entity;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common
{
    public class General
    {
        public delegate void SetProBar1ValCallback(int val);

        public static event SetProBar1ValCallback SetProBarValEventHandler;

        /// <summary>
        /// 日志内容记录容器
        /// </summary>
        private List<string> c_listLogContent;

        /// <summary>
        /// 日志逻辑
        /// </summary>
        private SystemLog.SystemLog c_sysLog;

        public General()
        {

        }

        /// <summary>
        /// 初始化日志记录逻辑专用构造函数
        /// </summary>
        /// <param name="l_sysLog"></param>
        public General(SystemLog.SystemLog l_sysLog)
        {
            this.c_listLogContent = new List<string>();
            this.c_sysLog = l_sysLog;
        }

        /// <summary>
        /// 获得本机上的所有Comm口的Port值列表
        /// </summary>
        /// <returns></returns>
        public static CommInfo[] GetCommInfos()
        {
            Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
            List<CommInfo> portList = null;
            CommInfo commInfo = null;

            if (pc != null)
            {
                portList = new List<CommInfo>();

                foreach (string s in pc.Ports.SerialPortNames)
                {
                    commInfo = new CommInfo();
                    string portIndex = string.Empty;
                    short port = 0;

                    portIndex = s.Replace("COM", string.Empty);

                    try
                    {
                        port = Convert.ToInt16(portIndex);
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }

                    commInfo.CommPort = port;

                    portList.Add(commInfo);
                }
            }

            CommInfo[] commInfos = null;

            if (portList != null && portList.Count > 0)
            {
                Reverser<CommInfo> reverser = new Reverser<CommInfo>(commInfo.GetType(), "CommPort", ReverserInfo.Direction.ASC);

                portList.Sort(reverser);

                commInfos = portList.ToArray();
            }
            else if (portList != null && portList.Count == 0)
            {
                CommInfo commInfoDefault = new CommInfo();
                commInfoDefault.CommPort = 1;
                portList.Add(commInfoDefault);
                commInfos = portList.ToArray();
            }

            return commInfos;

        }

        /// <summary>
        /// 数据类型验证
        /// </summary>
        /// <param name="verifyInfo">数据类型信息</param>
        /// <returns></returns>
        public static bool VerifyDataType(DataTypeVerifyInfo verifyInfo)
        {
            int textLength = verifyInfo.Text.Length;
            bool isMatch = false;
            switch (verifyInfo.Type)
            {
                case DataType.ChinaChar:		//中文字符
                    if (textLength == 0)
                    {
                        isMatch = false;
                    }
                    else
                    {
                        int i = System.Text.Encoding.Default.GetBytes(verifyInfo.Text).Length;
                        if (textLength != i)
                        {
                            isMatch = true;
                        }
                        else
                        {
                            isMatch = false;
                        }
                    }
                    break;
                case DataType.NumberChar:		//數字
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Decimal Number = Convert.ToDecimal(verifyInfo.Text);
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.DateChar:		//日期
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            DateTime Number = Convert.ToDateTime(verifyInfo.Text);
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.EnglishChar:	//英文字符(不含符號)
                    CharEnumerator cha = verifyInfo.Text.GetEnumerator();
                    isMatch = true;
                    while (cha.MoveNext())
                    {
                        if ('a' <= cha.Current && cha.Current <= 'z' || 'A' <= cha.Current && cha.Current <= 'Z')
                        {

                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    break;
                case DataType.NumeralString:	//數字字符(0-9)
                    CharEnumerator chr = verifyInfo.Text.GetEnumerator();
                    isMatch = true;
                    while (chr.MoveNext())
                    {
                        if ('0' <= chr.Current && chr.Current <= '9')
                        {

                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    break;
                case DataType.PlusNumberChar:		//正的數字
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Decimal Number = Convert.ToDecimal(verifyInfo.Text);
                            if (Number < 0)
                                isMatch = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.PlusInt32:		//正的數字32
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Int32 Number = Convert.ToInt32(verifyInfo.Text);
                            Decimal sNumber = Convert.ToDecimal(verifyInfo.Text);
                            if (Number != sNumber)
                                isMatch = false;
                            if (Number < 0)
                                isMatch = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.PlusInt64:		//正的數字64
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Int64 Number = Convert.ToInt64(verifyInfo.Text);
                            Decimal sNumber = Convert.ToDecimal(verifyInfo.Text);
                            if (Number != sNumber)
                                isMatch = false;
                            if (Number < 0)
                                isMatch = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.Int32:		//數字32
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Int32 Number = Convert.ToInt32(verifyInfo.Text);
                            Decimal sNumber = Convert.ToDecimal(verifyInfo.Text);
                            if (Number != sNumber)
                                isMatch = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.Int64:		//數字64
                    try
                    {
                        isMatch = true;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            Int64 Number = Convert.ToInt64(verifyInfo.Text);
                            Decimal sNumber = Convert.ToDecimal(verifyInfo.Text);
                            if (Number != sNumber)
                                isMatch = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        isMatch = false;
                    }
                    break;
                case DataType.Nembers:   //正整数 + 0
                    try
                    {
                        isMatch = false;

                        if (verifyInfo.Text.Trim() != string.Empty)
                        {

                            //isMatch = IsRegEx(@"^[0-9]*[1-9][0-9]*$", verifyInfo.Text);
                            isMatch = IsRegEx(@"^\d+$", verifyInfo.Text);

                        }
                    }
                    catch (Exception Ex)
                    {

                        isMatch = false;
                    }
                    break;
                case DataType.Price:  //正浮点数 + 0
                    try
                    {
                        isMatch = false;
                        if (verifyInfo.Text.Trim() != string.Empty)
                        {
                            isMatch = IsRegEx(@"^\d+(\.\d+)?$", verifyInfo.Text);
                        }
                    }
                    catch (Exception Ex)
                    {

                        isMatch = false;
                    }

                    break;
                default:

                    break;
            }

            return isMatch;
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="regExValue">正规表达式</param>
        /// <param name="itemValue">验证内容</param>
        /// <returns></returns>
        public static bool IsRegEx(string regExValue, string itemValue)
        {
            try
            {
                Regex regex = new System.Text.RegularExpressions.Regex(regExValue);
                if (regex.IsMatch(itemValue)) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {

            }

        }

        /// <summary>
        /// 数据类型验证
        /// </summary>
        /// <param name="strText">验证内容</param>
        /// <param name="dataType">验证类型</param>
        /// <returns></returns>
        public static DataTypeVerifyResultInfo VerifyDataType(string strText, DataType dataType)
        {
            bool isMatch = false;
            string messageText = string.Empty;
            DataTypeVerifyInfo dataValue = new DataTypeVerifyInfo();
            dataValue.Text = strText;
            dataValue.Type = dataType;

            try
            {
                isMatch = General.VerifyDataType(dataValue);
            }
            catch (Exception Ex)
            {
                isMatch = false;
                throw (new Exception(Ex.Message));
            }

            switch (dataType)
            {
                case DataType.ChinaChar:
                    if (isMatch)
                    {
                        messageText = "包含有中文字符!";
                        isMatch = false;
                    }
                    else
                    {
                        isMatch = true;
                    }
                    break;

                case DataType.DateChar:
                    if (!isMatch)
                        messageText = "请输入日期型字符串!";
                    break;

                case DataType.NumberChar:
                    if (!isMatch)
                        messageText = "请输入数字型字符!";
                    break;

                case DataType.EnglishChar:
                    if (!isMatch)
                        messageText = "请输入英文字符!";
                    break;

                case DataType.NumeralString:
                    if (!isMatch)
                        messageText = "请输入数字0-9!";

                    break;

                case DataType.PlusNumberChar:
                    if (!isMatch)
                        messageText = "请输入正整数!";

                    break;
                case DataType.PlusInt32:
                    if (!isMatch)
                        messageText = "请输入32位正整数!";
                    break;
                case DataType.Int32:
                    if (!isMatch)
                        messageText = "请输入32位数字!";
                    break;

                case DataType.Price:
                    if (!isMatch)
                    {
                        messageText = "请输入0或正数!";
                    }
                    break;
                case DataType.Nembers:

                    if (!isMatch)
                    {
                        messageText = "请输入0或正整数!";
                    }
                    break;
                default:
                    break;

            }

            DataTypeVerifyResultInfo verifyResult = new DataTypeVerifyResultInfo();

            verifyResult.IsMatch = isMatch;
            verifyResult.Message = messageText;

            return verifyResult;
        }

        /// <summary>
        /// 复制对象的属性值
        /// </summary>
        /// <typeparam name="TSource">数据源对象类型</typeparam>
        /// <typeparam name="TResult">返回结果数据对象类型</typeparam>
        /// <param name="sourceObject">数据源对象</param>
        /// <returns></returns>
        public static TResult CopyObjectValue<TSource, TResult>(TSource sourceObject)
        {
            TResult infoObject = default(TResult);

            if (sourceObject == null)
            {
                return infoObject;
            }

            object propertyValueObject;
            string classPropertyName = string.Empty;
            string classPropertyValue = string.Empty;
            string dataBaseColumnName = string.Empty;
            //Type classPropertyType;
            //Type propertyType;

            infoObject = Activator.CreateInstance<TResult>();


            Type pInfoType_source = sourceObject.GetType();
            PropertyInfo[] pInfos_source = pInfoType_source.GetProperties();
            PropertyInfo pInfo_source;

            Type pInfoType = infoObject.GetType();
            PropertyInfo[] pInfos = pInfoType.GetProperties();
            PropertyInfo pInfo;

            if (pInfos.Length == 0)
            {
                return infoObject;
            }

            for (int i = 0; i < pInfos_source.Length; i++)
            {
                propertyValueObject = null;
                pInfo_source = pInfos_source[i];
                classPropertyName = pInfo_source.Name;

                for (int j = 0; j < pInfos.Length; j++)
                {
                    pInfo = pInfos[j];
                    if (pInfo_source.Name.Trim() == pInfo.Name.Trim())
                    {
                        //classPropertyType = pInfo_source.PropertyType;
                        //propertyType = Nullable.GetUnderlyingType(classPropertyType);

                        //if (propertyType == null)
                        //{
                        //    propertyType = classPropertyType;
                        //}

                        propertyValueObject = pInfo_source.GetValue(sourceObject, null);
                        //propertyValueObject = Convert.ChangeType(pInfo_source.GetValue(pInfo_source, null), propertyType);

                        pInfo.SetValue(infoObject, propertyValueObject, null);

                        break;
                    }
                }
            }

            return infoObject;
        }

        /// <summary>
        /// 复制对象的属性值
        /// </summary>
        /// <typeparam name="TResult">返回结果数据对象类型</typeparam>
        /// <param name="sourceObject">数据源对象</param>
        /// <returns></returns>
        public static TResult CopyObjectValue<TResult>(object sourceObject)
        {
            TResult infoObject = default(TResult);

            if (sourceObject == null)
            {
                return infoObject;
            }

            object propertyValueObject;
            string classPropertyName = string.Empty;
            string classPropertyValue = string.Empty;
            string dataBaseColumnName = string.Empty;

            infoObject = Activator.CreateInstance<TResult>();

            Type pInfoType_source = sourceObject.GetType();
            PropertyInfo[] pInfos_source = pInfoType_source.GetProperties();
            PropertyInfo pInfo_source;

            Type pInfoType = infoObject.GetType();
            PropertyInfo[] pInfos = pInfoType.GetProperties();
            PropertyInfo pInfo;

            if (pInfos.Length == 0)
            {
                return infoObject;
            }

            for (int i = 0; i < pInfos_source.Length; i++)
            {
                propertyValueObject = null;
                pInfo_source = pInfos_source[i];
                classPropertyName = pInfo_source.Name;

                for (int j = 0; j < pInfos.Length; j++)
                {
                    pInfo = pInfos[j];
                    if (pInfo_source.Name.Trim() == pInfo.Name.Trim())
                    {

                        propertyValueObject = pInfo_source.GetValue(sourceObject, null);

                        pInfo.SetValue(infoObject, propertyValueObject, null);

                        break;
                    }
                }
            }

            return infoObject;
        }

        /// <summary>
        /// 将Model实体值赋予LingQ实体
        /// </summary>
        /// <typeparam name="LingQEntity"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static LingQEntity SetDataToLingQEntity<LingQEntity>(LingQEntity Entity, object sourceEntity)
        {
            LingQEntity infoObject = Entity;

            if (sourceEntity == null)
            {
                return infoObject;
            }

            object propertyValueObject;
            string classPropertyName = string.Empty;
            string classPropertyValue = string.Empty;
            string dataBaseColumnName = string.Empty;

            string errPropertyName = string.Empty;
            //infoObject = Activator.CreateInstance<TResult>();

            try
            {
                Type pInfoType_source = sourceEntity.GetType();
                PropertyInfo[] pInfos_source = pInfoType_source.GetProperties();
                PropertyInfo pInfo_source;

                Type pInfoType_result = infoObject.GetType();
                PropertyInfo[] pInfos_result = pInfoType_result.GetProperties();
                PropertyInfo pInfo_result;

                if (pInfos_result.Length == 0)
                {
                    return infoObject;
                }

                for (int i = 0; i < pInfos_result.Length; i++)
                {
                    propertyValueObject = null;
                    pInfo_result = pInfos_result[i];
                    classPropertyName = pInfo_result.Name;

                    for (int j = 0; j < pInfos_source.Length; j++)
                    {
                        pInfo_source = pInfos_source[j];
                        //获得目标属性
                        errPropertyName = pInfo_result.Name.Trim();
                        if (pInfo_source.Name.Trim().ToLower() == pInfo_result.Name.Trim().ToLower())
                        {
                            //提取来源属性值
                            propertyValueObject = pInfo_source.GetValue(sourceEntity, null);
                            //将来源属性值赋予目标属性
                            pInfo_result.SetValue(infoObject, propertyValueObject, null);

                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                //infoObject = null;
                Ex.Source = errPropertyName + ":" + Ex.Source;
                throw Ex;
            }

            return infoObject;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = string.Empty;
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        /// <summary>
        /// 验证邮箱是否有效
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static bool CheckIsMail(string mail)
        {
            string regexEmail = "\\w{1,}@\\w{1,}\\.\\w{1,}";

            System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace

                | System.Text.RegularExpressions.RegexOptions.Multiline)

                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.Regex regEmail = new System.Text.RegularExpressions.Regex(regexEmail, options);

            if (mail != null)
            {
                string email = mail;

                if (regEmail.IsMatch(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// RFID标签ID格式化
        /// </summary>
        /// <param name="TagID">RFID标签ID</param>
        /// <returns></returns>
        public static string RFIDTagIDFormat(string sourceTagID)
        {
            if (sourceTagID.Trim() == string.Empty)
            {
                return string.Empty;
            }

            char[] tagIDs = sourceTagID.Trim().ToCharArray();

            string formatString = string.Empty;

            int zeroIndex = 0;
            for (int i = 0; i < tagIDs.Length; i++)
            {
                if (tagIDs[i] != '0')
                {
                    zeroIndex = i;
                    break;
                }
            }

            string formatTagID = sourceTagID.Trim().Substring(zeroIndex);
            //if (formatTagID.Length<9)
            //{
            //    formatTagID = "0" + formatTagID;
            //}

            return formatTagID;
        }

        public static string RFIDTagIDFormatAddZero(string sourceTagID)
        {


            if (sourceTagID.Length < 9)
            {
                for (int i = 0; i <= (9 - sourceTagID.Length); i++)
                {
                    sourceTagID = "0" + sourceTagID;
                }
            }

            return sourceTagID;
        }

        /// <summary>
        /// 转换10位十进制或者8位十六进制的标签ID成七位十进制
        /// </summary>
        /// <param name="tagIDHex"></param>
        /// <returns></returns>
        public static string HandleTagID(string tagIDHex)
        {
            if (tagIDHex.Length != 8 && tagIDHex.Length != 10)
            {
                return string.Empty;
            }
            string res = string.Empty;
            if (tagIDHex.Length == 8)
            {
                long iLeft = Convert.ToInt64(tagIDHex.Substring(4, 2), 16);
                long iRight = Convert.ToInt64(tagIDHex.Substring(2, 2) + tagIDHex.Substring(0, 2), 16);
                res = iLeft.ToString() + iRight.ToString().PadLeft(5, '0');
            }
            else
            {
                string hexStr = Convert.ToString(long.Parse(tagIDHex), 16).PadLeft(8, '0');
                long iLeft = Convert.ToInt64(hexStr.Substring(2, 2), 16);
                long iRight = Convert.ToInt64(hexStr.Substring(4, 4), 16);
                res = iLeft.ToString() + iRight.ToString().PadLeft(5, '0');
            }
            return res;
        }

        /// <summary>
        /// 获取指定日期的星期数
        /// </summary>
        /// <param name="dtSpecified">指定的时间</param>
        /// <returns></returns>
        public static short GetSpecifiedWeek(DateTime dtSpecified)
        {
            short iWeek = 0;
            switch (dtSpecified.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        iWeek = 5;
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        iWeek = 1;
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        iWeek = 6;
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        iWeek = 0;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        iWeek = 4;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        iWeek = 2;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        iWeek = 3;
                        break;
                    }
                default:
                    {
                        iWeek = 1;
                        break;
                    }
            }
            return iWeek;
        }

        /// <summary>
        /// 调用动态引用的DLL函数
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="_namespace">首命名空间名称</param>
        /// <param name="className">除首命名空间外的类全称</param>
        /// <param name="funcName">被调用方法名</param>
        /// <param name="arrObj">参数列表</param>
        /// <returns></returns>
        public static ReturnValueInfo InvokeMethod(string strNamespace, string strClassName, string strMethodName, object[] arrObj, out object obj)
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            obj = new object();
            try
            {
                Assembly assembly = Assembly.Load(strNamespace);
                string fullName = strNamespace + "." + strClassName;
                Type type = assembly.GetType(fullName);
                obj = Activator.CreateInstance(type);
                MethodInfo medInfo = type.GetMethod(strMethodName);
                rvInfo = (ReturnValueInfo)medInfo.Invoke(obj, arrObj);
            }
            catch (Exception Ex)
            {
                rvInfo.isError = true;
                rvInfo.messageText = Ex.Message;
            }
            return rvInfo;
        }

        /// <summary>
        /// 调用动态引用的DLL函数
        /// </summary>
        /// <param name="_namespace">首命名空间名称</param>
        /// <param name="className">除首命名空间外的类全称</param>
        /// <param name="methodName">被调用方法名</param>
        /// <param name="arrObj">参数列表</param>
        /// <param name="obj">已存在的实例</param>
        /// <returns></returns>
        public static ReturnValueInfo InvokeMethod(string strNamespace, string strClassName, string strMethodName, object[] arrObj, object obj)
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                string fullName = strNamespace + "." + strClassName;
                Assembly assembly = Assembly.Load(strNamespace);
                Type type = assembly.GetType(fullName);
                MethodInfo medInfo = type.GetMethod(strMethodName);
                rvInfo = (ReturnValueInfo)medInfo.Invoke(obj, arrObj);
                return rvInfo;
            }
            catch (Exception Ex)
            {
                rvInfo.isError = true;
                rvInfo.messageText = Ex.Message;
            }
            return rvInfo;
        }

        #region Excel相关

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <typeparam name="LingQEntity"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static DataSet GetExcelDs(string filepath, string ddlSheetsName)
        {
            string strExcelCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";

            DataSet ds = new DataSet();
            try
            {
                using (OleDbConnection oleCn = new OleDbConnection(strExcelCon))
                {
                    oleCn.Open();
                    OleDbDataAdapter da = null;
                    string strSelect = "SELECT * FROM [" + ddlSheetsName + "$]";
                    da = new OleDbDataAdapter(strSelect, oleCn);
                    da.Fill(ds);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="LingQEntity"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static ReturnValueInfo ExportExcel(DataSet ds, string saveFileName)
        {
            ReturnValueInfo valueInfo = new ReturnValueInfo();

            if (File.Exists(saveFileName)) File.Delete(saveFileName);
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                valueInfo.boolValue = false;
                valueInfo.messageText = "无法创建Excel对象，可能您的机器未安装Excel";
                return valueInfo;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

            xlApp.Visible = false;
            //填充列

            //填充表头
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = ds.Tables[0].Columns[i].ColumnName.ToString();
                ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Font.Bold = true;

            }
            //填充数据
            List<int> LIST = new List<int>() { 8, 10, 12, 14 };/*PIC*/

            //图片路径
            List<string> PicList = new List<string>();


            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    #region 2012-2-6
                    if (!LIST.Contains(j))
                    {
                    
                       

                        if (ds.Tables[0].Columns[j].DataType == Type.GetType("System.Int32"))
                        {
                            worksheet.Cells[2 + i, j + 1] = ds.Tables[0].Rows[i][j].ToString();
                            ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2 + i, j + 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2 + i, j + 1]).NumberFormat = "@";
                            ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2 + i, j + 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            worksheet.Cells[2 + i, j + 1] = ds.Tables[0].Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != null)
                        {

                            //global::Common.FileMgtService.FileMgtSoapClient soapClient = WebSrvFactory.GetFileMgt();

                            global::ServiceManage.FilesManage.AbstracFilesManage soapClient = global::ServiceManage.FilesManage.FilesManageFactory.GetFilesManage();

                            Guid PicGuid = new Guid(ds.Tables[0].Rows[i][j].ToString());

                            Byte[] l_b = soapClient.GetFileBytes(PicGuid);

                            if (l_b != null && l_b.Length > 0)
                            {
                                MemoryStream MS = new MemoryStream(l_b);

                                Bitmap bitmap = new Bitmap(MS);

                                #region 1
                                object m_objOpt = System.Reflection.Missing.Value;
                                string picPath = string.Format(@"C:\{0}.jpg", i.ToString() + "_" + j.ToString());

                                bitmap.Save(picPath, System.Drawing.Imaging.ImageFormat.Jpeg);//保存临时图片文件到硬盘里 

                                PicList.Add(picPath);
                                Range m_objRange = null;

                                m_objRange = worksheet.get_Range((Excel.Range)worksheet.Cells[i + 2, j + 1], (Excel.Range)worksheet.Cells[i + 2, j + 1]);
                                m_objRange.Select();
                                m_objRange.RowHeight = 50;
                                float PicLeft, PicTop;
                                PicLeft = Convert.ToSingle(m_objRange.Left);
                                PicTop = Convert.ToSingle(m_objRange.Top);
                                worksheet.Shapes.AddPicture(picPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, PicLeft, PicTop, 50, 50);
                                #endregion

                            }
                            else
                            {
                                //worksheet.Cells[2 + i, j + 1] = ds.Tables[0].Rows[i][j].ToString();
                                //row[index++] = string.Empty;
                            }
                        }
                    }
                    #endregion

                }

            }

            object missing = System.Reflection.Missing.Value;


            try
            {
                workbook.Saved = true;
                //workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                //Excel.XlFileFormat.xlExcel9795
                workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);


                foreach (string item in PicList)
                {
                    File.Delete(item);
                }
            }
            catch (Exception Ex)
            {
                valueInfo.boolValue = false;
                valueInfo.messageText = "导出文件时出错,文件可能正被打开！";
                return valueInfo;
            }
            finally
            {
                xlApp.Quit();
                System.GC.Collect();
            }
            valueInfo.boolValue = true;
            return valueInfo;
        }

        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void OpenWithExcel(string fileName)
        {
            try
            {
                object missing = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();

                //打开新文件 
                myExcel.Application.Workbooks.Open(fileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                //将Excel显示出来 
                myExcel.Visible = true;
            }
            catch { }

            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// 读取Excel转换至DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex">索引从1开始</param>
        /// <returns></returns>
        /// /*Modify By Leoth LunLin On 2012/05/17*/
        public static System.Data.DataTable ReadExcelToTable(string filePath, int sheetIndex, bool IsPhoto)
        {
            Microsoft.Office.Interop.Excel.Application myExcel = null;

            Microsoft.Office.Interop.Excel.Workbooks myBooks = null;

            Microsoft.Office.Interop.Excel.Workbook myBook = null;
            try
            {

                myExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();

                myBooks = myExcel.Application.Workbooks;

                object oMissing = System.Reflection.Missing.Value;

                myBook = myBooks.Open(filePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

                //Excel.Workbook myBook = myExcel.Workbooks[1]; 
                int sheetint = myBook.Worksheets.Count;//能得到sheet的数量 

                Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[sheetIndex];

                int rowsint = mySheet.UsedRange.Cells.Rows.Count; //得到行数 

                int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数 


                System.Data.DataTable dt = new System.Data.DataTable();

                for (int i = 1; i <= columnsint; i++)
                {
                    Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[1, i];

                    dt.Columns.Add(r.Text.ToString(), System.Type.GetType("System.String"));
                }

                DataRow myRow;

                List<int> LIST = new List<int>() { 9, 11, 13, 15 };/*PIC*/
                for (int i = 2; i <= rowsint; i++) //第一行为标题，不读取 
                {
                    myRow = dt.NewRow();

                    for (int j = 1; j <= columnsint; j++)
                    {
                        /*Modify By Leoth LunLin On 2012/05/17*/
                        if (!IsPhoto)
                        {
                            Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[i, j];

                            string strValue = r.Text.ToString();

                            myRow[j - 1] = strValue;
                        }
                        else
                        {
                            if (!LIST.Contains(j))
                            {

                                Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[i, j];

                                string strValue = r.Text.ToString();

                                myRow[j - 1] = strValue;
                            }
                            else
                            {

                                Excel.Range rge = mySheet.Cells[i, j] as Excel.Range;
                                foreach (Excel.Shape sh in mySheet.Shapes)
                                {
                                    Excel.Range rg = myExcel.get_Range(sh.TopLeftCell, sh.BottomRightCell);
                                    if (myExcel.Intersect(rge, rg, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing) != null)
                                    {
                                        sh.CopyPicture(Appearance.Button, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlBitmap);
                                        IDataObject data = Clipboard.GetDataObject();
                                        if (data.GetDataPresent(DataFormats.Bitmap))
                                            if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                                            {
                                                System.Drawing.Image image = (Bitmap)data.GetData(DataFormats.Bitmap);
                                                image.Save(string.Format(@"D:\{0}.jpg", i.ToString() + j.ToString()), System.Drawing.Imaging.ImageFormat.Jpeg);

                                                string strValue = @"D:\" + i.ToString() + j.ToString() + ".jpg";
                                                myRow[j - 1] = strValue;
                                                // temp.Add(col.ToString(), @"C:\Users\Public\Pictures\" + row.ToString() + col.ToString() + ".jpg");
                                            }
                                    }
                                }

                            }
                        }
                    }
                    dt.Rows.Add(myRow);
                }

                #region 2012-2-2 LunLin
                //Dictionary<int, Dictionary<string, string>> allPic = new Dictionary<int, Dictionary<string, string>>();

                //Excel.Application excel = new Excel.Application();
                //Excel.Workbook workbook = excel.Workbooks.Add(filePath);
                //excel.UserControl = true;
                //excel.Visible = false;
                //Excel.Worksheet ws = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                //for (int row = 1; row <= ws.UsedRange.Rows.Count; row++)
                //{
                //    Dictionary<string, string> temp = new Dictionary<string, string>();
                //    for (int col = 1; col <= ws.UsedRange.Count; col++)
                //    {
                //        Excel.Range rge = ws.Cells[row, col] as Excel.Range;
                //        foreach (Excel.Shape sh in ws.Shapes)
                //        {
                //            Excel.Range rg = excel.get_Range(sh.TopLeftCell, sh.BottomRightCell);
                //            if (excel.Intersect(rge, rg, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing) != null)
                //            {
                //                sh.CopyPicture(Appearance.Button, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlBitmap);　
                //                IDataObject data = Clipboard.GetDataObject();
                //                if (data.GetDataPresent(DataFormats.Bitmap))
                //                    if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                //                    {
                //                        System.Drawing.Image image = (Bitmap)data.GetData(DataFormats.Bitmap);
                //                        image.Save(string.Format(@"C:\Users\Public\Pictures\{0}.jpg", row.ToString() + col.ToString()), System.Drawing.Imaging.ImageFormat.Jpeg);


                //                        temp.Add(col.ToString(), @"C:\Users\Public\Pictures\" + row.ToString() + col.ToString() + ".jpg");
                //                    }
                //            }
                //        }
                //    }
                //    allPic.Add(row, temp);
                //}
                #endregion

                return dt;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myExcel.Quit();

                IntPtr t = new IntPtr(myExcel.Hwnd);          //杀死进程的好方法，很有效

                int k = 0;

                GetWindowThreadProcessId(t, out k);

                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);

                p.Kill();

            }

        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);

        #endregion

        #region 检查IP

        /// <summary>
        /// 测试对应IP地址是否连通
        /// </summary>
        /// <param name="ip">目标IP地址</param>
        /// <returns></returns>
        public static bool Ping(string strIP)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = ping.Send(strIP, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据对应的服务器名称或IP判断网络连接情况
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <returns></returns>
        public static bool CheckIP(string hostNameOrAddress)
        {
            bool Result = false;
            try
            {
                Ping p = new Ping();
                PingReply pr = p.Send(hostNameOrAddress);
                Result = CheckIP(pr);
            }
            catch
            {
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// 网络连接信息
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        private static bool CheckIP(PingReply pr)
        {
            bool Result = false;
            try
            {
                Ping p = new Ping();

                switch (pr.Status)
                {
                    case IPStatus.Success:
                        Result = true;
                        break;

                    case IPStatus.TimedOut:
                        Result = false;

                        break;

                    default:
                        Result = false;

                        break;
                }
            }
            catch
            {
                Result = false;
            }
            return Result;
        }

        #endregion

        #region 消费密码

        /// <summary>
        /// 消费密码类型
        /// </summary>
        public enum PaymentPasswordType
        {
            /// <summary>
            /// 默认密码
            /// </summary>
            Defaul,

            /// <summary>
            /// 消费密码
            /// </summary>
            Consumption,

            /// <summary>
            /// 电话密码
            /// </summary>
            Phone,

            /// <summary>
            /// 水表管理卡密码
            /// </summary>
            Subsidize
        }

        /// <summary>
        /// 获取消费卡密码
        /// </summary>
        /// <param name="passwordType">消费密码类型</param>
        /// <returns>获取密码成功时boolValue为True, 密码内容在messageText中。</returns>
        public static ReturnValueInfo GetPaymentPassword(PaymentPasswordType passwordType)
        {
            ReturnValueInfo passwordInfo = new ReturnValueInfo();

            //消费卡的默认密码
            if (passwordType == PaymentPasswordType.Defaul)
            {
                try
                {
                    passwordInfo.messageText = ConfigurationSettings.AppSettings["OriginalPassword"];
                    passwordInfo.boolValue = true;
                }
                catch (Exception Ex)
                {
                    passwordInfo.boolValue = false;
                    passwordInfo.messageText = Ex.Message;
                }
            }
            //消费卡的消费密码
            else if (passwordType == PaymentPasswordType.Consumption)
            {
                UKey.AbstractUKey uKey = UKey.UKeyFactory.GetUKey();

                UKey.Entity.ReturnValueInfo returnInfo = null;

                returnInfo = uKey.ReadPassword();

                if (returnInfo != null)
                {
                    passwordInfo.boolValue = returnInfo.IsSuccess;
                    passwordInfo.messageText = returnInfo.MessageText;
                }

            }
            //消费卡的电话密码
            else if (passwordType == PaymentPasswordType.Phone)
            {
                try
                {
                    passwordInfo.messageText = ConfigurationSettings.AppSettings["PhonePassword"];
                    passwordInfo.boolValue = true;
                }
                catch (Exception Ex)
                {
                    passwordInfo.boolValue = false;
                    passwordInfo.messageText = Ex.Message;
                }
            }
            //水表管理卡的水表密码
            else if (passwordType == PaymentPasswordType.Subsidize)
            {
                try
                {
                    passwordInfo.messageText = ConfigurationSettings.AppSettings["SubsidizePassword"];
                    passwordInfo.boolValue = true;
                }
                catch (Exception Ex)
                {
                    passwordInfo.boolValue = false;
                    passwordInfo.messageText = Ex.Message;
                }
            }


            return passwordInfo;
        }

        #endregion

        /// <summary>
        /// 获得卡的默认有效期
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetPaymentCardDefaultExpiryDate()
        {
            DateTime? expiryDate = null;

            try
            {
                string dateString = ConfigurationSettings.AppSettings["PaymentCardDefaultExpiryDate"];
                List<string> dateList = dateString.Split('-').ToList();
                int expiryYears = DateTime.Now.Year + Convert.ToInt32(dateList[0]);
                string expiryDateString = expiryYears.ToString().Trim() + "-" + dateList[1] + "-" + dateList[2];

                expiryDate = Convert.ToDateTime(expiryDateString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return expiryDate;
        }

        /// <summary>
        /// 写本地日志
        /// </summary>
        /// <param name="strFuncName">当前函数名称</param>
        /// <param name="strMessage">日志内容</param>
        /// <param name="strRemarks">备注</param>
        /// <param name="logType">日志类型</param>
        /// <param name="fileType">保存方式（文件\数据库，使用文件类型时，需设置配置项"SIOT_LogsPath"登记日志文件保存路径，默认路径为:"C:\SIOT_Logs\"；使用数据库类型时，需设置配置项"SIOT_LogsConn"，并于数据库中添加数据表"LogDetail_lgd"）</param>
        /// <returns></returns>
        public static bool WriteLocalLogs(string strFuncName, string strMessage, string strRemarks, SystemLog.SystemLog.LogType logType, SystemLog.SystemLog.FileType fileType)
        {
            SystemLog.SystemLog sysLog = new SystemLog.SystemLog();
            sysLog.fileType = fileType;
            sysLog.Operator = "sa";
            sysLog.Remark = strRemarks;
            sysLog.LogMessage = strMessage;
            sysLog.ClassMethodName = strFuncName;
            sysLog.logType = logType;

            #region 获取本机IP地址和主机名称

            IPHostEntry host = Dns.Resolve(Dns.GetHostName());
            if (host != null && host.AddressList != null && host.AddressList.Length > 0)
            {
                sysLog.SystemName = host.HostName;
                IPAddress addr = host.AddressList[0];
                sysLog.ApplicationIP = addr.ToString();
            }

            #endregion

            bool res = false;

            if (fileType == SystemLog.SystemLog.FileType.DataBase)
            {
                string strConn;

                #region 获取数据库连接字符

                try
                {
                    strConn = ConfigurationSettings.AppSettings["SIOT_LogsConn"];
                }
                catch (Exception)
                {
                    strConn = @"Data Source=.;Initial Catalog=SchoolInternetOfThingsDB;User ID=sa;Password=!!!aaa111";
                }

                #endregion

                sysLog.CnnString = strConn;
            }
            else
            {
                string strPath;

                #region 获取日志文件存放路径

                try
                {
                    strPath = ConfigurationSettings.AppSettings["SIOT_LogsPath"];
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                }
                catch (Exception)
                {
                    strPath = @"C:\\SIOT_Logs\\";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                }

                #endregion

                sysLog.LogPath = strPath;
            }

            try
            {
                sysLog.WriteLog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            res = true;

            return res;
        }

        /// <summary>
        /// 获取当前函数的函数名
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFuncName()
        {
            string strFuncName;

            StackTrace stackTrace = new System.Diagnostics.StackTrace();
            StackFrame[] arrStackFrame = stackTrace.GetFrames();
            if (arrStackFrame != null && arrStackFrame.Length > 1)
            {
                string strFuncAll = stackTrace.GetFrames()[1].ToString();
                string[] arrStrTmp = strFuncAll.Split(' ');
                if (arrStrTmp != null && arrStrTmp.Length > 0)
                {
                    strFuncName = arrStrTmp[0];
                }
                else
                {
                    strFuncName = null;
                }
            }
            else
            {
                strFuncName = null;
            }

            return strFuncName;
        }

        /// <summary>
        /// 获取父级函数的函数名
        /// </summary>
        /// <param name="l_iLevel">层级数量，序号从-1开始到总使用层级</param>
        /// <returns></returns>
        public static string GetParentFuncName(int l_iLevel)
        {
            string strFuncName;

            StackTrace stackTrace = new System.Diagnostics.StackTrace();
            StackFrame[] arrStackFrame = stackTrace.GetFrames();
            if (arrStackFrame != null && arrStackFrame.Length > l_iLevel && l_iLevel > -2)
            {
                string strFuncAll = stackTrace.GetFrames()[l_iLevel + 1].ToString();
                string[] arrStrTmp = strFuncAll.Split(' ');
                if (arrStrTmp != null && arrStrTmp.Length > 0)
                {
                    strFuncName = arrStrTmp[0];
                }
                else
                {
                    strFuncName = null;
                }
            }
            else
            {
                strFuncName = null;
            }

            return strFuncName;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="l_strMessage">日志内容</param>
        /// <param name="l_strRemarks">备注</param>
        /// <returns></returns>
        public bool WriteLog(string l_strMessage, string l_strRemarks, SystemLog.SystemLog.LogType logType)
        {
            bool l_res = false;

            try
            {
                if (this.c_sysLog == null)
                {
                    return false;
                }

                if (this.c_listLogContent.Contains(l_strMessage))
                {
                    return true;
                }

                string strLocalSysName = ConfigurationSettings.AppSettings["SysName"];
                if (string.IsNullOrEmpty(this.c_sysLog.SystemName))
                {
                    this.c_sysLog.SystemName = strLocalSysName;
                    if (string.IsNullOrEmpty(strLocalSysName))
                    {
                        this.c_sysLog.SystemName = "SYS_AUTO";
                    }
                }
                this.c_sysLog.Operator = "sa";

                #region 获取本机IP地址

                IPHostEntry host = Dns.Resolve(Dns.GetHostName());
                if (host != null && host.AddressList != null && host.AddressList.Length > 0)
                {
                    IPAddress addr = host.AddressList[0];
                    this.c_sysLog.ApplicationIP = addr.ToString();
                }

                #endregion

                this.c_sysLog.logType = logType;
                this.c_sysLog.ClassMethodName = GetParentFuncName(1);
                this.c_sysLog.LogMessage = l_strMessage;
                this.c_sysLog.Remark = l_strRemarks;
                this.c_sysLog.WriteLog();

                this.c_listLogContent.Add(l_strMessage);
                l_res = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return l_res;
        }


        public static SystemLog.SystemLog BindLocalLogInfo()
        {
            SystemLog.SystemLog sysLog = new SystemLog.SystemLog();
            sysLog.fileType = SystemLog.SystemLog.FileType.LogFile;

            string strPath;

            #region 获取日志文件存放路径

            try
            {
                strPath = ConfigurationSettings.AppSettings["SIOT_LogsPath"];
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
            }
            catch (Exception)
            {
                strPath = @"D:\\SIOT_Logs\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
            }

            #endregion

            sysLog.LogPath = strPath;

            return sysLog;
        }

        public static SystemLog.SystemLog BindDbLogInfo()
        {
            SystemLog.SystemLog sysLog = new SystemLog.SystemLog();
            sysLog.fileType = SystemLog.SystemLog.FileType.DataBase;

            string strConn;

            #region 获取数据库连接字符

            try
            {
                strConn = ConfigurationSettings.AppSettings["SIOT_LogsConn"];
            }
            catch (Exception)
            {
                strConn = @"Data Source=.;Initial Catalog=SchoolInternetOfThingsDB;User ID=sa;Password=!!!aaa111";
            }

            #endregion

            sysLog.CnnString = strConn;

            return sysLog;
        }

        /// <summary>
        /// 数字转换为中国大写数字
        /// </summary>
        /// <param name="numeral">数字</param>
        /// <param name="isDecimalPoint">当没有小数位时是否补零</param>
        /// <returns></returns>
        public static List<string> ConvertNumeralToChineseNumeral(decimal numeral, bool isDecimalPoint)
        {
            if (numeral < 0)
            {
                return null;
            }

            List<string> chineseNumerals = new List<string>();
            string numeralString = numeral.ToString("0.00").Trim();


            string strInt;
            string strFloat;
            strInt = numeralString.Substring(0, numeralString.IndexOf(".", 0));//整数部分的值
            strFloat = numeralString.Substring((numeralString.IndexOf(".", 0) + 1), (numeralString.Length - numeralString.IndexOf(".", 0) - 1));//小数部分的值

            int numLen = strInt.Length;
            string dx = "零壹贰叁肆伍陆柒捌玖";
            string ws = "分角元拾佰仟万拾佰仟亿拾佰仟";

            int i = 0;

            //计算整数的大写！
            while (i < numLen)
            {
                chineseNumerals.Add(dx.Substring(Convert.ToInt32(strInt.Substring(i, 1)), 1));

                i++;
            }

            if (isDecimalPoint)
            {
                chineseNumerals.Add("点");
                chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(0, 1)), 1));
                chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(1, 1)), 1));
            }
            else
            {
                if (strFloat.Substring(0, 1) == "0" && strFloat.Substring(1, 1) != "0")
                {
                    chineseNumerals.Add("点");
                    chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(0, 1)), 1));
                    chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(1, 1)), 1));
                }
                else if (strFloat.Substring(0, 1) != "0" && strFloat.Substring(1, 1) == "0")
                {
                    chineseNumerals.Add("点");
                    chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(0, 1)), 1));
                }
                else if (strFloat.Substring(0, 1) != "0" && strFloat.Substring(1, 1) != "0")
                {
                    chineseNumerals.Add("点");
                    chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(0, 1)), 1));
                    chineseNumerals.Add(dx.Substring(Convert.ToInt32(strFloat.Substring(1, 1)), 1));
                }
            }

            return chineseNumerals;
        }

        /// <summary>
        /// Stream转byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            return bytes;
        }
    }
}
