using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 蔬菜品种种植成本
    /// </summary>
    public class VegetableSpeciesMasterCost_vsmc_Info : ModelBase
    {
        public VegetableSpeciesMasterCost_vsmc_Info()
        {
            this.vsmc_RecordID = Guid.Empty;
            this.vsmc_VSMRevordID = Guid.Empty;
            this.vsmc_cCostTypeNum = string.Empty;
            this.vsmc_fQty = 0;
            this.vsmc_fUnitPrice = 0;
            this.vsmc_cMTMNum = string.Empty;
            this.vsmc_cUnitNum = string.Empty;

        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid vsmc_RecordID { set; get; }
        /// <summary>
        /// 蔬菜品种记录ID
        /// </summary>
        public Guid vsmc_VSMRevordID { set; get; }
        /// <summary>
        /// 成本类型编号
        /// </summary>
        public string vsmc_cCostTypeNum { set; get; }
        /// <summary>
        /// 用量
        /// </summary>
        public decimal vsmc_fQty { set; get; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal vsmc_fUnitPrice { set; get; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string vsmc_cMTMNum { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string vsmc_cUnitNum { set; get; }


        #region IModelObject 成员

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
