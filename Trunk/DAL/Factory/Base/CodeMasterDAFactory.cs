using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Base;

namespace DAL.Factory.Base
{
    public class CodeMasterDAFactory
    {
        public static readonly CodeMasterDAFactory Instance = new CodeMasterDAFactory();
        private DataBaseType dataBaseType;
        public CodeMasterDAFactory() 
        {
            this.dataBaseType = FactoryLayerDefine.Instance.GetDataBaseType();
        }

        public ICodeMasterDA GetCodeMasterDA()
        {
            ICodeMasterDA iCodeMasterDA=null;

            if (this.dataBaseType == DataBaseType.Access)
            {
                iCodeMasterDA = new DAL.AccessDAL.Base.CodeMasterDA();
            }
            if (this.dataBaseType == DataBaseType.SqlServer)
            {
                iCodeMasterDA = new DAL.SqlDAL.Base.CodeMasterDA();
            }

            return iCodeMasterDA;
        }
    }
}
