using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.General;
using BLL.IBLL.General;
using BLL.Factory.General;
using Common;
using WindowUI.ClassLibrary.Public;
using Model.Management.DataRightsManagement;
using WindowUI.Management.Master;
using Model.Management.Master;
using Model.Base;
using BLL.IBLL.Management.DataRightsManagement;
using BLL.Factory.Management;
using Model.SysMaster;
using WindowUI.SysMaster;

namespace WindowUI.Management.DataRightsManagement
{
    public partial class DataRightsRoleSetting : BaseForm//Form  BaseForm //
    {
        IGeneralBL _generalBL;
        IDataRightsRoleBL _dateRightRoleBL;
        string iRecordID = string.Empty;
        string _payment = DefineConstantValue.SIOT_DataRightsTypeDefine.Payment;
        string _attendance = DefineConstantValue.SIOT_DataRightsTypeDefine.Attendance;

        DataRightsRole_drr_Info drrInfo = new DataRightsRole_drr_Info();
        DataRightsRole_drr_Info _BackUpdrrInfo = new DataRightsRole_drr_Info();
        List<Sys_UserMaster_usm_Info> _usmList = new List<Sys_UserMaster_usm_Info>();

        //DefineConstantValue.EditStateEnum EditState;
        public DataRightsRoleSetting()
        {
            InitializeComponent();
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._dateRightRoleBL = MasterBLLFactory.GetBLL<IDataRightsRoleBL>(MasterBLLFactory.DataRightsRole);


            BuildTree();

            rbtAttPart.Checked = true;
            rbtPayPart.Checked = true;

            Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        #region 增删键函数

        bool IsExistsItem(string text, string Type)
        {
            bool _result = false;
            if (Type == _payment)
            {
                for (int i = 0; i < lvwPay.Items.Count; i++)
                {
                    if (lvwPay.Items[i].SubItems[0].Text == text)
                    {
                        _result = true;
                    }
                }
            }
            if (Type == _attendance)
            {
                for (int i = 0; i < lvwAtt.Items.Count; i++)
                {
                    if (lvwAtt.Items[i].SubItems[0].Text == text)
                    {
                        _result = true;
                    }
                }
            }
            return _result;
        }

        void SetDataToLvw(string _type)
        {
            //try
            //{
            //    CardUserMasterSearch teacherSearch = new CardUserMasterSearch();
            //    teacherSearch.SelectTeacher();
            //    if (teacherSearch.DialogResult == DialogResult.OK)
            //    {
            //        List<CardUserMaster_cus_Info> List = teacherSearch._listinfo;
            //        foreach (CardUserMaster_cus_Info cus in List)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            list.Text = cus.cus_cNumber.ToString();
            //            if (!IsExistsItem(list.Text, _type))
            //            {
            //                ListViewItem it = new ListViewItem(cus.cus_cNumber.ToString());
            //                it.SubItems.Add(cus.cus_cIdentityNum.ToString());
            //                it.SubItems.Add(cus.cus_cChaName.ToString());
            //                if (_type == _payment)
            //                    lvwPay.Items.Add(it);
            //                if (_type == _attendance)
            //                    lvwAtt.Items.Add(it);
            //            }
            //        }
            //    }
            //    teacherSearch.Dispose();
            //    teacherSearch = null;
            //}
            //catch (Exception Ex)
            //{
            //    ShowErrorMessage(Ex);
            //}
        }

        private void btnAddPay_Click(object sender, EventArgs e)
        {
            SetDataToLvw(_payment);
        }

        private void btnDeletePay_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwPay.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("您確認要删除吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        lvwPay.Items.Remove(lvwPay.SelectedItems[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void btnAddAtt_Click(object sender, EventArgs e)
        {
            SetDataToLvw(_attendance);
        }

        private void btnDeleteAtt_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwAtt.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("您確認要删除吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        lvwAtt.Items.Remove(lvwAtt.SelectedItems[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
        #endregion

        #region TreeView 函数
        void BuildTree()
        {
            try
            {
                var schoolMaster = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                var specialtyMaster = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SpecialtyMaster);
                //var graduationPeriod = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.GraduationPeriod);
                //var cardUserClass = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserClass);
                //院校部

                foreach (ComboboxDataInfo school in schoolMaster)
                {
                    TreeNode node = new TreeNode(school.DisplayMember);
                    node.Tag = school;
                    trwPay.Nodes.Add(node);
                    //专业
                    foreach (ComboboxDataInfo specialty in specialtyMaster)
                    {
                        TreeNode s_node = new TreeNode(specialty.DisplayMember);
                        s_node.Tag = specialty;
                        node.Nodes.Add(s_node);
                        //届别                     
                        //foreach (ComboboxDataInfo gp in graduationPeriod)
                        //{
                        //    TreeNode c_node = new TreeNode(gp.DisplayMember);
                        //    c_node.Tag = gp;
                        //    s_node.Nodes.Add(c_node);
                        //    //班级
                        //    foreach (ComboboxDataInfo cla in cardUserClass)
                        //    {
                        //        TreeNode e_node = new TreeNode(cla.DisplayMember);
                        //        e_node.Tag = cla;
                        //        c_node.Nodes.Add(e_node);
                        //    }
                        //}
                    }
                }

                foreach (ComboboxDataInfo school in schoolMaster)
                {
                    TreeNode node = new TreeNode(school.DisplayMember);
                    node.Tag = school;
                    trwAtt.Nodes.Add(node);
                    //专业
                    foreach (ComboboxDataInfo specialty in specialtyMaster)
                    {
                        TreeNode s_node = new TreeNode(specialty.DisplayMember);
                        s_node.Tag = specialty;
                        node.Nodes.Add(s_node);
                        //届别                     
                        //foreach (ComboboxDataInfo gp in graduationPeriod)
                        //{
                        //    TreeNode c_node = new TreeNode(gp.DisplayMember);
                        //    c_node.Tag = gp;
                        //    s_node.Nodes.Add(c_node);
                        //    //班级
                        //    foreach (ComboboxDataInfo cla in cardUserClass)
                        //    {
                        //        TreeNode e_node = new TreeNode(cla.DisplayMember);
                        //        e_node.Tag = cla;
                        //        c_node.Nodes.Add(e_node);
                        //    }
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        void SetParentNodeState(bool checkState, TreeNode node)
        {
            node.Checked = checkState;
            if (node.Parent != null)
            {
                if (node.Checked)
                {
                    SetParentNodeState(checkState, node.Parent);
                }
                else
                {
                    bool existDiffent = false;
                    foreach (TreeNode tn in node.Parent.Nodes)
                    {
                        if (tn.Checked != node.Checked)
                        {
                            existDiffent = true;
                        }
                    }
                    if (!existDiffent)
                    {
                        SetParentNodeState(checkState, node.Parent);
                    }
                }
            }
        }

        void SetChildNodeState(bool checkState, TreeNode node)
        {
            foreach (TreeNode tn in node.Nodes)
            {
                tn.Checked = checkState;
                if (tn.Nodes.Count > 0)
                {
                    SetChildNodeState(checkState, tn);
                }
            }
        }

        private void trwPay_AfterCheck(object sender, TreeViewEventArgs e)
        {
            trwPay.AfterCheck -= trwPay_AfterCheck;
            if (e.Node.Nodes.Count > 0)
            {
                SetParentNodeState(e.Node.Checked, e.Node);
                SetChildNodeState(e.Node.Checked, e.Node);
            }
            else
            {
                SetParentNodeState(e.Node.Checked, e.Node);
            }
            trwPay.AfterCheck += trwPay_AfterCheck;
        }

        private void trwAtt_AfterCheck(object sender, TreeViewEventArgs e)
        {
            trwAtt.AfterCheck -= trwAtt_AfterCheck;
            if (e.Node.Nodes.Count > 0)
            {
                SetParentNodeState(e.Node.Checked, e.Node);
                SetChildNodeState(e.Node.Checked, e.Node);
            }
            else
            {
                SetParentNodeState(e.Node.Checked, e.Node);
            }
            trwAtt.AfterCheck += trwAtt_AfterCheck;
        }
        #endregion

        #region rbt按钮状态改变

        void SetState(string _button)
        {
            switch (_button)
            {
                case "rbtPayAll":
                    lvwPay.Enabled = false;
                    trwPay.Enabled = false;
                    btnAddPay.Enabled = false;
                    btnDeletePay.Enabled = false;
                    break;
                case "rbtPayPart":
                    lvwPay.Enabled = true;
                    trwPay.Enabled = true;
                    btnAddPay.Enabled = true;
                    btnDeletePay.Enabled = true;
                    break;
                case "rbtAttAll":
                    lvwAtt.Enabled = false;
                    trwAtt.Enabled = false;
                    btnAddAtt.Enabled = false;
                    btnDeleteAtt.Enabled = false;
                    break;
                case "rbtAttPart":
                    lvwAtt.Enabled = true;
                    trwAtt.Enabled = true;
                    btnAddAtt.Enabled = true;
                    btnDeleteAtt.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void rbtPayAll_CheckedChanged(object sender, EventArgs e)
        {
            SetState(rbtPayAll.Name.ToString());
        }

        private void rbtPayPart_CheckedChanged(object sender, EventArgs e)
        {
            SetState(rbtPayPart.Name.ToString());
        }

        private void rbtAttAll_CheckedChanged(object sender, EventArgs e)
        {
            SetState(rbtAttAll.Name.ToString());
        }

        private void rbtAttPart_CheckedChanged(object sender, EventArgs e)
        {
            SetState(rbtAttPart.Name.ToString());
        }
        #endregion

        #region tabControl 函数

        void AddDataToObject()
        {
            #region 2011-10-17备注
            DataRightsRole_TeacherList_dtl_Info dtlTeacher = null;
            DataRightsRole_ClassList_dtc_Info dtcClass = null;

            try
            {
                if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                {
                    drrInfo.drr_iRecordID = int.Parse(iRecordID);
                }
                drrInfo.drr_cNumber = txtNum.Text;
                drrInfo.drr_cName = txtName.Text;
                drrInfo.drr_cRemark = txtRemark.Text;

                if (rbtPayAll.Checked)
                {
                    dtlTeacher = new DataRightsRole_TeacherList_dtl_Info();
                    dtlTeacher.dtl_cRoleNumber = drrInfo.drr_cNumber;
                    dtlTeacher.dtl_cDataRightType = _payment;
                    dtlTeacher.dtl_iIsAllRights = true;
                    drrInfo.teacherList.Add(dtlTeacher);

                    dtcClass = new DataRightsRole_ClassList_dtc_Info();
                    dtcClass.dtc_cRoleNumber = drrInfo.drr_cNumber;
                    dtcClass.dtc_cDataRightType = _payment;
                    dtcClass.dtc_iIsAllRights = true;
                    drrInfo.classList.Add(dtcClass);
                }
                if (rbtPayPart.Checked)
                {
                    //ListView  Pay
                    for (int i = 0; i < lvwPay.Items.Count; i++)
                    {
                        dtlTeacher = new DataRightsRole_TeacherList_dtl_Info();
                        dtlTeacher.dtl_cRoleNumber = drrInfo.drr_cNumber;
                        dtlTeacher.dtl_cDataRightType = _payment;
                        dtlTeacher.dtl_iIsAllRights = false;
                        dtlTeacher.dtl_cTeacherNum = lvwPay.Items[i].SubItems[0].Text;
                        drrInfo.teacherList.Add(dtlTeacher);
                    }
                    //TreeView  Pay
                    AddTreeDataToObject(trwPay.Nodes, _payment);
                }
                if (rbtAttAll.Checked)
                {
                    dtlTeacher = new DataRightsRole_TeacherList_dtl_Info();
                    dtlTeacher.dtl_cRoleNumber = drrInfo.drr_cNumber;
                    dtlTeacher.dtl_cDataRightType = _attendance;
                    dtlTeacher.dtl_iIsAllRights = true;
                    drrInfo.teacherList.Add(dtlTeacher);

                    dtcClass = new DataRightsRole_ClassList_dtc_Info();
                    dtcClass.dtc_cRoleNumber = drrInfo.drr_cNumber;
                    dtcClass.dtc_cDataRightType = _attendance;
                    dtcClass.dtc_iIsAllRights = true;
                    drrInfo.classList.Add(dtcClass);
                }
                if (rbtAttPart.Checked)
                {
                    //ListView  Att
                    for (int i = 0; i < lvwAtt.Items.Count; i++)
                    {
                        dtlTeacher = new DataRightsRole_TeacherList_dtl_Info();
                        dtlTeacher.dtl_cRoleNumber = drrInfo.drr_cNumber;
                        dtlTeacher.dtl_cDataRightType = _attendance;
                        dtlTeacher.dtl_iIsAllRights = false;
                        dtlTeacher.dtl_cTeacherNum = lvwAtt.Items[i].SubItems[0].Text;
                        drrInfo.teacherList.Add(dtlTeacher);
                    }
                    //TreeView  Att 
                    AddTreeDataToObject(trwAtt.Nodes, _attendance);
                }
                if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)//(true)//
                {
                    drrInfo.drr_cAdd = txtcAdd.Text;
                    drrInfo.drr_dAddDate = DateTime.Now;
                }
                drrInfo.drr_cLast = txtcLast.Text;
                drrInfo.drr_dLastDate = DateTime.Now;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            #endregion
        }

        void AddTreeDataToObject(TreeNodeCollection nodes, string _type)
        {
            DataRightsRole_ClassList_dtc_Info dtcClass = null;
            try
            {
                foreach (TreeNode tn in nodes)
                {
                    if (tn.Level < 3)
                    {
                        AddTreeDataToObject(tn.Nodes, _type);
                    }
                    else
                    {
                        if (tn.Checked)
                        {
                            dtcClass = new DataRightsRole_ClassList_dtc_Info();
                            dtcClass.dtc_cRoleNumber = drrInfo.drr_cNumber;
                            dtcClass.dtc_cDataRightType = _type;
                            dtcClass.dtc_iIsAllRights = false;

                            dtcClass.dtc_cSchoolNum = ((ComboboxDataInfo)tn.Parent.Parent.Parent.Tag).ValueMember;
                            dtcClass.dtc_cSpecialtyNum = ((ComboboxDataInfo)tn.Parent.Parent.Tag).ValueMember;
                            dtcClass.dtc_cGraduationPeriod = ((ComboboxDataInfo)tn.Parent.Tag).ValueMember;
                            dtcClass.dtc_cClassNum = ((ComboboxDataInfo)tn.Tag).ValueMember;

                            drrInfo.classList.Add(dtcClass);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
        #endregion

        #region ToolBar函数
        void Backupobject(DataRightsRole_drr_Info info)
        {
            try
            {
                _BackUpdrrInfo = General.CopyObjectValue<DataRightsRole_drr_Info, DataRightsRole_drr_Info>(info);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        void SetFormState(DefineConstantValue.EditStateEnum _state)
        {
            this.EditState = _state;
            switch (_state)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    userToolBar.BtnSaveEnabled = false;
                    userToolBar.BtnCancelEnabled = false;
                    userToolBar.BtnNewEnabled = true;
                    userToolBar.BtnModifyEnabled = true;
                    userToolBar.BtnDeleteEnabled = true;
                    userToolBar.BtnFirstEnabled = true;
                    userToolBar.BtnPreviousEnabled = true;
                    userToolBar.BtnNextEnabled = true;
                    userToolBar.BtnLastEnabled = true;
                    userToolBar.BtnSearchEnabled = true;

                    txtNum.Enabled = false;
                    txtNum.TextBoxSetStatus(true);
                    txtName.Enabled = false;
                    txtName.TextBoxSetStatus(true);
                    txtRemark.Enabled = false;
                    txtRemark.TextBoxSetStatus(true);

                    rbtAttAll.Enabled = false;
                    rbtAttPart.Enabled = false;
                    rbtPayAll.Enabled = false;
                    rbtPayPart.Enabled = false;

                    btnAddPay.Enabled = false;
                    btnDeletePay.Enabled = false;
                    btnAddAtt.Enabled = false;
                    btnDeleteAtt.Enabled = false;
                    tabCol.Enabled = false;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    userToolBar.BtnSaveEnabled = true;
                    userToolBar.BtnCancelEnabled = true;
                    userToolBar.BtnNewEnabled = false;
                    userToolBar.BtnModifyEnabled = false;
                    userToolBar.BtnDeleteEnabled = false;
                    userToolBar.BtnFirstEnabled = false;
                    userToolBar.BtnPreviousEnabled = false;
                    userToolBar.BtnNextEnabled = false;
                    userToolBar.BtnLastEnabled = false;
                    userToolBar.BtnSearchEnabled = false;

                    //txtNum.Enabled = true;
                    //txtNum.TextBoxSetStatus(false);
                    txtName.Enabled = true;
                    txtName.TextBoxSetStatus(false);
                    txtRemark.Enabled = true;
                    txtRemark.TextBoxSetStatus(false);

                    rbtAttAll.Enabled = true;
                    rbtAttPart.Enabled = true;
                    rbtPayAll.Enabled = true;
                    rbtPayPart.Enabled = true;

                    btnAddPay.Enabled = true;
                    btnDeletePay.Enabled = true;
                    btnAddAtt.Enabled = true;
                    btnDeleteAtt.Enabled = true;
                    tabCol.Enabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    userToolBar.BtnSaveEnabled = true;
                    userToolBar.BtnCancelEnabled = true;
                    userToolBar.BtnNewEnabled = false;
                    userToolBar.BtnModifyEnabled = false;
                    userToolBar.BtnDeleteEnabled = false;
                    userToolBar.BtnFirstEnabled = false;
                    userToolBar.BtnPreviousEnabled = false;
                    userToolBar.BtnNextEnabled = false;
                    userToolBar.BtnLastEnabled = false;
                    userToolBar.BtnSearchEnabled = false;

                    txtNum.Enabled = true;
                    txtNum.TextBoxSetStatus(false);
                    txtName.Enabled = true;
                    txtName.TextBoxSetStatus(false);
                    txtRemark.Enabled = true;
                    txtRemark.TextBoxSetStatus(false);

                    rbtAttAll.Enabled = true;
                    rbtAttPart.Enabled = true;
                    rbtPayAll.Enabled = true;
                    rbtPayPart.Enabled = true;

                    btnAddPay.Enabled = true;
                    btnDeletePay.Enabled = true;
                    btnAddAtt.Enabled = true;
                    btnDeleteAtt.Enabled = true;
                    tabCol.Enabled = true;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Delete:
                    break;
                default:
                    break;
            }
        }

        void Handel(DefineConstantValue.GetReocrdEnum _type)
        {
            ReturnValueInfo msg = new ReturnValueInfo();
            DataBaseCommandInfo com = new DataBaseCommandInfo();
            DataBaseCommandKeyInfo comkey = new DataBaseCommandKeyInfo();
            comkey.KeyValue = iRecordID;
            com.KeyInfoList.Add(comkey);

            DataRightsRole_drr_Info info = null;
            switch (_type)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _dateRightRoleBL.GetRecord_First();
                        if (info.drr_iRecordID != 0)
                        {
                            ShowInfoToUI(info);
                            SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                            userToolBar.BtnModifyEnabled = false;
                            userToolBar.BtnDeleteEnabled = false;
                            userToolBar.BtnLastEnabled = false;
                            userToolBar.BtnNextEnabled = false;
                            userToolBar.BtnSearchEnabled = false;
                            SetNull();
                            Backupobject(info);
                        }
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Next:
                    try
                    {
                        info = _dateRightRoleBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _dateRightRoleBL.GetRecord_Last();
                            userToolBar.BtnLastEnabled = false;
                            userToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _dateRightRoleBL.GetRecord_Next(com);
                        }
                        ShowInfoToUI(info);
                        SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Previous:
                    try
                    {
                        info = _dateRightRoleBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _dateRightRoleBL.GetRecord_First();
                            userToolBar.BtnFirstEnabled = false;
                            userToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _dateRightRoleBL.GetRecord_Previous(com);
                        }

                        ShowInfoToUI(info);
                        SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Last:
                    try
                    {
                        info = _dateRightRoleBL.GetRecord_Last();
                        if (info.drr_iRecordID != 0)
                        {
                            ShowInfoToUI(info);
                            SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                            userToolBar.BtnFirstEnabled = false;
                            userToolBar.BtnPreviousEnabled = false;
                            userToolBar.BtnModifyEnabled = false;
                            userToolBar.BtnDeleteEnabled = false;
                            userToolBar.BtnLastEnabled = false;
                            userToolBar.BtnNextEnabled = false;
                            userToolBar.BtnSearchEnabled = false;
                            SetNull();
                            Backupobject(info);
                        }
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
            }
        }

        void ShowInfoToUI(DataRightsRole_drr_Info info)
        {
            SetNull();
            if (info == null)
            {
                return;
            }
            Backupobject(info);
            try
            {
                iRecordID = info.drr_iRecordID.ToString();
                txtNum.Text = info.drr_cNumber;
                txtName.Text = info.drr_cName;
                txtRemark.Text = info.drr_cRemark;
                txtcAdd.Text = info.drr_cAdd;
                txtdAddDate.Text = info.drr_dAddDate != null ? info.drr_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                txtcLast.Text = info.drr_cLast;
                txtdLastDate.Text = info.drr_dLastDate != null ? info.drr_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                foreach (DataRightsRole_TeacherList_dtl_Info item in info.teacherList)
                {
                    if (item.dtl_cDataRightType == _payment)
                    {
                        if (item.dtl_iIsAllRights)
                            rbtPayAll.Checked = true;
                        else
                        {
                            rbtPayPart.Checked = true;
                            ListViewItem it = new ListViewItem(item.dtl_cTeacherNum.ToString());
                            it.SubItems.Add(item.dtl_cRoleNumber.ToString());
                            it.SubItems.Add(item.dtl_cTeacherName.ToString());
                            lvwPay.Items.Add(it);
                        }

                    }
                    if (item.dtl_cDataRightType == _attendance)
                    {
                        if (item.dtl_iIsAllRights)
                            rbtAttAll.Checked = true;
                        else
                        {
                            rbtAttPart.Checked = true;
                            ListViewItem it = new ListViewItem(item.dtl_cTeacherNum.ToString());
                            it.SubItems.Add(item.dtl_cRoleNumber.ToString());
                            it.SubItems.Add(item.dtl_cTeacherName.ToString());
                            lvwAtt.Items.Add(it);
                        }
                    }
                }

                foreach (var scs in info.classList)
                {
                    foreach (TreeNode sch in trwPay.Nodes)
                    {
                        if ((sch.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cSchoolNum)
                        {
                            continue;
                        }
                        foreach (TreeNode spec in sch.Nodes)
                        {
                            if ((spec.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cSpecialtyNum)
                            {
                                continue;
                            }
                            foreach (TreeNode pd in spec.Nodes)
                            {
                                if ((pd.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cGraduationPeriod)
                                {
                                    continue;
                                }
                                foreach (TreeNode ev in pd.Nodes)
                                {
                                    if ((ev.Tag as ComboboxDataInfo).ValueMember == scs.dtc_cClassNum && scs.dtc_cDataRightType == _payment)
                                    {
                                        ev.Checked = true;
                                    }
                                }
                            }
                        }
                    }

                    foreach (TreeNode sch in trwAtt.Nodes)
                    {
                        if ((sch.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cSchoolNum)
                        {
                            continue;
                        }
                        foreach (TreeNode spec in sch.Nodes)
                        {
                            if ((spec.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cSpecialtyNum)
                            {
                                continue;
                            }
                            foreach (TreeNode pd in spec.Nodes)
                            {
                                if ((pd.Tag as ComboboxDataInfo).ValueMember != scs.dtc_cGraduationPeriod)
                                {
                                    continue;
                                }
                                foreach (TreeNode ev in pd.Nodes)
                                {
                                    if ((ev.Tag as ComboboxDataInfo).ValueMember == scs.dtc_cClassNum && scs.dtc_cDataRightType == _attendance)
                                    {
                                        ev.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }

                DataRightsRole_drr_Info drrInfo = new DataRightsRole_drr_Info();
                drrInfo.drr_cNumber = txtNum.Text;
                try
                {
                    _usmList = _dateRightRoleBL.GetRoleUserList(drrInfo);
                }
                catch (Exception Ex)
                {

                    ShowErrorMessage(Ex);
                }
                FullLvw(lvwUser, _usmList);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }


        private void FullLvw(ListView lvw, List<Sys_UserMaster_usm_Info> sources)
        {
            lvw.Items.Clear();
            foreach (Sys_UserMaster_usm_Info item in sources)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = item.usm_cChaName;
                li.SubItems[0].Name = item.usm_cUserLoginID;
                li.ImageIndex = 0;
                li.ToolTipText = item.usm_cRemark;

                lvw.Items.Add(li);
            }
        }

        void SetNull()
        {
            txtNum.Text = "";
            txtName.Text = "";
            txtRemark.Text = "";
            rbtPayPart.Checked = true;
            lvwPay.Items.Clear();
            foreach (TreeNode uam in trwPay.Nodes)
            {
                uam.Checked = false;
            }
            rbtAttPart.Checked = true;
            lvwAtt.Items.Clear();
            foreach (TreeNode uam in trwAtt.Nodes)
            {
                uam.Checked = false;
            }
        }

        private void userToolBar_BtnNewClick(object sender, EventArgs e)
        {
            SetFormState(DefineConstantValue.EditStateEnum.OE_Insert);
            SetNull();
            txtcAdd.Text = this.UserInformation.usm_cUserLoginID;
            txtdAddDate.Text = DateTime.Now.ToString();
            txtdLastDate.Text = DateTime.Now.ToString();

            _usmList.Clear();
            FullLvw(lvwUser,_usmList);

        }

        private void userToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            SetFormState(DefineConstantValue.EditStateEnum.OE_Update);
        }

        private void userToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            ReturnValueInfo msg = new ReturnValueInfo();
            DataBaseCommandInfo com = new DataBaseCommandInfo();
            DataBaseCommandKeyInfo comkey = new DataBaseCommandKeyInfo();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    drrInfo.drr_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _dateRightRoleBL.Save(drrInfo, DefineConstantValue.EditStateEnum.OE_Delete);

                    Handel(DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (drrInfo.drr_iRecordID == int.Parse(iRecordID))
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                }
            }
            catch (Exception Ex)
            {
                //ShowErrorMessage(Ex);
            }
        }

        private void userToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            ReturnValueInfo msg = new ReturnValueInfo();
            AddDataToObject();
            try
            {
                msg = _dateRightRoleBL.Save(drrInfo, this.EditState);//DefineConstantValue.EditStateEnum.OE_Insert);//

                #region 保存角色用户
                List<Sys_UserMaster_usm_Info> userList = new List<Sys_UserMaster_usm_Info>();
                List<DataRightsRole_drr_Info> roleList = new List<DataRightsRole_drr_Info>();

                foreach (ListViewItem item in lvwUser.Items)
                {
                    Sys_UserMaster_usm_Info userInfo = new Sys_UserMaster_usm_Info();
                    userInfo.usm_cUserLoginID = item.SubItems[0].Name;
                    userList.Add(userInfo);
                }

                roleList.Add(drrInfo);
                ReturnValueInfo returnInfo = _dateRightRoleBL.SaveUserToRole(userList, roleList, false,true);
                #endregion


                if (msg.boolValue && returnInfo.boolValue)
                {
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                        SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);

                }
                else
                    MessageBox.Show(msg.messageText.ToString());
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }

            drrInfo = new DataRightsRole_drr_Info();
        }

        private void userToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            try
            {
                ShowInfoToUI(this._BackUpdrrInfo);
                SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                this._BackUpdrrInfo = null;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void userToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            Handel(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void userToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            Handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void userToolBar_BtnNextClick(object sender, EventArgs e)
        {
            Handel(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void userToolBar_BtnLastClick(object sender, EventArgs e)
        {
            Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        private void userToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            DataRightsRoleSettingSearch win = new DataRightsRoleSettingSearch();
            win.ShowDialog();

            if (win.DialogResult == DialogResult.OK)
            {
                iRecordID = win.displayRecordID;
                DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                try
                {
                    info.drr_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _dateRightRoleBL.DisplayRecord(info);
                    info = result as DataRightsRole_drr_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetFormState(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                ShowInfoToUI(info);
            }


            win.Dispose();
            win = null;
        }
        #endregion

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            SysUserMasterSearch win = new SysUserMasterSearch();
            List<Sys_UserMaster_usm_Info> list = new List<Sys_UserMaster_usm_Info>();
            win.ShowForm(list);

            if (list.Count > 0)
            {
                foreach (Sys_UserMaster_usm_Info item in list)
                {
                    Sys_UserMaster_usm_Info tab = _usmList.SingleOrDefault(t => t.usm_cUserLoginID == item.usm_cUserLoginID);
                    if (tab == null)
                    {
                        _usmList.Add(item);
                    }
                }

                FullLvw(lvwUser, _usmList);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwUser.CheckedItems)
            {
                Sys_UserMaster_usm_Info usmInfo = new Sys_UserMaster_usm_Info();
                usmInfo = _usmList.FirstOrDefault(t => t.usm_cUserLoginID == item.SubItems[0].Name);
                if (usmInfo != null)
                {
                    _usmList.Remove(usmInfo);
                }

            }
            FullLvw(lvwUser, _usmList);
        }


    }
}
