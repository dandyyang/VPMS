using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowUI.ClassLibrary.Public;
using BLL.Factory.Management;
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using BLL.IBLL.General;
using Common;
using Common.Util;
using Model.IModel;
using Model.Management.Master;
using Model.General;
using System.Threading;


namespace WindowUI.Management.Master
{
    public partial class ExportCumData : BaseForm
    {
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;

        private List<ComboboxDataInfo> classes;
        private List<ComboboxDataInfo> deptes;

        private string exportFileName;

        public ExportCumData()
        {
            InitializeComponent();

            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
        }

        private void ExportCumData_Load(object sender, EventArgs e)
        {
            //BindCombox(DefineConstantValue.MasterType.CardUserSex);
            BindCombox(DefineConstantValue.MasterType.CardUserIdentity);
            BindCombox(DefineConstantValue.MasterType.SchoolMaster);
            BindCombox(DefineConstantValue.MasterType.SpecialtyMaster);
            BindCombox(DefineConstantValue.MasterType.CardUserClass);
            BindCombox(DefineConstantValue.MasterType.DepartmentMaster);


        }

        private void BindCombox(DefineConstantValue.MasterType mType)
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                result = _generalBL.GetMasterDataInformations(mType);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            switch (mType)
            {

                case DefineConstantValue.MasterType.SchoolMaster:
                    cbcSchool.SetDataSource(result);
                    break;
                case DefineConstantValue.MasterType.CardUserIdentity:
                    cbcIdentityNum.SetDataSource(result);
                    cbcIdentityNum.Text = "";

                    break;
                case DefineConstantValue.MasterType.SpecialtyMaster:
                    cbcSpecialty.SetDataSource(result);
                    cbcSpecialty.Text = "";
                    break;
                case DefineConstantValue.MasterType.CardUserClass:
                    lstClass.SetDataSource(result);
                    lstClass.SelectedItems.Clear();
                    break;
                case DefineConstantValue.MasterType.DepartmentMaster:
                    lstDept.SetDataSource(result);
                    lstDept.SelectedItems.Clear();
                    break;
                default:
                    break;
            }
        }
        private SaveFileDialog saveFileDialog1;
        private Thread invokeDialogThread;
        private DialogResult result;
        private void InvokeMethod()
        {
            result = saveFileDialog1.ShowDialog();
        }
        private void BtnOK_Click(object sender, EventArgs e)
        {
            BtnOK.Enabled = false;
            btnCancel.Enabled = false;

            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Excle文件(*.xls)|*.xls|所有文件(*.*)|*.*";


            if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
            {
                saveFileDialog1.FileName += "学生资料(" + dtpGraduationPeriod.Text + cbcSpecialty.Text;
                if (lstClass.SelectedItems.Count == 1)
                {
                    saveFileDialog1.FileName += (lstClass.SelectedItem as ComboboxDataInfo).DisplayMember;
                }
                saveFileDialog1.FileName += ")";
            }
            else
            {
                saveFileDialog1.FileName += "教职人员资料(" + cbcSchool.Text;
                if (lstClass.SelectedItems.Count == 1)
                {
                    saveFileDialog1.FileName += (lstDept.SelectedItem as ComboboxDataInfo).DisplayMember;
                }
                saveFileDialog1.FileName += ")";
            }

            ///開啟保存對話框線程

            invokeDialogThread = new Thread(new ThreadStart(InvokeMethod));
            invokeDialogThread.SetApartmentState(ApartmentState.STA);
            invokeDialogThread.Start();
            invokeDialogThread.Join();

            if (result == DialogResult.OK)
            {
                exportFileName = saveFileDialog1.FileName;


                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Text = "";
            }
            else
            {

                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel1.Text = "";

                BtnOK.Enabled = true;
                btnCancel.Enabled = true;
            }


            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", cbcIdentityNum.SelectedValue.ToString());
            param.Add("school", cbcSchool.SelectedValue.ToString());
            param.Add("specialty", cbcSpecialty.SelectedValue.ToString());
            param.Add("graduationPeriod", dtpGraduationPeriod.Text);
            param.Add("specialtyText", ((ComboboxDataInfo)cbcSpecialty.SelectedItem).DisplayMember);
            param.Add("schoolText", cbcSchool.Text);

            if (lstClass.Enabled)
            {
                classes = new List<ComboboxDataInfo>();
                foreach (ComboboxDataInfo item in lstClass.SelectedItems)
                {
                    classes.Add(item);
                }
            }
            if (lstDept.Enabled)
            {
                deptes = new List<ComboboxDataInfo>();
                foreach (ComboboxDataInfo item in lstDept.SelectedItems)
                {
                    deptes.Add(item);
                }
            }

            backgroundWorker1.RunWorkerAsync(param);


        }

