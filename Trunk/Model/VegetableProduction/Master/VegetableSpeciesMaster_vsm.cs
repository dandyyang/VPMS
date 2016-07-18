using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 蔬菜品种主档
    /// </summary>
    public class VegetableSpeciesMaster_vsm : IModelObject
    {
        public VegetableSpeciesMaster_vsm()
        {
            this.vsm_RecordID = Guid.Empty;
            this.vsm_cNum = string.Empty;
            this.vsm_iSeq = 0;
            this.vsm_cName = string.Empty;
            this.vsm_cTypeNum = string.Empty;
            this.vsm_cByClassificationNum = string.Empty;
            this.vsm_iNurseryStage = 0;
            this.vsm_iGrowingPeriod = 0;
            this.vsm_iPickingPeriod = 0;
            this.vsm_iAcreYield = 0;
            this.vsm_iPlastochron = 0;
            this.vsm_cRemark = string.Empty;
            this.vsm_cAdd = string.Empty;
            this.vsm_cLast = string.Empty;

        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid vsm_RecordID { set; get; }
        /// <summary>
        /// 品种编号
        /// </summary>
        public string vsm_cNum { set; get; }
        /// <summary>
        /// 序号
        /// </summary>
        public int vsm_iSeq { set; get; }
        /// <summary>
        /// 品种名称
        /// </summary>
        public string vsm_cName { set; get; }
        /// <summary>
        /// 品种类别编号
        /// </summary>
        public string vsm_cTypeNum { set; get; }
        /// <summary>
        /// 品种科别编号
        /// </summary>
        public string vsm_cByClassificationNum { set; get; }
        /// <summary>
        /// 育苗期（天）
        /// </summary>
        public int vsm_iNurseryStage { set; get; }
        /// <summary>
        /// 生长期（天）
        /// </summary>
        public int vsm_iGrowingPeriod { set; get; }
        /// <summary>
        /// 采摘期（天）
        /// </summary>
        public int vsm_iPickingPeriod { set; get; }
        /// <summary>
        /// 亩产量（公斤）
        /// </summary>
        public int vsm_iAcreYield { set; get; }
        /// <summary>
        /// 种植间隔期（天）
        /// </summary>
        public int vsm_iPlastochron { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string vsm_cRemark { set; get; }
        /// <summary>
        /// 新增者
        /// </summary>
        public string vsm_cAdd { set; get; }
        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime? vsm_dAddDate { set; get; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string vsm_cLast { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? vsm_dLastDate { set; get; }



        #region IModelObject 成员

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
