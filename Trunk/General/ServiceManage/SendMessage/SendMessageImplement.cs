using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.SendEmail;


namespace ServiceManage.SendMessage
{
    public class SendMessageImplement
    {
        /// <summary>
        /// 短信网关发送帐户
        /// </summary>
        public static string _userID = string.Empty;

        /// <summary>
        /// 短信网关发送帐户密码
        /// </summary>
        public static string _passWord = string.Empty;

        /// <summary>
        /// 邮件服务地址
        /// </summary>
        public static string _emailService = string.Empty;

        /// <summary>
        /// 发送邮件帐户
        /// </summary>
        public static string _emailUserID = string.Empty;

        /// <summary>
        /// 发送邮件帐户密码
        /// </summary>
        public static string _emailUserPassWord = string.Empty;

        public SendMessageImplement() { }

        public static ResultInfo SendSMS(string toPhone, string messageText)
        {
            ResultInfo returnInfo = new ResultInfo();

            try
            {
                //_userID = ConfigurationSettings.AppSettings["SendMessageUserID"].Trim();
                //_passWord = ConfigurationSettings.AppSettings["SendMessagePassWord"].Trim();
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;

                return returnInfo;
            }

            if (toPhone.Trim() == "")
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = "不存在接收的电话号码！";
                return returnInfo;
            }

            AbstractSendMessage sendMessage = SendMessageFactory.GetSendMessage(_userID, _passWord);

            ResultInfo resultInfo = null;
            resultInfo = sendMessage.SendMessageText(toPhone, messageText);

            if (resultInfo != null)
            {
                returnInfo.boolValue = resultInfo.boolValue;
                returnInfo.isError = resultInfo.isError;
                returnInfo.messageText = resultInfo.messageText;
                returnInfo.ValueObject = resultInfo.ValueObject;
            }

            return returnInfo;
        }

        public static ResultInfo SendEmail(string to, string subject, string body)
        {
            ResultInfo returnInfo = new ResultInfo();

            try
            {
                //_emailService = ConfigurationSettings.AppSettings["EmailService"].Trim();
                //_emailUserID = ConfigurationSettings.AppSettings["EmailUserID"].Trim();
                //_emailUserPassWord = ConfigurationSettings.AppSettings["EmailUserPassWord"].Trim();
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;

                return returnInfo;
            }

            if (_emailService.Trim() == "")
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = "不存在邮件服务地址！";
                return returnInfo;
            }

            if (_emailUserID.Trim() == "")
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = "不存在发件人地址！";
                return returnInfo;
            }

            if (to.Trim() == "")
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = "不存在接收邮件地址！";
                return returnInfo;
            }

            AbstractSendEmail sendMessage = SendEmailFactory.GetSendSendEmail(_emailService, _emailUserID, _emailUserPassWord);

            ResultInfo resultInfo = null;

            string htmlBody = body.Replace("\r\n", "<br>");
            resultInfo = sendMessage.Send(to, "", subject, htmlBody);

            if (resultInfo != null)
            {
                returnInfo.boolValue = resultInfo.boolValue;
                returnInfo.isError = resultInfo.isError;
                returnInfo.messageText = resultInfo.messageText;
                returnInfo.ValueObject = resultInfo.ValueObject;
            }

            return returnInfo;
        }
    }
}
