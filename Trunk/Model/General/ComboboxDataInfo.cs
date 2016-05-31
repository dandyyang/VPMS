using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.General
{
    /// <summary>
    /// Combobox数据信息
    /// </summary>
    public class ComboboxDataInfo:IModelObject
    {
        public ComboboxDataInfo()
        {
            this.ValueMember = string.Empty;
            this.DisplayMember = string.Empty;
        }
        
        public string DisplayMember
        {
            get;
            set;
        }
        public string ValueMember
        {
            get;
            set;
        }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }

    public enum ComboboxDataInfoEnum
    {
        DisplayMember,
        ValueMember
    }
}
