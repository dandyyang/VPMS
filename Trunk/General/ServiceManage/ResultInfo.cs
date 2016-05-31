using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceManage
{
    public class ResultInfo
    {
        public ResultInfo()
        {
            this.boolValue = false;
            this.ValueObject = null;
            this.messageText = string.Empty;
            this.isError = false;
        }

        /// <summary>
        /// 布爾值
        /// </summary>
        public bool boolValue
        {
            set;
            get;
        }
        /// <summary>
        /// 對象值
        /// </summary>
        public object ValueObject
        {
            set;
            get;
        }

        /// <summary>
        /// 信息文本
        /// </summary>
        public string messageText
        {
            set;
            get;
        }

        /// <summary>
        /// 布爾值
        /// </summary>
        public bool isError
        {
            set;
            get;
        }
    }
}
