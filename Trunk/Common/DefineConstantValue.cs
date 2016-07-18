using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace Common
{
    public class DefineConstantValue
    {
        public DefineConstantValue() { }

        /// <summary>
        /// 系統信息提示描述定義
        /// </summary>
        public struct SystemMessage
        {
            public string strSystemText;
            /// <summary>
            /// 系统提示
            /// </summary>
            public string strSystemMessage;
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
            /// 信息--提示文件上传失敗
            /// </summary>
            public string strMessageText_I_UploadFileFail;
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
            /// 信息--提示选择要打印的记录
            /// </summary>
            public string strMessageText_I_SelectRecordToPrint;

            /// <summary>
            /// 导出文件成功
            /// </summary>
            public string strMessageText_I_ExportFileSuccess;

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
            /// 询问--是否打印所选择的记录
            /// </summary>
            public string strMessageText_Q_PrintSelectedRecord;

            /// <summary>
            /// 询问--是否打印充值凭证
            /// </summary>
            public string strMessageText_Q_PrintRechargeMoneyVoucher;

            /// <summary>
            /// 询问--是否打印充值历史凭证
            /// </summary>
            public string strMessageText_Q_PrintRechargeMoneyHistoryVoucher;

            /// <summary>
            /// 警告--不可以為空，請輸入！
            /// </summary>
            public string strMessageText_W_CannotEmpty;
            /// <summary>
            /// 警告--搜索条件不可以為空，請輸入！
            /// </summary>
            public string strMessageText_W_ConditionCannotEmpty;
            /// <summary>
            /// 警告--要保存的记录为空
            /// </summary>
            public string strMessageText_W_RecordIsEmpty;
            /// <summary>
            /// 警告--对象不能为空对象
            /// </summary>
            public string strMessageText_W_ObjectCannotNull;

            /// <summary>
            /// 错误--发生未知错误！
            /// </summary>
            public string strMessageText_E_UnknownError;

            public SystemMessage(string str)
            {
                this.strSystemText = "有限公司";
                this.strSystemMessage = "系统提示";
                this.strSystemError = "错误: ";
                this.strQuestion = "询问: ";
                this.strInformation = "信息: ";
                this.strWarning = "警告: ";
                this.strInputBoxPassword = "密码输入框:";
                this.strMessageTitle = "系统--";
                this.strMessageText_I_RecordByDelete = "记录已被删除!";
                this.strMessageText_I_SaveSuccess = "存档成功!";
                this.strMessageText_I_SaveFail = "存档不成功!";
                this.strMessageText_I_AddSuccess = "新增记录成功!";
                this.strMessageText_I_AddFail = "新增记录失败!";
                this.strMessageText_I_UpdateSuccess = "修改成功!";
                this.strMessageText_I_UpdateFail = "修改失败!";
                this.strMessageText_I_UploadFileFail = "文件上传失败!";
                this.strMessageText_I_RecordIsExist = "记录已存在!";
                this.strMessageText_I_strNotPurviewTitle = "您没有权限使用此功能！";
                this.strMessageText_I_strInputPassword = "请输入密码!";
                this.strMessageText_I_strNotFindRecord = "没有找到记录！";
                this.strMessageText_I_SelectRecordToPrint = "请选择要进行打印的记录！";
                this.strMessageText_I_ExportFileSuccess = "导出文件成功！";
                this.strMessageText_Q_Delete = "是否真的要删除记录?";
                this.strMessageText_Q_Cancel = "是否真的要取消?";
                this.strMessageText_Q_Cancel_NR = "是否真的要取消新增记录?";
                this.strMessageText_Q_Cancel_UR = "是否真的要取消修改记录?";
                this.strMessageText_Q_ExitWinForm = "是否真的要退出?";
                this.strMessageText_Q_ExitApplication = "是否真的要关闭程序?";
                this.strMessageText_Q_RecordByUpdate = "记录已被修改，要继续存档吗?";
                this.strMessageText_Q_RecordIsExistToDisp = "记录已存在，要显示此记录吗?";
                this.strMessageText_Q_ContinueAddRecord = "是否继续新增记录?";
                this.strMessageText_Q_PrintSelectedRecord = "是否打印所选择的记录？";
                this.strMessageText_Q_PrintRechargeMoneyVoucher = "是否打印充值凭证？";
                this.strMessageText_Q_PrintRechargeMoneyHistoryVoucher = "是否打印所选的历史充值记录凭证？";
                this.strMessageText_W_CannotEmpty = "不可以为空，请输入！";
                this.strMessageText_W_ConditionCannotEmpty = "搜索条件不可以为空，请输入！";
                this.strMessageText_W_RecordIsEmpty = "记录为空!";
                this.strMessageText_W_ObjectCannotNull = "对象不能为空！";
                this.strMessageText_E_UnknownError = "发生未知错误！";
            }
        }
        /// <summary>
        /// 系統信息提示描述定義
        /// </summary>
        public static readonly SystemMessage SystemMessageText = new SystemMessage(string.Empty);

        /// <summary>
        ///发送通知服务消息类型
        /// </summary>
        public enum SendMessageType
        {
            SMS = 0,
            EMAIL = 1,
            SMSandEMAIL = 2,
            SMSorEMAIL = 3
        }

        /// <summary>
        /// 標識Class對數據庫的操作(通過位運算保存Object的操作)
        /// </summary>
        public enum EditStateEnum
        {
            OE_Insert = 1,
            OE_Update = 2,
            OE_Delete = 4,
            OE_ReaOnly = 8,
        }

        /// <summary>
        /// 標式Class對獲取記錄的操作(通過位運算保存Object的操作)
        /// </summary>
        public enum GetReocrdEnum
        {
            GR_First = 1,
            GR_Next = 2,
            GR_Middle = 3,
            GR_Previous = 4,
            GR_Last = 8,
            GR_Null = 10
        }

        /// <summary>
        /// 星期，中文对应数字
        /// </summary>
        public enum Week
        {
            星期一 = 1,
            星期二 = 2,
            星期三 = 3,
            星期四 = 4,
            星期五 = 5,
            星期六 = 6,
            星期日 = 0
        }

        /// <summary>
        /// 主檔類型
        /// </summary>
        public enum MasterType
        {
            /// <summary>
            /// 院系部主檔
            /// </summary>
            SchoolMaster,

            /// <summary>
            /// 專業主檔
            /// </summary>
            SpecialtyMaster,

            /// <summary>
            /// 建築物主檔
            /// </summary>
            BuildingMaster,

            /// <summary>
            /// 科室主檔
            /// </summary>
            DepartmentMaster,

            /// <summary>
            /// 地點主檔
            /// </summary>
            SiteMaster,

            /// <summary>
            /// 科目主檔
            /// </summary>
            CourseMaster,

            /// <summary>
            /// 皮肤
            /// </summary>
            WinFormSkin,

            /// <summary>
            /// CodeMasterKey1
            /// </summary>
            CodeMaster_Key1,

            /// <summary>
            /// CodeMasterKey2
            /// </summary>
            CodeMaster_Key2,

            /// <summary>
            /// 消息平台
            /// </summary>
            WebMessage,

            /// <summary>
            /// 公用设备管理 入仓类型
            /// </summary>
            SIOT_AMS_INTRX,

            /// <summary>
            /// 公用设备管理 出仓类型
            /// </summary>
            SIOT_AMS_OUTTRX,

            /// <summary>
            /// 建築物地點主檔
            /// 備註：其實是地點主檔，但是在格式是地點前增加建築物名稱，如：教學樓--102室
            /// </summary>
            BuildingSiteMaster,

            /// <summary>
            /// 查询老师消费明细权限
            /// </summary>
            RightToQueryForTeacherDetailPaymentInfo,

            /// <summary>
            /// 查询班级学生消费明细权限
            /// </summary>
            RightToQueryForClassDetailPaymentInfo,

            /// <summary>
            /// 查询老师考勤明细权限
            /// </summary>
            RightToQueryForTeachervAttenaceInfo,

            /// <summary>
            /// 查询班级考勤红明细权限
            /// </summary>
            RightToQueryForStuddentAttenaceInfo,

            /// <summary>
            /// 科室下所有教师
            /// </summary>
            Department_Teacher,

            /// <summary>
            /// 供应商
            /// </summary>
            SupplierMaster,

        }

        /// <summary>
        /// 日期格式  yyyy/MM/dd
        /// </summary>
        public const string gc_DateFormat = "yyyy/MM/dd";

        /// <summary>
        /// 日期格式  yyyy-MM-dd
        /// </summary>
        public const string gc_DateFormat2 = "yyyy-MM-dd";

        /// <summary>
        /// 密码日期格式
        /// </summary>
        public const string gc_PwdDateFormat = "yyyy,MM,dd";

        /// <summary>
        /// 密码日期格式（互动离线系统）
        /// </summary>
        public const string gc_PwdDateFormat1 = "yyyyMMdd";

        /// <summary>
        /// 查詢後返回最大行數
        /// </summary>
        public const int ListRecordMaxCount = 1000;

        /// <summary>
        /// 长日期時間格式
        /// </summary>
        public const string gc_DateTimeFormat = "yyyy/MM/dd HH:mm:ss";

        public const string gc_DateTimeFormat2 = "HH:mm";

        /// <summary>
        /// 锁记录的最大时间（小时）
        /// </summary>
        public const short LockRecordMaxTime = 5;

        public const string gc_WeeklyDec = "周假";

        /// <summary>
        /// 报表文件根目录
        /// </summary>
        public static string ReportFileBasePath = Application.StartupPath + @"\Files\Report\";

        /// <summary>
        /// 系统文件路径
        /// </summary>
        public static string FilesPath = Application.StartupPath + @"\Files\";

        /// <summary>
        /// 系统类型主档记录中的编号与名称的分隔符号定义
        /// </summary>
        public static string SystemTypeMasterRecordNumAndNameSeparator = "-";

        /// <summary>
        /// 功能描述
        /// </summary>
        public struct stuSysFunction
        {
            /// <summary>
            /// 新增
            /// </summary>
            public string INSERT;
            /// <summary>
            /// 修改
            /// </summary>
            public string UPDATE;
            /// <summary>
            /// 删除
            /// </summary>
            public string DELETE;
            /// <summary>
            /// 浏览
            /// </summary>
            public string BROWSE;

            public stuSysFunction(string str)
            {
                this.INSERT = "INSERT";
                this.UPDATE = "UPDATE";
                this.DELETE = "DELETE";
                this.BROWSE = "BROWSE";
            }
        }

        public struct CodeMasterDefineStruct
        {
            /// <summary>
            /// Key1值定義--卡用戶管理主檔.性別
            /// </summary>
            public string KEY1_SIOT_CardUserSex;

            /// <summary>
            /// Key1值定義--卡用戶管理主檔.身份
            /// </summary>
            public string KEY1_SIOT_CardUserIdentity;

            /// <summary>
            /// Key2值定義--教育物聯網--卡用戶身份--學生
            /// </summary>
            public string KEY2_SIOT_CardUserIdentity_Student;

            /// <summary>
            /// Key2值定義--教育物聯網--卡用戶身份--教職人員
            /// </summary>
            public string KEY2_SIOT_CardUserIdentity_Staff;

            /// <summary>
            /// Key1值定義--卡用戶管理主檔.班級
            /// </summary>
            public string KEY1_SIOT_CardUserClass;

            /// <summary>
            /// Key1值定義--課程考勤設置
            /// </summary>
            public string KEY1_SIOT_CourseCOWASetting;

            /// <summary>
            /// Key2值定義--教育物聯網--課程考勤設置--全部
            /// </summary>
            public string KEY2_SIOT_CourseCOWASetting_All;

            /// <summary>
            /// Key2值定義--教育物聯網--課程考勤設置--上課
            /// </summary>
            public string KEY2_SIOT_CourseCOWASetting_AttendClass;

            /// <summary>
            /// Key2值定義--教育物聯網--課程考勤設置--下課
            /// </summary>
            public string KEY2_SIOT_CourseCOWASetting_FinishClass;

            /// <summary>
            /// Key2值定義--教育物聯網--課程考勤設置--N/A
            /// </summary>
            public string KEY2_SIOT_CourseCOWASetting_NA;

            /// <summary>
            /// Key1值定義--教育物聯網--考勤读写器使用类型
            /// </summary>
            public string KEY1_SIOT_READEROFCONTROLLER;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤读写器使用类型--教师
            /// </summary>
            public string KEY2_SIOT_READEROFCONTROLLER_T;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤读写器使用类型--课室
            /// </summary>
            public string KEY2_SIOT_READEROFCONTROLLER_C;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤读写器使用类型--门禁
            /// </summary>
            public string KEY2_SIOT_READEROFCONTROLLER_R;

            /// <summary>
            /// Key1值定義--教育物聯網--RFID卡使用状态
            /// </summary>
            public string KEY1_SIOT_RFIDCARDUSESTATUS;

            /// <summary>
            /// Key2值定義--教育物聯網--RFID卡使用状态--未使用
            /// </summary>
            public string KEY2_SIOT_RFIDCARDUSESTATUS_UNUSED;

            /// <summary>
            /// Key2值定義--教育物聯網--RFID卡使用状态--使用中
            /// </summary>
            public string KEY2_SIOT_RFIDCARDUSESTATUS_USING;

            /// <summary>
            /// Key2值定義--教育物聯網--RFID卡使用状态--挂失
            /// </summary>
            public string KEY2_SIOT_RFIDCARDUSESTATUS_MISSING;

            /// <summary>
            /// Key2值定義--教育物聯網--RFID卡使用状态--报废
            /// </summary>
            public string KEY2_SIOT_RFIDCARDUSESTATUS_SCRAP;

            /// <summary>
            /// Key2值定義--教育物聯網--RFID卡使用状态--遗失/损毁
            /// </summary>
            public string KEY2_SIOT_RFIDCARDUSESTATUS_DESTROY;

            /// <summary>
            /// Key1值定義--教育物聯網--考勤時段設置中的出入类型
            /// </summary>
            public string KEY1_SIOT_COWASETTINGPASSTYPE;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤時段設置中的出入类型--出
            /// </summary>
            public string KEY2_SIOT_COWASETTINGPASSTYPE_OUT;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤時段設置中的出入类型--入
            /// </summary>
            public string KEY2_SIOT_COWASETTINGPASSTYPE_IN;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤時段設置中的出入类型--停留
            /// </summary>
            public string KEY2_SIOT_COWASETTINGPASSTYPE_STOP;

            /// <summary>
            /// Key2值定義--教育物聯網--考勤時段設置中的出入类型--无记录
            /// </summary>
            public string KEY2_SIOT_COWASETTINGPASSTYPE_NONE;

            /// <summary>
            /// 周末日期定义
            /// </summary>
            public string KEY1_SIOT_WeeklySetting;

            /// <summary>
            /// WinForm皮肤
            /// </summary>
            public string KEY1_WINFORM_SKIN;

            /// <summary>
            /// Key2值定義--教育物聯網--周末假期设置登记
            /// </summary>
            public string KEY1_SIOT_WeekHolidaySetting;

            /// <summary>
            /// Key1门禁类型
            /// </summary>
            public string KEY1_SIOT_EntranceGuardType;

            /// <summary>
            /// Key2门禁类型--常开
            /// </summary>
            public string KEY2_SIOT_EntranceGuardType_O;

            /// <summary>
            /// Key2门禁类型--常关
            /// </summary>
            public string KEY2_SIOT_EntranceGuardType_L;

            /// <summary>
            /// Key2门禁类型--权限开门
            /// </summary>
            public string KEY2_SIOT_EntranceGuardType_R;

            /// <summary>
            /// Key1监控信息发送事件
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGEEVENT;

            /// <summary>
            /// Key2监控信息发送事件--到达时发信息
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGEEVENT_ARRIVEIN;

            /// <summary>
            /// Key2监控信息发送事件--末到时发信息
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGEEVENT_NORECORD;

            /// <summary>
            /// Key2监控信息发送事件--迟到时发信息
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGEEVENT_COMELATE;

            /// <summary>
            /// Key1监控信息接收对象
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGERECIPIENT;

            /// <summary>
            /// Key2监控信息接收对象--短信家长
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGERECIPIENT_MSMPATRIARCH;

            /// <summary>
            /// Key2监控信息接收对象--邮件家长
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGERECIPIENT_EMAILPATRIARCH;

            /// <summary>
            /// Key2监控信息接收对象--短信老师
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGERECIPIENT_MSMTEACH;

            /// <summary>
            /// Key2监控信息接收对象--邮件老师
            /// </summary>
            public string KEY1_SIOT_MONITORMESSAGERECIPIENT_EMAILTEACH;

            /// <summary>
            /// Key1公用设备管理 入仓类型
            /// </summary>
            public string KEY1_SIOT_AMS_INTRX;

            /// <summary>
            /// Key2公用设备管理--入仓
            /// </summary>
            public string KEY1_SIOT_AMS_INTRX_IN;

            /// <summary>
            /// Key2公用设备管理--返仓
            /// </summary>
            public string KEY1_SIOT_AMS_INTRX_RETURN;

            /// <summary>
            /// Key1公用设备管理  出仓类型
            /// </summary>
            public string KEY1_SIOT_AMS_OUTTRX;

            /// <summary>
            /// Key2公用设备管理--出仓
            /// </summary>
            public string KEY1_SIOT_AMS_OUTTRX_OUT;

            /// <summary>
            /// Key2公用设备管理--报废
            /// </summary>
            public string KEY1_SIOT_AMS_OUTTRX_SCRAP;

            /// <summary>
            /// Key2公用设备管理--借用
            /// </summary>
            public string KEY1_SIOT_AMS_OUTTRX_BORROW;

            /// <summary>
            /// 校园卡打印模板
            /// </summary>
            public string KEY1_SIOT_PINTER_MODEL;

            /// <summary>
            /// 2.4G考勤数据异常处理时--选择原因
            /// </summary>
            public string KEY1_SIOT_24G_UNUSUALRECORD_CAUSE;

            /// <summary>
            /// 2.4G考勤数据异常处理时--选择原因1
            /// </summary>
            public string KEY2_SIOT_24G_UNUSUALRECORD_CAUSE_CAUSE1;

            /// <summary>
            /// 2.4G考勤数据异常处理时--选择原因2
            /// </summary>
            public string KEY2_SIOT_24G_UNUSUALRECORD_CAUSE_CAUSE2;

            /// <summary>
            /// 学生走读类型
            /// </summary>
            public string KEY1_SIOT_GOTOSCHOOLTYPE;

            /// <summary>
            /// 用户卡操作类型
            /// </summary>
            public string KEY1_SIOT_CARDOPERATIONTYPE;

            /// <summary>
            /// 用户卡操作类型--卡挂失
            /// </summary>
            public string KEY2_SIOT_CARDOPERATIONTYPE_SUSPEND;

            /// <summary>
            /// 用户操作类型--卡解挂
            /// </summary>
            public string KEY2_SIOT_CARDOPERATIONTYPE_DEBLOCK;

            /// <summary>
            /// 财务--缴费记录类型
            /// </summary>
            public string KEY1_SIOT_FINANCEPAYMENTRECORDTYPE;

            /// <summary>
            /// 财务--缴费记录类型--餐费
            /// </summary>
            public string KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_TABLEMONEY;

            /// <summary>
            /// 财务--缴费记录类型--子类型--早餐
            /// </summary>
            public string KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_BREAKFAST;

            /// <summary>
            /// 财务--缴费记录类型--子类型--正餐
            /// </summary>
            public string KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_DINNER;

            /// <summary>
            /// 缴费方式
            /// </summary>
            public string KEY1_SIOT_TypeOfPayment;

            /// <summary>
            /// 缴费方式--现金
            /// </summary>
            public string KEY2_SIOT_TypeOfPayment_Cash;

            /// <summary>
            /// 缴费方式--银行代扣
            /// </summary>
            public string KEY2_SIOT_TypeOfPayment_Bank;

            /// <summary>
            /// 物品类型根类型定义
            /// </summary>
            public string KEY1_SIOT_ARTICLETYPEDEFINE;

            /// <summary>
            /// 物品类型根类型定义--财务支出类型
            /// </summary>
            public string KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE;

            /// <summary>
            /// 物品类型根类型定义--财务收入类型
            /// </summary>
            public string KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE;

            /// <summary>
            /// 物品类型根类型定义--财务资产类型
            /// </summary>
            public string KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY;

            /// <summary>
            /// 物品类型根类型定义--财务负债类型
            /// </summary>
            public string KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES;

            /// <summary>
            /// 物品类型根类型定义--财务净资产类型
            /// </summary>
            public string KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS;

            /// <summary>
            /// 财务收入支出方式
            /// </summary>
            public string KEY1_SIOT_FinanceIncomeOrPayPattern;

            /// <summary>
            /// 财务收入支出方式--现金
            /// </summary>
            public string KEY2_SIOT_FinanceIncomeOrPayPattern_Cash;

            /// <summary>
            /// 财务收入支出方式--银行
            /// </summary>
            public string KEY2_SIOT_FinanceIncomeOrPayPattern_Bank;


            public CodeMasterDefineStruct(string str)
            {
                this.KEY2_SIOT_24G_UNUSUALRECORD_CAUSE_CAUSE2 = "SIOT_24GUnusualRecordCause2";

                this.KEY2_SIOT_24G_UNUSUALRECORD_CAUSE_CAUSE1 = "SIOT_24GUnusualRecordCause1";

                this.KEY1_SIOT_24G_UNUSUALRECORD_CAUSE = "SIOT_24GUnusualRecordCause";

                this.KEY1_SIOT_PINTER_MODEL = "SIOT_Pinter_Model";

                this.KEY1_SIOT_CardUserSex = "SIOT_CARDUSERSEX";

                this.KEY1_SIOT_CardUserIdentity = "SIOT_CARDUSERIDENTITY";
                this.KEY2_SIOT_CardUserIdentity_Student = "STUDENT";
                this.KEY2_SIOT_CardUserIdentity_Staff = "STAFF";


                this.KEY1_SIOT_CardUserClass = "SIOT_CARDUSERCLASS";

                this.KEY1_SIOT_CourseCOWASetting = "SIOT_COURSECOWASETTING";
                this.KEY2_SIOT_CourseCOWASetting_All = "ALL";
                this.KEY2_SIOT_CourseCOWASetting_AttendClass = "ATTENDCLASS";
                this.KEY2_SIOT_CourseCOWASetting_FinishClass = "FINISHCLASS";
                this.KEY2_SIOT_CourseCOWASetting_NA = "NA";

                this.KEY1_SIOT_READEROFCONTROLLER = "READERUSETYPE";
                this.KEY2_SIOT_READEROFCONTROLLER_C = "C";
                this.KEY2_SIOT_READEROFCONTROLLER_T = "T";
                this.KEY2_SIOT_READEROFCONTROLLER_R = "R";


                this.KEY1_SIOT_RFIDCARDUSESTATUS = "SIOT_RFIDCARDUSESTATUS";
                this.KEY2_SIOT_RFIDCARDUSESTATUS_UNUSED = "UNUSED";
                this.KEY2_SIOT_RFIDCARDUSESTATUS_USING = "USING";
                this.KEY2_SIOT_RFIDCARDUSESTATUS_MISSING = "MISSING";
                this.KEY2_SIOT_RFIDCARDUSESTATUS_SCRAP = "SCRAP";
                this.KEY2_SIOT_RFIDCARDUSESTATUS_DESTROY = "DESTROY";

                this.KEY1_SIOT_COWASETTINGPASSTYPE = "SIOT_COWASETTINGPASSTYPE";
                this.KEY2_SIOT_COWASETTINGPASSTYPE_OUT = "OUT";
                this.KEY2_SIOT_COWASETTINGPASSTYPE_IN = "IN";
                this.KEY2_SIOT_COWASETTINGPASSTYPE_STOP = "STOP";
                this.KEY2_SIOT_COWASETTINGPASSTYPE_NONE = "NONE";

                this.KEY1_WINFORM_SKIN = "WINFORM_SKIN";

                this.KEY1_SIOT_WeeklySetting = "SIOT_WeeklySetting";

                this.KEY1_SIOT_WeekHolidaySetting = "SIOT_WEEKHOLIDAYSETTING";

                this.KEY1_SIOT_EntranceGuardType = "SIOT_EntranceGuardType";
                this.KEY2_SIOT_EntranceGuardType_O = "O";
                this.KEY2_SIOT_EntranceGuardType_L = "L";
                this.KEY2_SIOT_EntranceGuardType_R = "R";

                this.KEY1_SIOT_MONITORMESSAGEEVENT = "SIOT_MONITORMESSAGEEVENT";
                this.KEY1_SIOT_MONITORMESSAGEEVENT_ARRIVEIN = "ARRIVEIN";
                this.KEY1_SIOT_MONITORMESSAGEEVENT_NORECORD = "NORECORD";
                this.KEY1_SIOT_MONITORMESSAGEEVENT_COMELATE = "COMELATE";

                this.KEY1_SIOT_MONITORMESSAGERECIPIENT = "SIOT_MONITORMESSAGERECIPIENT";
                this.KEY1_SIOT_MONITORMESSAGERECIPIENT_MSMPATRIARCH = "MSMPATRIARCH";
                this.KEY1_SIOT_MONITORMESSAGERECIPIENT_EMAILPATRIARCH = "EMAILPATRIARCH";
                this.KEY1_SIOT_MONITORMESSAGERECIPIENT_MSMTEACH = "MSMTEACH";
                this.KEY1_SIOT_MONITORMESSAGERECIPIENT_EMAILTEACH = "EMAILTEACH";

                this.KEY1_SIOT_AMS_INTRX = "SIOT_AMS_INTRX";
                this.KEY1_SIOT_AMS_INTRX_IN = "SIOT_AMS_IN";
                this.KEY1_SIOT_AMS_INTRX_RETURN = "SIOT_AMS_RETURN";

                this.KEY1_SIOT_AMS_OUTTRX = "SIOT_AMS_OUTTRX";
                this.KEY1_SIOT_AMS_OUTTRX_OUT = "SIOT_AMS_OUT";
                this.KEY1_SIOT_AMS_OUTTRX_SCRAP = "SIOT_AMS_SCRAP";
                this.KEY1_SIOT_AMS_OUTTRX_BORROW = "SIOT_AMS_BORROW";

                this.KEY1_SIOT_GOTOSCHOOLTYPE = "SIOT_GOTOSCHOOLTYPE";

                this.KEY1_SIOT_CARDOPERATIONTYPE = "SIOT_CARDOPERATIONTYPE";
                this.KEY2_SIOT_CARDOPERATIONTYPE_SUSPEND = "SIOT_CARDOPERATIONTYPE_SUSPEND";
                this.KEY2_SIOT_CARDOPERATIONTYPE_DEBLOCK = "SIOT_CARDOPERATIONTYPE_DEBLOCK";

                this.KEY1_SIOT_FINANCEPAYMENTRECORDTYPE = "SIOT_FINANCEPAYMENTRECORDTYPE";
                this.KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_TABLEMONEY = "TABLEMONEY";
                this.KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_BREAKFAST = "BREAKFAST";
                this.KEY2_SIOT_FINANCEPAYMENTRECORDTYPE_DINNER = "DINNER";

                this.KEY1_SIOT_ARTICLETYPEDEFINE = "SIOT_ARTICLETYPEDEFINE";
                this.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE = "FINANCEEXPENDITURE";
                this.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE = "FINANCIALREVENUE";
                this.KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY = "PROPERTY";
                this.KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES = "LIABILITIES";
                this.KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS = "NETASSETS";



                this.KEY1_SIOT_TypeOfPayment = "SIOT_TYPEOFPAYMENT";
                this.KEY2_SIOT_TypeOfPayment_Cash = "CASH";
                this.KEY2_SIOT_TypeOfPayment_Bank = "BANK";

                this.KEY1_SIOT_FinanceIncomeOrPayPattern = "SIOT_FINANCEINCOMEORPAYPATTERN";
                this.KEY2_SIOT_FinanceIncomeOrPayPattern_Cash = "CASH";
                this.KEY2_SIOT_FinanceIncomeOrPayPattern_Bank = "BANK";

            }
        }
        /// <summary>
        /// 字码主档定义
        /// </summary>
        public static readonly DefineConstantValue.CodeMasterDefineStruct CodeMasterDefine = new CodeMasterDefineStruct(string.Empty);

        public struct FormFunctionNum
        {
            /// <summary>
            /// 新增
            /// </summary>
            public string New;

            /// <summary>
            /// 修改
            /// </summary>
            public string Modify;

            /// <summary>
            /// 刪除
            /// </summary>
            public string Delete;

            /// <summary>
            /// 发卡
            /// </summary>
            public string CardIssuance;

            /// <summary>
            /// 导入
            /// </summary>
            public string DataInput;

            /// <summary>
            /// 导出
            /// </summary>
            public string DataExport;

            /// <summary>
            /// 退卡
            /// </summary>
            public string CardReturn;

            /// <summary>
            /// 掛失
            /// </summary>
            public string CardMissing;

            /// <summary>
            /// 解掛
            /// </summary>
            public string CardRecovery;

            /// <summary>
            /// 報廢
            /// </summary>
            public string CardScrap;

            /// <summary>
            /// 浏览
            /// </summary>
            public string BROWSE;

            /// <summary>
            /// 查询
            /// </summary>
            public string SEARCH;

            /// <summary>
            /// 导出卡用户数据
            /// </summary>
            public string ExpCusData;

            /// <summary>
            /// 导出卡用户数据模板
            /// </summary>
            public string ExportTemplate;

            /// <summary>
            /// 导出相片
            /// </summary>
            public string ExportCardUserPhoto;

            /// <summary>
            /// 导入卡用户数据相片
            /// </summary>
            public string ImportCardUserPhoto;

            /// <summary>
            /// 导入卡用户数据
            /// </summary>
            public string ImportCardUserData;

            /// <summary>
            /// 卡用户 用户监控事项组别设定
            /// </summary>
            public string GroupPersonSetting;

            /// <summary>
            /// 职工请假
            /// </summary>
            public string StaffAbsence;

            /// <summary>
            /// 学生请假
            /// </summary>
            public string StudentAbsence;

            /// <summary>
            /// 导入题库
            /// </summary>
            public string ImportTheme;

            /// <summary>
            /// 导出题库
            /// </summary>
            public string ExportTheme;

            public FormFunctionNum(string txt)
            {
                this.New = "NEW";
                this.Modify = "MODIFY";
                this.Delete = "DELETE";
                this.CardIssuance = "CARDISSUANCE";
                this.DataInput = "DATAINPUT";
                this.CardReturn = "CARDRETURN";
                this.CardMissing = "CARDMISSING";
                this.CardRecovery = "CARDRECOVERY";
                this.CardScrap = "CARDSCRAP";
                this.BROWSE = "BROWSE";
                this.SEARCH = "SEARCH";
                this.DataExport = "DATAEXPORT";
                this.ExpCusData = "EXPCUSDATA";
                this.ExportTemplate = "EXPROTTEMPLATE";
                this.ExportCardUserPhoto = "EXPROTCARDUSERPHOTO";
                this.ImportCardUserPhoto = "IMPORTCARDUSERPHOTO";
                this.ImportCardUserData = "IMPORTCARDUSERDATA";
                this.GroupPersonSetting = "GROUPPERSONSETTING";

                this.StaffAbsence = "STAFFABSENCE";
                this.StudentAbsence = "STUDENTABSENCE";

                this.ImportTheme = "IMPORTTHEME";
                this.ExportTheme = "EXPORTTHEME";
            }
        }
        /// <summary>
        /// 界面功能编号
        /// </summary>
        public static readonly FormFunctionNum Sys_FormFunctionNum = new FormFunctionNum(string.Empty);

        /// <summary>
        /// 客户代码
        /// </summary>
        public struct ClientKeyStruct
        {
            public string ClientKey;

            public ClientKeyStruct(string txt)
            {

                try
                {
                    this.ClientKey = ConfigurationSettings.AppSettings["ClientKey"].Trim();
                }
                catch (Exception Ex)
                {

                    try
                    {
                        this.ClientKey = System.Configuration.ConfigurationSettings.AppSettings["ClientKey"].Trim();
                    }
                    catch (Exception ex)
                    {

                        this.ClientKey = string.Empty;
                    }

                }
            }
        }
        public static readonly ClientKeyStruct ClientKey = new ClientKeyStruct(string.Empty);

        /// <summary>
        /// 数据权限类型
        /// </summary>
        public struct SIOT_DataRightsType
        {
            /// <summary>
            /// 消费
            /// </summary>
            public string Payment;
            /// <summary>
            /// 考勤
            /// </summary>
            public string Attendance;

            public SIOT_DataRightsType(string str)
            {
                this.Payment = "PAYMENT";
                this.Attendance = "ATTENDANCE";
            }
        }
        public static readonly SIOT_DataRightsType SIOT_DataRightsTypeDefine = new SIOT_DataRightsType(string.Empty);

        /// <summary>
        /// 教育物联网--日志内容类型
        /// </summary>
        public struct SIOT_LogDetailType
        {
            /// <summary>
            /// 异常型记录
            /// </summary>
            public string TypeError;
            /// <summary>
            /// 特殊型记录
            /// </summary>
            public string TypeWarning;
            /// <summary>
            /// 普通型记录
            /// </summary>
            public string TypeTrace;

            public SIOT_LogDetailType(string str)
            {
                this.TypeError = "Error";
                this.TypeWarning = "Warning";
                this.TypeTrace = "Trace";
            }
        }
        /// <summary>
        /// 日志内容类型
        /// </summary>
        public static readonly SIOT_LogDetailType SIOT_LogDetailTypeDefine = new SIOT_LogDetailType(string.Empty);

    }
}
