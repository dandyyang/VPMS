using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 种植事务时间
    /// </summary>
    public class VegetableSpeciesTransactionTime_vstt : IModelObject
    {
        public VegetableSpeciesTransactionTime_vstt()
        {
            this.vstt_RecordID = Guid.Empty;
            this.vstt_VSMRecordID = Guid.Empty;
            this.vstt_cPlantingStatusNum = string.Empty;
            this.vstt_iCycleDatetime = 0;
            this.vstt_cRemark = string.Empty;
            this.VegetableSpeciesCycleTransactions = null;
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid vstt_RecordID { set; get; }

        /// <summary>
        /// 蔬菜品种记录ID
        /// </summary>
        public Guid vstt_VSMRecordID { set; get; }

        /// <summary>
        /// 定植状态编号
        /// </summary>
        public string vstt_cPlantingStatusNum { set; get; }
        /// <summary>
        /// 周期天数
        /// </summary>
        public int vstt_iCycleDatetime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string vstt_cRemark { set; get; }
        /// <summary>
        /// 种植周期事务列表
        /// </summary>
        public List<VegetableSpeciesCycleTransaction_vsct> VegetableSpeciesCycleTransactions { set; get; }


        #region IModelObject 成员

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
