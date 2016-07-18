using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model
{
    public class ModelBase : IModelObject
    {
        public ModelBase()
        {
            this.RecordEditStatus = 0;
        }

        /// <summary>
        /// 记录编辑状态（Insert=1,Update=2,Delete=4）
        /// </summary>
        public int RecordEditStatus
        {
            set;
            get;
        }

        #region IModelObject 成员

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
