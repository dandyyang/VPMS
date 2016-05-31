using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Base;

namespace DAL.Factory.Base
{
    public class SystemMenuDALFactory
    {
        public static readonly SystemMenuDALFactory Instance = new SystemMenuDALFactory();
        private DataBaseType dataBaseType;
        public SystemMenuDALFactory() 
        {
            this.dataBaseType = FactoryLayerDefine.Instance.GetDataBaseType();
        }

        public ISystemMenuDA GetSystemMenuDA()
        {
            //ISystemMenuDA iSystemMenuDA = null;

            //if (this.dataBaseType == DataBaseType.SqlServer)
            //{
            //    iSystemMenuDA = new DAL.SqlDAL.Base.SystemMenuDA();
            //}

            //return iSystemMenuDA;

            string accessorFullName = "DAL.SqlDAL.Base.SystemMenuDA,DAL";

            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (ISystemMenuDA)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
