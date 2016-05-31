using System;

namespace WindowUI.ClassLibrary.Public
{
	/// <summary>
	/// CheckDataTypeValue 數據驗證信息
	/// </summary>
	public class CheckDataTypeValue
	{
		private string m_strText="";
		private plc_ConstValue.g_enuCheckType m_enuType;
		public CheckDataTypeValue()
		{
			//
			// TODO: 在這裡加入建構函式的程式碼
			//
		}
		/// <summary>
        /// 要驗證的內容
		/// </summary>
		public string strText
		{
			get
			{
				return m_strText;
			}

			set
			{
				m_strText=value;
			}
			
		}
		/// <summary>
        /// 要驗證的數據類型
		/// </summary>
		public plc_ConstValue.g_enuCheckType enuType
		{
			get
			{
				return m_enuType;
			}
			set
			{
				m_enuType=value;
			}

		}
	}
}
