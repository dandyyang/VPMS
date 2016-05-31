using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;
using Common;

namespace Model.IModel
{
    public interface IRecordEditStatusInfo:IModelObject
    {
        /// <summary>
        /// 記錄編輯狀態
        /// </summary>
        DefineConstantValue.EditStateEnum RecordEditStatus
        {
            get;
            set;
        }

    }
}
