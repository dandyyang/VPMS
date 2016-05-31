using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.SendEmail;
using ServiceManage.SendEmail.SendEmailImplement;

namespace ServiceManage.SendEmail
{
    public class SendEmailFactory
    {
        static readonly string _sendEmail = "ServiceManage.SendEmail.SendEmailImplement.SendEmailSMTP,ServiceManage";

        static AbstractSendEmail _sendEmailObject = null;

        /// <summary>
        /// 获得发送邮件实现对象
        /// </summary>
        /// <param name="accessorFullName">类型名称</param>
        /// <returns></returns>
        public static AbstractSendEmail GetSendSendEmail(string serviceAddress, string user, string password)
        {
            if (_sendEmailObject == null)
            {
                //動態創建實例類型 
                try
                {
                    Type accessorType = Type.GetType(_sendEmail, false);
                    return (AbstractSendEmail)Activator.CreateInstance(accessorType, new object[] { serviceAddress, user, password });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return _sendEmailObject;
            }

        }
    }
}
