using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Model.Base;


namespace WindowUI.ClassLibrary.Public
{
	/// <summary>
	/// plc_ConstValue 定義恆量值
	/// </summary>
	public class plc_ConstValue
	{
        /// <summary>
        /// 用戶信息
        /// </summary>
        public static UserInformationInfo userInformationInfo=null;

		public plc_ConstValue()
		{
			
		}

	#region 炵苀價掛扢离硉

		/// <summary>
		/// 炵苀髡夔晤瘍陓洘
		/// </summary>
		public struct stuSystemFunctionMessage
		{
			/// <summary>
			/// 炵苀髡夔晤瘍腔KEY1硉.
			/// </summary>
			public string strSYSFNum_Key1;
			/// <summary>
			/// 炵苀髡夔晤瘍--蚚誧扢隅
			/// </summary>
			public string strSYSFNum_PWWM10U1;

			public stuSystemFunctionMessage(string str)
			{
				this.strSYSFNum_Key1="SYSTEMFUNCTION";
				this.strSYSFNum_PWWM10U1="PWWM10U1";

			}

		}

		/// <summary>
		/// 炵苀髡夔晤瘍陓洘
		/// </summary>
		public static plc_ConstValue.stuSystemFunctionMessage g_stuSystemFunctionMessage=new stuSystemFunctionMessage("");

		/// <summary>
		/// 炵苀等擂陓洘
		/// </summary>
		public struct stuSystemBillOfDocumentMessage
		{
			/// <summary>
			/// 炵苀等擂陓洘腔KEY1硉
			/// </summary>
			public string strBillOfDocumentM_Key1;
			/// <summary>
			/// 炵苀等擂陓洘腔KEY2硉--粒劃扠ワ等
			/// </summary>
			public string strBillOfDocumentM_RF;

			public stuSystemBillOfDocumentMessage(string str)
			{
				this.strBillOfDocumentM_Key1="PWW_BILLOFDOCUMENTMESSAGE";
				this.strBillOfDocumentM_RF="RF";
			}
		}

		/// <summary>
		/// 炵苀等擂陓洘
		/// </summary>
		public static plc_ConstValue.stuSystemBillOfDocumentMessage g_stuSystemBillOfDocumentMessage=new stuSystemBillOfDocumentMessage("");

		/// <summary>
		/// 炵苀髡夔癹陓洘
		/// </summary>
		public struct stuSystemFunctionPopedom
		{
			/// <summary>
			/// 炵苀髡夔癹腔KEY1硉.
			/// </summary>
			public string strSFPopedomNum_Key1;
			/// <summary>
			/// 炵苀髡夔癹--陔崝
			/// </summary>
			public string strSFPopedomNum_ADD;
			/// <summary>
			/// 炵苀髡夔癹--党蜊
			/// </summary>
			public string strSFPopedomNum_MODIFY;
			/// <summary>
			/// 炵苀髡夔癹--刉壺
			/// </summary>
			public string strSFPopedomNum_DELETE;
			/// <summary>
			/// 炵苀髡夔癹--銡擬
			/// </summary>
			public string strSFPopedomNum_READONLY;
			/// <summary>
			/// 炵苀髡夔癹--蠶袧
			/// </summary>
			public string strSFPopedomNum_CONFIRM;
			/// <summary>
			/// 炵苀髡夔癹--蛁种
			/// </summary>
			public string strSFPopedomNum_LOGOUT;
			/// <summary>
			/// 炵苀髡夔癹--湖荂
			/// </summary>
			public string strSFPopedomNum_PRINT;
			/// <summary>
			/// 炵苀髡夔癹--瞄袧
			/// </summary>
			public string strSFPopedomNum_APPROVE;
			/// <summary>
			/// 炵苀髡夔癹--枑蝠
			/// </summary>
			public string strSFPopedomNum_SUBMIT;
			/// <summary>
			/// 炵苀髡夔癹--眒堤等
			/// </summary>
			public string strSFPopedomNum_PRINTED;
			/// <summary>
			/// 炵苀髡夔癹--机蠶
			/// </summary>
			public string strSFPopedomNum_EXAMINEANDAPPROVE;
			/// <summary>
			/// 炵苀髡夔癹--蛌累
			/// </summary>
			public string strSFPopedomNum_CHANGEWAREHOUSE;
			/// <summary>
			/// 炵苀髡夔癹--癹扢离
			/// </summary>
			public string strSFPopedomNum_SETPOPEDOM;
			/// <summary>
			/// 炵苀髡夔癹--脤艘惆歎
			/// </summary>
			public string strSFPopedomNum_CONSULTPRICE;
			/// <summary>
			/// 炵苀髡夔癹--陔崝堤踱
			/// </summary>
			public string strSFPopedomNum_SHIPMENTADD;
			/// <summary>
			/// 炵苀髡夔癹--党蜊堤踱
			/// </summary>
			public string strSFPopedomNum_SHIPMENTMODIFY;
			/// <summary>
			/// 炵苀髡夔癹--刉壺堤踱
			/// </summary>
			public string strSFPopedomNum_SHIPMENTDELETE;

