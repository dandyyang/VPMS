using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowControls.ClassLibrary.Public
{
    class DefineConstantValue
    {
        public DefineConstantValue() { }

        /// <summary>
        /// 系統信息提示描述定義
        /// </summary>
        public struct SystemMessage
        {
            public string strSystemText;
            /// <summary>
            /// 錯誤
            /// </summary>
            public string strSystemError;
            /// <summary>
            /// 詢問
            /// </summary>
            public string strQuestion;
            /// <summary>
            /// 信息
            /// </summary>
            public string strInformation;
            /// <summary>
            /// 警告
            /// </summary>
            public string strWarning;
            /// <summary>
            /// 密碼輸入框

            /// </summary>
            public string strInputBoxPassword;
            /// <summary>
            /// 標題
            /// </summary>
            public string strMessageTitle;
            /// <summary>
            /// 信息--提示記錄已被刪除
            /// </summary>
            public string strMessageText_I_RecordByDelete;
            /// <summary>
            /// 信息--提示記錄存檔失敗
            /// </summary>
            public string strMessageText_I_SaveFail;
            /// <summary>
            /// 信息--提示記錄新增成功
            /// </summary>
            public string strMessageText_I_AddSuccess;
            /// <summary>
            /// 信息--提示記錄新增失敗
            /// </summary>
            public string strMessageText_I_AddFail;
            /// <summary>
            /// 信息--提示記錄存檔成功
            /// </summary>
            public string strMessageText_I_SaveSuccess;
            /// <summary>
            /// 信息--提示記錄修改成功
            /// </summary>
            public string strMessageText_I_UpdateSuccess;
            /// <summary>
            /// 信息--提示記錄修改失敗
            /// </summary>
            public string strMessageText_I_UpdateFail;
            /// <summary>
            /// 信息--提示已經存在記錄
            /// </summary>
            public string strMessageText_I_RecordIsExist;
            /// <summary>
            /// 信息--提示用戶沒有功能權限
            /// </summary>
            public string strMessageText_I_strNotPurviewTitle;
            /// <summary>
            /// 没有找到记录
            /// </summary>
            public string strMessageText_I_strNotFindRecord;
            /// <summary>
            /// 輸入密碼提示
            /// </summary>
            public string strMessageText_I_strInputPassword;
            /// <summary>
            /// 詢問--是否要取消

            /// </summary>
            public string strMessageText_Q_Cancel;
            /// <summary>
            /// 詢問--是否要取消新增记录

            /// </summary>
            public string strMessageText_Q_Cancel_NR;
            /// <summary>
            /// 詢問--是否要取消修改记录

            /// </summary>
            public string strMessageText_Q_Cancel_UR;
            /// <summary>
            /// 詢問--是否要退出窗口

            /// </summary>
            public string strMessageText_Q_ExitWinForm;
            /// <summary>
            /// 詢問--是否要退出程序

            /// </summary>
            public string strMessageText_Q_ExitApplication;
            /// <summary>
            /// 詢問--是否要修改記錄

            /// </summary>
            public string strMessageText_Q_RecordByUpdate;
            /// <summary>
            /// 詢問--是否要刪除記錄

            /// </summary>
            public string strMessageText_Q_Delete;
            /// <summary>
            /// 詢問--記錄已存在是否要顯示
            /// </summary>
            public string strMessageText_Q_RecordIsExistToDisp;
            /// <summary>
            /// 询问--是否继续新增记录
            /// </summary>
            public string strMessageText_Q_ContinueAddRecord;
            /// <summary>
            /// 警告--不可以為空，請輸入！
            /// </summary>
            public string strMessageText_W_CannotEmpty;
            /// <summary>
            /// 警告--搜索条件不可以為空，請輸入！
            /// </summary>
            public string strMessageText_W_ConditionCannotEmpty;

            public SystemMessage(string str)
            {
                this.strSystemText = "江門利奧信領科技有限公司";
                this.strSystemError = "錯誤: ";
                this.strQuestion = "詢問: ";
                this.strInformation = "信息: ";
                this.strWarning = "警告: ";
                this.strInputBoxPassword = "密碼輸入框:";
                this.strMessageTitle = "系統--";
                this.strMessageText_I_RecordByDelete = "記錄已被刪除!";
                this.strMessageText_I_SaveSuccess = "存檔成功!";
                this.strMessageText_I_SaveFail = "存檔不成功!";
                this.strMessageText_I_AddSuccess = "新增記錄成功!";
                this.strMessageText_I_AddFail = "新增記錄失敗!";
                this.strMessageText_I_UpdateSuccess = "修改成功!";
                this.strMessageText_I_UpdateFail = "修改失敗!";
                this.strMessageText_I_RecordIsExist = "記錄已存在!";
                this.strMessageText_I_strNotPurviewTitle = "您沒有權限使用此功能！";
                this.strMessageText_I_strInputPassword = "請輸入密碼!";
                this.strMessageText_I_strNotFindRecord = "没有找到记录！";
                this.strMessageText_Q_Delete = "是否真的要刪除記錄嗎?";
                this.strMessageText_Q_Cancel = "是否真的要取消嗎?";
                this.strMessageText_Q_Cancel_NR = "是否真的要取消新增记录嗎?";
                this.strMessageText_Q_Cancel_UR = "是否真的要取消修改记录嗎?";
                this.strMessageText_Q_ExitWinForm = "是否真的要退出嗎?";
                this.strMessageText_Q_ExitApplication = "是否真的要關閉程序嗎?";
                this.strMessageText_Q_RecordByUpdate = "記錄已被修改，要繼續存檔嗎?";
                this.strMessageText_Q_RecordIsExistToDisp = "記錄已存在，要顯示此記錄嗎?";
                this.strMessageText_Q_ContinueAddRecord = "是否继续新增记录?";
                this.strMessageText_W_CannotEmpty = "不可以為空，請輸入！";
                this.strMessageText_W_ConditionCannotEmpty = "搜索条件不可以為空，請輸入！"; ;


            }
        }

        /// <summary>
        /// 系統信息提示描述定義
        /// </summary>
        public static readonly DefineConstantValue.SystemMessage SystemMessageText = new SystemMessage("");
    }
}
