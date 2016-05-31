using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysFunction;
using Model.SysFunction;
using System.Data;

namespace DAL.SqlDAL.SysFunction
{
    class SysUserMasterDA:ISysUserMasterDA 
    {
        DataAccessLayer db;
        public SysUserMasterDA()
        {
            if (db == null)
            {
                db = new DataAccessLayer();
            }

        }


        #region IMainDA<Sys_UserMaster_usm> Members

        public bool InsertRecord(Sys_UserMaster_usm infoObject)
        {
            //throw new NotImplementedException();
            string Sql = " INSERT INTO [Sys_UserMaster_usm] " + Environment.NewLine;
            Sql = Sql + "([usm_cUserLoginID]" + Environment.NewLine;
            Sql = Sql + ",[usm_cChaName]" + Environment.NewLine;
            Sql = Sql + ",[usm_cPasswork]" + Environment.NewLine;
            Sql = Sql + " ,[usm_cEMail]" + Environment.NewLine;
            Sql = Sql + ",[usm_iLock]" + Environment.NewLine;
            Sql = Sql + ",[usm_cRemark]" + Environment.NewLine;
            Sql = Sql + ",[usm_cAdd]" + Environment.NewLine;
            Sql = Sql + ",[usm_dAddDate]" + Environment.NewLine;
            Sql = Sql + ",[usm_cLast]" + Environment.NewLine;
            Sql = Sql + ",[usm_dLastDate])" + Environment.NewLine;
            Sql = Sql + "VALUES" + Environment.NewLine;
            Sql = Sql + "('"+infoObject.usm_cUserLoginID +"'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cChaName  + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cPasswork  + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cEMail  + "'" + Environment.NewLine;
            Sql = Sql + "," +(infoObject.usm_iLock ? 1 :0)  + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cRemark  + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cAdd  + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_cLast  + "'" + Environment.NewLine;
            Sql = Sql + ",'" + infoObject.usm_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) + "')" + Environment.NewLine;

            return false;

        }

        public bool UpdateRecord(Sys_UserMaster_usm infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<Sys_UserMaster_usm> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBaseCommandDA<Sys_UserMaster_usm> Members

        public Sys_UserMaster_usm GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 內部使用函數

        string GetSelectSql()
        {
            string sql = "  usm_iRecordID,usm_cUserLoginID,usm_cChaName,usm_cPasswork,usm_cEMail"+Environment.NewLine ;
            sql = sql + ",usm_iLock,usm_cRemark,usm_cAdd,usm_dAddDate,usm_cLast,usm_dLastDate " + Environment.NewLine ;
            sql = sql + " From Sys_UserMaster_usm" + Environment.NewLine;

            return sql;
        }

        /// <summary>
        /// 根據SQL獲得對應的實體集合
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        List<Sys_UserMaster_usm > GetItemsBySql(string Sql)
        {

            List<Sys_UserMaster_usm> items;

            using (IDataReader dr = db.GetDataReader(Sql))
            {
                items = Common.MappingTools.MappingDAO.FillCollection<Sys_UserMaster_usm>(dr);
            }


            return items;

        }

        /// <summary>
        /// 根據Sql獲得對應實體
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        Sys_UserMaster_usm GetItemBySql(string Sql)
        {
            Sys_UserMaster_usm item;

            using (IDataReader dr = db.GetDataReader(Sql))
            {
                item = Common.MappingTools.MappingDAO.FillEntity<Sys_UserMaster_usm>(dr);
            }


            return item;
        }


        #endregion

    }
}
