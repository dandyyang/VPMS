using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.Service;
using LinqToSQLModel;
using Common;
using Model.IModel;

namespace DAL.SqlDAL.Management.Service
{
    class GeneralDA : IGeneralDA
    {
        /// <summary>
        /// 检查指定的日期是否是节假日
        /// </summary>
        /// <param name="date">指定的日期</param>
        /// <returns></returns>
        public bool IsDateInHolidays(DateTime date)
        {
            bool isHolidays = false;

            if (date == null)
            {
                return isHolidays;
            }

            string theDate = date.ToString("yyyy/MM/dd");
            DateTime nowDateTime = Convert.ToDateTime(theDate);
            string sqlString = string.Empty;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    HolidaysSetting_hds holidaysSettingInfo = null;

                    holidaysSettingInfo = db.HolidaysSetting_hds.FirstOrDefault<HolidaysSetting_hds>(t => t.hds_dStartTime <= nowDateTime && t.hds_dEndTime >= nowDateTime);

                    if (holidaysSettingInfo != null)
                    {
                        isHolidays = true;
                    }

                    if (!isHolidays)
                    {
                        string week = date.DayOfWeek.ToString("d");
                        CodeMaster_cmt codeMasterInfo = null;

                        codeMasterInfo = db.CodeMaster_cmts.FirstOrDefault<CodeMaster_cmt>(t => t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_WeekHolidaySetting && t.cmt_cKey2 == week);

                        if (codeMasterInfo != null)
                        {
                            isHolidays = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isHolidays;
        }
    }
}
