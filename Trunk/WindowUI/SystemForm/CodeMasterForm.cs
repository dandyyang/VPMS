using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.IBLL.General;
using BLL.Factory.General;
using Common;
using Model.IModel;
using WindowUI.ClassLibrary.Public;
using BLL.IBLL.Management.Master;
using BLL.Factory.Management;
using Model.Management.Master;

namespace WindowUI.SystemForm
{
    public partial class CodeMasterForm : BaseForm
    {
        IGeneralBL _generalBL;
        ICodeMasterBL _codeMasterBL;
        CodeMaster_cmt_Info _info=new CodeMaster_cmt_Info();

        public CodeMasterForm()
        {
            InitializeComponent();
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._codeMasterBL = MasterBLLFactory.GetBLL<ICodeMasterBL>(MasterBLLFactory.CodeMaster_cmt);
            BindCombox(DefineConstantValue.MasterType.CodeMaster_Key1,null);
        }

        private void BindCombox(DefineConstantValue.MasterType mType,string key)
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                if (key != null)
                {
                    result = _generalBL.GetMasterDataInformations(mType,key); 
                }
                else
                {
                    result = _generalBL.GetMasterDataInformations(mType); 
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            switch (mType)
            {
                case DefineConstantValue.MasterType.CodeMaster_Key1:
                    cbocKey1.SetDataSource(result);

                    break;
                case DefineConstantValue.MasterType.CodeMaster_Key2:
                    cbocKey2.SetDataSource(result);

                    break;
                default:
                    break;
            }
        }

        private void cbocKey1_SelectedValueChanged(object sender, EventArgs e)
        {
            if ( cbocKey1.SelectedValue!=null)
            {
                BindCombox(DefineConstantValue.MasterType.CodeMaster_Key2, cbocKey1.SelectedValue.ToString()); 
            }
        }

        private void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch (editStatus)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    cbocKey1.Enabled = false;
                    cbocKey2.Enabled = false;
                    this.txtcValue.TextBoxSetStatus(true);
                    this.txtfNum.TextBoxSetStatus(true);
                    this.txtcRemark.TextBoxSetStatus(true);
                    this.txtcRemark2.TextBoxSetStatus(true);

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
                    cbocKey1.Enabled = false;
                    cbocKey2.Enabled = false;
                    this.txtcValue.TextBoxSetStatus(false);
                    this.txtfNum.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.txtcRemark2.TextBoxSetStatus(false);

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
                    cbocKey1.Enabled = true;
                    cbocKey2.Enabled = true;
                    this.txtcValue.TextBoxSetStatus(false);
                    this.txtfNum.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.txtcRemark2.TextBoxSetStatus(false);

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

        private void CodeMasterForm_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            try
            {
                _info = _codeMasterBL.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
            showData(_info);

            if (_info.cmt_iRecordID == 0 || _info.cmt_iRecordID == null) 
            {
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
            }

        }

        private void showData(CodeMaster_cmt_Info info) 
        {
            if (info != null)
            {
                cbocKey1.Text = info.cmt_cKey1;
                cbocKey2.Text = info.cmt_cKey2;
                txtcValue.Text = info.cmt_cValue;
                txtfNum.Text = info.cmt_fNumber.ToString();
                txtcRemark.Text = info.cmt_cRemark;
                txtcRemark2.Text = info.cmt_cRemark2;
                txtcLast.Text = info.cmt_cLast;

                txtcAdd.Text = info.cmt_cAdd;
                try
                {
                    txtdAddDate.Text = ((info.cmt_dAddDate != null) ? Convert.ToDateTime(info.cmt_dAddDate).ToString(DefineConstantValue.gc_DateFormat) : "");
                   
                    txtdLastDate.Text = ((info.cmt_dLastDate != null) ? Convert.ToDateTime(info.cmt_dLastDate).ToString(DefineConstantValue.gc_DateFormat) : "");
                }
                catch (Exception Ex)
                {
                    
                    ShowErrorMessage(Ex);
                }
            }
            else 
            {
                cbocKey1.Text = "";
                cbocKey2.Text = "";
                txtcValue.Text ="";
                txtfNum.Text = "";
                txtcRemark.Text ="";
                txtcRemark2.Text ="";

                txtcAdd.Text ="";
                txtdAddDate.Text ="";
                txtcLast.Text = "";
                txtdLastDate.Text ="";
            }
        }

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

