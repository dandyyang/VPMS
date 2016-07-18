using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 蔬菜品种预计产量
    /// </summary>
    public class VegetableSpeciesPerdictYield_vspy : IModelObject
    {
        public VegetableSpeciesPerdictYield_vspy()
        {
            this.vsmc_RecordID = Guid.Empty;
            this.vsmc_VSMRevordID = Guid.Empty;
            this.vspy_iBeginDays = 0;
            this.vspy_iEndDays = 0;
            this.vspy_iYieldQty = 0;
            this.vspy_cRemark = string.Empty;
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
        /// 开始天数
        /// </summary>
        public int vspy_iBeginDays { set; get; }
        /// <summary>
        /// 结束天数
        /// </summary>
        public int vspy_iEndDays { set; get; }
        /// <summary>
        /// 预计产量（公斤）
        /// </summary>
        public int vspy_iYieldQty { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string vspy_cRemark { set; get; }

        #region IModelObject 成员

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
