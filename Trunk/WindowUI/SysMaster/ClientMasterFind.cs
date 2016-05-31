using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Master;
using Common;
using BLL.Master;
using WindowUI.ClassLibrary.Public;

namespace WindowUI.SysMaster
{
    public partial class ClientMasterFind : Form
    {
        private List<string> _propertyNameList = null;
        private BLL.Master.ClientMaster _clientMasterBll = null;
        private DefineConstantValue.SystemMessage _systemMessageText;

        public ClientMasterInfo _selectResult;

        /// <summary>
        /// ListView栏位定义
        /// </summary>
        private enum enuLvwMstrColumn
        {
            /// <summary>
            /// 記錄ID
            /// </summary>
            enuRecordID,
            /// <summary>
            /// 客户编号
            /// </summary>
            enuClientNum,
            /// <summary>
            /// 中文名称
            /// </summary>
            enuChinaName,
            /// <summary>
            /// 英文名称
            /// </summary>
            enuEnglishName,
            /// <summary>
            /// 联系人

            /// </summary>
            enuLinkman,
            /// <summary>
            /// 客户地址
            /// </summary>
            enuAddress,
            /// <summary>
            /// 电话
            /// </summary>
            enuPhone,
            /// <summary>
            /// 传真
            /// </summary>
            enuFax,
            /// <summary>
            /// 网站
            /// </summary>
            enuWebSite,
            /// <summary>
            /// 备注
            /// </summary>
            enuRemark,
            /// <summary>
            /// 新增者

            /// </summary>
            enuAdd,
            /// <summary>
            /// 新增日期
            /// </summary>
            enuAddDate,
            /// <summary>
            /// 修改者

            /// </summary>
            enuLast,
            /// <summary>
            ///修改时间
            /// </summary>
            enuLastDate
        }

        public ClientMasterFind()
        {
            InitializeComponent();

            this._selectResult = null;
            this._clientMasterBll = new BLL.Master.ClientMaster();
            this._systemMessageText = new DefineConstantValue.SystemMessage(string.Empty);

            SetLVWPropertyName();
        }

        private void SetLVWPropertyName()
        {
            this._propertyNameList = new List<string>();

            this._propertyNameList.Add(ClientMasterInfoEnum.ClmIRecID.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCClientNum.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCChinaName.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCEnglishName.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCLinkman.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCAddress.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCPhone.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCFax.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCWebSite.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCRemark.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCAdd.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmDAddDate.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmCLast.ToString());
            this._propertyNameList.Add(ClientMasterInfoEnum.ClmDLastDate.ToString());
        }

        /// <summary>
        /// 显示ListView数据
        /// </summary>
        private void SearchRecords()
        {
            ClientMasterInfo[] clientMasterInfos = null;
            ClientMasterInfo searchCondition = new ClientMasterInfo();

            searchCondition.ClmCClientNum = this.txtcCNum.Text;
            searchCondition.ClmCChinaName = this.txtcChinaName.Text;

            try
            {
                clientMasterInfos = this._clientMasterBll.SearchRecords(searchCondition);
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }

            this.lbliCount.Text = "0";
            if (clientMasterInfos != null)
            {
                this.lbliCount.Text = clientMasterInfos.Length.ToString().Trim();
            }

            plc_InitControlData.InitListViewItem<ClientMasterInfo>(clientMasterInfos, this._propertyNameList, this.lvwMstr);

        }

        private void SelectRecord()
        {
            if (this.lvwMstr.SelectedItems.Count > 0)
            {
                this._selectResult = new ClientMasterInfo();

                this._selectResult.ClmIRecID = Convert.ToInt32(this.lvwMstr.SelectedItems[0].SubItems[(int)enuLvwMstrColumn.enuRecordID].Text);
                this._selectResult.ClmCClientNum = this.lvwMstr.SelectedItems[0].SubItems[(int)enuLvwMstrColumn.enuClientNum].Text.Trim();
                this._selectResult.ClmCChinaName = this.lvwMstr.SelectedItems[0].SubItems[(int)enuLvwMstrColumn.enuChinaName].Text.Trim();

                this.Close();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SearchRecords();
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;

            this.btnSelect.Enabled = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectRecord();
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMstr.SelectedItems.Count > 0)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            SelectRecord();
        }

    }
}