			/// <summary>
			/// 炵苀髡夔癹--陔崝載蜊踱湔講
			/// </summary>
			public string strSFPopedomNum_CHECKADD;
			/// <summary>
			/// 炵苀髡夔癹--党蜊載蜊踱湔講
			/// </summary>
			public string strSFPopedomNum_CHECKMODIFY;
			/// <summary>
			/// 炵苀髡夔癹--刉壺載蜊踱湔講
			/// </summary>
			public string strSFPopedomNum_CHECKDELETE;

			public stuSystemFunctionPopedom(string str)
			{
				this.strSFPopedomNum_Key1="PWW_FUNCTIONPOPEDOM";
				this.strSFPopedomNum_ADD="ADD";
				this.strSFPopedomNum_MODIFY="MODIFY";
				this.strSFPopedomNum_DELETE="DELETE";
				this.strSFPopedomNum_READONLY="READONLY";
				this.strSFPopedomNum_CONFIRM="CONFIRM";
				this.strSFPopedomNum_LOGOUT="LOGOUT";
				this.strSFPopedomNum_PRINT="PRINT";
				this.strSFPopedomNum_APPROVE="APPROVE";
				this.strSFPopedomNum_SUBMIT="SUBMIT";
				this.strSFPopedomNum_PRINTED="PRINTED";
				this.strSFPopedomNum_EXAMINEANDAPPROVE="EXAMINEANDAPPROVE";
				this.strSFPopedomNum_CHANGEWAREHOUSE="CHANGEWAREHOUSE";
				this.strSFPopedomNum_SETPOPEDOM="SETPOPEDOM";
				this.strSFPopedomNum_CONSULTPRICE="CONSULTPRICE";
				this.strSFPopedomNum_SHIPMENTADD="SHIPMENTADD";
				this.strSFPopedomNum_SHIPMENTMODIFY="SHIPMENTMODIFY";
				this.strSFPopedomNum_SHIPMENTDELETE="SHIPMENTDELETE";
				this.strSFPopedomNum_CHECKADD="CHECKADD";
				this.strSFPopedomNum_CHECKMODIFY="CHECKMODIFY";
				this.strSFPopedomNum_CHECKDELETE="CHECKDELETE";
			}
		}

		/// <summary>
		/// 炵苀髡夔癹陓洘
		/// </summary>
		public static plc_ConstValue.stuSystemFunctionPopedom g_stuSystemFunctionPopedom=new stuSystemFunctionPopedom("");

		/// <summary>
		/// 敦极晇伎
		/// </summary>
		public static Color g_stuFormColor;

        public static Color testC;

		#region 炵苀髡夔粕等(MenuForm)

