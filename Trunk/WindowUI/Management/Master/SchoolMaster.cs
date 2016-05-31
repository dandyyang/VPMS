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
    public partial class SchoolMaster : BaseForm
    {
        ISchoolMasterBL _schoolMasterBL;
        string scm_iRecordID;
        string displayRecordID;

        public SchoolMaster()
        {
            InitializeComponent();
            this._schoolMasterBL = MasterBLLFactory.GetBLL<ISchoolMasterBL>(MasterBLLFactory.SchoolMaster);
        }

        private void SchoolMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            SchoolMaster_scm_Info info = null;
            try
            {
                SetControlLength();
                scm_iRecordID = "0";
                info = _schoolMasterBL.GetRecord_Last();
                showData(info);
                txtcNumber.TextBoxSetStatus(true);
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                if (scm_iRecordID == "0" || scm_iRecordID==null) 
                {
                    setNullDataStatc();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void SetControlLength()
        {
            SchoolMaster_scm_Info info = null;
            try
            {
                info = this._schoolMasterBL.GetTableFieldLenght() as SchoolMaster_scm_Info;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (info != null)
            {
                this.txtcNumber.MaxLength = info.scm_cNumber_Length;
                this.txtcName.MaxLength = info.scm_cName_Length ;
                this.txtcRemark.MaxLength = info.scm_cRemark_Length;
            }
        }

        //處理頁面數據顯示
        private void showData(SchoolMaster_scm_Info info) 
        {
            if (info != null)
            {
                try
                {
                    scm_iRecordID = info.scm_iRecordID.ToString();
                    txtcNumber.Text = info.scm_cNumber;
                    txtcName.Text = info.scm_cName;
                    txtcRemark.Text = info.scm_cRemark;
                    txtcAdd.Text = info.scm_cAdd;
                    txtdAddDate.Text = info.scm_dAddDate != null ? info.scm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                    txtcLast.Text = info.scm_cLast;
                    txtdLastDate.Text = info.scm_dLastDate != null ? info.scm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
            }
        }

        private void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch(editStatus)
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
            SchoolMaster_scm_Info info = null;
            try
            {
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_First: 
                        info = _schoolMasterBL.GetRecord_First();
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Last: 
                        info = _schoolMasterBL.GetRecord_Last();
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
            SchoolMaster_scm_Info info = null;
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
                comKey.KeyValue = scm_iRecordID;
                com.KeyInfoList.Add(comKey);

                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_Next: 
                        info = _schoolMasterBL.GetRecord_Next(com);
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
                        info = _schoolMasterBL.GetRecord_Previous(com);
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
                displayRecordID=scm_iRecordID;
                scm_iRecordID = null;
                txtcNumber.Focus();
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcNumber.Text, DataType.ChinaChar);
            if (vInfo.IsMatch)
            {
                SchoolMaster_scm_Info info = new SchoolMaster_scm_Info();
                Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
                info.scm_cNumber = txtcNumber.Text.Trim();
                info.scm_cName = txtcName.Text.Trim();
                info.scm_cRemark = txtcRemark.Text.Trim();
                try
                {
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                    {
                        //新增記錄
                        info.scm_cAdd = this.UserInformation.usm_cUserLoginID;
                        info.scm_dAddDate = DateTime.Now;
                        info.scm_cLast = this.UserInformation.usm_cUserLoginID;
                        info.scm_dLastDate = DateTime.Now;
                        msg = _schoolMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                    }
                    else
                    {
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            //修改記錄
                            info.RecordID = Convert.ToInt32(scm_iRecordID);
                            info.scm_cLast = this.UserInformation.usm_cUserLoginID;
                            info.scm_dLastDate = DateTime.Now;
                            msg = _schoolMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
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
                            info = _schoolMasterBL.GetRecord_Last();
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
                        info = _schoolMasterBL.DisplayRecord(info) as SchoolMaster_scm_Info;
                        txtcNumber.Text = info.scm_cNumber;
                        txtcName.Text = info.scm_cName;
                        txtcRemark.Text = info.scm_cRemark;
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
                ShowInformationMessage("院系部編號不能"+vInfo.Message);
            }
        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            SchoolMaster_scm_Info info = new SchoolMaster_scm_Info();
            try 
            {
                info.RecordID = Convert.ToInt32(displayRecordID);
                Model.IModel.IModelObject result;
                result = _schoolMasterBL.DisplayRecord(info);
                info = result as SchoolMaster_scm_Info;
            }
            catch( Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetControlStatus(this.EditState);
            showData(info);
            if (scm_iRecordID == "0")
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
            SchoolMaster_scm_Info info =new SchoolMaster_scm_Info();
            info.RecordID =Convert.ToInt32( scm_iRecordID);
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            try
            {
                msg = _schoolMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Delete);
                HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);

                if (scm_iRecordID == info.RecordID.ToString())
                {
                    HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
                }
                SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                if (scm_iRecordID == info.RecordID.ToString())
                {
                    setNullDataStatc();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            //ShowInformationMessage(msg.messageText);
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
            scm_iRecordID = "0";
        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            displayRecordID=scm_iRecordID;
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            SchoolMasterSearch win = new SchoolMasterSearch();
            win.ShowDialog();
           
            if (win.DialogResult == DialogResult.OK) 
            {
                scm_iRecordID = win.displayRecordID;
                SchoolMaster_scm_Info info = new SchoolMaster_scm_Info();
                try
                {
                    info.RecordID = Convert.ToInt32(scm_iRecordID);
                    Model.IModel.IModelObject result = _schoolMasterBL.DisplayRecord(info);
                    info = result as SchoolMaster_scm_Info;
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
    }
}
