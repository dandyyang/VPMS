using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.DataTypeVerify;
using WeifenLuo.WinFormsUI.Docking;
using Model.SysMaster;
using WindowControls;
using Model.IModel;
using Sunisoft.IrisSkin;

namespace WindowUI
{
    public partial class BaseForm : DockContent
    {
        /// <summary>
        /// 系統提示信息文字定義
        /// </summary>
        public readonly DefineConstantValue.SystemMessage SystemMessageText;

        /// <summary>
        /// 記錄編輯狀態
        /// </summary>
        public DefineConstantValue.EditStateEnum EditState;

        /// <summary>
        /// 記錄ToolBar狀態
        /// </summary>
        public DefineConstantValue.GetReocrdEnum TBViewStatc;

        /// <summary>
        /// 用戶信息
        /// </summary>
        public Sys_UserMaster_usm_Info UserInformation;

        /// <summary>
        /// 功能列表
        /// </summary>
        public List<Sys_FunctionMaster_fum_Info> FunctionList;

        public List<Sys_FunctionMaster_fum_Info> _setFunctionList;

        public static Sunisoft.IrisSkin.SkinEngine skin = new SkinEngine();

        public BaseForm()
        {

            InitializeComponent();

            this.TBViewStatc = DefineConstantValue.GetReocrdEnum.GR_First;
            this.SystemMessageText = new DefineConstantValue.SystemMessage(string.Empty);
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="toolBar"></param>
        public void SetPurview(UserToolBar toolBar)
        {
            FunctionList = _setFunctionList;

            if (toolBar != null)
            {
                toolBar.BtnNewVisible = false;
                toolBar.BtnModifyVisible = false;
                toolBar.BtnDeleteVisible = false;
                toolBar.BtnCardIssuanceVisible = false;
                toolBar.BtnCardReturnVisible = false;
                toolBar.BtnCardMissingVisible = false;
                toolBar.BtnCardRecoveryVisible = false;
                toolBar.BtnCardScrapVisible = false;

                toolBar.BtnDataInputVisible = false;
                toolBar.BtnDataExportVisible = false;
                toolBar.BtnExpCusDataVisible = false;
                toolBar.BtnExportTemplateVisible = false;
                toolBar.BtnExportCardUserPhotoVisible = false;

                toolBar.BtnImportPhotoVisible = false;
                toolBar.BtnGroupPersonVisible = false;

                toolBar.btnImportDataVisible = false;
                toolBar.btnExportDataVisible = false;

                if (this.FunctionList != null && this.FunctionList.Count > 0)
                {
                    for (int i = 0; i < this.FunctionList.Count; i++)
                    {

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.New)
                        {
                            toolBar.BtnNewVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.Modify)
                        {
                            toolBar.BtnModifyVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.Delete)
                        {
                            toolBar.BtnDeleteVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.CardIssuance)
                        {
                            toolBar.BtnCardIssuanceVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.DataInput)
                        {
                            toolBar.BtnDataInputVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.CardMissing)
                        {
                            toolBar.BtnCardMissingVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.CardReturn)
                        {
                            toolBar.BtnCardReturnVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.CardRecovery)
                        {
                            toolBar.BtnCardRecoveryVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.CardScrap)
                        {
                            toolBar.BtnCardScrapVisible = true;
                            continue;
                        }
                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.DataExport)
                        {
                            toolBar.BtnDataExportVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ExpCusData)
                        {
                            toolBar.BtnExpCusDataVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ExportTemplate)
                        {
                            toolBar.BtnExportTemplateVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ExportCardUserPhoto)
                        {
                            toolBar.BtnExportCardUserPhotoVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ImportCardUserPhoto)
                        {
                            toolBar.BtnImportPhotoVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ImportCardUserData)
                        {
                            toolBar.BtnImportCardUserDataVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.GroupPersonSetting)
                        {
                            toolBar.BtnGroupPersonVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ImportTheme)
                        {
                            toolBar.btnImportDataVisible = true;
                            continue;
                        }

                        if (this.FunctionList[i].fum_cFunctionNumber.Trim() == DefineConstantValue.Sys_FormFunctionNum.ExportTheme)
                        {
                            toolBar.btnExportDataVisible = true;
                            continue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 顯示出錯信息
        /// </summary>
        /// <param name="Ex"></param>
        public void ShowErrorMessage(Exception Ex)
        {
            MessageBox.Show(Ex.Message.Trim(), this.SystemMessageText.strMessageTitle + this.SystemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 顯示出錯信息
        /// </summary>
        /// <param name="Ex"></param>
        public void ShowErrorMessage(string text)
        {
            MessageBox.Show(text.Trim(), this.SystemMessageText.strMessageTitle + this.SystemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 顯示提示信息
        /// </summary>
        /// <param name="text"></param>
        public void ShowInformationMessage(string text)
        {
            MessageBox.Show(text, this.SystemMessageText.strMessageTitle + this.SystemMessageText.strInformation.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 顯示警告信息
        /// </summary>
        /// <param name="text"></param>
        public void ShowWarningMessage(string text)
        {
            MessageBox.Show(text, this.SystemMessageText.strMessageTitle + this.SystemMessageText.strWarning.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 顯示確認信息
        /// </summary>
        /// <param name="text"></param>
        public bool ShowQuestionMessage(string text)
        {
            bool isYes = false;
            if (MessageBox.Show(text, this.SystemMessageText.strMessageTitle + this.SystemMessageText.strQuestion.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isYes = true;
            }

            return isYes;
        }

        /// <summary>
        /// 數據類型驗證
        /// </summary>
        /// <param name="fieldText">欄位標題</param>
        /// <param name="text">驗證內容</param>
        /// <param name="dataType">數據類型</param>
        /// <returns></returns>
        public bool VerifyDataType(string fieldText, string text, DataType dataType)
        {
            DataTypeVerifyResultInfo verifyResult = null;

            verifyResult = General.VerifyDataType(text, dataType);

            if (verifyResult != null)
            {
                if (!verifyResult.IsMatch)
                {
                    MessageBox.Show(fieldText + "： " + verifyResult.Message, this.SystemMessageText.strMessageTitle + this.SystemMessageText.strWarning.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return verifyResult.IsMatch;
        }

        /// <summary>
        /// 设置窗口为选择状态
        /// 不能最大化，不能最小化，不能调整大小
        /// </summary>
        public void SetFormSelectState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
