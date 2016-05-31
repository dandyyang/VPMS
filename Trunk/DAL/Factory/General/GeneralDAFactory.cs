using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.General;

namespace DAL.Factory.General
{
    public class GeneralDAFactory
    {
        /// <summary>
        /// 通用方法類
        /// </summary>
        public static readonly string General = "DAL.SqlDAL.General.GeneralDA,DAL";
        
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
