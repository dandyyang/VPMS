using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.Util;
using BLL.Factory.General;
using BLL.IBLL.General;
using Model.General;
using Model.IModel;
using WindowUI.ClassLibrary.Public;
using System.IO;
using System.Threading;


namespace WindowUI.Management.Master
{
    public partial class SelectExpTpt : BaseForm
    {

        IGeneralBL _generalBL;
        private Thread invokeDialogThread;
        private DialogResult result;

        public SelectExpTpt()
        {
            InitializeComponent();
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);

            cbcIdentityNum.SetDataSource(_generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserIdentity));
        }




        private void btnOK_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excle文件(*.xls)|*.xls|所有文件(*.*)|*.*";

            string xlsSavePath = "";

            if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
            {
                saveFileDialog1.FileName = "学生模板";
            }
            else if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
            {
                saveFileDialog1.FileName = "教职人员模板";
            }

            

            ///開啟保存對話框線程

            invokeDialogThread = new Thread(new ThreadStart(InvokeMethod));
            invokeDialogThread.SetApartmentState(ApartmentState.STA);
            invokeDialogThread.Start();
            invokeDialogThread.Join();

            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                xlsSavePath = saveFileDialog1.FileName;
                progressBar1.Visible = true;
                // ExportTemplate(xlsSavePath);
                backgroundWorker1.RunWorkerAsync(xlsSavePath);
            }
            else
            {
                if (result == DialogResult.OK)
                {
                    DialogResult resutl = MessageBox.Show("请填写文件名称。", "系统提示", MessageBoxButtons.OKCancel);
                    if (resutl == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
            }

        }
        private void InvokeMethod()
        {
            result = saveFileDialog1.ShowDialog();
        }
        private void ExportTemplate(string xlsSavePath)
        {
            ExcelUtil excelUtil = null;
            try
            {
                excelUtil = new ExcelUtil();

                if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
                {
                    excelUtil.Open(Application.StartupPath + "/ExcelTemplate/student1.xls");
                    backgroundWorker1.ReportProgress(10);
                    var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                    AddDataToStuSheet(excelUtil, 2, sexList);
                    backgroundWorker1.ReportProgress(30);
                    var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                    AddDataToStuSheet(excelUtil, 3, schoolList);
                    backgroundWorker1.ReportProgress(50);
                    var specialtyList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SpecialtyMaster);
                    AddDataToStuSheet(excelUtil, 4, specialtyList);
                    backgroundWorker1.ReportProgress(70);
                    var classList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserClass);
                    AddDataToStuSheet(excelUtil, 5, classList);
                    backgroundWorker1.ReportProgress(75);

                    var buildSiteList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.BuildingSiteMaster);
                    AddDataToStuSheet(excelUtil, 7, buildSiteList);
                    backgroundWorker1.ReportProgress(85);

                    var goToSchoolTypeList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.GoToSchoolType);
                    AddDataToStuSheet(excelUtil, 8, goToSchoolTypeList);

                    backgroundWorker1.ReportProgress(90);
                }
                else if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
                {

                    excelUtil.Open(Application.StartupPath + "/ExcelTemplate/teacher1.xls");
                    backgroundWorker1.ReportProgress(10);
                    var sexList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CardUserSex);
                    AddDataToStuSheet(excelUtil, 2, sexList);
                    backgroundWorker1.ReportProgress(30);
                    var schoolList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.SchoolMaster);
                    AddDataToStuSheet(excelUtil, 3, schoolList);
                    backgroundWorker1.ReportProgress(50);
                    var deptList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.DepartmentMaster);
                    AddDataToStuSheet(excelUtil, 4, deptList);
                    backgroundWorker1.ReportProgress(70);
                    var courseList = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.CourseMaster);
                    AddDataToStuSheet(excelUtil, 5, courseList);
                    backgroundWorker1.ReportProgress(90);
                }

                excelUtil.SaveCopyAs(xlsSavePath);
                excelUtil.Close();
                backgroundWorker1.ReportProgress(100);
                ShowInformationMessage("导出成功。");
                this.Close();
            }
            catch (Exception ex)
            {
                ShowInformationMessage("导出错误，请联系管理员。错误原因：" + ex.Message);
          
            }
            finally
            {
                //add by justinleung 2011/09/06
                if (excelUtil != null)
                {
                    excelUtil.Close();
                }
                this.Close();
            }

        }

        private void AddDataToSheet(ExcelUtil excelUtil, int sheetIndex, string startCell, List<IModelObject> cb)
        {
            int temp = 0;
            //  StringBuilder dataBuilder = new StringBuilder();
            // string data = "";

            foreach (ComboboxDataInfo item in cb)
            {
                temp++;
                excelUtil.AddValueToCell(sheetIndex, temp, 1, item.DisplayMember);
                excelUtil.AddValueToCell(sheetIndex, temp, 2, item.ValueMember);
                // dataBuilder.Append(item.DisplayMember).Append(",");
            }
            //  Microsoft.Office.Interop.Excel.Range rang = excelUtil.GetRange(startCell, startCell).get_Resize(10000, 1);
            //  data = dataBuilder.ToString();
            //  excelUtil.AddValidation(rang, data.Substring(0, data.Length - 1));
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(5);
            ExportTemplate(e.Argument.ToString());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
