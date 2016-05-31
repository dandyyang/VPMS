using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL.IDAL.Master;
//using DAL.LocalDefine.Master;
using Model.Master;
using Model.Base;
using Model.IModel;
using DAL.LocalDefine;
using DAL.SqlDAL.LocalLayer;

namespace DAL.SqlDAL.Master
{
    class ClientMasterDA:IClientMasterDA
    {
        public ClientMasterDA()
        {

        }


        #region IClientMasterDA Members

        public List<ClientMasterInfo> SearchRecords(ClientMasterInfo clientMasterInfo)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP 100 clm_iRecID,"+Environment.NewLine;
            sqlString+="clm_cClientNum,"+Environment.NewLine;
            sqlString+="clm_cEnglishName,"+Environment.NewLine;
            sqlString+="clm_cChinaName,"+Environment.NewLine;
            sqlString += "clm_cTaxNumber," + Environment.NewLine;
            sqlString+="clm_cLinkman,"+Environment.NewLine;
            sqlString+="clm_cAddress,"+Environment.NewLine;
            sqlString+="clm_cPhone,"+Environment.NewLine;
            sqlString+="clm_cFax,"+Environment.NewLine;
            sqlString+="clm_cWebSite,"+Environment.NewLine;
            sqlString+="clm_cRemark,"+Environment.NewLine;
            sqlString+="clm_cAdd,"+Environment.NewLine;
            sqlString+="clm_dAddDate,"+Environment.NewLine;
            sqlString+="clm_cLast,"+Environment.NewLine;
            sqlString+="clm_dLastDate "+Environment.NewLine;
            sqlString+=" FROM ClientMaster_clm ";

            if (clientMasterInfo != null)
            {
                whereString = " WHERE 1=1 ";
                if (clientMasterInfo.ClmCClientNum.Trim().Length > 0)
                {
                    whereString += " AND clm_cClientNum='" + clientMasterInfo.ClmCClientNum.Trim().Replace("'","''")+"'";
                }
                if (clientMasterInfo.ClmCChinaName.Trim().Length > 0)
                {
                    //whereString += " AND clm_cChinaName LIKE N'" + General.ReplaceSQLLikeCondition(clientMasterInfo.ClmCChinaName.Trim().Replace("'", "''")) + "'";
                }
            }

            sqlString += whereString;

            DataAccessLayer dal = new DataAccessLayer();
            List<ClientMasterInfo> clientMasterInfoList=null;
            SqlDataReader dr = null;

