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
using Model.IModel;
using Model.Management.Master;
using Model.General;
using System.IO;
using Common.FileMgtService;


namespace WindowUI.Management.Master
{
    public partial class ExportCardUserPhoto : BaseForm
    {
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;

        private List<ComboboxDataInfo> classes;
        private List<ComboboxDataInfo> deptes;
        private string folderPath;

        public ExportCardUserPhoto()
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


        private void BtnOK_Click(object sender, EventArgs e)
        {


            BtnOK.Enabled = false;
            btnCancel.Enabled = false;

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


            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult dialogRes = folderBrowserDialog1.ShowDialog(this);
            if (dialogRes == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                backgroundWorker1.RunWorkerAsync(param);

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Text = "";
            }
            else
            {
                toolStripProgressBar1.Visible = false;
                BtnOK.Enabled = true;
                btnCancel.Enabled = true;
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
            List<IModelObject> data = new List<IModelObject>();

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


                #endregion
            }
            else if (param["id"] == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
            {

                #region 教职人员资料


                if (folderPath != "")
                {


                    #region 查询数据
                    if (param["school"] != null)
                    {
                        info.cus_cSchoolNum = param["school"];
                    }
                    info.cus_cIdentityNum = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff;


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





                }
                #endregion
            }


            try
            {
                FileMgtSoapClient soap = WebSrvFactory.GetFileMgt();
                int i = 1;
                foreach (CardUserMaster_cus_Info cu in data)
                {
                    Pro_File pfile = soap.GetPro_File(cu.cus_guidPhotoKey);
                    if (pfile != null)
                    {
                        string file = folderBrowserDialog1.SelectedPath + "/" + cu.cus_cNumber + "." + pfile.pfl_cSuffix;
                        var bytes = soap.GetFileBytes(pfile.pfl_RecordID);
                        if (bytes != null)
                        {
                            File.WriteAllBytes(file, soap.GetFileBytes(pfile.pfl_RecordID));
                        }
                    }
                    backgroundWorker1.ReportProgress(i * 100 / data.Count, i.ToString() + "/" + data.Count.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("导出错误，请联系管理员。错误原因：" + ex.Message, "系统提示", MessageBoxButtons.OK);
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
            toolStripStatusLabel1.Text = "导出完成";
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
