using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UKey.Entity;
namespace UKey.UKeyImplement
{
    class NT119API:AbstractUKey
    {
        //查找加密锁
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTFindFirst(string NTCode);

        //查询硬件ID
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTGetHardwareID(StringBuilder hardwareID);

        //登录加密锁
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTLogin(string NTpassword);


        //第一存储区数据读取
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead(int address, int Length, byte[] pBuffer);

        //第一存储区数据写入
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTWrite(int address, int Length, byte[] pBuffer);

        //第二存储区数据读取
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead2(int address, int Length, byte[] pBuffer);

        //第三存储区数据读取
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead3(int address, int Length, byte[] pBuffer);

        //3DES解密
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NT3DESCBCDecrypt(byte[] vi, byte[] pDataBuffer, int Length);

        //3DES加密
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NT3DESCBCEncrypt(byte[] vi, byte[] pDataBuffer, int Length);

        //验证许可证
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTCheckLicense(int licenseCode);

        //MD5运算
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTMD5(StringBuilder pInBuffer, int dataLen, byte[] md5value);


        //生成请求激活许可证信息
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTChangeLicenseRequest(StringBuilder requestString, int Length);


        //激活许可证
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTChangeLicense(StringBuilder requestString);

        //生成请求注册信息
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRegisterProductRequest(StringBuilder registerData, int dataLen, StringBuilder requestString, int requestStringLen);


        //注册操作
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRegisterProduct(StringBuilder responseString);

        //验证注册
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTVerifyProduct(StringBuilder registerData, int Length);



        //登出加密锁
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTLogout();

        public NT119API()
        {
            
        }

        ReturnValueInfo FindUKey(string code)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;

            rtn = NTFindFirst(code);//查找指定加密锁识别码的加密锁，如果返回值为 0，表示加密锁存在。如果返回值不为0，则可以通过返回值Rtn查看错误代码

            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "没有找到加密锁！";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "已找到加密锁！";
            }

            return returnValue;
        }

        public override ReturnValueInfo FindUKeyFirst(string code)
        {
            return FindUKey(code);
        }

        public override ReturnValueInfo FindUKeyFirst()
        {
            return FindUKey(this._code);
        }

        ReturnValueInfo LoginUKey_NT(string password)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;

            rtn = NTLogin(password);//登录加密锁，如果返回值为 0，表示加密锁登录成功。如果返回值不为0，则可以通过返回值Rtn查看错误代码

            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "登录加密锁失败！";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "登录加密锁成功！";
            }

            return returnValue;
        }

        public override ReturnValueInfo LoginUKey(string password)
        {
            return LoginUKey_NT(password);
        }

        public override ReturnValueInfo LoginUKey()
        {
            return LoginUKey_NT(this._loginPassword);
        }

        public override ReturnValueInfo WriteUKeyContent(string newPassword)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;
            int address = 0; //加密锁读取数据的起始位置,可以自定义加密锁读取数据的起始位置，最大为1024
            byte[] pBuffer = new byte[this.UKeyContentLenght];

            //Start*************************当新密码的长度比所定义的长度小时，在新密码后面加上足够长的“F”字符串***************************************************
            int newPasswordLenght = 0;
            if (newPassword.Trim().Length < this.UKeyContentLenght)
            {
                int difference = this.UKeyContentLenght - newPasswordLenght;
                string differenceString = string.Empty;

                for (int i = 0; i < difference; i++)
                {
                    differenceString += "F";
                }

                newPassword = newPassword.Trim() + differenceString;
            }
            //End*************************当新密码的长度比所定义的长度小时，在新密码后面加上足够长的“F”字符串***************************************************

            pBuffer = Encoding.GetEncoding("gb2312").GetBytes(newPassword);

            rtn = NT119API.NTWrite(address, this.UKeyContentLenght, pBuffer);//存储区数据写入，如果返回值为 0，表示数据写入成功。
            //如果返回值不为0，则可以通过返回值Rtn查看错误代码
            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "写入内容失败！";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "写入内容成功！";
            }

            return returnValue;
        }

        public override ReturnValueInfo ReadUKeyContent()
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;
            int address = 0; //加密锁读取数据的起始位置,可以自定义加密锁读取数据的起始位置，最大为1024

            byte[] pBuffer = new byte[this.UKeyContentLenght];//存储区数据读取，如果返回值为 0，表示数据写入成功。
            //如果返回值不为0，则可以通过返回值Rtn查看错误代码

            rtn = NT119API.NTRead(address, this.UKeyContentLenght, pBuffer);
            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "读取密码失败！";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = System.Text.Encoding.Default.GetString(pBuffer).Trim();
            }

            return returnValue;
        }
    }
}
