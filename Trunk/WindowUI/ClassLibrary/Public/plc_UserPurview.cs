using System;

namespace WindowUI.ClassLibrary.Public
{
	/// <summary>
	/// 蚚誧髡夔癹陓洘濬.
	/// </summary>
	public class plc_UserPurview
	{
		private bool m_bolAdd=false;
		private bool m_bolModify=false;
		private bool m_bolDelete=false;
		private bool m_bolReadOnly=false;
		private bool m_bolConfirm=false;
		private bool m_bolLogout=false;
		private bool m_bolPrint=false;
		private bool m_bolApprove=false;
		private bool m_bolSubmit=false;
		private bool m_bolPrinted=false;
		private bool m_bolExamineAndApprove=false;
		private bool m_bolChangeWarehouse=false;
		private bool m_bolSetPurview=false;
		private bool m_bolConsultPrice=false;
		private bool m_bolShipmentAdd=false;
		private bool m_bolShipmentModify=false;
		private bool m_bolShipmentDelete=false;

		private bool m_bolCheckAdd=false;
		private bool m_bolCheckModify=false;
		private bool m_bolCheckDelete=false;

		/// <summary>
		/// 蚚誧癹陓洘扽俶
		/// </summary>
		public plc_UserPurview()
		{
			//
			// TODO: 婓森揭氝樓凳婖滲杅軀憮
			//
		}

		/// <summary>
		/// 硐黍
		/// </summary>
		public bool bolReadOnly
		{
			get
			{
				return m_bolReadOnly;
			}
			set
			{
				m_bolReadOnly=value;
			}
		}

		/// <summary>
		/// 陔崝
		/// </summary>
		public bool bolAdd
		{
			get
			{
				return m_bolAdd;
			}
			set
			{
				m_bolAdd=value;
			}
		}

		/// <summary>
		/// 党蜊
		/// </summary>
		public bool bolModify
		{
			get
			{
				return m_bolModify;
			}
			set
			{
				m_bolModify=value;
			}
		}

		/// <summary>
		/// 刉壺
		/// </summary>
		public bool bolDelete
		{
			get
			{
				return m_bolDelete;
			}
			set
			{
				m_bolDelete=value;
			}
		}

		/// <summary>
		/// 蠶袧
		/// </summary>
		public bool bolConfirm
		{
			get
			{
				return m_bolConfirm;
			}
			set
			{
				m_bolConfirm=value;
			}
		}

		/// <summary>
		/// 蛁种
		/// </summary>
		public bool bolLogout
		{
			get
			{
				return m_bolLogout;
			}
			set
			{
				m_bolLogout=value;
			}
		}

		/// <summary>
		/// 湖荂
		/// </summary>
		public bool bolPrint
		{
			get
			{
				return m_bolPrint;
			}
			set
			{
				m_bolPrint=value;
			}
		}

		/// <summary>
		/// 瞄袧
		/// </summary>
		public bool bolApprove
		{
			get
			{
				return m_bolApprove;
			}
			set
			{
				m_bolApprove=value;
			}
		}

		/// <summary>
		/// 枑蝠
		/// </summary>
		public bool bolSubmit
		{
			get
			{
				return m_bolSubmit;
			}
			set
			{
				m_bolSubmit=value;
			}
		}

		/// <summary>
		/// 眒堤等
		/// </summary>
		public bool bolPrinted
		{
			get
			{
				return m_bolPrinted;
			}
			set
			{
				m_bolPrinted=value;
			}
		}

		/// <summary>
		/// 机蠶
		/// </summary>
		public bool bolExamineAndApprove
		{
			get
			{
				return m_bolExamineAndApprove;
			}
			set
			{
				m_bolExamineAndApprove=value;
			}
		}

		/// <summary>
		/// 蛌累
		/// </summary>
		public bool bolChangeWarehouse
		{
			get
			{
				return m_bolChangeWarehouse;
			}
			set
			{
				m_bolChangeWarehouse=value;
			}
		}

		/// <summary>
		/// 癹扢离
		/// </summary>
		public bool bolSetPurview
		{
			get
			{
				return m_bolSetPurview;
			}
			set
			{
				m_bolSetPurview=value;
			}
		}

		/// <summary>
		/// 脤艘惆歎
		/// </summary>
		public bool bolConsultPrice
		{
			get
			{
				return m_bolConsultPrice;
			}
			set
			{
				m_bolConsultPrice=value;
			}
		}

		/// <summary>
		/// 陔崝堤踱
		/// </summary>
		public bool bolShipmentAdd
		{
			get
			{
				return m_bolShipmentAdd;
			}
			set
			{
				m_bolShipmentAdd=value;
			}
		}

		/// <summary>
		/// 党蜊堤踱
		/// </summary>
		public bool bolShipmentModify
		{
			get
			{
				return m_bolShipmentModify;
			}
			set
			{
				m_bolShipmentModify=value;
			}
		}

		/// <summary>
		/// 刉壺堤踱
		/// </summary>
		public bool bolShipmentDelete
		{
			get
			{
				return m_bolShipmentDelete;
			}
			set
			{
				m_bolShipmentDelete=value;
			}
		}

		/// <summary>
		/// 陔崝載蜊踱湔講
		/// </summary>
		public bool bolCheckAdd
		{
			get
			{
				return m_bolCheckAdd;
			}
			set
			{
				m_bolCheckAdd=value;
			}
		}

		/// <summary>
		/// 党蜊載蜊踱湔講
		/// </summary>
		public bool bolCheckModify
		{
			get
			{
				return m_bolCheckModify;
			}
			set
			{
				m_bolCheckModify=value;
			}
		}

		/// <summary>
		/// 刉壺載蜊踱湔講
		/// </summary>
		public bool bolCheckDelete
		{
			get
			{
				return m_bolCheckDelete;
			}
			set
			{
				m_bolCheckDelete=value;
			}
		}

	}
}
