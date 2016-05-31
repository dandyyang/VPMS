using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Master;
using Model.Base;
using WindowUI.ClassLibrary.Public;
using Common;
using Model.General;

namespace WindowUI.SysMaster
{
    public partial class ClientMaster : BaseForm
    {
        private plc_ConstValue.DataOperationMode _enuEditMode = plc_ConstValue.DataOperationMode.enuRecIsNull;
        private BLL.Master.ClientMaster _clientMasterBll = new BLL.Master.ClientMaster();
        private DefineConstantValue.SystemMessage _systemMessageText = new DefineConstantValue.SystemMessage(string.Empty);
        private int _oldRecordID;
        public string _recordID;

        public ClientMaster()
        {
            InitializeComponent();

            this._oldRecordID = 0;
            this._recordID = "0";

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //LoadData();
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        #region 类方法


        /// <summary>
        /// 设置各控件的状态

        /// </summary>
        /// <param name="EditMode">用户操作模式</param>
        private void setControlState(plc_ConstValue.DataOperationMode EditMode)
        {
            switch (EditMode)
            {
                case plc_ConstValue.DataOperationMode.enuAdd:		//新增
                    setControlState(plc_ConstValue.DataOperationMode.enuAllClear);
                    plc_MainClass.setControlEnable(this.txtcCNum, true);
                    plc_MainClass.setControlEnable(this.txtcChinaName, true);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, true);
                    plc_MainClass.setControlEnable(this.txtcTaxNumber, true);
                    plc_MainClass.setControlEnable(this.txtcLinkman, true);
                    plc_MainClass.setControlEnable(this.txtcAddress, true);
                    plc_MainClass.setControlEnable(this.txtcPhone, true);
                    plc_MainClass.setControlEnable(this.txtcFax, true);
                    plc_MainClass.setControlEnable(this.txtcWebSite, true);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, true);
                    plc_MainClass.setControlEnable(this.txtcRemark, true);

                    this.pnlCommand.Enabled = false;
                    this.pnlMoveButton.Enabled = false;
                    this.pnlSave.Enabled = true;

                    if (this.txtcCNum.CanFocus)
                        this.txtcCNum.Focus();
                    break;
                case plc_ConstValue.DataOperationMode.enuModify:		//修改
                    plc_MainClass.setControlEnable(this.txtcCNum, false);
                    plc_MainClass.setControlEnable(this.txtcChinaName, true);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, true);
                    plc_MainClass.setControlEnable(this.txtcTaxNumber, true);
                    plc_MainClass.setControlEnable(this.txtcLinkman, true);
                    plc_MainClass.setControlEnable(this.txtcAddress, true);
                    plc_MainClass.setControlEnable(this.txtcPhone, true);
                    plc_MainClass.setControlEnable(this.txtcFax, true);
                    plc_MainClass.setControlEnable(this.txtcRemark, true);
                    plc_MainClass.setControlEnable(this.txtcWebSite, true);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, true);

                    this.pnlCommand.Enabled = false;
                    this.pnlMoveButton.Enabled = false;
                    this.pnlSave.Enabled = true;
                    if (this.txtcChinaName.CanFocus)
                        this.txtcChinaName.Focus();
                    break;
                case plc_ConstValue.DataOperationMode.enuReadOnly:		//只读
                    plc_MainClass.setControlEnable(this.txtcCNum, false);
                    plc_MainClass.setControlEnable(this.txtcChinaName, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);
                    plc_MainClass.setControlEnable(this.txtcTaxNumber, false);
                    plc_MainClass.setControlEnable(this.txtcLinkman, false);
                    plc_MainClass.setControlEnable(this.txtcAddress, false);
                    plc_MainClass.setControlEnable(this.txtcPhone, false);
                    plc_MainClass.setControlEnable(this.txtcFax, false);
                    plc_MainClass.setControlEnable(this.txtcRemark, false);
                    plc_MainClass.setControlEnable(this.txtcWebSite, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);

                    this.pnlCommand.Enabled = false;
                    this.pnlMoveButton.Enabled = false;
                    this.pnlSave.Enabled = false;
                    break;
                case plc_ConstValue.DataOperationMode.enuView:			//浏览
                    plc_MainClass.setControlEnable(this.txtcCNum, false);
                    plc_MainClass.setControlEnable(this.txtcChinaName, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);
                    plc_MainClass.setControlEnable(this.txtcTaxNumber, false);
                    plc_MainClass.setControlEnable(this.txtcLinkman, false);
                    plc_MainClass.setControlEnable(this.txtcAddress, false);
                    plc_MainClass.setControlEnable(this.txtcPhone, false);
                    plc_MainClass.setControlEnable(this.txtcFax, false);
                    plc_MainClass.setControlEnable(this.txtcRemark, false);
                    plc_MainClass.setControlEnable(this.txtcWebSite, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);