		public struct stuMenuFormCodeMaster
		{
			/// <summary>
			/// 粕等攷赽炵苀梓枙腔趼鎢翋紫KEY1硉
			/// </summary>
			public string strMenuTreeTitle_Key1;
			/// <summary>
			/// 粕等攷赽炵苀梓枙腔趼鎢翋紫Value硉(炵苀梓枙梓キ)
			/// </summary>
			public string strMenuTreeTitle_Tag;
			/// <summary>
			/// 粕等攷赽炵苀梓枙腔趼鎢翋紫KEY2硉(踱湔炵苀)
			/// </summary>
			public string strMenuTreeTitle_Warehouse;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY1硉
			/// </summary>
			public string strMenuTreeFunction_Key1;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--蚚誧翋紫扢离)
			/// </summary>
			public string strMenuTree_WMaster;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--蚚誧扢离)
			/// </summary>
			public string strMenuTree_WUserMaster;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--惆歎)
			/// </summary>
			public string strMenuTree_WQuotePrice;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--昜ⅲ啎隱)
			/// </summary>
			public string strMenuTree_WObligate;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--等擂蝠眢)
			/// </summary>
			public string strMenuTree_WForm;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--机蠶)
			/// </summary>
			public string strMenuTree_WExamineAndApprove;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--⻌踱)
			/// </summary>
			public string strMenuTree_WWarehouse;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--堤踱)
			/// </summary>
			public string strMenuTree_WShipment;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--攫萸)
			/// </summary>
			public string strMenuTree_WCheck;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--蛌累)
			/// </summary>
			public string strMenuTree_WChangeWarehouse;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--惆煙摯惆囮)
			/// </summary>
			public string strMenuTree_WScrapAndLose;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--昜ⅲ踱湔講假枑尨)
			/// </summary>
			public string strMenuTree_WRemind;
			/// <summary>
			/// 粕等攷髡夔砐腔趼鎢翋紫KEY2硉(踱湔--惆桶)
			/// </summary>
			public string strMenuTree_WReport;

			public stuMenuFormCodeMaster(string str)
			{
				this.strMenuTreeTitle_Key1="MENUTREETITLE";
				this.strMenuTreeTitle_Tag="SystemTitle";
				this.strMenuTreeTitle_Warehouse="Warehouse";
				this.strMenuTreeFunction_Key1="MENUTREEFUNCTION";
				this.strMenuTree_WMaster="pnlWMaster";
				this.strMenuTree_WUserMaster="pnlWUserMaster";
				this.strMenuTree_WQuotePrice="pnlWQuotePrice";
				this.strMenuTree_WObligate="pnlWObligate";
				this.strMenuTree_WForm="pnlWForm";
				this.strMenuTree_WExamineAndApprove="pnlWExamineAndApprove";
				this.strMenuTree_WWarehouse="pnlWWarehouse";
				this.strMenuTree_WShipment="pnlWShipment";
				this.strMenuTree_WCheck="pnlWCheck";
				this.strMenuTree_WChangeWarehouse="pnlWChangeWarehouse";
				this.strMenuTree_WScrapAndLose="pnlWScrapAndLose";
				this.strMenuTree_WRemind="pnlWRemind";
				this.strMenuTree_WReport="pnlWReport";
			}
		}

		public static plc_ConstValue.stuMenuFormCodeMaster g_stuMenuFormCodeMaster=new stuMenuFormCodeMaster("");

		#endregion

		/// <summary>
        ///数据验证类型
		/// </summary>
		public enum g_enuCheckType
		{
			/// <summary>
			/// 中文
			/// </summary>
			enuChinaChar,
			/// <summary>
			/// 數字
			/// </summary>
			enuNumberChar,
			/// <summary>
			/// 日期
			/// </summary>
			enuDateChar,
			/// <summary>
			/// 英文
			/// </summary>
			enuEnglishChar,
			/// <summary>
			/// 數字(0-9)
			/// </summary>
			enuNumeralString,
			/// <summary>
			/// 正數字
			/// </summary>
			enuPlusNumberChar,
            /// <summary>
            /// 正整數32
            /// </summary>
            enuPlusInt32,
            /// <summary>
            /// 整數32
            /// </summary>
            enuInt32,
            /// <summary>
            /// 正整數64
            /// </summary>
            enuPlusInt64,
            /// <summary>
            /// 整數64
            /// </summary>
            enuInt64

		}

		#endregion

	#region 蚚誧窒藷陓洘

		/// <summary>
		/// 蚚誧窒藷陓洘
		/// </summary>
		public struct stuUserDeptMessage
		{
			/// <summary>
			/// 杻梗窒藷腔KEY1硉
			/// </summary>
			public string strSpecialtiesDept_Key1;
			/// <summary>
			/// 杻梗窒藷腔KEY2硉(扽衾垀衄窒藷)
			/// </summary>
			public string strSpecialtiesDept_All;

			public stuUserDeptMessage(string str)
			{
				this.strSpecialtiesDept_Key1="USERDEPARTMENTMESSAGE";
				this.strSpecialtiesDept_All="AllDept";
			}
		}


		/// <summary>
		/// 蚚誧窒藷陓洘
		/// </summary>
		public static plc_ConstValue.stuUserDeptMessage g_stuUserDeptMessage=new stuUserDeptMessage("");

		#endregion

	#region 鼠侗訧蹋扢离(PWCOMPANY)

		/// <summary>
		/// 鼠侗訧蹋腔隅砱硉
		/// </summary>
		public struct stuPWCompanyInfromation
		{
			/// <summary>
			/// 鼠侗翋紫訧蹋腔KEY硉
			/// </summary>
			public string strCompanyKey;
			/// <summary>
			/// 芞⑵繚噤
			/// </summary>
			public string strPicPath;

			public stuPWCompanyInfromation(string str)
			{
				this.strCompanyKey="CompanyKey";
				this.strPicPath=Application.StartupPath.ToString().Trim()+@"\CompanyPic";
			}
		}
		public static plc_ConstValue.stuPWCompanyInfromation g_stuPWCompanyInfromation=new plc_ConstValue.stuPWCompanyInfromation("");
	#endregion

	#region 界面狀態定義
		/// <summary>
		/// 數據操作模式
		/// </summary>
		public enum DataOperationMode
		{	
			/// <summary>
			/// 新增
			/// </summary>
			enuAdd,
			/// <summary>
			/// 修改
			/// </summary>
			enuModify,
			/// <summary>
			/// 刪除
			/// </summary>
			enuDelete,
			/// <summary>
			/// 瀏覽
			/// </summary>
			enuView,
			/// <summary>
			/// 只讀
			/// </summary>
			enuReadOnly,
			/// <summary>
			/// 沒有記錄
			/// </summary>
			enuRecIsNull,
			/// <summary>
			/// 清空所有
			/// </summary>
			enuAllClear
		}
		/// <summary>
		/// 控件不可用時的顏色
		/// </summary>
		public static Color g_stuControlDisenableColor=Color.FromName("Info");
		/// <summary>
        /// 控件可用時的顏色
		/// </summary>
		public static Color g_stuControlEnableColor=Color.FromName("Window");

	#endregion

	}
}
