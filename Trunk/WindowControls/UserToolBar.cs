using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowControls.ClassLibrary.Public;

namespace WindowControls
{
    public partial class UserToolBar : UserControl
    {
        /// <summary>
        /// 記錄存在的索引位置類型


        /// </summary>
        public enum RecordIndexType
        {
            /// <summary>
            /// 第一條記錄



            /// </summary>
            First,
            /// <summary>
            /// 正態(有記錄存在，不是第一條記錄也不是最後一條記錄)
            /// </summary>
            Normal,
            /// <summary>
            /// 最後一條記錄



            /// </summary>
            Last,
            /// <summary>
            /// 沒有存在記錄
            /// </summary>
            None
        }

        /// <summary>
        /// 控的狀態模式
        /// </summary>
        public enum ControlStatusMode
        {
            /// <summary>
            /// 瀏覽模式
            /// </summary>
            View,
            /// <summary>
            /// 編輯模式
            /// </summary>
            Edit
        }

        private DefineConstantValue.SystemMessage _systemMessageText = new DefineConstantValue.SystemMessage("");

        public UserToolBar()
        {
            InitializeComponent();

            this.RecordExistPosition = RecordIndexType.None;
            this.AutoSetStatus = true;
        }

        /// <summary>
        /// 設置各按鈕控件的狀態
        /// </summary>
        /// <param name="controlStatusMode">控件狀態模式</param>
        public void SetControlStatus(ControlStatusMode controlStatusMode)
        {
            if (!this.AutoSetStatus)
            {
                return;
            }

            if (controlStatusMode == ControlStatusMode.View)
            {

                if (this.RecordExistPosition == RecordIndexType.First)
                {
                    this.btnNew.Enabled = true;
                    this.btnModify.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                    this.btnSearch.Enabled = true;
                    return;
                }

                if (this.RecordExistPosition == RecordIndexType.Last)
                {
                    this.btnNew.Enabled = true;
                    this.btnModify.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnFirst.Enabled = true;
                    this.btnPrevious.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                    this.btnSearch.Enabled = true;
                    return;
                }

                if (this.RecordExistPosition == RecordIndexType.None)
                {
                    this.btnNew.Enabled = true;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                    this.btnSearch.Enabled = true;
                    return;
                }

                if (this.RecordExistPosition == RecordIndexType.Normal)
                {
                    this.btnNew.Enabled = true;
                    this.btnModify.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnFirst.Enabled = true;
                    this.btnPrevious.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                    this.btnSearch.Enabled = true;

                    return;
                }
            }
            else if (controlStatusMode == ControlStatusMode.Edit)
            {
                this.btnNew.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = true;
                this.btnCancel.Enabled = true;
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.btnSearch.Enabled = false;
            }
        }

        #region 內部控件事件

