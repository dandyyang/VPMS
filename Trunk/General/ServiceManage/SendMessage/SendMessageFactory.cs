using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.WebService;
using ServiceManage;

namespace ServiceManage.SendMessage
{
    public class SendMessageFactory
    {

        static readonly string _sendMessage = "ServiceManage.WebService.SendMessage,ServiceManage";

        static AbstractSendMessage _sendMessageObject=null;

        /// <summary>
        /// 獲得發送信息實體類
        /// </summary>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static AbstractSendMessage GetSendMessage(string userID ,string password)
        {
            if (_sendMessageObject == null)
            {
                //動態創建實例類型 
                try
                {
                    Type accessorType = Type.GetType(_sendMessage, false);
                    return (AbstractSendMessage)Activator.CreateInstance(accessorType,new object[] {userID,password});
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return _sendMessageObject;
            }

        }
    }
}
