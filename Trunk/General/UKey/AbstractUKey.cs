using System;
using System.Collections.Generic;
using System.Text;
using UKey.Entity;

namespace UKey
{
    public abstract class AbstractUKey
    {
        protected string _code;
        protected string _loginPassword;

        string _codeErrorMessage;

        public AbstractUKey()
        {
            this._codeErrorMessage = "程序代码发生错误，请联系供应商！";

            this._code = "UKey_SchoolInternetOfThings";
            //UKey种子码1234567890ABCDEF
            //this._loginPassword = "5d77a67b65d59b5ceec9a7e3cc068e6c";
            this._loginPassword = "af15bf60cb60ebe1a7f66fe2d8decfe7";
            this.UKeyContentLenght = 12;
        }

        /// <summary>
        /// 查找指定加密锁识别码的加密锁
        /// </summary>
        /// <param name="code">加密锁识别码</param>
        /// <returns></returns>
        public abstract ReturnValueInfo FindUKeyFirst(string code);

        /// <summary>
        /// 查找指定加密锁识别码的加密锁
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo FindUKeyFirst();

        /// <summary>
        /// 登录加密锁（已自动寻找加密锁）
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public virtual ReturnValueInfo Login(string password)
        {
            ReturnValueInfo returnInfo = null;

            //Start****************************查找指定加密锁识别码的加密锁****************************
            returnInfo = this.FindUKeyFirst(this._code);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************查找指定加密锁识别码的加密锁****************************

            //Start****************************登录加密锁****************************
            returnInfo = this.LoginUKey(password);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************登录加密锁****************************

            return returnInfo;
        }

        /// <summary>
        /// 登录加密锁（已自动寻找加密锁）
        /// </summary>
        /// <returns></returns>
        public virtual ReturnValueInfo Login()
        {
            ReturnValueInfo returnInfo = null;

            //Start****************************查找指定加密锁识别码的加密锁****************************
            returnInfo = this.FindUKeyFirst(this._code);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************查找指定加密锁识别码的加密锁****************************

            //Start****************************登录加密锁****************************
            returnInfo = this.LoginUKey(this._loginPassword);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************登录加密锁****************************

            return returnInfo;
        }

        /// <summary>
        /// 登录加密锁（需要调用寻找加密锁方法“FindUKeyFirst()”）
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public abstract ReturnValueInfo LoginUKey(string password);

        /// <summary>
        /// 登录加密锁（需要调用寻找加密锁方法“FindUKeyFirst()”）
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo LoginUKey();

        /// <summary>
        /// 写密码（已自动寻找加密锁及自动登录加密锁）
        /// </summary>
        /// <param name="newUserPassword">用户新密码</param>
        /// <returns></returns>
        public virtual ReturnValueInfo WritePassword(string newUserPassword)
        {
            return WritePasswordToUKey(this._loginPassword, newUserPassword);
        }

        /// <summary>
        /// 写入消费密码（12位）
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public virtual ReturnValueInfo WritePaymentPassword(string newPassword)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();

            if (newPassword.Length != this.UKeyContentLenght)
            {
                returnValue.IsSuccess = false;
                returnValue.MessageText = "密码长度出错，请输入一个" + this.UKeyContentLenght.ToString().Trim() + "位的密码！";

                return returnValue;
            }

            returnValue = WritePasswordToUKey(this._loginPassword, newPassword);

            return returnValue;
        }

        /// <summary>
        /// 写密码（已自动寻找加密锁及自动登录加密锁）
        /// </summary>
        /// <param name="loginPassword">登录加密锁密码</param>
        /// <param name="newUserPassword">用户新密码</param>
        /// <returns></returns>
        public virtual ReturnValueInfo WritePassword(string loginPassword, string newUserPassword)
        {
            return WritePasswordToUKey(loginPassword, newUserPassword);
        }

        /// <summary>
        /// 写密码（已自动寻找加密锁及自动登录加密锁）
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        ReturnValueInfo WritePasswordToUKey(string loginPassword, string newPassword)
        {
            ReturnValueInfo returnInfo = null;

            //Start****************************查找指定加密锁识别码的加密锁****************************
            returnInfo = this.FindUKeyFirst(this._code);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************查找指定加密锁识别码的加密锁****************************


            //Start****************************登录加密锁****************************
            returnInfo = this.LoginUKey(loginPassword);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************登录加密锁****************************

            //Start****************************写密码到加密锁中****************************
            returnInfo = this.WriteUKeyContent(newPassword);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    returnInfo.MessageText = "写密码失败！";
                    return returnInfo;
                }
                else
                {
                    returnInfo.MessageText = "写密码成功！";
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************写密码到加密锁中****************************



            return returnInfo;
        }

        /// <summary>
        /// 写内容到加密锁中（需要调用寻找加密锁方法“FindUKeyFirst()”，及登录加密锁方法“Login()”）
        /// </summary>
        /// <param name="content">写入到加密锁中的内容</param>
        /// <returns></returns>
        public abstract ReturnValueInfo WriteUKeyContent(string content);

        /// <summary>
        /// 读取密码
        /// </summary>
        /// <returns></returns>
        public virtual ReturnValueInfo ReadPassword()
        {
            ReturnValueInfo returnInfo = null;

            //Start****************************查找指定加密锁识别码的加密锁****************************
            returnInfo = this.FindUKeyFirst(this._code);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************查找指定加密锁识别码的加密锁****************************


            //Start****************************登录加密锁****************************
            returnInfo = this.LoginUKey(this._loginPassword);
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************登录加密锁****************************

            //Start****************************读加密锁中的密码****************************
            returnInfo = this.ReadUKeyContent();
            if (returnInfo != null)
            {
                if (!returnInfo.IsSuccess)
                {
                    returnInfo.MessageText = "读密码失败！";
                    return returnInfo;
                }
            }
            else
            {
                returnInfo = new ReturnValueInfo();
                returnInfo.IsSuccess = false;
                returnInfo.MessageText = this._codeErrorMessage;

                return returnInfo;
            }
            //End****************************读加密锁中的密码****************************



            return returnInfo;
        }

        /// <summary>
        /// 读加密锁中的内容（需要调用寻找加密锁方法“FindUKeyFirst()”，及登录加密锁方法“Login()”）
        /// </summary>
        /// <returns></returns>
        public abstract ReturnValueInfo ReadUKeyContent();

        /// <summary>
        /// 加密锁识别码
        /// </summary>
        public string Code
        {
            set
            {
                this._code = value;
            }
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword
        {
            set
            {
                this._loginPassword = value;
            }

            get
            {
                return "";
            }
        }

        /// <summary>
        /// 加密锁内容数据的长度，可以自定义加密锁内容数据的长度，最大为1024
        /// </summary>
        public int UKeyContentLenght
        {
            set;
            get;
        }
    }
}
