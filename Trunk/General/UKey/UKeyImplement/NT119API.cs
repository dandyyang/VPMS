using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UKey.Entity;
namespace UKey.UKeyImplement
{
    class NT119API:AbstractUKey
    {
        //���Ҽ�����
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTFindFirst(string NTCode);

        //��ѯӲ��ID
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTGetHardwareID(StringBuilder hardwareID);

        //��¼������
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTLogin(string NTpassword);


        //��һ�洢�����ݶ�ȡ
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead(int address, int Length, byte[] pBuffer);

        //��һ�洢������д��
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTWrite(int address, int Length, byte[] pBuffer);

        //�ڶ��洢�����ݶ�ȡ
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead2(int address, int Length, byte[] pBuffer);

        //�����洢�����ݶ�ȡ
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRead3(int address, int Length, byte[] pBuffer);

        //3DES����
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NT3DESCBCDecrypt(byte[] vi, byte[] pDataBuffer, int Length);

        //3DES����
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NT3DESCBCEncrypt(byte[] vi, byte[] pDataBuffer, int Length);

        //��֤���֤
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTCheckLicense(int licenseCode);

        //MD5����
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTMD5(StringBuilder pInBuffer, int dataLen, byte[] md5value);


        //�������󼤻����֤��Ϣ
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTChangeLicenseRequest(StringBuilder requestString, int Length);


        //�������֤
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTChangeLicense(StringBuilder requestString);

        //��������ע����Ϣ
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRegisterProductRequest(StringBuilder registerData, int dataLen, StringBuilder requestString, int requestStringLen);


        //ע�����
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTRegisterProduct(StringBuilder responseString);

        //��֤ע��
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTVerifyProduct(StringBuilder registerData, int Length);



        //�ǳ�������
        [DllImport(@"DLL\NT119.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NTLogout();

        public NT119API()
        {
            
        }

        ReturnValueInfo FindUKey(string code)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;

            rtn = NTFindFirst(code);//����ָ��������ʶ����ļ��������������ֵΪ 0����ʾ���������ڡ��������ֵ��Ϊ0�������ͨ������ֵRtn�鿴�������

            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "û���ҵ���������";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "���ҵ���������";
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

            rtn = NTLogin(password);//��¼���������������ֵΪ 0����ʾ��������¼�ɹ����������ֵ��Ϊ0�������ͨ������ֵRtn�鿴�������

            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "��¼������ʧ�ܣ�";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "��¼�������ɹ���";
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
            int address = 0; //��������ȡ���ݵ���ʼλ��,�����Զ����������ȡ���ݵ���ʼλ�ã����Ϊ1024
            byte[] pBuffer = new byte[this.UKeyContentLenght];

            //Start*************************��������ĳ��ȱ�������ĳ���Сʱ�����������������㹻���ġ�F���ַ���***************************************************
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
            //End*************************��������ĳ��ȱ�������ĳ���Сʱ�����������������㹻���ġ�F���ַ���***************************************************

            pBuffer = Encoding.GetEncoding("gb2312").GetBytes(newPassword);

            rtn = NT119API.NTWrite(address, this.UKeyContentLenght, pBuffer);//�洢������д�룬�������ֵΪ 0����ʾ����д��ɹ���
            //�������ֵ��Ϊ0�������ͨ������ֵRtn�鿴�������
            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "д������ʧ�ܣ�";
            }
            else
            {
                returnValue.IsSuccess = true;
                returnValue.MessageText = "д�����ݳɹ���";
            }

            return returnValue;
        }

        public override ReturnValueInfo ReadUKeyContent()
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            int rtn = 0;
            int address = 0; //��������ȡ���ݵ���ʼλ��,�����Զ����������ȡ���ݵ���ʼλ�ã����Ϊ1024

            byte[] pBuffer = new byte[this.UKeyContentLenght];//�洢�����ݶ�ȡ���������ֵΪ 0����ʾ����д��ɹ���
            //�������ֵ��Ϊ0�������ͨ������ֵRtn�鿴�������

            rtn = NT119API.NTRead(address, this.UKeyContentLenght, pBuffer);
            if (rtn != 0)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "��ȡ����ʧ�ܣ�";
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
