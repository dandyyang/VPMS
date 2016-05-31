using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Factory.PublisherTool
{
    public class PublisherToolDAFactory
    {
        public static readonly string NewRFIDCardManage = "DAL.AccessDAL.PublisherTool.NewRFIDCardManageDA,DAL";

        /// <summary>
        /// 根據對應的Accessor接口 獲得對應的DAL
        /// </summary>
        /// <typeparam name="IAccessor"></typeparam>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static IDAL GetDAL<IDAL>(string accessorFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (IDAL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
