using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.Util;
using Common.DataTypeVerify;
using BLL.Factory.Management;
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using BLL.IBLL.General;
using Model.Management.Master;
using Model.General;
using Model.SysMaster;
using Model.IModel;


namespace WindowUI.Management.Master
{
    public partial class CardUserMasterInput : BaseForm
    {
        private string _cardUserIdentity;
        private string _excelFilePath;

        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;

        private List<IModelObject> _sexList;
        private List<IModelObject> _classList;
        private List<IModelObject> _identifyList;
        private List<IModelObject> _schoolList;
        private List<IModelObject> _specialList;
        private List<IModelObject> _departmentList;
        private List<IModelObject> _courseList;
        private List<IModelObject> _siteList;
        private List<IModelObject> _gotoSchoolTypeList;


        public CardUserMasterInput(string excelFilePath)
        {
            InitializeComponent();
            _excelFilePath = excelFilePath;

            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);


            _sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
            _classList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserClass);
            _identifyList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserIdentity);
            _schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
            _siteList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.BuildingSiteMaster);
            _gotoSchoolTypeList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.GoToSchoolType);

        }


        Timer timer = new Timer();
        private void CardUserMasterInput_Shown(object sender, EventArgs e)
        {

            timer.Tick += LoadExcelData;
            timer.Interval = 500;
            timer.Start();

        }

        private void LoadExcelData(object sender, EventArgs e)
        {
            timer.Tick -= LoadExcelData;
            ExcelUtil excelUtil = new ExcelUtil();
            try
            {

                bool dataValiRst = true;
                excelUtil.Open(_excelFilePath);
                Microsoft.Office.Interop.Excel.Worksheet stuSheet = excelUtil.GetSheet("学生");
                Microsoft.Office.Interop.Excel.Worksheet teaSheet = excelUtil.GetSheet("教职人员");
                toolStripProgressBar1.Value = 20;

                if (stuSheet != null)
                {

                    #region 学生
                    _specialList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SpecialtyMaster);

                    _cardUserIdentity = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student;

                    string graduationPeriod = "";
                    string cardUserClass = "";
                    string specialty = "";
                    string school = "";

                    #region 检查数据是否选中
                    object tmp = stuSheet.get_Range("b2", "b2").Value2;
                    if (tmp == null)
                    {
                        MessageBox.Show("届别没有选中", "系统提示", MessageBoxButtons.OK);
                        dataValiRst = false;
                    }
                    else
                    {
                        graduationPeriod = tmp.ToString();
                        lblGraduationPeriod.Text = graduationPeriod;

                        tmp = stuSheet.get_Range("e2", "e2").Value2.ToString();
                        if (tmp == null)
                        {
                            MessageBox.Show("班级没有选中", "系统提示", MessageBoxButtons.OK);
                            dataValiRst = false;
                        }
                        else
                        {
                            cardUserClass = tmp.ToString();
                            lblClass.Text = cardUserClass;

                            tmp = stuSheet.get_Range("e1", "e1").Value2.ToString();
                            if (tmp == null)
                            {
                                MessageBox.Show("专业没有选中", "系统提示", MessageBoxButtons.OK);
                                dataValiRst = false;
                            }
                            else
                            {
                                specialty = tmp.ToString();
                                lblSpecialty.Text = specialty;

                                tmp = stuSheet.get_Range("b1", "b1").Value2.ToString();
                                if (tmp == null)
                                {
                                    MessageBox.Show("院系部没有选中", "系统提示", MessageBoxButtons.OK);
                                    dataValiRst = false;
                                }
                                else
                                {
                                    school = tmp.ToString();
                                    lblSchool.Text = school;
                                }

                            }
                        }
                    }
                    #endregion


                    if (dataValiRst)
                    {


                        excelUtil.DeleteRows(1, 1, 3);

                        string tempFile = Application.StartupPath + "/temp/student.xls";
                        bool r = excelUtil.SaveCopyAs(tempFile);


                        System.Threading.Thread.Sleep(3000);
                       
                        toolStripProgressBar1.Value = 50;
                        DataSet ds = General.GetExcelDs(tempFile, "学生");
                        excelUtil.Close();
                        toolStripProgressBar1.Value = 80;
                        DataTable ndt = ds.Tables[0].DefaultView.ToTable(true, new string[] { "用户编号","学号", "中文名称", "英文名称", "性別","短信通知","短信电话1", "短信电话2","短信电话3","短信电话4",
              "电邮地址",  "邮件通知","走读类型","宿舍地点","床位","允许消费","亲情号码1","亲情号码2","亲情号码3","亲情号码4"});
                        ndt.Columns.Add("sex");
                        ndt.Columns.Add("identity");
                        ndt.Columns.Add("school");
                        ndt.Columns.Add("specialty");
                        ndt.Columns.Add("graduationPeriod");
                        ndt.Columns.Add("class");
                        ndt.Columns.Add("siteNum");
                        ndt.Columns.Add("gotoSchoolType");
                  
                        foreach (DataRow row in ndt.Rows)
                        {
                            row["sex"] = GetValueMember(_sexList, row["性別"].ToString());                           
                            row["identity"] = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student;
                            row["school"] = GetValueMember(_schoolList, school);
                            row["specialty"] = GetValueMember(_specialList, specialty);
                            row["graduationPeriod"] = graduationPeriod;
                            row["class"] = GetValueMember(_classList, cardUserClass);
                            row["siteNum"] = GetValueMember(_siteList, row["宿舍地点"].ToString());
                            row["gotoSchoolType"] = GetValueMember(_gotoSchoolTypeList, row["走读类型"].ToString());
                        }
                        gvwCardUser.DataSource = ndt;
                        toolStripProgressBar1.Value = 90;
                        CheckStuData();

                        toolStripProgressBar1.Value = 100;
                        toolStripProgressBar1.Visible = false;
                    }
                    else
                    {
                        toolStripProgressBar1.Value = 100;
                        toolStripProgressBar1.Visible = false;

                        excelUtil.Close();
                    }


                    #endregion

                }
                else if (teaSheet != null)
                {

                    #region 隐藏导入学生用的菜单栏



                    lblClass.Visible = false;
                    lblGraduationPeriod.Visible = false;
                    lblSchool.Visible = false;
                    lblSpecialty.Visible = false;
                    toolStripSeparator1.Visible = false;
                    toolStripSeparator2.Visible = false;
                    toolStripSeparator3.Visible = false;
                    toolStripSeparator4.Visible = false;
                    #endregion

                    #region 教职人员
                    _departmentList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.DepartmentMaster);
                    _courseList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CourseMaster);

                    excelUtil.Close();
                    DataSet ds = General.GetExcelDs(_excelFilePath, "教职人员");

                    _cardUserIdentity = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff;
                    DataTable ndt = ds.Tables[0].DefaultView.ToTable(true, new string[] { "有效", "用户编号", "中文名称", "英文名称", "性別", "院系部", "科室",  "短信接收电话", 
                    "电邮地址", "短信通知", "邮件通知","负责课程","亲情号码1","亲情号码2","亲情号码3","亲情号码4" });

                    ndt.Columns.Add("sex");
                    ndt.Columns.Add("identity");
                    ndt.Columns.Add("school");
                    ndt.Columns.Add("department");
                    ndt.Columns.Add("inChargeCourse");

                    foreach (DataRow row in ndt.Rows)
                    {

                        row["sex"] = GetValueMember(_sexList, row["性別"].ToString());
                        row["identity"] = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff;
                        row["school"] = GetValueMember(_schoolList, row["院系部"].ToString());
                        row["department"] = GetValueMember(_departmentList, row["科室"].ToString());
                        row["inChargeCourse"] = GetCourseValueMember(_courseList, row["负责课程"].ToString());
                    }
                    gvwCardUser.DataSource = ndt;
                    toolStripProgressBar1.Value = 90;
                    CheckStaffData();
                    toolStripProgressBar1.Value = 100;
                    toolStripProgressBar1.Visible = false;
                    #endregion

                }

            }
            catch (Exception ex)
            {

                ShowErrorMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnExpEorData.Enabled = false;

            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            string cus_cIdentityNum = "";
            toolStripProgressBar1.Visible = true;
            foreach (DataGridViewRow row in gvwCardUser.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(row.ErrorText))
                    {
                        continue;
                    }

                    info.cus_cIdentityNum = row.Cells["identity"].Value.ToString();
                    if (info.cus_cIdentityNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
                    {
                        #region 学生资料
                        info.cus_lValid = true;
                        info.cus_cNumber = row.Cells["用户编号"].Value.ToString();
                        info.cus_cStudentId = row.Cells["学号"].Value.ToString();
                        info.cus_cChaName = row.Cells["中文名称"].Value.ToString();
                        info.cus_cEngName = row.Cells["英文名称"].Value.ToString();
                        info.cus_cSexNum = row.Cells["sex"].Value.ToString();

                        info.cus_cSchoolNum = row.Cells["school"].Value.ToString();//院系部
                        info.cus_cSpecialtyNum = row.Cells["specialty"].Value.ToString();//专业
                        info.cus_cGraduationPeriod = row.Cells["graduationPeriod"].Value.ToString();//届别
                        info.cus_cClassNum = row.Cells["class"].Value.ToString();//班级

                        info.cus_cGotoSchoolType = row.Cells["gotoSchoolType"].Value.ToString();//走读类型
                        info.cus_cDormitorySiteNum = row.Cells["siteNum"].Value.ToString();//宿舍地点
                        info.cus_cBedNum = row.Cells["床位"].Value.ToString();

                        info.cus_lIsSendSMS = row.Cells["短信通知"].Value.ToString() == "Y";
                        info.cus_cSMSReceivePhone = row.Cells["短信电话1"].Value.ToString();
                        info.cus_cAppendPhone1 = row.Cells["短信电话2"].Value.ToString();
                        info.cus_cAppendPhone2 = row.Cells["短信电话3"].Value.ToString();
                        info.cus_cAppendPhone3 = row.Cells["短信电话4"].Value.ToString();

                        info.cus_lIsSendEmail = row.Cells["邮件通知"].Value.ToString() == "Y";
                        info.cus_cMailAddress = row.Cells["电邮地址"].Value.ToString();


                        info.cus_lCashPay = row.Cells["允许消费"].Value.ToString() == "Y";

                        info.cus_cGroupNum = "";

                        info.CardUserPhoneNum = new CardUserPhoneNumMaster_cup_Info();
                        info.CardUserPhoneNum.cup_Phone1 = row.Cells["亲情号码1"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone2 = row.Cells["亲情号码2"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone3 = row.Cells["亲情号码3"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone4 = row.Cells["亲情号码4"].Value.ToString();
                        #endregion
                    }
                    else if (info.cus_cIdentityNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
                    {
                        #region 教职人员资料

                        info.cus_lValid = row.Cells["有效"].Value.ToString() == "Y";
                        info.cus_cNumber = row.Cells["用户编号"].Value.ToString();
                        info.cus_cChaName = row.Cells["中文名称"].Value.ToString();
                        info.cus_cEngName = row.Cells["英文名称"].Value.ToString();

                        info.cus_cSexNum = row.Cells["sex"].Value.ToString();
                        info.cus_cSchoolNum = row.Cells["school"].Value.ToString();
                        info.cus_cDepartmentNum = row.Cells["department"].Value.ToString();

                        //if (row.Cells["床位"] != null)
                        //{
                        //    info.cus_cBedNum = row.Cells["床位"].Value.ToString();
                        //}
                        info.cus_cBedNum = "";

                        info.cus_cSMSReceivePhone = row.Cells["短信接收电话"].Value.ToString();
                        info.cus_cMailAddress = row.Cells["电邮地址"].Value.ToString();

                        info.cus_lIsSendSMS = row.Cells["短信通知"].Value.ToString() == "Y";
                        info.cus_lIsSendEmail = row.Cells["邮件通知"].Value.ToString() == "Y";

                        info.CardUserPhoneNum = new CardUserPhoneNumMaster_cup_Info();
                        info.CardUserPhoneNum.cup_Phone1 = row.Cells["亲情号码1"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone2 = row.Cells["亲情号码2"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone3 = row.Cells["亲情号码3"].Value.ToString();
                        info.CardUserPhoneNum.cup_Phone4 = row.Cells["亲情号码4"].Value.ToString();

                        #endregion
                    }

                    info.cus_cAdd = this.UserInformation.usm_cUserLoginID;
                    info.cus_dAddDate = DateTime.Now;
                    info.cus_cLast = this.UserInformation.usm_cUserLoginID;
                    info.cus_dLastDate = DateTime.Now;


                    ReturnValueInfo returnInfo = _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                    if (!returnInfo.boolValue)
                    {
                        row.ErrorText = returnInfo.messageText;
                        cus_cIdentityNum += row.Cells["用户编号"].Value.ToString() + ",";
                    }
                }
                catch (Exception ex)
                {
                    row.ErrorText = ex.Message;
                    cus_cIdentityNum += row.Cells["用户编号"].Value.ToString() + ",";
                }

                toolStripProgressBar1.Value = (row.Index + 1) / gvwCardUser.Rows.Count;
            }
            toolStripProgressBar1.Visible = false;
            string msg = "保存完成。";
            if (cus_cIdentityNum != string.Empty)
            {
                msg += "用户编号：{0}数据存在问题,请检查。";
                msg = string.Format(msg, cus_cIdentityNum.Substring(0, cus_cIdentityNum.Length - 1));
            }
            btnExpEorData.Enabled = true;
            MessageBox.Show(msg, "系统提示", MessageBoxButtons.OK);

        }

        private void CheckStuData()
        {
            for (int rowIndex = 0; rowIndex < gvwCardUser.Rows.Count; rowIndex++)
            {

                DataGridViewRow row = gvwCardUser.Rows[rowIndex];
                DataTypeVerifyResultInfo vInfo = null;
                string txtcNumber = row.Cells["用户编号"].Value.ToString();

                if (txtcNumber == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["用户编号"]);
                    //row.DefaultCellStyle.BackColor = Color.Gray;    
                    row.ErrorText = "用户编号不能为空";
                }
                else
                {
                    vInfo = General.VerifyDataType(txtcNumber, DataType.ChinaChar);
                    if (!vInfo.IsMatch)
                    {
                        SetCellBackColorError(row.Cells["用户编号"]);
                        row.ErrorText = vInfo.Message;
                    }

                }


                string txtcChaName = row.Cells["中文名称"].Value.ToString();
                if (txtcChaName == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["中文名称"]);
                    row.ErrorText = "中文名称不能为空";
                }

                //string txtcEngName = row.Cells["英文名称"].Value.ToString();
                //if (txtcEngName == string.Empty)
                //{
                //    SetCellBackColorEmpty(row.Cells["英文名称"]);
                //    row.ErrorText = "英文名称不能为空";
                //}

                //if (txtcNumber == string.Empty && txtcChaName == string.Empty && txtcEngName == string.Empty)
                //{
                //    gvwCardUser.Rows.Remove(row);
                //    continue;
                //}

                if (txtcNumber == string.Empty && txtcChaName == string.Empty)
                {
                    gvwCardUser.Rows.Remove(row);
                    continue;
                }

                if (row.Cells["性別"] != null)
                {
                    string cbcSexNum = row.Cells["性別"].Value.ToString().Trim();
                    if (string.IsNullOrEmpty(cbcSexNum))
                    {
                        SetCellBackColorEmpty(row.Cells["性別"]);
                        row.ErrorText = "性別不能为空";
                    }
                }
                if (row.Cells["走读类型"] != null)
                {
                    string cellValue = row.Cells["走读类型"].Value.ToString().Trim();
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        SetCellBackColorEmpty(row.Cells["走读类型"]);
                        row.ErrorText = "走读类型不能为空";
                    }
                }

                string cus_lIsSendSMS = row.Cells["短信通知"].Value.ToString();
                if (cus_lIsSendSMS == string.Empty)
                {
                    row.Cells["短信通知"].Value = "N";
                }
                string cus_lIsSendEmail = row.Cells["邮件通知"].Value.ToString();
                if (cus_lIsSendEmail == string.Empty)
                {
                    row.Cells["邮件通知"].Value = "N";
                }
                string cus_lIsAllowPay = row.Cells["允许消费"].Value.ToString();
                if (cus_lIsAllowPay == string.Empty)
                {
                    row.Cells["允许消费"].Value = "N";
                }
            }



            //隐藏字段，保存下拉KEY
            gvwCardUser.Columns["sex"].Visible = false;
            gvwCardUser.Columns["identity"].Visible = false;
            gvwCardUser.Columns["school"].Visible = false;
            gvwCardUser.Columns["specialty"].Visible = false;
            gvwCardUser.Columns["graduationPeriod"].Visible = false;
            gvwCardUser.Columns["class"].Visible = false;
            gvwCardUser.Columns["siteNum"].Visible = false;
            gvwCardUser.Columns["gotoSchoolType"].Visible = false;
            gvwCardUser.AutoResizeColumns();

        }

        private string GetValueMember(List<IModelObject> list, string displayMember)
        {
            foreach (ComboboxDataInfo item in list)
            {
                if (item.DisplayMember == displayMember)
                {
                    return item.ValueMember;
                }
            }
            return null;
        }

        private string GetCourseValueMember(List<IModelObject> list, string courseDisplayMember)
        {
            string temp = "";
            string[] names = courseDisplayMember.Split(',');
            foreach (ComboboxDataInfo item in list)
            {
                foreach (var name in names)
                {
                    if (name == string.Empty)
                    {
                        continue;
                    }
                    if (item.DisplayMember == name)
                    {
                        temp += item.ValueMember + ",";
                        break;
                    }
                }
            }
            if (temp == string.Empty)
            {
                return temp;
            }
            return temp.Substring(0, temp.Length - 1);
        }

        private void CheckStaffData()
        {
            for (int rowIndex = 0; rowIndex < gvwCardUser.Rows.Count; rowIndex++)
            {

                DataGridViewRow row = gvwCardUser.Rows[rowIndex];
                DataTypeVerifyResultInfo vInfo = null;
                string txtcNumber = row.Cells["用户编号"].Value.ToString();
                if (txtcNumber == string.Empty)
                {

                    row.DefaultCellStyle.BackColor = Color.Gray;
                    row.Cells["用户编号"].Selected = true;
                    gvwCardUser.Rows.Remove(row);
                    continue;
                }
                else
                {
                    vInfo = General.VerifyDataType(txtcNumber, DataType.ChinaChar);
                    if (!vInfo.IsMatch)
                    {
                        SetCellBackColorError(row.Cells["用户编号"]);
                    }

                }



                string cus_lValid = row.Cells["有效"].Value.ToString();
                if (cus_lValid == string.Empty)
                {
                    row.Cells["有效"].Value = "N";
                }


                string txtcChaName = row.Cells["中文名称"].Value.ToString();
                if (txtcChaName == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["中文名称"]);
                }

                string txtcEngName = row.Cells["英文名称"].Value.ToString();
                if (txtcEngName == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["英文名称"]);
                }

                if (row.Cells["性別"] != null)
                {
                    string cbcSexNum = row.Cells["性別"].Value.ToString().Trim();
                    if (cbcSexNum == string.Empty)
                    {
                        SetCellBackColorEmpty(row.Cells["性別"]);
                    }
                }

                //string cbcIdentityNum = row.Cells["身份"].Value.ToString();
                //if (cbcIdentityNum == string.Empty)
                //{
                //    SetCellBackColorEmpty(row.Cells["身份"]);
                //}

                string cbcSchool = row.Cells["院系部"].Value.ToString();
                if (cbcSchool == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["院系部"]);
                }

                string cbcDepartment = row.Cells["科室"].Value.ToString();
                if (cbcDepartment == string.Empty)
                {
                    SetCellBackColorEmpty(row.Cells["科室"]);
                }




                //  string txtcSMSReceivePhone = row.Cells["短信接收电话"].Value.ToString();
                //  string txtcMailAddress = row.Cells["电邮地址"].Value.ToString();

                string cus_lIsSendSMS = row.Cells["短信通知"].Value.ToString();
                if (cus_lIsSendSMS == string.Empty)
                {
                    row.Cells["短信通知"].Value = "N";
                }
                string cus_lIsSendEmail = row.Cells["邮件通知"].Value.ToString();
                if (cus_lIsSendEmail == string.Empty)
                {
                    row.Cells["邮件通知"].Value = "N";
                }
            }



            //隐藏字段，保存下拉KEY
            gvwCardUser.Columns["sex"].Visible = false;
            gvwCardUser.Columns["identity"].Visible = false;
            gvwCardUser.Columns["school"].Visible = false;
            gvwCardUser.Columns["department"].Visible = false;
            gvwCardUser.Columns["inChargeCourse"].Visible = false;

            gvwCardUser.AutoResizeColumns();

        }

        private void SetCellBackColorEmpty(DataGridViewCell cell)
        {
            cell.Style.BackColor = Color.Orange;
        }

        private void SetCellBackColorError(DataGridViewCell cell)
        {
            cell.Style.BackColor = Color.Red;
        }

        private void SetCellBackColorOriginal(DataGridViewCell cell)
        {
            cell.Style.BackColor = Color.White;
        }

        private void btnExpEorData_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Excle文件(*.xls)|*.xls|所有文件(*.*)|*.*";

            string xlsSavePath = "";

            DialogResult a = saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {

                xlsSavePath = saveFileDialog1.FileName;


                ExcelUtil excelUtil = new ExcelUtil();
                try
                {
                    if (_cardUserIdentity == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
                    {
                        excelUtil.Open(Application.StartupPath + "/ExcelTemplate/student1.xls");
                        var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                        AddDataToStuSheet(excelUtil, 2, sexList);
                        var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                        AddDataToStuSheet(excelUtil, 3, schoolList);
                        var specialtyList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SpecialtyMaster);
                        AddDataToStuSheet(excelUtil, 4, specialtyList);
                        var classList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserClass);
                        AddDataToStuSheet(excelUtil, 5, classList);


                        excelUtil.AddValueToCell(1, 1, 2, lblSchool.Text);
                        excelUtil.AddValueToCell(1, 1, 4, lblSpecialty.Text);
                        excelUtil.AddValueToCell(1, 2, 2, lblGraduationPeriod.Text);
                        excelUtil.AddValueToCell(1, 2, 4, lblClass.Text);

                        int temp = 5;
                        foreach (DataGridViewRow row in gvwCardUser.Rows)
                        {
                            if (string.IsNullOrEmpty(row.ErrorText))
                            {
                                continue;
                            }
                            excelUtil.AddValueToCell(1, temp, 1, row.Cells["用户编号"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 2, row.Cells["中文名称"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 3, row.Cells["英文名称"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 4, row.Cells["性別"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 5, row.Cells["短信接收电话"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 6, row.Cells["电邮地址"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 7, row.Cells["短信通知"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 8, row.Cells["邮件通知"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 9, row.Cells["亲情号码1"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 10, row.Cells["亲情号码2"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 11, row.Cells["亲情号码3"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 12, row.Cells["亲情号码4"].Value.ToString());
                            temp++;
                        }



                    }
                    else if (_cardUserIdentity == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
                    {

                        excelUtil.Open(Application.StartupPath + "/ExcelTemplate/teacher1.xls");
                        var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                        AddDataToStuSheet(excelUtil, 2, sexList);
                        var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                        AddDataToStuSheet(excelUtil, 3, schoolList);
                        var departmentList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.DepartmentMaster);
                        AddDataToStuSheet(excelUtil, 4, departmentList);
                        var courseList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CourseMaster);
                        AddDataToStuSheet(excelUtil, 5, courseList);

                        int temp = 2;
                        foreach (DataGridViewRow row in gvwCardUser.Rows)
                        {
                            if (string.IsNullOrEmpty(row.ErrorText))
                            {
                                continue;
                            }
                            excelUtil.AddValueToCell(1, temp, 1, row.Cells["有效"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 2, row.Cells["用户编号"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 3, row.Cells["中文名称"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 4, row.Cells["英文名称"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 5, row.Cells["性別"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 6, row.Cells["院系部"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 7, row.Cells["科室"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 8, row.Cells["短信接收电话"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 9, row.Cells["电邮地址"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 10, row.Cells["短信通知"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 11, row.Cells["邮件通知"].Value.ToString());
                            excelUtil.AddValueToCell(1, temp, 12, row.Cells["负责课程"].Value.ToString());
                            temp++;
                        }




                    }

                    excelUtil.SaveCopyAs(xlsSavePath);
                    excelUtil.Close();
                    MessageBox.Show("导出成功", "系统提示", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    excelUtil.Close();
                    MessageBox.Show("导出错误，请联系管理员。错误原因：" + ex.Message, "系统提示", MessageBoxButtons.OK);
                }
            }


        }

        private void AddDataToStuSheet(ExcelUtil excelUtil, int sheetIndex, List<IModelObject> cb)
        {

            int temp = 0;
            foreach (ComboboxDataInfo item in cb)
            {
                temp++;
                excelUtil.AddValueToCell(sheetIndex, temp, 1, item.DisplayMember);
                excelUtil.AddValueToCell(sheetIndex, temp, 2, item.ValueMember);
            }

        }



    }
}
