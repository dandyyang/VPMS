using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.LocalDefine
{
    class GeneralDefine
    {
        public static readonly GeneralDefine Instance = new GeneralDefine();
        public GeneralDefine() { }

        /// <summary>
        /// 换行符
        /// </summary>
        public string NewLineSymbol
        {
            get { return "\r\n"; }
        }

        /// <summary>
        /// 定義字碼主檔常量值
        /// </summary>
        public struct CodeMasterDefineStruct
        {
            /// <summary>
            /// 主鍵一(財務現金收入類型)
            /// </summary>
            public string strKey1_FinanceCashIncomeType;
            /// <summary>
            /// 主鍵一(財務現金收入形式)
            /// </summary>
            public string strKey1_FinanceCashIPFashion;
            /// <summary>
            /// 主鍵一(財務成本類型)
            /// </summary>
            public string strKey1_FinanceCostType;

            /// <summary>
            /// 主鍵一(24G 题库表中的问题类型)
            /// </summary>
            public string strKey1_ExamineSystem_EQType;

            /// <summary>
            /// 主鍵一(24G 使用2.4G讀寫器時所使用的COMM信息)
            /// </summary>
            public string strKey1_TH24GReaderCommInformation;
            /// <summary>
            /// 主鍵二(24G 使用2.4G讀寫器時所使用的COMM信息__Port值)
            /// </summary>
            public string strKey2_TH24GReaderCommInformation_Port;

            public CodeMasterDefineStruct(string str)
            {
                this.strKey1_FinanceCashIncomeType = "FINANCECASHINCOMETYPE";
                this.strKey1_FinanceCashIPFashion = "FINANCECASHIPFASHION";
                this.strKey1_FinanceCostType = "FINANCECOSTTYPE";
                this.strKey1_ExamineSystem_EQType = "EXAMINESYSTEMEQTYPE";
                this.strKey1_TH24GReaderCommInformation = "TH24GREADERCOMMINFO";
                this.strKey2_TH24GReaderCommInformation_Port = "PORTVALUE";
            }

        }

        /// <summary>
        /// 定義字碼主檔常量值
        /// </summary>
        public CodeMasterDefineStruct CodeMasterDefine
        {
            get { return new CodeMasterDefineStruct(""); }
        }
    }
}
