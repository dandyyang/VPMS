using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 事务中所用到物料
    /// </summary>
    public class VSCTransactionMaterial_vctm_Info : ModelBase
    {
        public VSCTransactionMaterial_vctm_Info()
        {
            this.vctm_VSCTRecordID = Guid.Empty;
            this.vctm_cMTMNum = string.Empty;
            this.vctm_fQty = 0;
            this.vctm_cUnitNum = string.Empty;

        }

        /// <summary>
        /// 种植周期事务记录ID
        /// </summary>
        public Guid vctm_VSCTRecordID { set; get; }
        /// <summary>
        /// 物料編號
        /// </summary>
        public string vctm_cMTMNum { set; get; }
        /// <summary>
        /// 用量
        /// </summary>
        public decimal vctm_fQty { set; get; }
        /// <summary>
        /// 单位编号
        /// </summary>
        public string vctm_cUnitNum { set; get; }

    }
}