                    this.pnlCommand.Enabled = true;
                    this.pnlMoveButton.Enabled = true;
                    this.pnlSave.Enabled = false;
                    this.btnAdd.Enabled = true;
                    this.btnModify.Enabled = true;
                    this.btnDelete.Enabled = true;
                    break;
                case plc_ConstValue.DataOperationMode.enuRecIsNull:		//没有记录
                    plc_MainClass.setControlEnable(this.txtcCNum, false);
                    plc_MainClass.setControlEnable(this.txtcChinaName, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);
                    plc_MainClass.setControlEnable(this.txtcTaxNumber, false);
                    plc_MainClass.setControlEnable(this.txtcLinkman, false);
                    plc_MainClass.setControlEnable(this.txtcAddress, false);
                    plc_MainClass.setControlEnable(this.txtcPhone, false);
                    plc_MainClass.setControlEnable(this.txtcFax, false);
                    plc_MainClass.setControlEnable(this.txtcRemark, false);
                    plc_MainClass.setControlEnable(this.txtcWebSite, false);
                    plc_MainClass.setControlEnable(this.txtcEnglishName, false);

                    this.pnlCommand.Enabled = true;
                    this.btnAdd.Enabled = true;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.pnlMoveButton.Enabled = false;
                    this.pnlSave.Enabled = false;
                    break;

                case plc_ConstValue.DataOperationMode.enuAllClear:		//清空所有控件内容

                    this.txtcCNum.Text = "";
                    this.txtcChinaName.Text = "";
                    this.txtcEnglishName.Text = "";
                    this.txtcTaxNumber.Text = "";
                    this.txtcLinkman.Text = "";
                    this.txtcAddress.Text = "";
                    this.txtcPhone.Text = "";
                    this.txtcFax.Text = "";
                    this.txtcWebSite.Text = "";
                    this.txtcRemark.Text = "";
                    this.txtcAdd.Text = "";
                    this.txtdAddDate.Text = "";
                    this.txtcLast.Text = "";
                    this.txtdLastDate.Text = "";
                    break;