        private void btnNew_Click(object sender, EventArgs e)
        {
            SetControlStatus(ControlStatusMode.Edit);
            if (this.BtnNewClick != null)
            {
                this.BtnNewClick(sender, e);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.BtnModifyClick != null)
            {
                this.BtnModifyClick(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this._systemMessageText.strMessageText_Q_Delete, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                SetControlStatus(ControlStatusMode.View);
            }
            else
            {
                return;
            }
            if (this.BtnDeleteClick != null)
            {
                this.BtnDeleteClick(sender, e);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.BtnSaveClick != null)
            {
                this.BtnSaveClick(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this._systemMessageText.strMessageText_Q_Cancel, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                SetControlStatus(ControlStatusMode.View);
            }
            else
            {
                return;
            }
            if (this.BtnCancelClick != null)
            {
                this.BtnCancelClick(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.BtnSearchClick != null)
            {
                this.BtnSearchClick(sender, e);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this.BtnFirstClick != null)
            {
                this.BtnFirstClick(sender, e);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (this.BtnPreviousClick != null)
            {
                this.BtnPreviousClick(sender, e);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.BtnNextClick != null)
            {
                this.BtnNextClick(sender, e);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.BtnLastClick != null)
            {
                this.BtnLastClick(sender, e);
            }
        }

        private void btnCardReturn_Click(object sender, EventArgs e)
        {
            if (this.BtnCardReturnClick != null)
            {
                this.BtnCardReturnClick(sender, e);
            }
        }

        private void btnCardMissing_Click(object sender, EventArgs e)
        {
            if (this.BtnCardMissingClick != null)
            {
                this.BtnCardMissingClick(sender, e);
            }
        }

        private void btnCardRecovery_Click(object sender, EventArgs e)
        {
            if (this.BtnCardRecoveryClick != null)
            {
                this.BtnCardRecoveryClick(sender, e);
            }
        }

        private void btnCardScrap_Click(object sender, EventArgs e)
        {
            if (this.BtnCardScrapClick != null)
            {
                this.BtnCardScrapClick(sender, e);
            }
        }

        private void btnIssuance_Click(object sender, EventArgs e)
        {
            if (this.BtnCardIssuanceClick != null)
            {
                this.BtnCardIssuanceClick(sender, e);
            }
        }

        private void btnDataInput_Click(object sender, EventArgs e)
        {
            if (this.BtnDataInputClick != null)
            {
                this.BtnDataInputClick(sender, e);
            }
        }

        private void btnDataExport_Click(object sender, EventArgs e)
        {

        }

        private void btnExportTemplate_Click(object sender, EventArgs e)
        {

            if (this.BtnExportTemplateClick != null)
            {
                this.BtnExportTemplateClick(sender, e);
            }
        }

        private void btnExpCusData_Click(object sender, EventArgs e)
        {

            if (this.BtnExpCusDataClick != null)
            {
                this.BtnExpCusDataClick(sender, e);

            }
        }

        private void btnExportCardUserPhoto_Click(object sender, EventArgs e)
        {

            if (this.btnExportCardUserPhotoClick != null)
            {
                this.btnExportCardUserPhotoClick(sender, e);
            }
        }

        private void btnImportCardUserPhoto_Click(object sender, EventArgs e)
        {
            if (this.BtnImportCardUserPhotoClick != null)
            {
                this.BtnImportCardUserPhotoClick(sender, e);
            }
        }
        private void btnImportCardUserData_Click(object sender, EventArgs e)
        {

            if (this.BtnImportCardUserDataClick != null)
            {
                this.BtnImportCardUserDataClick(sender, e);
            }
        }

        private void btnGroupPerson_Click(object sender, EventArgs e)
        {

            if (this.BtnGroupPersonClick != null)
            {
                this.BtnGroupPersonClick(sender, e);
            }
        }

        #endregion

        #region 控件屬性定義

        #region New Button

        public bool BtnNewEnabled
        {
            set
            {
                this.btnNew.Enabled = value;
            }
            get
            {
                return this.btnNew.Enabled;
            }
        }

        public bool BtnNewVisible
        {
            set
            {
                this.btnNew.Visible = value;
            }
            get
            {
                return this.btnNew.Visible;
            }
        }

        public event EventHandler BtnNewClick;

        #endregion

        #region CardIssuance Button

        public bool BtnCardIssuanceEnabled
        {
            set
            {
                this.btnIssuance.Enabled = value;
            }
            get
            {
                return this.btnIssuance.Enabled;
            }
        }

        public bool BtnCardIssuanceVisible
        {
            set
            {
                this.btnIssuance.Visible = value;
            }
            get
            {
                return this.btnIssuance.Visible;
            }
        }

        public event EventHandler BtnCardIssuanceClick;

        #endregion

        #region DataInput Button

        public bool BtnDataInputEnabled
        {
            set
            {
                this.btnDataInput.Enabled = value;
            }
            get
            {
                return this.btnDataInput.Enabled;
            }
        }

        public bool BtnDataInputVisible
        {
            set
            {
                this.btnDataInput.Visible = value;
            }
            get
            {
                return this.btnDataInput.Visible;
            }
        }

        public event EventHandler BtnDataInputClick;

        #endregion

        #region DataExport Button

        public bool BtnDataExportEnabled
        {
            set
            {
                this.btnDataExport.Enabled = value;
            }
            get
            {
                return this.btnDataExport.Enabled;
            }
        }

        public bool BtnDataExportVisible
        {
            set
            {
                this.btnDataExport.Visible = value;
            }
            get
            {
                return this.btnDataExport.Visible;
            }
        }
        public event EventHandler BtnDataExportClick;

        #endregion

        #region ExportCardUserPhoto

        public bool BtnExportCardUserPhotoEnabled
        {
            set
            {
                this.btnExportCardUserPhoto.Enabled = value;
            }
            get
            {
                return this.btnExportCardUserPhoto.Enabled;
            }
        }
        public bool BtnExportCardUserPhotoVisible
        {
            set
            {
                this.btnExportCardUserPhoto.Visible = value;
            }
            get
            {
                return this.btnExportCardUserPhoto.Visible;
            }
        }
        public event EventHandler btnExportCardUserPhotoClick;
        #endregion

        #region ImportCardUserPhoto
        public bool BtnImportPhotoEnabled
        {
            set
            {
                this.btnImportCardUserPhoto.Enabled = value;
            }
            get
            {
                return this.btnImportCardUserPhoto.Enabled;
            }
        }
        public bool BtnImportPhotoVisible
        {
            set
            {
                this.btnImportCardUserPhoto.Visible = value;
            }
            get
            {
                return this.btnImportCardUserPhoto.Visible;
            }
        }
        public event EventHandler BtnImportCardUserPhotoClick;
        #endregion

        #region ImportCardUserData
        public bool BtnImportCardUserDataEnabled
        {
            set
            {
                this.btnImportCardUserData.Enabled = value;
            }
            get
            {
                return this.btnImportCardUserData.Enabled;
            }
        }
        public bool BtnImportCardUserDataVisible
        {
            set
            {
                this.btnImportCardUserData.Visible = value;
            }
            get
            {
                return this.btnImportCardUserData.Visible;
            }
        }
        public event EventHandler BtnImportCardUserDataClick;
        #endregion

        #region ExportTemplate
        public bool BtnExportTemplateEnabled
        {
            set
            {
                this.BtnExportTemplate.Enabled = value;
            }
            get
            {
                return this.BtnExportTemplate.Enabled;
            }
        }
        public bool BtnExportTemplateVisible
        {
            set
            {
                this.BtnExportTemplate.Visible = value;
            }
            get
            {
                return this.BtnExportTemplate.Visible;
            }
        }
        public event EventHandler BtnExportTemplateClick;
        #endregion

        #region ExpCusData
        public bool BtnExpCusDataEnabled
        {
            set
            {
                this.btnExpCusData.Enabled = value;
            }
            get
            {
                return this.btnExpCusData.Enabled;
            }
        }

        public bool BtnExpCusDataVisible
        {
            set
            {
                this.btnExpCusData.Visible = value;
            }
            get
            {
                return this.btnExpCusData.Visible;
            }
        }
        public event EventHandler BtnExpCusDataClick;
        #endregion

        #region Modify Button

        public bool BtnModifyEnabled
        {
            set
            {
                this.btnModify.Enabled = value;
            }
            get
            {
                return this.btnModify.Enabled;
            }
        }

        public bool BtnModifyVisible
        {
            set
            {
                this.btnModify.Visible = value;
            }
            get
            {
                return this.btnModify.Visible;
            }
        }

        public event EventHandler BtnModifyClick;

        #endregion

        #region Delete Button

        public bool BtnDeleteEnabled
        {
            set
            {
                this.btnDelete.Enabled = value;
            }
            get
            {
                return this.btnDelete.Enabled;
            }
        }

        public bool BtnDeleteVisible
        {
            set
            {
                this.btnDelete.Visible = value;
            }
            get
            {
                return this.btnDelete.Visible;
            }
        }

        public event EventHandler BtnDeleteClick;

        #endregion

        #region Save Button

        public bool BtnSaveEnabled
        {
            set
            {
                this.btnSave.Enabled = value;
            }
            get
            {
                return this.btnSave.Enabled;
            }
        }

        public bool BtnSaveVisible
        {
            set
            {
                this.btnSave.Visible = value;
            }
            get
            {
                return this.btnSave.Visible;
            }
        }

        public event EventHandler BtnSaveClick;

        #endregion

        #region Cancel Button

        public bool BtnCancelEnabled
        {
            set
            {
                this.btnCancel.Enabled = value;
            }
            get
            {
                return this.btnCancel.Enabled;
            }
        }

        public bool BtnCancelVisible
        {
            set
            {
                this.btnCancel.Visible = value;
            }
            get
            {
                return this.btnCancel.Visible;
            }
        }

        public event EventHandler BtnCancelClick;

        #endregion

        #region ToolStripSeparator1

        public bool toolStripSeparator11Visible
        {
            set
            {
                this.toolStripSeparator11.Visible = value;
            }
            get
            {
                return this.toolStripSeparator11.Visible;
            }
        }

        public bool toolStripSeparator12Visible
        {
            set
            {
                this.toolStripSeparator12.Visible = value;
            }
            get
            {
                return this.toolStripSeparator12.Visible;
            }
        }

        #endregion

        #region First Button

        public bool BtnFirstEnabled
        {
            set
            {
                this.btnFirst.Enabled = value;
            }
            get
            {
                return this.btnFirst.Enabled;
            }
        }

        public bool BtnFirstVisible
        {
            set
            {
                this.btnFirst.Visible = value;
            }
            get
            {
                return this.btnFirst.Visible;
            }
        }

        public event EventHandler BtnFirstClick;

        #endregion

        #region Previous Button

        public bool BtnPreviousEnabled
        {
            set
            {
                this.btnPrevious.Enabled = value;
            }
            get
            {
                return this.btnPrevious.Enabled;
            }
        }

        public bool BtnPreviousVisible
        {
            set
            {
                this.btnPrevious.Visible = value;
            }
            get
            {
                return this.btnPrevious.Visible;
            }
        }

        public event EventHandler BtnPreviousClick;

        #endregion

        #region Next Button

        public bool BtnNextEnabled
        {
            set
            {
                this.btnNext.Enabled = value;
            }
            get
            {
                return this.btnNext.Enabled;
            }
        }

        public bool BtnNextVisible
        {
            set
            {
                this.btnNext.Visible = value;
            }
            get
            {
                return this.btnNext.Visible;
            }
        }

        public event EventHandler BtnNextClick;

        #endregion

        #region Last Button

        public bool BtnLastEnabled
        {
            set
            {
                this.btnLast.Enabled = value;
            }
            get
            {
                return this.btnLast.Enabled;
            }
        }

        public bool BtnLastVisible
        {
            set
            {
                this.btnLast.Visible = value;
            }
            get
            {
                return this.btnLast.Visible;
            }
        }

        public event EventHandler BtnLastClick;

        #endregion

        #region ToolStripSeparator2

        public bool toolStripSeparator21Visible
        {
            set
            {
                this.toolStripSeparator21.Visible = value;
            }
            get
            {
                return this.toolStripSeparator21.Visible;
            }
        }

        public bool toolStripSeparator22Visible
        {
            set
            {
                this.toolStripSeparator22.Visible = value;
            }
            get
            {
                return this.toolStripSeparator22.Visible;
            }
        }

        #endregion

        #region Search Button

        public bool BtnSearchEnabled
        {
            set
            {
                this.btnSearch.Enabled = value;
            }
            get
            {
                return this.btnSearch.Enabled;
            }
        }

        public bool BtnSearchVisible
        {
            set
            {
                this.btnSearch.Visible = value;
            }
            get
            {
                return this.btnSearch.Visible;
            }
        }

        public event EventHandler BtnSearchClick;

        #endregion

        #region CardReturn Button

        public bool BtnCardReturnEnabled
        {
            set
            {
                this.btnCardReturn.Enabled = value;
            }
            get
            {
                return this.btnCardReturn.Enabled;
            }
        }

        public bool BtnCardReturnVisible
        {
            set
            {
                this.btnCardReturn.Visible = value;
            }
            get
            {
                return this.btnCardReturn.Visible;
            }
        }

        public event EventHandler BtnCardReturnClick;

        #endregion

        #region CardMissing Button

        public bool BtnCardMissingEnabled
        {
            set
            {
                this.btnCardMissing.Enabled = value;
            }
            get
            {
                return this.btnCardMissing.Enabled;
            }
        }

        public bool BtnCardMissingVisible
        {
            set
            {
                this.btnCardMissing.Visible = value;
            }
            get
            {
                return this.btnCardMissing.Visible;
            }
        }

        public event EventHandler BtnCardMissingClick;

        #endregion

        #region CardRecovery Button

        public bool BtnCardRecoveryEnabled
        {
            set
            {
                this.btnCardRecovery.Enabled = value;
            }
            get
            {
                return this.btnCardRecovery.Enabled;
            }
        }

        public bool BtnCardRecoveryVisible
        {
            set
            {
                this.btnCardRecovery.Visible = value;
            }
            get
            {
                return this.btnCardRecovery.Visible;
            }
        }

        public event EventHandler BtnCardRecoveryClick;

        #endregion

        #region CardScrapButton

        public bool BtnCardScrapEnabled
        {
            set
            {
                this.btnCardScrap.Enabled = value;
            }
            get
            {
                return this.btnCardScrap.Enabled;
            }
        }

        public bool BtnCardScrapVisible
        {
            set
            {
                this.btnCardScrap.Visible = value;
            }
            get
            {
                return this.btnCardScrap.Visible;
            }
        }

        public event EventHandler BtnCardScrapClick;

        #endregion


        #region BtnGroupPersonButton

        public bool BtnGroupPersonEnabled
        {
            set
            {
                this.btnGroupPerson.Enabled = value;
            }
            get
            {
                return this.btnGroupPerson.Enabled;
            }
        }

        public bool BtnGroupPersonVisible
        {
            set
            {
                this.btnGroupPerson.Visible = value;
            }
            get
            {
                return this.btnGroupPerson.Visible;
            }
        }

        public event EventHandler BtnGroupPersonClick;

        #endregion
        /// <summary>
        /// 設置當前記錄存在的索引位置類型



        /// </summary>
        public RecordIndexType RecordExistPosition
        {
            set;
            get;
        }

        /// <summary>
        /// 自動設置狀態



        /// </summary>
        public bool AutoSetStatus
        {
            set;
            get;
        }

        #endregion


        public bool btnImportDataEnabled
        {
            set
            {
                this.btnImportData.Enabled = value;
            }
            get
            {
                return this.btnImportData.Enabled;
            }
        }

        public bool btnImportDataVisible
        {
            set
            {
                this.btnImportData.Visible = value;
            }
            get
            {
                return this.btnImportData.Visible;
            }
        }

        public event EventHandler btnImportDataClick;

        private void btnImportData_Click(object sender, EventArgs e)
        {
            if (this.btnImportDataClick != null)
            {
                this.btnImportDataClick(sender, e);
            }
        }

        public bool btnExportDataEnabled
        {
            set
            {
                this.btnExportData.Enabled = value;
            }
            get
            {
                return this.btnExportData.Enabled;
            }
        }

        public bool btnExportDataVisible
        {
            set
            {
                this.btnExportData.Visible = value;
            }
            get
            {
                return this.btnExportData.Visible;
            }
        }

        public event EventHandler btnExportDataClick;

        private void btnExportData_Click(object sender, EventArgs e)
        {
            if (this.btnExportDataClick != null)
            {
                this.btnExportDataClick(sender, e);
            }
        }

        #region 导出模版

        public bool btnExportTempEnabled
        {
            set
            {
                this.btnExportTemp.Enabled = value;
            }
            get
            {
                return this.btnExportTemp.Enabled;
            }
        }

        public bool btnExportTempVisible
        {
            set
            {
                this.btnExportTemp.Visible = value;
            }
            get
            {
                return this.btnExportTemp.Visible;
            }
        }

        public event EventHandler btnExportTempClick;
        private void btnExportTemp_Click(object sender, EventArgs e)
        {
            if (this.btnExportTempClick != null)
            {
                this.btnExportTempClick(sender, e);
            }
        } 
        #endregion

    }
}
