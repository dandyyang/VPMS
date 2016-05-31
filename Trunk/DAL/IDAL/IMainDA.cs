using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace DAL.IDAL
{
    public interface IMainDA<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns></returns>
        bool InsertRecord(T infoObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns></returns>
        bool UpdateRecord(T infoObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        bool DeleteRecord(IModelObject KeyObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        T DisplayRecord(IModelObject KeyObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MDobject"></param>
        /// <returns></returns>
        List<T> SearchRecords(IModelObject searchCondition);
    }
}
