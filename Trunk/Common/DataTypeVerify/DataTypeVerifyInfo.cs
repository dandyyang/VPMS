using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Common.DataTypeVerify
{
    public class DataTypeVerifyInfo
    {
        public DataTypeVerifyInfo()
		{
            this.Text = string.Empty;
		}
		/// <summary>
        /// 要驗證的內容
		/// </summary>
		public string Text
		{
            get;
            set;
		}
		/// <summary>
        /// 要驗證的數據類型
		/// </summary>
        public DataType Type
        {
            get;
            set;
        }
    }

    /// <summary>
    ///數據驗證類型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 中文
        /// </summary>
        ChinaChar,
        /// <summary>
        /// 數字
        /// </summary>
        NumberChar,
        /// <summary>
        /// 日期
        /// </summary>
        DateChar,
        /// <summary>
        /// 英文
        /// </summary>
        EnglishChar,
        /// <summary>
        /// 數字(0-9)
        /// </summary>
        NumeralString,
        /// <summary>
        /// 正的數字
        /// </summary>
        PlusNumberChar,
        /// <summary>
        /// 正整數32
        /// </summary>
        PlusInt32,
        /// <summary>
        /// 整數32
        /// </summary>
        Int32,
        /// <summary>
        /// 正整數64
        /// </summary>
        PlusInt64,
        /// <summary>
        /// 整數64
        /// </summary>
        Int64,
        /// <summary>
        /// 数量(正数 和 0)
        /// </summary>
        Nembers,
        /// <summary>
        /// 单价(正浮点 和 0)
        /// </summary>
        Price



    }
}