                default:
                    break;

            }
        }


        /// <summary>
        /// 初始化数据

        /// </summary>
        private void LoadData()
        {
            try
            {
                setFieldMaxLength();

                ClientMasterInfo clientMasterInfo = null;
                clientMasterInfo = this._clientMasterBll.GetRecord_Last();
                DisplayRecord(clientMasterInfo);
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }

            SetContralStatus();

        }

        /// <summary>
        /// 显示基本资料记录
        /// </summary>
        private void DisplayRecord(ClientMasterInfo clientMasterInfo)
        {
            if (clientMasterInfo == null)
            {
                return;
            }
            setControlState(plc_ConstValue.DataOperationMode.enuAllClear);

            this._oldRecordID = clientMasterInfo.ClmIRecID;
            this.txtcCNum.Text = clientMasterInfo.ClmCClientNum;
            this.txtcChinaName.Text = clientMasterInfo.ClmCChinaName;
            this.txtcEnglishName.Text = clientMasterInfo.ClmCEnglishName;
            this.txtcTaxNumber.Text = clientMasterInfo.ClmCTaxNumber;
            this.txtcLinkman.Text = clientMasterInfo.ClmCLinkman;
            this.txtcAddress.Text = clientMasterInfo.ClmCAddress;
            this.txtcPhone.Text = clientMasterInfo.ClmCPhone;
            this.txtcFax.Text = clientMasterInfo.ClmCFax;
            this.txtcWebSite.Text = clientMasterInfo.ClmCWebSite;
            this.txtcRemark.Text = clientMasterInfo.ClmCRemark;
            this.txtcAdd.Text = clientMasterInfo.ClmCAdd;
            this.txtdAddDate.Text = clientMasterInfo.ClmDAddDate.Value.ToString("yyyy-MM-dd");
            this.txtcLast.Text = clientMasterInfo.ClmCLast;
            this.txtdLastDate.Text = clientMasterInfo.ClmDLastDate.Value.ToString("yyyy-MM-dd");

        }

        /// <summary>
        /// 设置字段的最大输入长度.
        /// </summary>
        private void setFieldMaxLength()
        {
            ClientMasterInfo clientMasterInfo;

            try
            {
                clientMasterInfo = this._clientMasterBll.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }

            this.txtcCNum.MaxLength = clientMasterInfo.ClmCClientNum_Lenght;
            this.txtcChinaName.MaxLength = clientMasterInfo.ClmCChinaName_Lenght;
            this.txtcEnglishName.MaxLength = clientMasterInfo.ClmCEnglishName_Lenght;
            this.txtcTaxNumber.MaxLength = clientMasterInfo.ClmCTaxNumber_Lenght;
            this.txtcLinkman.MaxLength = clientMasterInfo.ClmCAddress_Lenght;
            this.txtcAddress.MaxLength = clientMasterInfo.ClmCAddress_Lenght;
            this.txtcPhone.MaxLength = clientMasterInfo.ClmCPhone_Lenght;
            this.txtcFax.MaxLength = clientMasterInfo.ClmCFax_Lenght;
            this.txtcWebSite.MaxLength = clientMasterInfo.ClmCWebSite_Lenght;
            this.txtcRemark.MaxLength = clientMasterInfo.ClmCRemark_Lenght;

        }

        private void SetContralStatus()
        {
            if (this.txtcCNum.Text.Trim().Length > 0)
            {
                this._enuEditMode = plc_ConstValue.DataOperationMode.enuView;
            }
            else
            {
                this._enuEditMode = plc_ConstValue.DataOperationMode.enuRecIsNull;
            }
            setControlState(this._enuEditMode);
        }

        #endregion

        #region 数据库操作


        /// <summary>
        /// 检查是否存在记录

        /// </summary>
        /// <returns>true/false</returns>
        private bool IsExistRecord()
        {
            bool l_bolExist = false;

            try
            {
                l_bolExist = this._clientMasterBll.IsExistRecord(this.txtcCNum.Text);
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }
            return l_bolExist;
        }
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <returns>true/false</returns>
        private bool AddRecord()
        {
            bool l_bolSuccess = false;
            ClientMasterInfo clientMasterInfo = new ClientMasterInfo();
            ReturnValueInfo returnValueInfo = null;

            clientMasterInfo.ClmCClientNum = this.txtcCNum.Text;
            clientMasterInfo.ClmCChinaName = this.txtcChinaName.Text;
            clientMasterInfo.ClmCEnglishName = this.txtcEnglishName.Text;
            clientMasterInfo.ClmCTaxNumber = this.txtcTaxNumber.Text;
            clientMasterInfo.ClmCLinkman = this.txtcLinkman.Text;
            clientMasterInfo.ClmCAddress = this.txtcAddress.Text;
            clientMasterInfo.ClmCPhone = this.txtcPhone.Text;
            clientMasterInfo.ClmCFax = this.txtcFax.Text;
            clientMasterInfo.ClmCWebSite = this.txtcWebSite.Text;
            clientMasterInfo.ClmCRemark = this.txtcRemark.Text;
            clientMasterInfo.ClmCAdd = this.txtcAdd.Text;
            clientMasterInfo.ClmCLast = this.txtdLastDate.Text;

            try
            {
                returnValueInfo = this._clientMasterBll.InsertRecord(clientMasterInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            l_bolSuccess = returnValueInfo.boolValue;
            if (!l_bolSuccess)
            {
                MessageBox.Show(returnValueInfo.messageText, this._systemMessageText.strMessageTitle + this._systemMessageText.strWarning.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return l_bolSuccess;

        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <returns>true/false</returns>
        private bool UpdateRecord()
        {
            bool l_bolSuccess = false;
            ClientMasterInfo clientMasterInfo = new ClientMasterInfo();
            ReturnValueInfo returnValueInfo = null;

            clientMasterInfo.ClmIRecID = this._oldRecordID;
            clientMasterInfo.ClmCClientNum = this.txtcCNum.Text;
            clientMasterInfo.ClmCChinaName = this.txtcChinaName.Text;
            clientMasterInfo.ClmCEnglishName = this.txtcEnglishName.Text;
            clientMasterInfo.ClmCTaxNumber = this.txtcTaxNumber.Text;
            clientMasterInfo.ClmCLinkman = this.txtcLinkman.Text;
            clientMasterInfo.ClmCAddress = this.txtcAddress.Text;
            clientMasterInfo.ClmCPhone = this.txtcPhone.Text;
            clientMasterInfo.ClmCFax = this.txtcFax.Text;
            clientMasterInfo.ClmCWebSite = this.txtcWebSite.Text;
            clientMasterInfo.ClmCRemark = this.txtcRemark.Text;
            clientMasterInfo.ClmCAdd = this.txtcAdd.Text;
            clientMasterInfo.ClmCLast = this.txtdLastDate.Text;

            try
            {
                returnValueInfo = this._clientMasterBll.UpdateRecord(clientMasterInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            l_bolSuccess = returnValueInfo.boolValue;
            if (!l_bolSuccess)
            {
                MessageBox.Show(returnValueInfo.messageText, this._systemMessageText.strMessageTitle + this._systemMessageText.strWarning.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return l_bolSuccess;

        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns>true/false</returns>
        private bool DeleteRecord(int recordID)
        {
            bool l_bolSuccess = false;

            ClientMasterInfo info = new ClientMasterInfo();

            info.RecordID = recordID;

            try
            {
                l_bolSuccess = this._clientMasterBll.DeleteRecord(info);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return l_bolSuccess;
        }

        #endregion

        #region 事件

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this._enuEditMode = plc_ConstValue.DataOperationMode.enuAdd;
            this.setControlState(this._enuEditMode);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            this._enuEditMode = plc_ConstValue.DataOperationMode.enuModify;
            this.setControlState(this._enuEditMode);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            if (MessageBox.Show(this._systemMessageText.strMessageText_Q_Delete, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    ClientMasterInfo info = new ClientMasterInfo();

                    info.RecordID = this._oldRecordID;
                    isSuccess = this._clientMasterBll.DeleteRecord(info);
                    if (isSuccess)
                    {
                        setControlState(plc_ConstValue.DataOperationMode.enuAllClear);
                        ClientMasterInfo clientMasterInfo = null;
                        clientMasterInfo = this._clientMasterBll.GetRecord_Last();
                        DisplayRecord(clientMasterInfo);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            SetContralStatus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClientMasterInfo clientMasterInfo = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this._enuEditMode == plc_ConstValue.DataOperationMode.enuAdd)
                {
                    if (AddRecord())
                    {
                        clientMasterInfo = this._clientMasterBll.GetRecord(this.txtcCNum.Text);
                        DisplayRecord(clientMasterInfo);
                        this._enuEditMode = plc_ConstValue.DataOperationMode.enuView;
                        setControlState(this._enuEditMode);
                    }

                }
                if (this._enuEditMode == plc_ConstValue.DataOperationMode.enuModify)
                {
                    if (IsExistRecord())
                    {
                        if (UpdateRecord())
                        {
                            clientMasterInfo = this._clientMasterBll.GetRecord(this.txtcCNum.Text);
                            DisplayRecord(clientMasterInfo);
                            this._enuEditMode = plc_ConstValue.DataOperationMode.enuView;
                            setControlState(this._enuEditMode);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this._systemMessageText.strMessageText_Q_Cancel, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ClientMasterInfo clientMasterInfo = null;

                try
                {
                    ClientMasterInfo info = new ClientMasterInfo();

                    info.RecordID = this._oldRecordID;

                    clientMasterInfo = this._clientMasterBll.GetRecord(info);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DisplayRecord(clientMasterInfo);

                SetContralStatus();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClientMasterInfo clientMasterInfo = null;
            try
            {
                clientMasterInfo = this._clientMasterBll.GetRecord_First();
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayRecord(clientMasterInfo);

            Cursor.Current = Cursors.Default;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterBll.GetRecord_Previous(this._oldRecordID);
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayRecord(clientMasterInfo);

            Cursor.Current = Cursors.Default;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterBll.GetRecord_Next(this._oldRecordID);
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayRecord(clientMasterInfo);

            Cursor.Current = Cursors.Default;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClientMasterInfo clientMasterInfo = null;

            try
            {
                clientMasterInfo = this._clientMasterBll.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayRecord(clientMasterInfo);

            Cursor.Current = Cursors.Default;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtcCNum_Leave(object sender, EventArgs e)
        {
            if (!plc_MainClass.CheckDataType(this.txtcCNum.Text, plc_ConstValue.g_enuCheckType.enuChinaChar))
                this.txtcCNum.Focus();
        }

        private void txtcEnglishName_Leave(object sender, EventArgs e)
        {
            if (!plc_MainClass.CheckDataType(this.txtcEnglishName.Text, plc_ConstValue.g_enuCheckType.enuChinaChar))
                this.txtcEnglishName.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClientMasterFind frm = new ClientMasterFind();
            frm.ShowDialog();

            ClientMasterInfo selectResult = null;
            selectResult = frm._selectResult;

            frm.Dispose();

            if (selectResult == null)
            {
                return;
            }

            try
            {
                ClientMasterInfo clientMasterInfo = null;
                clientMasterInfo.RecordID = selectResult.ClmIRecID;

                clientMasterInfo = this._clientMasterBll.GetRecord(clientMasterInfo);
                DisplayRecord(clientMasterInfo);
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }
        }

        #endregion

        private void ClientMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.userToolBar1);
        }

    }
}
