using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Model.General;
using Common;

namespace WindowUI.ClassLibrary.Public
{
	/// <summary>
	/// plc_MainClass 系統界面通用功能類
	/// </summary>
	public class plc_MainClass
	{
		public plc_MainClass()
		{
			
		}
		/// <summary>
		/// 數據類型驗證
		/// </summary>
        /// <param name="clsDataType">數據類型信息</param>
		/// <returns></returns>
		public static bool CheckDataType(CheckDataTypeValue clsDataType)
		{
			int l_iLength=clsDataType.strText.Length;
			bool l_blnReturn=false;
			switch(clsDataType.enuType)
			{
				case plc_ConstValue.g_enuCheckType.enuChinaChar:		//中文字符
					if(l_iLength==0)
					{
						l_blnReturn= false;
					}
					else
					{
						int i=System.Text.Encoding.Default.GetBytes(clsDataType.strText).Length;
						if(l_iLength!=i)
						{
							l_blnReturn= true;
						}
						else
						{
							l_blnReturn= false;
						}
					}
					break;
				case plc_ConstValue.g_enuCheckType.enuNumberChar:		//數字
					try
					{
						l_blnReturn=true;
						if(clsDataType.strText.ToString().Trim()!="")
						{
							Decimal Number=Convert.ToDecimal(clsDataType.strText);
						}
					}
					catch(Exception Ex)
					{
						l_blnReturn=false;
					}
					break;
				case plc_ConstValue.g_enuCheckType.enuDateChar:		//日期
					try
					{
						l_blnReturn=true;
						if(clsDataType.strText.ToString().Trim()!="")
						{
							DateTime Number=Convert.ToDateTime(clsDataType.strText);
						}
					}
					catch(Exception Ex)
					{
						l_blnReturn=false;
					}
					break;
				case plc_ConstValue.g_enuCheckType.enuEnglishChar:	//英文字符(不含符號)
					CharEnumerator cha=clsDataType.strText.GetEnumerator();
					l_blnReturn=true;
					while(cha.MoveNext())
					{
						if('a'<=cha.Current && cha.Current<='z' || 'A'<=cha.Current && cha.Current<='Z')
						{
							
						}
						else
						{
							l_blnReturn=false;
							break;
						}
					}
					break;
				case plc_ConstValue.g_enuCheckType.enuNumeralString:	//數字字符(0-9)
					CharEnumerator chr=clsDataType.strText.GetEnumerator();
					l_blnReturn=true;
					while(chr.MoveNext())
					{
						if('0'<=chr.Current && chr.Current<='9')
						{
							
						}
						else
						{
							l_blnReturn=false;
							break;
						}
					}
					break;
				case plc_ConstValue.g_enuCheckType.enuPlusNumberChar:		//正的數字
					try
					{
						l_blnReturn=true;
						if(clsDataType.strText.ToString().Trim()!="")
						{
							Decimal Number=Convert.ToDecimal(clsDataType.strText);
							if(Number<0)
								l_blnReturn=false;
						}
					}
					catch(Exception Ex)
					{
						l_blnReturn=false;
					}
					break;
                case plc_ConstValue.g_enuCheckType.enuPlusInt32:		//正的數字32
                    try
                    {
                        l_blnReturn = true;
                        if (clsDataType.strText.ToString().Trim() != "")
                        {
                            Int32 Number = Convert.ToInt32(clsDataType.strText);
                            Decimal sNumber = Convert.ToDecimal(clsDataType.strText);
                            if(Number!=sNumber)
                                l_blnReturn = false;
                            if (Number < 0)
                                l_blnReturn = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        l_blnReturn = false;
                    }
                    break;
                case plc_ConstValue.g_enuCheckType.enuPlusInt64:		//正的數字64
                    try
                    {
                        l_blnReturn = true;
                        if (clsDataType.strText.ToString().Trim() != "")
                        {
                            Int64 Number = Convert.ToInt64(clsDataType.strText);
                            Decimal sNumber = Convert.ToDecimal(clsDataType.strText);
                            if (Number != sNumber)
                                l_blnReturn = false;
                            if (Number < 0)
                                l_blnReturn = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        l_blnReturn = false;
                    }
                    break;
                case plc_ConstValue.g_enuCheckType.enuInt32:		//數字32
                    try
                    {
                        l_blnReturn = true;
                        if (clsDataType.strText.ToString().Trim() != "")
                        {
                            Int32 Number = Convert.ToInt32(clsDataType.strText);
                            Decimal sNumber = Convert.ToDecimal(clsDataType.strText);
                            if (Number != sNumber)
                                l_blnReturn = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        l_blnReturn = false;
                    }
                    break;
                case plc_ConstValue.g_enuCheckType.enuInt64:		//數字64
                    try
                    {
                        l_blnReturn = true;
                        if (clsDataType.strText.ToString().Trim() != "")
                        {
                            Int64 Number = Convert.ToInt64(clsDataType.strText);
                            Decimal sNumber = Convert.ToDecimal(clsDataType.strText);
                            if (Number != sNumber)
                                l_blnReturn = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        l_blnReturn = false;
                    }
                    break;
				default:
					
					break;
			}
			return l_blnReturn;
		}
		
		/// <summary>
		/// 數據類型驗證
		/// </summary>
		/// <param name="strText">驗證內容</param>
		/// <param name="enuCheckTye">驗證類型</param>
		/// <returns></returns>
		public static bool CheckDataType(string strText,plc_ConstValue.g_enuCheckType enuCheckTye)
		{
			bool l_bolTrue=false;
			string l_strMessage="";
			CheckDataTypeValue DataValue=new CheckDataTypeValue();
			DataValue.strText=strText;
			DataValue.enuType=enuCheckTye;

			try
			{
				l_bolTrue=plc_MainClass.CheckDataType(DataValue);
			}
			catch(Exception Ex)
			{
				l_bolTrue=false;
				throw(new Exception(Ex.Message));
			}

			switch(enuCheckTye)
			{
				case plc_ConstValue.g_enuCheckType.enuChinaChar:
					if(l_bolTrue)
					{
						l_strMessage="祥褫眕怀⻌笢恅趼,ワ潰脤!";
						l_bolTrue=false;
					}
					else
					{
						l_bolTrue=true;
					}
					break;

				case plc_ConstValue.g_enuCheckType.enuDateChar:
					if(!l_bolTrue)
					l_strMessage="ワ怀⻌゜ヽ!";
					break;

				case plc_ConstValue.g_enuCheckType.enuNumberChar:
					if(!l_bolTrue)
					l_strMessage="ワ怀⻌杅趼!";
					break;

				case plc_ConstValue.g_enuCheckType.enuEnglishChar:
					if(!l_bolTrue)
						l_strMessage="ワ怀⻌曾荎恅趼譫!";
					break;

				case plc_ConstValue.g_enuCheckType.enuNumeralString:
					if(!l_bolTrue)
						l_strMessage="ワ怀⻌曾杅趼0-9!";

					break;

				case plc_ConstValue.g_enuCheckType.enuPlusNumberChar:
					if(!l_bolTrue)
						l_strMessage="ワ怀⻌淏硉杅趼!";

					break;
                case plc_ConstValue.g_enuCheckType.enuPlusInt32:
                    if (!l_bolTrue)
                        l_strMessage = "ワ怀⻌淏淕杅!";
                    break;
                case plc_ConstValue.g_enuCheckType.enuInt32:
                    if (!l_bolTrue)
                        l_strMessage = "ワ怀⻌淕杅!";
                    break;
				default:
					break;

			}
			if(l_strMessage.Trim().Length>0)
			{
                MessageBox.Show(l_strMessage, DefineConstantValue.SystemMessageText.strMessageTitle + DefineConstantValue.SystemMessageText.strWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			
			return l_bolTrue;
		}
		

		/// <summary>
		/// 設置控件的狀態顏色
		/// </summary>
		/// <param name="ctlName">控件對象</param>
		/// <param name="bolIsEnabled">可用狀態</param>
		public static void setControlEnable(Control ctlName,bool isEnabled)
		{
			if(ctlName is TextBox || ctlName is ComboBox )
			{
                if (isEnabled == true)
				{
					ctlName.BackColor=plc_ConstValue.g_stuControlEnableColor;
				}
				else
				{
					ctlName.BackColor=plc_ConstValue.g_stuControlDisenableColor;
				}
                ctlName.Enabled = isEnabled;
			}

		}

		/// <summary>
        /// 獲得樹節點數組
		/// </summary>
		/// <param name="treeNodeInfos"></param>
		/// <returns></returns>
        public static TreeNode[] GetTreeNodeList(TreeNodeInfo[] treeNodeInfos)
		{
            if (treeNodeInfos == null)
            {
                return null;
            }
            if (treeNodeInfos.Length == 0)
            {
                return null;
            }
            List<TreeNode> rootNodeList = new List<TreeNode>();
			TreeNode rootNode=null;
            TreeNode[] tNodes=null;
            TreeNodeInfo[] nodeInfos = null;
            for (int i = 0; i < treeNodeInfos.Length; i++)
            {
                rootNode = new TreeNode();
                rootNode.Text = treeNodeInfos[i].Text;
                rootNode.Tag = treeNodeInfos[i].Tag;
                rootNode.Name = treeNodeInfos[i].Name;
                //rootNode.Index = treeNodeInfos[i].Index;
                nodeInfos = treeNodeInfos[i].TreeNodeInfos;
                if (nodeInfos != null)
                {
                    tNodes = GetTreeNodeList(nodeInfos);
                    if (tNodes != null)
                    {
                        rootNode.Nodes.AddRange(tNodes);
                    }
                }
                rootNodeList.Add(rootNode);
            }

            return rootNodeList.ToArray();
		}


	}
}
