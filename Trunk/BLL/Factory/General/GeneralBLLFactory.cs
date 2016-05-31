using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Factory.General
{
    public class GeneralBLLFactory
    {
        /// <summary>
        /// 院系部主檔

        /// </summary>
        public static readonly string General = "BLL.DAL.General.GeneralBL,BLL";
        /// <summary>
        /// 根據對應的Accessor接口 獲得對應的DAL
        /// </summary>
        /// <typeparam name="IAccessor"></typeparam>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static IBLL GetBLL<IBLL>(string accessorFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (IBLL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
