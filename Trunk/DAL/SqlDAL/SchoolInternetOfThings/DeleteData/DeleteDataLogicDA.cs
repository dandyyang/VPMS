using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.DeleteData;
using Model.Management.DeleteData;
using Model.General;
using LinqToSQLModel;


namespace DAL.SqlDAL.Management.DeleteData
{
    class DeleteDataLogicDA : IDeleteDataLogicDA
    {
        #region IDeleteDataLogicDA Members

        public ReturnValueInfo DeleteData(DeleteDataLogic_ddl_Info info)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            returnValue.boolValue = false;

            return returnValue;
        }

        /// <summary>
        /// 删除卡记录逻辑
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ReturnValueInfo CheckUserCard(DeleteDataLogic_ddl_Info info)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                RFIDCardIssuance_rci tab;
                RFIDCardManage_rcm RFIDcard;
                try
                {
                    RFIDcard = db.RFIDCardManage_rcms.SingleOrDefault(t => t.rcm_iRecordID == info.ddl_ID);

                    if (RFIDcard != null && RFIDcard.rcm_cCardNum != "")
                    {
                        try
                        {
                            tab = db.RFIDCardIssuance_rcis.SingleOrDefault(t => t.rci_cCardNum == RFIDcard.rcm_cCardNum);
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }
                        if (tab != null)
                        {
                            returnValue.boolValue = false;
                            returnValue.messageText = "请先进行退卡操作！";
                        }
                        else
                        {
                            db.RFIDCardManage_rcms.DeleteOnSubmit(RFIDcard);
                            db.SubmitChanges();
                            returnValue.boolValue = true;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

            }
            return returnValue;
        }

        /// <summary>
        /// 删除用户记录逻辑
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ReturnValueInfo CheckCardUser(DeleteDataLogic_ddl_Info info)
        {
            ReturnValueInfo returnValue = new ReturnValueInfo();
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                CardUserMaster_cus CardUser;
                RFIDCardIssuance_rci tab;
                try
                {
                    CardUser = db.CardUserMaster_cus.SingleOrDefault(t => t.cus_iRecordID == info.ddl_ID);
                    if (CardUser != null && CardUser.cus_cNumber != "")
                    {
                        tab = db.RFIDCardIssuance_rcis.SingleOrDefault(t => t.rci_cCardUserNum == CardUser.cus_cNumber);
                        if (tab != null)
                        {
                            returnValue.boolValue = false;
                            returnValue.messageText = "请先进行退卡操作！";
                        }
                        else
                        {

                            db.CourseMasterTeacher_cut.DeleteAllOnSubmit(CardUser.CourseMasterTeacher_cuts);

                            IEnumerable<CardUserPhoneNumMaster_cup> cupTab =
                                from t in db.CardUserPhoneNumMaster_cup
                                where t.cup_CardUserNum == CardUser.cus_cNumber
                                select t;

                            if (cupTab.Count() > 0) 
                            {
                                db.CardUserPhoneNumMaster_cup.DeleteAllOnSubmit(cupTab);
                            }

                            db.CardUserMaster_cus.DeleteOnSubmit(CardUser);
                            db.SubmitChanges();
                            returnValue.boolValue = true;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            return returnValue;
        }

        #endregion
    }
}
