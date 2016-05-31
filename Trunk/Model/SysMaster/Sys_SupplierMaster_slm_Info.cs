using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SysMaster
{
    public class Sys_SupplierMaster_slm_Info:Model.IModel.IModelObject
    {
        public Sys_SupplierMaster_slm_Info() 
        {
            this.RecordID = 0;

            this.slm_iRecordID = 0;

            this.slm_cClientNum = string.Empty;

            this.slm_cChinaName = string.Empty;

            this.slm_cEnglishName = string.Empty;

            this.slm_cTaxNumber = string.Empty;

            this.slm_cLinkman = string.Empty;

            this.slm_cAddress = string.Empty;

            this.slm_cPhone = string.Empty;

            this.slm_cFax = string.Empty;

            this.slm_cWebSite = string.Empty;

            this.slm_cRemark = string.Empty;

            this.slm_cAdd = string.Empty;

            this.slm_cLast = string.Empty;

            this.slm_cClientNumLength = 0;

            this.slm_cChinaNameLength = 0;

            this.slm_cEnglishNameLength = 0;

            this.slm_cTaxNumberLength = 0;

            this.slm_cLinkmanLength = 0;

            this.slm_cAddressLength = 0;

            this.slm_cPhoneLength = 0;

            this.slm_cFaxLength = 0;

            this.slm_cWebSiteLength = 0;

            this.slm_cRemarkLength = 0;
        }

        #region 数据库字段
        /// <summary>
        /// 记录ID
        /// </summary>
        public int slm_iRecordID { get; set; }

        /// <summary>
        /// 编号 
        /// </summary>
        public string slm_cClientNum { get; set; }

        /// <summary>
        /// 编号长度
        /// </summary>
        public int slm_cClientNumLength { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string slm_cChinaName { get; set; }

        /// <summary>
        /// 名称长度
        /// </summary>
        public int slm_cChinaNameLength { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string slm_cEnglishName { get; set; }

        /// <summary>
        /// 英文名称长度
        /// </summary>
        public int slm_cEnglishNameLength { get; set; }

        /// <summary>
        /// 企业税号
        /// </summary>
        public string slm_cTaxNumber { get; set; }

        /// <summary>
        /// 企业税号长度
        /// </summary>
        public int slm_cTaxNumberLength { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string slm_cLinkman { get; set; }

        /// <summary>
        /// 联系人长度
        /// </summary>
        public int slm_cLinkmanLength { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string slm_cAddress { get; set; }

        /// <summary>
        /// 地址长度
        /// </summary>
        public int slm_cAddressLength { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string slm_cPhone { get; set; }

        /// <summary>
        /// 电话长度
        /// </summary>
        public int slm_cPhoneLength { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string slm_cFax { get; set; }

        /// <summary>
        /// 传真长度
        /// </summary>
        public int slm_cFaxLength { get; set; }

        /// <summary>
        /// 网站
        /// </summary>
        public string slm_cWebSite { get; set; }

        /// <summary>
        /// 网站长度
        /// </summary>
        public int slm_cWebSiteLength{get;set;}

        /// <summary>
        /// 备注 
        /// </summary>
        public string slm_cRemark { get; set; }

        /// <summary>
        /// 备注长度
        /// </summary>
        public int slm_cRemarkLength { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string slm_cAdd { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime slm_dAddDate { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string slm_cLast { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime slm_dLastDate { get; set; } 
        #endregion

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
