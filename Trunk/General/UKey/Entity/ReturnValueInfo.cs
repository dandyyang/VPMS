using System;
using System.Collections.Generic;
using System.Text;

namespace UKey.Entity
{
    public class ReturnValueInfo
    {
        public ReturnValueInfo()
        {
            this.IsSuccess = false;
            this.MessageText = string.Empty;
            this.Object = null;
        }

        public bool IsSuccess
        {
            set;
            get;
        }

        public object Object
        {
            set;
            get;
        }

        public string MessageText
        {
            set;
            get;
        }
    }
}
