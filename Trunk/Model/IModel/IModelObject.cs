using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.IModel
{
    public interface  IModelObject
    {

        /// <summary>
        /// 对象ID
        /// </summary>
        int  RecordID
        {
            get;
            set;
        }
    }
}