        private void HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum statc)
        {
            try
            {
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_First: _info = _codeMasterBL.GetRecord_First();
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Last: _info = _codeMasterBL.GetRecord_Last();
                        break;
                    default: break;
                }

                //設置ToolBar狀態


                setToolBarViewStatc(statc);

                //數據顯示
                showData(_info);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum statc)
        {
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
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
                    info = _info;
                    comKey.KeyValue = _info.cmt_iRecordID.ToString();
                    com.KeyInfoList.Add(comKey);

                    switch (statc)
                    {
                        case DefineConstantValue.GetReocrdEnum.GR_Next:
                            _info = _codeMasterBL.GetRecord_Next(com);
                            if (_info != null)
                            {
                                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                            }
                            else
                            {
                                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                                _info = info;
                            }
                            break;
                        case DefineConstantValue.GetReocrdEnum.GR_Previous:
                            _info = _codeMasterBL.GetRecord_Previous(com);
                            if (_info != null)
                            {
                                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                            }
                            else
                            {
                                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_First);
                                _info = info;
                            }
                            break;
                        default:
                            break;
                    }

                    //顯視數據處理
                    showData(_info); 

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            showData(_info);
        }

        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo returnValue = new Model.General.ReturnValueInfo();
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            info.cmt_iRecordID = _info.cmt_iRecordID;
           returnValue= _codeMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Delete);
           if (returnValue.boolValue) 
           {
               com.CommandType = Model.Base.DataBaseCommandType.Next;
               Model.Base.DataBaseCommandKeyInfo comKey = new Model.Base.DataBaseCommandKeyInfo();
               comKey.KeyValue = info.cmt_iRecordID.ToString();
               com.KeyInfoList.Add(comKey);
               try
               {
                   _info = _codeMasterBL.GetRecord_Next(com);
               }
               catch (Exception Ex)
               {
                   ShowErrorMessage (Ex);
               }
               if (_info == null)
               {
                   com.CommandType = Model.Base.DataBaseCommandType.Previous;
                   try
                   {
                       _info = _codeMasterBL.GetRecord_Previous(com);
                   }
                   catch (Exception Ex)
                   {
                       ShowErrorMessage (Ex);
                   }
                   showData(_info);
                   this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                   SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                   setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                   if (_info == null)
                   {
                       cbocKey1.Text = "";
                       setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
                   }
               }
           }

        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
        }

        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Insert);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            
            //重置表格
            showData(info);
            cbocKey1.Text = "";
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info.cmt_cKey1 = cbocKey1.Text;
            info.cmt_cKey2 = cbocKey2.Text;
            info.cmt_cValue = txtcValue.Text;
            info.cmt_fNumber =Convert.ToDecimal( txtfNum.Text);
            info.cmt_cRemark = txtcRemark.Text;
            info.cmt_cRemark2 = txtcRemark2.Text;

            info.cmt_cAdd = UserInformation.usm_cUserLoginID;
            info.cmt_dAddDate = DateTime.Now;
            info.cmt_cLast = UserInformation.usm_cUserLoginID;
            info.cmt_dLastDate = DateTime.Now;
            switch (this.EditState)
            {
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    SaveSub(info);
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    info.cmt_iRecordID = _info.cmt_iRecordID;
                    UpdateSub(info);
                    break;
                default:
                    break;
            }

        }

        private void SaveSub(CodeMaster_cmt_Info info) 
        {
            try
            {
                Model.General.ReturnValueInfo returnValue = new Model.General.ReturnValueInfo();
                returnValue = _codeMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                if (returnValue.boolValue)
                {
                    BindCombox(DefineConstantValue.MasterType.CodeMaster_Key1, null);
                    _info = _codeMasterBL.GetRecord_Last();
                    SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;

                    showData(_info);
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage (Ex);
            }
        }

        private void UpdateSub(CodeMaster_cmt_Info info) 
        {
            try
            {
                Model.General.ReturnValueInfo returnValue = new Model.General.ReturnValueInfo();
                returnValue = _codeMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                if (returnValue.boolValue)
                {
                    SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage( Ex);
            }
        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            CodeMasterFormSearch win = new CodeMasterFormSearch();
            win.ShowForm(_info);
            if (win.DialogResult == DialogResult.OK) 
            {
                showData(_info);
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
            }
            win.Dispose();
            win = null;
        }
    }
}
