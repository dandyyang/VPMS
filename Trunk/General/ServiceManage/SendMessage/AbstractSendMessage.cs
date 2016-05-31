using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage;

namespace ServiceManage.SendMessage
{
    public abstract class AbstractSendMessage
    {
        /// <summary>
        /// 發信息時的用戶名稱
        /// </summary>
        protected string UserID;

        /// <summary>
        /// 密碼
        /// </summary>
        protected string PassWord;

        public AbstractSendMessage()
        {
            this.UserID = string.Empty;
            this.PassWord = string.Empty;
            this.ServiceAddress = string.Empty;
            this.SMSBodyMaxLenght = 300;
            this.ReceivedByMaxQuatity = 11;
        }

        /// <summary>
        /// 发送多条即时短信
        /// </summary>
        /// <param name="listRecPhone">接收人电话列表</param>
        /// <param name="strMsg">信息内容</param>
        /// <returns></returns>
        public abstract ResultInfo SendMessageText(List<string> listRecPhone, string strMsg);

        /// <summary>
        /// 发送单条即时短信
        /// </summary>
        /// <param name="listRecPhone">接收人电话</param>
        /// <param name="strMsg">信息内容</param>
        /// <returns></returns>
        public abstract ResultInfo SendMessageText(string toPhone, string messageText);

        /// <summary>
        /// 发送多条延时短信
        /// </summary>
        /// <param name="listRecPhone">接收人电话列表</param>
        /// <param name="strMsg">信息内容</param>
        /// <param name="strServName">发送方发送方标识</param>
        /// <returns></returns>
        public abstract ResultInfo SendDelaySMS(List<string> listRecPhone, string strMsg, string strServName);

        /// <summary>
        /// 发送单条延时短信
        /// </summary>
        /// <param name="strPhone">接收人电话</param>
        /// <param name="strMsg">信息内容</param>
        /// <param name="strServName">发送方标识</param>
        /// <returns></returns>
        public abstract ResultInfo SendDelaySMS(string strPhone, string strMsg, string strServName);

        /// <summary>
        /// 服務地址
        /// </summary>
        public string ServiceAddress
        {
            set;
            get;
        }

        /// <summary>
        /// 短信内容最大长度

        /// </summary>
        public int SMSBodyMaxLenght
        {
            set;
            get;
        }

        /// <summary>
        /// 最大接收人数量
        /// </summary>
        public int ReceivedByMaxQuatity
        {
            set;
            get;
        }
    }
}
