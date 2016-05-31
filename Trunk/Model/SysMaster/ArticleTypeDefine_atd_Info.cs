using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SysMaster
{
    public class ArticleTypeDefine_atd_Info : Model.IModel.IModelObject
    {
        public ArticleTypeDefine_atd_Info()
        {
            this.RecordID = 0;

            this.atd_iRecordID = 0;

            this.atd_cTypeNum = string.Empty;

            this.atd_cParentRecordNum = string.Empty;

            this.atd_cDescript = string.Empty;

            this.atd_lValid = false;

            this.atd_cSysNum = string.Empty;

        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int atd_iRecordID { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public string atd_cTypeNum { get; set; }

        /// <summary>
        /// 父节点记录编号
        /// </summary>
        public string atd_cParentRecordNum { get; set; }

        /// <summary>
        /// 节点描述
        /// </summary>
        public string atd_cDescript { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool atd_lValid { get; set; }

        /// <summary>
        /// 是否含有子项
        /// </summary>
        public bool hasChild { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string atd_cSysNum { get; set; }


        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
