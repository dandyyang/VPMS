using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.VegetableProduction.Master
{
    /// <summary>
    /// 适种月份表
    /// </summary>
    public class VegetableSpeciesSuitPlantTime_vspt : IModelObject
    {
        public VegetableSpeciesSuitPlantTime_vspt()
        {
            this.vsmc_RecordID = Guid.Empty;
            this.vsmc_VSMRevordID = Guid.Empty;
            this.vspt_iMonth = string.Empty;
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
        /// 月份
        /// </summary>
        public string vspt_iMonth { set; get; }

        #region IModelObject 成员

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
