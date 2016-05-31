using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.General
{
    public class SendMessageInfo : IModelObject
    {
        public SendMessageInfo()
        {
            this.smi_emailService = string.Empty;
            this.smi_emailUserID = string.Empty;
            this.smi_emailUserPassWord = string.Empty;
            this.smi_passWord = string.Empty;
            this.smi_userID = string.Empty;
        }

        /// <summary>
        /// 短信网关发送帐户
        /// </summary>
        public string smi_userID { get; set; }

        /// <summary>
        /// 短信网关发送帐户密码
        /// </summary>
        public string smi_passWord { get; set; }

        /// <summary>
        /// 邮件服务地址
        /// </summary>
        public string smi_emailService { get; set; }

        /// <summary>
        /// 发送邮件帐户
        /// </summary>
        public string smi_emailUserID { get; set; }

        /// <summary>
        /// 发送邮件帐户密码
        /// </summary>
        public string smi_emailUserPassWord { get; set; }

        #region IModelObject 成员

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
