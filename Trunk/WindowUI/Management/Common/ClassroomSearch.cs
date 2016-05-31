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
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using WindowUI.ClassLibrary.Public;
using Common;
using BLL.IBLL.General;
using Model.General;

namespace WindowUI.Management.Common
{
    /// <summary>
    /// 课室搜寻表单
    /// </summary>
    public partial class ClassroomSearch : BaseForm
    {
        ISiteMasterBL _siteBL;
        IGeneralBL _generalBL;
        SiteMaster_stm_Info _selectItemInfo;

        enum LVWColumnDefine
        {
            /// <summary>
            /// 地點編號
            /// </summary>
            stm_cNumber,
            /// <summary>
            /// 建築物名稱

            /// </summary>
            BuildingName,
            /// <summary>
            /// 地點名稱
            /// </summary>
            stm_cName,
            /// <summary>
            /// 備注
            /// </summary>
            stm_cRemark
        }

        public ClassroomSearch()
        {
            InitializeComponent();

            this._siteBL = MasterBLLFactory.GetBLL<ISiteMasterBL>(MasterBLLFactory.SiteMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._selectItemInfo = null;

            try
            {
                InitData();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        public ClassroomSearch(string formText)
            : this()
        {
            this.Text = formText;
        }


        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="selectItemInfo">选择项目数据的信息</param>
        public void ShowForm(SiteMaster_stm_Info selectItemInfo)
        {
            if (selectItemInfo == null)
            {
                ShowWarningMessage("参数不可以为NULL!");
                return;
            }
            this._selectItemInfo = selectItemInfo;
            this.ShowDialog();
        }

        private void InitData()
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                result = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.BuildingMaster);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            this.cboBuilding.SetDataSource(result, 0);
        }

        private void SelectItem()
        {
            if (this.lvwMstr.SelectedItems.Count > 0)
            {
                //this._selectItemInfo = new SiteMaster_stm_Info();

                this._selectItemInfo.stm_cNumber = this.lvwMstr.SelectedItems[0].SubItems[(int)LVWColumnDefine.stm_cNumber].Text.Trim();
                this._selectItemInfo.BuildingName = this.lvwMstr.SelectedItems[0].SubItems[(int)LVWColumnDefine.BuildingName].Text.Trim();
                this._selectItemInfo.stm_cName = this.lvwMstr.SelectedItems[0].SubItems[(int)LVWColumnDefine.stm_cName].Text.Trim();
                this._selectItemInfo.stm_cRemark = this.lvwMstr.SelectedItems[0].SubItems[(int)LVWColumnDefine.stm_cRemark].Text.Trim();

                this.Close();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SiteMaster_stm_Info info = new SiteMaster_stm_Info();

            if (this.cboBuilding.SelectedValue != null)
            {
                info.stm_cBuildingNumber = this.cboBuilding.SelectedValue.ToString().Trim();
            }
            info.stm_cName = this.txtcClassroomName.Text;
            info.stm_cNumber = this.txtcNum.Text;

            List<IModelObject> iInfoList = null;
            try
            {
                iInfoList = this._siteBL.SearchRecords(info);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            List<SiteMaster_stm_Info> infoList = null;
            if (iInfoList != null)
            {
                infoList = new List<SiteMaster_stm_Info>();
                foreach (SiteMaster_stm_Info sInfo in iInfoList)
                {
                    infoList.Add(sInfo);
                }
            }

            this.lvwMstr.SetDataSource<SiteMaster_stm_Info>(infoList);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectItem();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMstr.SelectedItems.Count > 0)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false; ;
            }
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            SelectItem();
        }
    }
}
