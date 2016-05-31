using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Factory;
using BLL.IBLL.Base;

namespace BLL.Factory.Base
{
    public class SystemMenuBLLFactory
    {
        public static readonly SystemMenuBLLFactory Instance = new SystemMenuBLLFactory();
        private DataSourceType dataSourceType;
       
        public SystemMenuBLLFactory() 
        {
            this.dataSourceType = FactoryLayerDefine.Instance.GetDataSourceType();
        }

        public ISystemMenu GetISystemMenuBLL()
        {
            //ISystemMenu iSystemMenuBll = null;

            //if (this.dataSourceType == DataSourceType.DAL)
            //{
            //    iSystemMenuBll = new BLL.DAL.Base.SystemMenu();
            //}

            string accessorFullName = "BLL.DAL.Base.SystemMenu,BLL";

            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (ISystemMenu)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //return iSystemMenuBll;
        }
    }
}
