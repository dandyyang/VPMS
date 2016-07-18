using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 种植周期事务
    /// </summary>
    public class VegetableSpeciesCycleTransaction_vsct : IModelObject
    {
        public VegetableSpeciesCycleTransaction_vsct()
        {
            this.vsct_RecordID = Guid.Empty;
            this.vsct_VSTTRecordID = Guid.Empty;
            this.vsct_cTransactionNum = string.Empty;
            this.vsct_lIsRemind = false;
            this.vsct_iAheadOfTimeRemind = 0;
            this.VSCTransactionMaterials = null;
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid vsct_RecordID { set; get; }
        /// <summary>
        /// 种植事务时间记录ID
        /// </summary>
        public Guid vsct_VSTTRecordID { set; get; }
        /// <summary>
        /// 事务名称编号
        /// </summary>
        public string vsct_cTransactionNum { set; get; }
        /// <summary>
        /// 是否提醒
        /// </summary>
        public bool vsct_lIsRemind { set; get; }
        /// <summary>
        /// 提前提醒时间（天）
        /// </summary>
        public int vsct_iAheadOfTimeRemind { set; get; }
        /// <summary>
        /// 事务中所用到物料列表
        /// </summary>
        public List<VSCTransactionMaterial_vctm> VSCTransactionMaterials { set; get; }


        #region IModelObject 成员

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
