using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.SendMessage;
using System.Configuration;

namespace ServiceManage.WebService
{
    public class SendMessage : AbstractSendMessage
    {
        SendMessagesService.hsyzSoapClient _sendClient;

        private SendDelayMessage.wsMsgSending _MsgDelaySender;

        public SendMessage(string userID, string passWord)
        {
            this.UserID = userID;
            this.PassWord = passWord;

            try
            {
                this._sendClient = new ServiceManage.SendMessagesService.hsyzSoapClient();

                this.SMSBodyMaxLenght = Convert.ToInt32(ConfigurationSettings.AppSettings["SendMessageBodyMaxLenght"].Trim());

                this._MsgDelaySender = new ServiceManage.SendDelayMessage.wsMsgSending();
                this._MsgDelaySender.Url = ConfigurationSettings.AppSettings["MsgDelaySenderWebService"];
            }
            catch (Exception Ex)
            {

            }
        }

        public override ResultInfo SendMessageText(List<string> toPhoneList, string messageText)
        {
            ResultInfo resultInfo = new ResultInfo();
            string toPhones = string.Empty;

            if (toPhoneList != null && toPhoneList.Count > 0)
            {
                int count = 11;

                if (toPhoneList.Count < count)
                {
                    count = toPhoneList.Count;
                }

                for (int i = 0; i < count; i++)
                {
                    toPhones += toPhones.Trim() == "" ? toPhoneList[i].Trim() : "," + toPhoneList[i].Trim();
                }
            }
            else
            {
                resultInfo.boolValue = false;
                resultInfo.messageText = "To phone List is empty!";

                return resultInfo;
            }

            resultInfo = Send(toPhones, messageText);

            return resultInfo;
        }

        public override ResultInfo SendMessageText(string toPhone, string messageText)
        {
            ResultInfo resultInfo = new ResultInfo();
            if (toPhone.Trim() == "")
            {
                resultInfo.boolValue = false;
                resultInfo.messageText = "To phone is empty!";

                return resultInfo;
            }

            resultInfo = Send(toPhone, messageText);

            return resultInfo;
        }

        private ResultInfo Send(string toPhones, string messageText)
        {
            string rulst = string.Empty;
            ResultInfo resultInfo = new ResultInfo();

            if (this.UserID.Trim() == "")
            {
                resultInfo.boolValue = false;
                resultInfo.messageText = "没有登录帐户!";

                return resultInfo;
            }

            if (this._sendClient != null)
            {
                string body = messageText;

                if (messageText.Length > this.SMSBodyMaxLenght)
                {
                    body = messageText.Substring(0, this.SMSBodyMaxLenght);
                }

                try
                {
                    rulst = this._sendClient.SendSms(this.UserID, this.PassWord, toPhones, body);
                }
                catch (Exception Ex)
                {
                    resultInfo.isError = true;
                    resultInfo.messageText = Ex.Message;

                    return resultInfo;
                }
            }

            if (rulst.Trim() == "1")
            {
                resultInfo.boolValue = true;
                resultInfo.messageText = "Send Message Successed!";
            }
            else
            {
                resultInfo.boolValue = false;
                resultInfo.messageText = "Send Message Failed!";
            }

            return resultInfo;
        }

        public override ResultInfo SendDelaySMS(List<string> listRecPhone, string strMsg, string strServName)
        {
            ResultInfo rvInfo = new ResultInfo();
            try
            {
                if (listRecPhone == null)
                {
                    rvInfo.isError = true;
                    rvInfo.messageText = "Params Invalid : data list is null";
                    return rvInfo;
                }
                if (listRecPhone.Count == 0)
                {
                    rvInfo.isError = true;
                    rvInfo.messageText = "Params Invalid : data list is empty";
                    return rvInfo;
                }

                rvInfo.boolValue = this._MsgDelaySender.SendBatchSms(listRecPhone.ToArray(), strMsg, strServName);

            }
            catch (Exception ex)
            {
                rvInfo.messageText = ex.Message;
                rvInfo.isError = true;
            }
            return rvInfo;
        }

        public override ResultInfo SendDelaySMS(string strPhone, string strMsg, string strServName)
        {
            List<string> listUserNum = new List<string>();
            listUserNum.Add(strPhone);
            return SendDelaySMS(listUserNum, strMsg, strServName);
        }
    }
}
