using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DataTypeVerify
{
    /// <summary>
    /// 數據類型驗證結果信息
    /// </summary>
    public class DataTypeVerifyResultInfo
    {
        public DataTypeVerifyResultInfo()
        {
            this.IsMatch = false;
            this.Message = string.Empty;
        }

        /// <summary>
        /// 是否匹配
        /// </summary>
        public bool IsMatch
        {
            set;
            get;
        }

        /// <summary>
        /// 驗證結果信息
        /// </summary>
        public string Message
        {
            set;
            get;
        }
    }
}
