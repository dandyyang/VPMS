using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entity
{
    public class ReturnValueInfo
    {
        public ReturnValueInfo()
        {
            boolValue = false;
            isError = false;
        }

        /// <summary>
        /// 布尔值
        /// </summary>
        public bool boolValue
        {
            set;
            get;
        }

        /// <summary>
        /// 对象值
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
        /// 判断外布尔值
        /// </summary>
        public bool isError
        {
            set;
            get;
        }
    }
}