            try
            {
                dr = dal.GetDataReader(sqlString);

                if (dr != null)
                {
                    clientMasterInfoList = new List<ClientMasterInfo>();
                    ClientMasterInfo cmInfo = null;
                    while (dr.Read())
                    {
                        cmInfo = GetRecord(dr);
                        clientMasterInfoList.Add(cmInfo);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return clientMasterInfoList;
        }

        public ClientMasterInfo GetRecord(string clientNum)
        {
            string sqlString = string.Empty;

            sqlString = "SELECT clm_iRecID," + Environment.NewLine;
            sqlString += "clm_cClientNum," + Environment.NewLine;
            sqlString += "clm_cEnglishName," + Environment.NewLine;
            sqlString += "clm_cChinaName," + Environment.NewLine;
            sqlString += "clm_cTaxNumber," + Environment.NewLine;
            sqlString += "clm_cLinkman," + Environment.NewLine;
            sqlString += "clm_cAddress," + Environment.NewLine;
            sqlString += "clm_cPhone," + Environment.NewLine;
            sqlString += "clm_cFax," + Environment.NewLine;
            sqlString += "clm_cWebSite," + Environment.NewLine;
            sqlString += "clm_cRemark," + Environment.NewLine;
            sqlString += "clm_cAdd," + Environment.NewLine;
            sqlString += "clm_dAddDate," + Environment.NewLine;
            sqlString += "clm_cLast," + Environment.NewLine;
            sqlString += "clm_dLastDate " + Environment.NewLine;
            sqlString += " FROM ClientMaster_clm "+Environment.NewLine;
            sqlString += " WHERE clm_cClientNum='" + clientNum.Trim().Replace("'", "''") + "'";


            DataAccessLayer dal = new DataAccessLayer();
            ClientMasterInfo clientMaster = null;
            SqlDataReader dr = null;

            try
            {
                dr = dal.GetDataReader(sqlString);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        clientMaster = GetRecord(dr);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return clientMaster;
        }

        public bool IsExistRecord(string clientNum)
        {
            bool isExist = false;
            string sqlString = string.Empty;
            object valueObject = null;

            sqlString = "SELECT COUNT(1) FROM ClientMaster_clm" + Environment.NewLine;
            sqlString += " WHERE clm_cClientNum='" + clientNum.Trim().Replace("'", "''") + "'";

            DataBaseCommand<ClientMasterInfo> dataBaseCommand = new DataBaseCommand<ClientMasterInfo>();

            try
            {
                valueObject = dataBaseCommand.GetValue(sqlString, typeof(int));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            int count = (int)valueObject;
            if (count > 0)
            {
                isExist = true;
            }

            return isExist;
        }

        public bool IsLockRecord(long recordID)
        {
            return false;
        }

        public ClientMasterInfo GetTableFieldLenght()
        {
            ClientMasterInfo cmInfo = new ClientMasterInfo();

            cmInfo.ClmCClientNum_Lenght = 30;
            cmInfo.ClmCChinaName_Lenght = 50;
            cmInfo.ClmCEnglishName_Lenght = 100;
            cmInfo.ClmCTaxNumber_Lenght = 50;
            cmInfo.ClmCLinkman_Lenght = 50;
            cmInfo.ClmCAddress_Lenght = 100;
            cmInfo.ClmCPhone_Lenght = 50;
            cmInfo.ClmCFax_Lenght = 50;
            cmInfo.ClmCWebSite_Lenght = 50;
            cmInfo.ClmCRemark_Lenght = 200;

            return cmInfo;
        }

        public ClientMasterInfo GetRecord_Last()
        {
            LocalLayer.LocalDefine localDefine = new DAL.SqlDAL.LocalLayer.LocalDefine();
            string sqlString = string.Empty;

            sqlString = localDefine.GetCommandSQL_Last("ClientMaster_clm");

            ClientMasterInfo clientMaster = null;

            try
            {
                clientMaster = GetRecordForSql(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMaster;
        }

        public ClientMasterInfo GetRecord_First()
        {
            LocalLayer.LocalDefine localDefine = new DAL.SqlDAL.LocalLayer.LocalDefine();
            string sqlString = string.Empty;

            sqlString = localDefine.GetCommandSQL_First("ClientMaster_clm");

            ClientMasterInfo clientMaster = null;
            
            try
            {
                clientMaster = GetRecordForSql(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMaster;
        }

        public ClientMasterInfo GetRecord_Previous(int currentRecordID)
        {
            LocalLayer.LocalDefine localDefine = new DAL.SqlDAL.LocalLayer.LocalDefine();
            string sqlString = string.Empty;
            DataBaseCommandKeyInfo dataBaseCommandKeyInfo = new DataBaseCommandKeyInfo();
            List<DataBaseCommandKeyInfo> keyList = new List<DataBaseCommandKeyInfo>();

            dataBaseCommandKeyInfo.Key = "clm_iRecID";
            dataBaseCommandKeyInfo.KeyValue = currentRecordID.ToString();
            dataBaseCommandKeyInfo.KeyValueType = TypeCode.Int32;

            keyList.Add(dataBaseCommandKeyInfo);

            sqlString = localDefine.GetCommandSQL_Previous(keyList,"ClientMaster_clm");

            ClientMasterInfo clientMaster = null;

            try
            {
                clientMaster = GetRecordForSql(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMaster;
        }

        public ClientMasterInfo GetRecord_Next(int currentRecordID)
        {
            LocalLayer.LocalDefine localDefine = new DAL.SqlDAL.LocalLayer.LocalDefine();
            string sqlString = string.Empty;
            DataBaseCommandKeyInfo dataBaseCommandKeyInfo = new DataBaseCommandKeyInfo();
            List<DataBaseCommandKeyInfo> keyList = new List<DataBaseCommandKeyInfo>();

            dataBaseCommandKeyInfo.Key = "clm_iRecID";
            dataBaseCommandKeyInfo.KeyValue = currentRecordID.ToString();
            dataBaseCommandKeyInfo.KeyValueType = TypeCode.Int32;

            keyList.Add(dataBaseCommandKeyInfo);

            sqlString = localDefine.GetCommandSQL_Next(keyList, "ClientMaster_clm");

            ClientMasterInfo clientMaster = null;

            try
            {
                clientMaster = GetRecordForSql(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return clientMaster;
        }

        #endregion

        #region IMainDA<ClientMasterInfo> Members

        public bool InsertRecord(ClientMasterInfo infoObject)
        {
            string l_strSQL = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            l_strSQL = "INSERT ClientMaster_clm(";
            l_strSQL += "clm_cClientNum," + Environment.NewLine;
            l_strSQL += "clm_cEnglishName," + Environment.NewLine;
            l_strSQL += "clm_cChinaName," + Environment.NewLine;
            l_strSQL += "clm_cTaxNumber," + Environment.NewLine;
            l_strSQL += "clm_cLinkman," + Environment.NewLine;
            l_strSQL += "clm_cAddress," + Environment.NewLine;
            l_strSQL += "clm_cPhone," + Environment.NewLine;
            l_strSQL += "clm_cFax," + Environment.NewLine;
            l_strSQL += "clm_cWebSite," + Environment.NewLine;
            l_strSQL += "clm_cRemark," + Environment.NewLine;
            l_strSQL += "clm_cAdd," + Environment.NewLine;
            l_strSQL += "clm_dAddDate," + Environment.NewLine;
            l_strSQL += "clm_cLast," + Environment.NewLine;
            l_strSQL += "clm_dLastDate )" + Environment.NewLine;
            l_strSQL += " VALUES(";
            l_strSQL += "'" + infoObject.ClmCClientNum.Trim().Replace("'", "''") + "'";
            l_strSQL += ",'" + infoObject.ClmCEnglishName.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCChinaName.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCTaxNumber.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCLinkman.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCAddress.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCPhone.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCFax.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCWebSite.Replace("'", "''").Trim() + "'";
            l_strSQL += ",N'" + infoObject.ClmCRemark.Replace("'", "''").Trim() + "'";
            l_strSQL += ",'" + infoObject.ClmCAdd.Replace("'", "''").Trim() + "'";
            l_strSQL += ",GETDATE()";
            l_strSQL += ",'" + infoObject.ClmCLast.Replace("'", "''") + "'";
            l_strSQL += ",GETDATE())";

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(l_strSQL);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public bool UpdateRecord(ClientMasterInfo infoObject)
        {
            string l_strSQL = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            l_strSQL = "UPDATE ClientMaster_clm SET ";
            l_strSQL += "clm_cEnglishName='"+infoObject.ClmCEnglishName.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cChinaName=N'"+infoObject.ClmCChinaName.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cTaxNumber=N'" + infoObject.ClmCTaxNumber.Replace("'", "''").Trim() + "'," + Environment.NewLine;
            l_strSQL += "clm_cLinkman=N'"+infoObject.ClmCLinkman.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cAddress=N'"+infoObject.ClmCAddress.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cPhone=N'"+infoObject.ClmCPhone.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cFax=N'"+infoObject.ClmCFax.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cWebSite=N'"+infoObject.ClmCWebSite.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cRemark=N'"+infoObject.ClmCRemark.Replace("'", "''").Trim()+"'," + Environment.NewLine;
            l_strSQL += "clm_cLast='"+infoObject.ClmCLast.Replace("'", "''")+"'," + Environment.NewLine;
            l_strSQL += "clm_dLastDate=GETDATE() " + Environment.NewLine;
            l_strSQL += " WHERE clm_iRecID=" + infoObject.ClmIRecID.ToString();
            

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(l_strSQL);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            string l_strSQL = string.Empty;
            bool isSuccess = false;

            l_strSQL = "DELETE ClientMaster_clm ";

            l_strSQL += " WHERE clm_iRecID=" + KeyObject.RecordID.ToString();


            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(l_strSQL);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public ClientMasterInfo DisplayRecord(IModelObject KeyObject)
        {
            string sqlString = string.Empty;

            sqlString = "SELECT clm_iRecID," + Environment.NewLine;
            sqlString += "clm_cClientNum," + Environment.NewLine;
            sqlString += "clm_cEnglishName," + Environment.NewLine;
            sqlString += "clm_cChinaName," + Environment.NewLine;
            sqlString += "clm_cTaxNumber," + Environment.NewLine;
            sqlString += "clm_cLinkman," + Environment.NewLine;
            sqlString += "clm_cAddress," + Environment.NewLine;
            sqlString += "clm_cPhone," + Environment.NewLine;
            sqlString += "clm_cFax," + Environment.NewLine;
            sqlString += "clm_cWebSite," + Environment.NewLine;
            sqlString += "clm_cRemark," + Environment.NewLine;
            sqlString += "clm_cAdd," + Environment.NewLine;
            sqlString += "clm_dAddDate," + Environment.NewLine;
            sqlString += "clm_cLast," + Environment.NewLine;
            sqlString += "clm_dLastDate " + Environment.NewLine;
            sqlString += " FROM ClientMaster_clm " + Environment.NewLine;
            sqlString += " WHERE clm_iRecID=" + KeyObject.RecordID.ToString();


            DataAccessLayer dal = new DataAccessLayer();
            ClientMasterInfo clientMaster = null;
            SqlDataReader dr = null;

            try
            {
                dr = dal.GetDataReader(sqlString);

                if (dr != null)
                {
                    if (dr.Read())
                    {
                        clientMaster = GetRecord(dr);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return clientMaster;
        }

        #endregion

        private ClientMasterInfo GetRecordForSql(string sql)
        {
            LocalLayer.LocalDefine localDefine = new DAL.SqlDAL.LocalLayer.LocalDefine();
            string sqlString = string.Empty;

            DataAccessLayer dal = new DataAccessLayer();
            ClientMasterInfo clientMaster = null;
            SqlDataReader dr = null;

            try
            {
                dr = dal.GetDataReader(sql);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        clientMaster = GetRecord(dr);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return clientMaster;
        }

        private ClientMasterInfo GetRecord(SqlDataReader dr)
        {
            ClientMasterInfo clientMaster = null;

            if (dr != null)
            {
                clientMaster = new ClientMasterInfo();
                if (dr["clm_iRecID"] != null)
                {
                    clientMaster.ClmIRecID = Convert.ToInt32(dr["clm_iRecID"]);
                }
                if (dr["clm_cClientNum"] != null)
                {
                    clientMaster.ClmCClientNum = dr["clm_cClientNum"].ToString().Trim();
                }
                if (dr["clm_cEnglishName"] != null)
                {
                    clientMaster.ClmCEnglishName = dr["clm_cEnglishName"].ToString().Trim();
                }
                if (dr["clm_cChinaName"] != null)
                {
                    clientMaster.ClmCChinaName = dr["clm_cChinaName"].ToString().Trim();
                }
                if (dr["clm_cTaxNumber"] != null)
                {
                    clientMaster.ClmCTaxNumber = dr["clm_cTaxNumber"].ToString().Trim();
                }
                if (dr["clm_cLinkman"] != null)
                {
                    clientMaster.ClmCLinkman = dr["clm_cLinkman"].ToString().Trim();
                }
                if (dr["clm_cAddress"] != null)
                {
                    clientMaster.ClmCAddress = dr["clm_cAddress"].ToString().Trim();
                }
                if (dr["clm_cPhone"] != null)
                {
                    clientMaster.ClmCPhone = dr["clm_cPhone"].ToString().Trim();
                }
                if (dr["clm_cFax"] != null)
                {
                    clientMaster.ClmCFax = dr["clm_cFax"].ToString().Trim();
                }
                if (dr["clm_cWebSite"] != null)
                {
                    clientMaster.ClmCWebSite = dr["clm_cWebSite"].ToString().Trim();
                }
                if (dr["clm_cRemark"] != null)
                {
                    clientMaster.ClmCRemark = dr["clm_cRemark"].ToString().Trim();
                }
                if (dr["clm_cAdd"] != null)
                {
                    clientMaster.ClmCAdd = dr["clm_cAdd"].ToString().Trim();
                }
                if (dr["clm_dAddDate"] != null)
                {
                    clientMaster.ClmDAddDate = Convert.ToDateTime(dr["clm_dAddDate"]);
                }
                if (dr["clm_cLast"] != null)
                {
                    clientMaster.ClmCLast = dr["clm_cLast"].ToString().Trim();
                }
                if (dr["clm_dLastDate"] != null)
                {
                    clientMaster.ClmDLastDate = Convert.ToDateTime(dr["clm_dLastDate"]);
                }
            }

            return clientMaster;
        }

        #region IMainDA<ClientMasterInfo> Members

        public List<ClientMasterInfo> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
