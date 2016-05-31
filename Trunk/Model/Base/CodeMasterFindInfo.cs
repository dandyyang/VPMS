using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Base
{
    public class CodeMasterFindInfo
    {
        public CodeMasterFindInfo() 
        {
            this.CmtCKey1 = "";
            this.CmtCKey2 = "";
            this.CmtCValue = "";
        }

        public string CmtCKey1
        {
            get;
            set;
        }

        public string CmtCKey2
        {
            get;
            set;
        }

        public string CmtCValue
        {
            get;
            set;
        }


    }
}