        private void AddDataToSheet(ExcelUtil excelUtil, int sheetIndex, List<IModelObject> cb)
        {
            int temp = 0;

            foreach (ComboboxDataInfo item in cb)
            {
                temp++;
                excelUtil.AddValueToCell(sheetIndex, temp, 1, item.DisplayMember);
                excelUtil.AddValueToCell(sheetIndex, temp, 2, item.ValueMember);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbcIdentityNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
            {

                cbcSpecialty.Enabled = false;
                dtpGraduationPeriod.Enabled = false;
                lstClass.Enabled = false;

                lstDept.Enabled = true;


            }
            else
            {
                cbcSpecialty.Enabled = true;
                dtpGraduationPeriod.Enabled = true;
                lstClass.Enabled = true;

                lstDept.Enabled = false;
            }

            lstClass.SelectedItems.Clear();
            lstDept.SelectedItems.Clear();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Dictionary<string, string> param = e.Argument as Dictionary<string, string>;

            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();


            if (param["id"] == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
            {
                #region 导出学生


                #region 查询数据
                if (param["school"] != null)
                {
                    info.cus_cSchoolNum = param["school"];
                }
                if (param["specialty"] != null)
                {
                    info.cus_cSpecialtyNum = param["specialty"];
                }

                info.cus_cGraduationPeriod = dtpGraduationPeriod.Value.Year.ToString();
                info.cus_cIdentityNum = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student;
                List<IModelObject> data;
                if (classes.Count == 1)
                {
                    info.cus_cClassNum = classes[0].ValueMember;
                    data = _cardUserMasterBL.SearchRecords(info);

                }
                else
                {
                    data = new List<IModelObject>();
                    foreach (var c in classes)
                    {
                        info.cus_cClassNum = c.ValueMember;
                        data.AddRange(_cardUserMasterBL.SearchRecords(info));
                    }


                }
                #endregion



                if (exportFileName != "")
                {

                    ExcelUtil excelUtil = new ExcelUtil();
                    try
                    {
                        string template;
                        if (classes.Count == 1)
                        {
                            template = "/ExcelTemplate/student1.xls";
                        }
                        else//多個班，增加一列
                        {
                            template = "/ExcelTemplate/student.xls";
                        }

                        excelUtil.Open(Application.StartupPath + template);
                        var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                        AddDataToSheet(excelUtil, 2, sexList);

                        var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                        AddDataToSheet(excelUtil, 3, schoolList);

                        var specialtyList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SpecialtyMaster);
                        AddDataToSheet(excelUtil, 4, specialtyList);

                        var classList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserClass);
                        AddDataToSheet(excelUtil, 5, classList);

                        var buildSiteList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.BuildingSiteMaster);
                        AddDataToSheet(excelUtil, 7, buildSiteList);


                        excelUtil.AddValueToCell(1, 1, 2, param["schoolText"]);
                        excelUtil.AddValueToCell(1, 1, 4, param["specialtyText"]);
                        excelUtil.AddValueToCell(1, 2, 2, param["graduationPeriod"]);
                        if (classes.Count == 1)
                        {
                            excelUtil.AddValueToCell(1, 2, 4, classes[0].DisplayMember);
                        }
                        else
                        {
                            excelUtil.AddValueToCell(1, 2, 4, "");
                        }

                        int temp = 5;
                        foreach (CardUserMaster_cus_Info cusInfo in data)
                        {
                            var state = ((temp - 4) + "/" + data.Count);
                            var percent = (temp - 4) * 100 / data.Count;
                            backgroundWorker1.ReportProgress(percent, state.ToString());
                            excelUtil.AddValueToCell(1, temp, 1, cusInfo.cus_cNumber);
                            excelUtil.AddValueToCell(1, temp, 2, cusInfo.cus_cStudentId);
                            excelUtil.AddValueToCell(1, temp, 3, cusInfo.cus_cChaName);
                            excelUtil.AddValueToCell(1, temp, 4, cusInfo.cus_cEngName);
                            excelUtil.AddValueToCell(1, temp, 5, cusInfo.cus_cSexNum);
                            excelUtil.AddValueToCell(1, temp, 6, cusInfo.cus_lIsSendSMS ? "Y" : "N");
                            excelUtil.AddValueToCell(1, temp, 7, cusInfo.cus_cSMSReceivePhone);
                            excelUtil.AddValueToCell(1, temp, 8, cusInfo.cus_cAppendPhone1);
                            excelUtil.AddValueToCell(1, temp, 9, cusInfo.cus_cAppendPhone2);
                            excelUtil.AddValueToCell(1, temp, 10, cusInfo.cus_cAppendPhone3);
                            excelUtil.AddValueToCell(1, temp, 11, cusInfo.cus_lIsSendEmail ? "Y" : "N");
                            excelUtil.AddValueToCell(1, temp, 12, cusInfo.cus_cMailAddress);
                            excelUtil.AddValueToCell(1, temp, 13, cusInfo.cus_cGotoSchoolType);


                            int rowIndex = 14;
                            if (classes.Count > 1)
                            {
                                excelUtil.AddValueToCell(1, temp, rowIndex, cusInfo.cus_cClassNum);//多個班時，顯示班

                                rowIndex++;//推后一列 ，宿舍地點

                                excelUtil.AddValueToCell(1, temp, rowIndex, cusInfo.cus_cDormitorySiteName);
                            }
                            else
                            {
                                excelUtil.AddValueToCell(1, temp, rowIndex, cusInfo.cus_cDormitorySiteName);
                            }
                            rowIndex++;//推后一列，床位
                            excelUtil.AddValueToCell(1, temp, rowIndex, cusInfo.cus_cBedNum);
                            rowIndex++;
                            excelUtil.AddValueToCell(1, temp, rowIndex, (cusInfo.cus_lCashPay == true ? "允许" : "不允许"));

                            if (cusInfo.CardUserPhoneNum != null)
                            {
                                excelUtil.AddValueToCell(1, temp, ++rowIndex, (cusInfo.CardUserPhoneNum.cup_Phone1 != null ? cusInfo.CardUserPhoneNum.cup_Phone1 : ""));
                                excelUtil.AddValueToCell(1, temp, ++rowIndex, (cusInfo.CardUserPhoneNum.cup_Phone2 != null ? cusInfo.CardUserPhoneNum.cup_Phone2 : ""));
                                excelUtil.AddValueToCell(1, temp, ++rowIndex, (cusInfo.CardUserPhoneNum.cup_Phone3 != null ? cusInfo.CardUserPhoneNum.cup_Phone3 : ""));
                                excelUtil.AddValueToCell(1, temp, ++rowIndex, (cusInfo.CardUserPhoneNum.cup_Phone4 != null ? cusInfo.CardUserPhoneNum.cup_Phone4 : ""));
                            }
                            temp++;
                        }
                        excelUtil.SaveCopyAs(exportFileName);
                        excelUtil.Close();
                        backgroundWorker1.ReportProgress(100, "导出成功");

                    }
                    catch (Exception ex)
                    {
                        excelUtil.Close();
                        MessageBox.Show("导出错误，请联系管理员。错误原因：" + ex.Message, "系统提示", MessageBoxButtons.OK);
                    }
                }

                #endregion
            }
            else if (param["id"] == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
            {

                #region 教职人员资料

                if (exportFileName != "")
                {

                    ExcelUtil excelUtil = null;
                    try
                    {
                        excelUtil = new ExcelUtil();
                        excelUtil.Open(Application.StartupPath + "/ExcelTemplate/teacher1.xls");


                        var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                        AddDataToSheet(excelUtil, 2, sexList);
                        var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                        AddDataToSheet(excelUtil, 3, schoolList);
                        var departmentList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.DepartmentMaster);
                        AddDataToSheet(excelUtil, 4, departmentList);
                        var courseList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CourseMaster);
                        AddDataToSheet(excelUtil, 5, courseList);



                        #region 查询数据
                        if (param["school"] != null)
                        {
                            info.cus_cSchoolNum = param["school"];
                        }
                        info.cus_cIdentityNum = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff;

                        List<IModelObject> data;
                        if (deptes.Count == 1)
                        {
                            info.cus_cDepartmentNum = deptes[0].ValueMember;
                            data = _cardUserMasterBL.SearchRecords(info);
                        }
                        else
                        {
                            data = new List<IModelObject>();
                            foreach (var c in deptes)
                            {
                                info.cus_cDepartmentNum = c.ValueMember;
                                data.AddRange(_cardUserMasterBL.SearchRecords(info));
                            }
                        }
                        #endregion

                        int temp = 2;
                        foreach (CardUserMaster_cus_Info cusInfo in data)
                        {
                            var state = (temp - 1) + "/" + data.Count;
                            var percent = (temp - 1) * 100 / data.Count;
                            backgroundWorker1.ReportProgress(percent, state.ToString());

                            excelUtil.AddValueToCell(1, temp, 1, cusInfo.cus_lValid ? "Y" : "N");
                            excelUtil.AddValueToCell(1, temp, 2, cusInfo.cus_cNumber);
                            excelUtil.AddValueToCell(1, temp, 3, cusInfo.cus_cChaName);
                            excelUtil.AddValueToCell(1, temp, 4, cusInfo.cus_cEngName);
                            excelUtil.AddValueToCell(1, temp, 5, cusInfo.cus_cSexNum);
                            excelUtil.AddValueToCell(1, temp, 6, cusInfo.cus_cSchoolNum);
                            excelUtil.AddValueToCell(1, temp, 7, cusInfo.cus_cDepartmentNum);
                            excelUtil.AddValueToCell(1, temp, 8, cusInfo.cus_cSMSReceivePhone);
                            excelUtil.AddValueToCell(1, temp, 9, cusInfo.cus_cMailAddress);
                            excelUtil.AddValueToCell(1, temp, 10, cusInfo.cus_lIsSendSMS ? "Y" : "N");
                            excelUtil.AddValueToCell(1, temp, 11, cusInfo.cus_lIsSendEmail ? "Y" : "N");
                            excelUtil.AddValueToCell(1, temp, 12, "");

                            if (cusInfo.CardUserPhoneNum != null)
                            {
                                excelUtil.AddValueToCell(1, temp, 13, (cusInfo.CardUserPhoneNum.cup_Phone1 != null ? cusInfo.CardUserPhoneNum.cup_Phone1 : ""));
                                excelUtil.AddValueToCell(1, temp, 14, (cusInfo.CardUserPhoneNum.cup_Phone2 != null ? cusInfo.CardUserPhoneNum.cup_Phone2 : ""));
                                excelUtil.AddValueToCell(1, temp, 15, (cusInfo.CardUserPhoneNum.cup_Phone3 != null ? cusInfo.CardUserPhoneNum.cup_Phone3 : ""));
                                excelUtil.AddValueToCell(1, temp, 16, (cusInfo.CardUserPhoneNum.cup_Phone4 != null ? cusInfo.CardUserPhoneNum.cup_Phone4 : ""));
                            }

                            temp++;
                        }

                        excelUtil.SaveCopyAs(exportFileName);
                        excelUtil.Close();
                        backgroundWorker1.ReportProgress(100, "导出成功");
                    }
                    catch (Exception ex)
                    {
                        if (excelUtil != null)
                        {
                            excelUtil.Close();
                        }
                        MessageBox.Show("导出错误，请联系管理员。错误原因：" + ex.Message, "系统提示", MessageBoxButtons.OK);
                    }

                }
                #endregion
            }
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            toolStripProgressBar1.Value = e.ProgressPercentage;

            toolStripStatusLabel1.Text = e.UserState.ToString();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //// statusStrip1.Visible = false;
            toolStripProgressBar1.Visible = false;
            BtnOK.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void cbcSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstClass.SelectedItems.Clear();
            lstDept.SelectedItems.Clear();
        }

        private void cbcSpecialty_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstClass.SelectedItems.Clear();
            lstDept.SelectedItems.Clear();
        }
    }
}
