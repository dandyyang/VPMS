using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceManage.SendEmail
{
    public abstract class AbstractSendEmail
    {
        protected string ServiceAddress;
        protected string User;
        protected string Password;

        /// <summary>
        /// 发送邮件

        /// </summary>
        /// <param name="from">发送人地址</param>
        /// <param name="to">接收人</param>
        /// <param name="cc">副接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public abstract ResultInfo Send(string to, string cc, string subject, string body);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送人地址</param>
        /// <param name="to">接收人列表</param>
        /// <param name="cc">副接收人列表</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public abstract ResultInfo Send(List<string> to, List<string> cc, string subject, string body);

        /// <summary>
        /// 发送批量延时邮件
        /// </summary>
        /// <param name="listRecAddr">接收人邮件地址列表</param>
        /// <param name="strTitle">邮件主题</param>
        /// <param name="strMsg">邮件内容</param>
        /// <param name="strServName">发送方标识</param>
        /// <returns></returns>
        public abstract ResultInfo SendDelayEmail(List<string> listRecAddr, string strTitle, string strMsg, string strServName);

        /// <summary>
        /// 发送单条延时邮件
        /// </summary>
        /// <param name="strAddr">接收人邮箱地址</param>
        /// <param name="strTitle">邮件主题</param>
        /// <param name="strMsg">邮件内容</param>
        /// <param name="strServName">发送方标识</param>
        /// <returns></returns>
        public abstract ResultInfo SendDelayEmail(string strAddr, string strTitle, string strMsg, string strServName);
    }
}
