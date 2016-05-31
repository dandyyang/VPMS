using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.IModel;
using Model.Management.Master;
using BLL.Factory.Management;
using BLL.IBLL.Management.Master;
using WindowUI.ClassLibrary.Public;
using Common;
using Common.DataTypeVerify;

namespace WindowUI.Management.Master
{
    public partial class BuildingMaster : BaseForm
    {
        IBuildingMasterBL _buildingMasterBL;
        string bdm_iRecordID;
        string displayRecordID;

        public BuildingMaster()
        {
            InitializeComponent();
            this._buildingMasterBL = MasterBLLFactory.GetBLL<IBuildingMasterBL>(MasterBLLFactory.BuildingMaster);
        }

        private void SetControlLength()
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                info = this._buildingMasterBL.GetTableFieldLenght() as BuildingMaster_bdm_Info;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (info != null)
            {
                this.txtcNumber.MaxLength = info.bdm_cNumber_Length;
                this.txtcName.MaxLength = info.bdm_cName_Length;
                this.txtcRemark.MaxLength = info.bdm_cRemark_Length;
            }
        }

        //處理頁面數據顯示
        private void showData(BuildingMaster_bdm_Info info)
        {
            try
            {
                if (info != null)
                {
                    bdm_iRecordID = info.bdm_iRecordID.ToString();
                    txtcNumber.Text = info.bdm_cNumber;
                    txtcName.Text = info.bdm_cName;
                    txtcRemark.Text = info.bdm_cRemark;
                    txtcAdd.Text = info.bdm_cAdd;
                    txtdAddDate.Text = info.bdm_dAddDate != null ? info.bdm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                    txtcLast.Text = info.bdm_cLast;
                    txtdLastDate.Text = info.bdm_dLastDate != null ? info.bdm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch (editStatus)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    this.txtcNumber.TextBoxSetStatus(true);
                    this.txtcName.TextBoxSetStatus(true);
                    this.txtcRemark.TextBoxSetStatus(true);
                    this.ToolBar.BtnNewEnabled = true;
                    this.ToolBar.BtnModifyEnabled = true;
                    this.ToolBar.BtnDeleteEnabled = true;
                    this.ToolBar.BtnSaveEnabled = false;
                    this.ToolBar.BtnCancelEnabled = false;
                    this.ToolBar.BtnFirstEnabled = true;
                    this.ToolBar.BtnPreviousEnabled = true;
                    this.ToolBar.BtnNextEnabled = true;
                    this.ToolBar.BtnLastEnabled = true;
                    this.ToolBar.BtnSearchEnabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    this.txtcNumber.TextBoxSetStatus(true);
                    this.txtcName.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.ToolBar.BtnNewEnabled = false;
                    this.ToolBar.BtnModifyEnabled = false;
                    this.ToolBar.BtnDeleteEnabled = false;
                    this.ToolBar.BtnSaveEnabled = true;
                    this.ToolBar.BtnCancelEnabled = true;
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnSearchEnabled = false;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    this.txtcNumber.TextBoxSetStatus(false);
                    this.txtcName.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.ToolBar.BtnNewEnabled = false;
                    this.ToolBar.BtnModifyEnabled = false;
                    this.ToolBar.BtnDeleteEnabled = false;
                    this.ToolBar.BtnSaveEnabled = true;
                    this.ToolBar.BtnCancelEnabled = true;
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnSearchEnabled = false;
                    break;
                default:

                    break;
            }
        }

        /// <summary>
        /// 首條、尾條、上條、下條事件


        /// </summary>
        #region
        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        private void HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum statc)
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_First: info = _buildingMasterBL.GetRecord_First();
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Last: info = _buildingMasterBL.GetRecord_Last();
                        break;
                    default: break;
                }

                //設置ToolBar狀態


                setToolBarViewStatc(statc);

                //數據顯示
                showData(info);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum statc)
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_Next: com.CommandType = Model.Base.DataBaseCommandType.Next;
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Previous: com.CommandType = Model.Base.DataBaseCommandType.Previous;
                        break;
                    default:
                        break;
                }

                Model.Base.DataBaseCommandKeyInfo comKey = new Model.Base.DataBaseCommandKeyInfo();
                comKey.KeyValue = bdm_iRecordID;
                com.KeyInfoList.Add(comKey);

                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_Next:
                        info = _buildingMasterBL.GetRecord_Next(com);
                        if (info != null)
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                        }
                        else
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                        }
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Previous:
                        info = _buildingMasterBL.GetRecord_Previous(com);
                        if (info != null)
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                        }
                        else
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_First);
                        }
                        break;
                    default:
                        break;
                }

                //顯視數據處理
                showData(info);

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

        }
        #endregion

        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Insert);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;
            txtcNumber.Text = "";
            txtcName.Text = "";
            txtcRemark.Text = "";
            displayRecordID = bdm_iRecordID;
            bdm_iRecordID = null;
            txtcNumber.Focus();
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            DataTypeVerifyResultInfo vInfo=null;
            vInfo = General.VerifyDataType(txtcNumber.Text, DataType.ChinaChar);
            if (vInfo.IsMatch)
            {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            info.bdm_cNumber = txtcNumber.Text.Trim();
            info.bdm_cName = txtcName.Text.Trim();
            info.bdm_cRemark = txtcRemark.Text.Trim();
            try
            {
                if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                {
                    //新增記錄
                    info.bdm_cAdd = this.UserInformation.usm_cUserLoginID;
                    info.bdm_cLast = this.UserInformation.usm_cUserLoginID;
                    info.bdm_dAddDate = DateTime.Now;
                    info.bdm_dLastDate = DateTime.Now;
                    msg = _buildingMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                }
                else
                {
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                    {
                        //修改記錄
                        info.bdm_iRecordID = Convert.ToInt32(bdm_iRecordID);
                        info.bdm_cLast = this.UserInformation.usm_cUserLoginID;
                        info.bdm_dLastDate = DateTime.Now;
                        msg = _buildingMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            if (msg.messageText != "")
            {
                ShowInformationMessage(msg.messageText);
            }

            if (msg.boolValue)
            {
                SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                {
                    //新增完成後跳至瀏覽最後一跳記錄

                    setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                    info = null;
                    try
                    {
                        SetControlLength();
                        info = _buildingMasterBL.GetRecord_Last();
                        showData(info);
                        txtcNumber.TextBoxSetStatus(true);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                }
                else
                {
                    //修改完成後跳至瀏覽相應記錄
                    setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                    info = _buildingMasterBL.DisplayRecord(info) as BuildingMaster_bdm_Info;
                    txtcNumber.Text = info.bdm_cNumber;
                    txtcName.Text = info.bdm_cName;
                    txtcRemark.Text = info.bdm_cRemark;
                    this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                    SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                }
            }
            else
            {
                //編號重復
                txtcNumber.Focus();
            }
            }
            else
            {
                ShowInformationMessage("建築物編號不能"+vInfo.Message);
            }
        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();
            try
            {
                info.bdm_iRecordID = Convert.ToInt32(displayRecordID);
                Model.IModel.IModelObject result = _buildingMasterBL.DisplayRecord(info);
                info = result as BuildingMaster_bdm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetControlStatus(this.EditState);
            showData(info);
            if (bdm_iRecordID == "0")
            {
                setNullDataStatc();
            }
        }

        //處理ToolBar狀態


        private void setToolBarViewStatc(DefineConstantValue.GetReocrdEnum statc)
        {
            switch (statc)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    ToolBar.BtnFirstEnabled = false;
                    ToolBar.BtnPreviousEnabled = false;
                    ToolBar.BtnNextEnabled = true;
                    ToolBar.BtnLastEnabled = true;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Last:
                    ToolBar.BtnFirstEnabled = true;
                    ToolBar.BtnPreviousEnabled = true;
                    ToolBar.BtnNextEnabled = false;
                    ToolBar.BtnLastEnabled = false;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Middle:
                    ToolBar.BtnFirstEnabled = true;
                    ToolBar.BtnPreviousEnabled = true;
                    ToolBar.BtnNextEnabled = true;
                    ToolBar.BtnLastEnabled = true;
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Null:
                    ToolBar.BtnFirstEnabled = false;
                    ToolBar.BtnPreviousEnabled = false;
                    ToolBar.BtnNextEnabled = false;
                    ToolBar.BtnLastEnabled = false;
                    break;
                default: break;
            }
        }

        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();
            info.RecordID = Convert.ToInt32(bdm_iRecordID);
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            try
            {
                msg = _buildingMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Delete);
                //ShowInformationMessage(msg.messageText);
                HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);

                if (bdm_iRecordID == info.RecordID.ToString())
                {
                    HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
                }
                SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                if (bdm_iRecordID == info.RecordID.ToString())
                {
                    setNullDataStatc();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        //設置無數據狀態


        private void setNullDataStatc()
        {
            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
            ToolBar.BtnModifyEnabled = false;
            ToolBar.BtnDeleteEnabled = false;
            ToolBar.BtnModifyEnabled = false;
            ToolBar.BtnSearchEnabled = false;
            ToolBar.BtnNewEnabled = true;
            txtcNumber.Text = "";
            txtcName.Text = "";
            txtcRemark.Text = "";
            txtcAdd.Text = "";
            txtdAddDate.Text = "";
            txtcLast.Text = "";
            txtdLastDate.Text = "";
            bdm_iRecordID = "0";
        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            displayRecordID = bdm_iRecordID;
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();
            BuildingMasterSearch win = new BuildingMasterSearch();
            //win.ShowDialog();
            win.ShowForm(info);
            if (win.DialogResult == DialogResult.OK)
            {
                bdm_iRecordID =info.bdm_iRecordID.ToString();
                
                try
                {
                    info.RecordID = Convert.ToInt32(bdm_iRecordID);
                    Model.IModel.IModelObject result = _buildingMasterBL.DisplayRecord(info);
                    info = result as BuildingMaster_bdm_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetControlStatus(this.EditState);
                showData(info);
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
            }
            win.Dispose();
            win = null;
        }

        private void BuildingMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            BuildingMaster_bdm_Info info = null;
            try
            {
                SetControlLength();
                bdm_iRecordID = "0";
                info = _buildingMasterBL.GetRecord_Last();
                showData(info);
                txtcNumber.TextBoxSetStatus(true);
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                if (bdm_iRecordID == "0")
                {
                    setNullDataStatc();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

    }
}
