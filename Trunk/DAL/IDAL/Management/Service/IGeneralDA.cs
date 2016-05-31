using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace DAL.IDAL.Management.Service
{
    public interface IGeneralDA
    {
        /// <summary>
        /// 指定的日期是否为节假日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        bool IsDateInHolidays(DateTime date);
    }
}
