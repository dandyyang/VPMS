using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using DAL.LocalDefine;

namespace DAL.AccessDAL
{
    /// <summary>
    /// 数据库处理层
    /// </summary>
    class DataAccessLayer
    {
        private OleDbConnection m_cnn;
        private string m_strCnn;

        /// <summary>
        /// 数据库处理层
        /// </summary>
        public DataAccessLayer()
        {

            m_strCnn = ConfigurationSettings.AppSettings["AccessCnnStr"].Trim();

            /*Modify By Leoth LunLin On 04/06/2012 Start 系统为XP替换连接字符串*/
            //if (IsXP())
            //{
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString().Trim();
                m_strCnn = m_strCnn.Replace("~", path);
            //}
            /*End*/
        }

        /*Create By Leoth LunLin On 04/06/2012 Start 查询系统是否为XP*/
        bool IsXP()
        {
            Version ver = System.Environment.OSVersion.Version;
            if (ver.Major == 5 && ver.Minor == 1)
            {
                //strClient = "Win XP";
                return true;
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                //strClient = "Win Vista";
                return false;
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                //strClient = "Win 7";
                return false;
            }
            else if (ver.Major == 5 && ver.Minor == 0)
            {
                //strClient = "Win 2000";
                return false;
            }
            else
            {
                //strClient = "未知";
                return false;
            }
        }
        /*End*/

        /// <summary>
        /// 数据库处理层
        /// </summary>
        /// <param name="m_strCnnKey">获得配置文件中数据库连接字符串的Key值</param>
        public DataAccessLayer(string m_strCnnKey)
        {
            m_strCnn = ConfigurationSettings.AppSettings[m_strCnnKey].Trim();
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (m_cnn == null)
            {
                m_cnn = new OleDbConnection(m_strCnn);
                m_cnn.Open();
            }
            else
            {
                if (m_cnn.State == ConnectionState.Closed) m_cnn.Open();
            }
        }

        /// <summary>
        /// 获得SqlConnection对象
        /// </summary>
        /// <returns>SqlConnection</returns>
        public OleDbConnection GetCnn()
        {
            if (m_cnn == null)
            {
                Open();
            }
            else
            {
                if (m_cnn.State == ConnectionState.Closed) m_cnn.Open();
            }

            return m_cnn;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (m_cnn != null)
            {
                m_cnn.Close();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="Cnn">SqlConnection对象</param>
        public void Close(OleDbConnection Cnn)
        {
            if (Cnn != null)
            {
                Cnn.Close();
            }
        }

        /// <summary>
        /// 获得DataReader对象
        /// </summary>
        /// <param name="drObject">DataReader对象</param>
        public void CloseDataReader(OleDbDataReader drObject)
        {
            if (drObject != null)
                drObject.Close();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (m_cnn != null)
                {
                    if (m_cnn.State == ConnectionState.Open)
                    {
                        m_cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获得DataSet对象
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string strSql)
        {
            this.Open();
            OleDbDataAdapter l_da = new OleDbDataAdapter(strSql, m_cnn);
            DataSet l_dataset = new DataSet();
            try
            {
                l_dataset.Clear();
                l_da.Fill(l_dataset);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                this.Close();
            }

            return l_dataset;
        }

        /// <summary>
        /// 获得DataSet对象
        /// </summary>
        /// <param name="SPName">Stored Procedures名称</param>
        /// <param name="Parameters">OleDbParameter对象数组</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string SPName, OleDbParameter[] Parameters)
        {
            OleDbCommand cmd = this.GetOleDbCommand(SPName, Parameters);
            OleDbDataAdapter l_da = new OleDbDataAdapter(cmd);
            DataSet l_dataset = new DataSet();
            try
            {
                l_dataset.Clear();
                l_da.Fill(l_dataset);
                return l_dataset;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 获得SqlDataReader对象
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <returns>SqlDataReader</returns>
        public OleDbDataReader GetDataReader(string StrSql)
        {
            this.Open();
            OleDbCommand command = new OleDbCommand(StrSql, m_cnn);
            command.CommandType = CommandType.Text;
            OleDbDataReader l_dr;

            try
            {
                l_dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }

            return l_dr;

        }

        /// <summary>
        /// 获得SqlDataReader对象
        /// </summary>
        /// <param name="spName">Stored Procedures名称</param>
        /// <param name="Parameters">SqlParameter对象数组</param>
        /// <returns>SqlDataReader</returns>
        public OleDbDataReader GetDataReader(string spName, OleDbParameter[] Parameters)
        {
            this.Open();
            OleDbDataReader l_dr;
            OleDbCommand command = new OleDbCommand(spName, m_cnn);
            command.CommandType = CommandType.StoredProcedure;


            if (Parameters != null)
            {
                foreach (OleDbParameter Parameter in Parameters)
                {
                    command.Parameters.Add(Parameter);
                }
            }

            try
            {
                l_dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }

            return l_dr;
        }

        /// <summary>
        ///  获得DataTable对象
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string strSQL)
        {
            DataSet l_ds = new DataSet("Table");
            DataTable l_Table = null;
            this.Open();
            try
            {

                OleDbCommand l_Command = new OleDbCommand();
                l_Command.CommandText = strSQL;
                l_Command.Connection = m_cnn;

                OleDbDataAdapter l_da = new OleDbDataAdapter(l_Command);
                l_da.Fill(l_ds);
                l_Table = l_ds.Tables["Table"];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                this.Close();
            }

            return l_Table;
        }

        /// <summary>
        /// 获得SqlCommand对象
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>SqlCommand</returns>
        public OleDbCommand GetOleDbCommand(string strSql)
        {
            this.Open();
            OleDbCommand cmd = new OleDbCommand(strSql, m_cnn);

            cmd.CommandType = CommandType.Text;

            this.Close();

            return cmd;
        }

        /// <summary>
        /// 获得SqlCommand对象
        /// </summary>
        /// <param name="spName">Stored Procedures名称</param>
        /// <param name="Parameters">SqlParameter对象数组</param>
        /// <returns>SqlCommand</returns>
        public OleDbCommand GetOleDbCommand(string spName, OleDbParameter[] Parameters)
        {
            this.Open();
            OleDbCommand cmd = new OleDbCommand(spName, m_cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (OleDbParameter Parameter in Parameters)
                {
                    cmd.Parameters.Add(Parameter);
                }
            }

            this.Close();

            return cmd;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <returns>bool</returns>
        public bool ExecNonQuery(string StrSql)
        {
            bool l_exesucess;

            l_exesucess = true;

            this.Open();
            OleDbCommand cmd = new OleDbCommand(StrSql, m_cnn);

            cmd.CommandType = CommandType.Text;

            OleDbTransaction Tran = m_cnn.BeginTransaction();

            cmd.Transaction = Tran;

            try
            {
                cmd.ExecuteNonQuery();
                Tran.Commit();

                l_exesucess = true;

            }
            catch (Exception Ex)
            {
                Tran.Rollback();
                l_exesucess = false;
                throw Ex;
            }

            finally
            {
                this.Close();
            }

            return l_exesucess;
        }

        /// <summary>
        /// Non Transaction Handle in this method
        /// </summary>
        /// <param name="StrSql"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool ExecNonQuery(string StrSql, OleDbTransaction tran, OleDbConnection cnn)
        {
            bool l_exesucess;

            l_exesucess = true;

            OleDbCommand cmd = new OleDbCommand(StrSql, cnn);

            cmd.CommandType = CommandType.Text;

            cmd.Transaction = tran;

            try
            {
                cmd.ExecuteNonQuery();
                l_exesucess = true;
            }
            catch (Exception Ex)
            {
                l_exesucess = false;
                throw Ex;
            }

            return l_exesucess;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public bool ExecNonQuery_WithoutTran(string SPName, OleDbParameter[] Parameters)
        {

            if (Parameters == null)
            {
                return false;
            }
            bool l_exesucess;

            l_exesucess = true;

            Open();

            OleDbCommand cmd = new OleDbCommand(SPName, m_cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (OleDbParameter para in Parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }

            try
            {
                cmd.ExecuteNonQuery();

                l_exesucess = true;
            }

            catch (Exception Ex)
            {
                l_exesucess = false;
                throw Ex;
            }

            finally
            {
                this.Close();
            }

            return l_exesucess;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public bool ExecNonQuery(string SPName, OleDbParameter[] Parameters)
        {

            bool l_exesucess;

            l_exesucess = true;

            Open();

            OleDbCommand cmd = new OleDbCommand(SPName, m_cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (OleDbParameter para in Parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }

            OleDbTransaction tran = m_cnn.BeginTransaction();

            cmd.Transaction = tran;

            try
            {
                cmd.ExecuteNonQuery();

                tran.Commit();

                l_exesucess = true;
            }

            catch (Exception Ex)
            {
                tran.Rollback();
                //ErrLogInfo.WriteErrLog(Ex);
                l_exesucess = false;
                throw Ex;
            }

            finally
            {
                this.Close();
            }

            return l_exesucess;

        }

        /// <summary>
        /// Non Transaction Handle in this method
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="Parameters"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool ExecNonQuery(string SPName, OleDbParameter[] Parameters, OleDbTransaction tran, OleDbConnection cnn)
        {

            bool l_exesucess;

            l_exesucess = true;

            OleDbCommand cmd = new OleDbCommand(SPName, cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (OleDbParameter para in Parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }

            cmd.Transaction = tran;

            try
            {
                cmd.ExecuteNonQuery();
                l_exesucess = true;
            }

            catch (Exception Ex)
            {
                l_exesucess = false;
                throw Ex;
            }

            return l_exesucess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <param name="Direction"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private OleDbParameter MakeParam(string ParamName, OleDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            OleDbParameter Para;

            if (Size > 0)
                Para = new OleDbParameter(ParamName, DbType, Size);
            else
                Para = new OleDbParameter(ParamName, DbType);

            Para.Direction = Direction;

            if (!(Para.Direction == ParameterDirection.Output && Value == null))
            {
                Para.Value = Value;
            }

            return Para;

        }

        private OleDbParameter MakeParam(string ParamName, OleDbType DbType, Int32 Size, ParameterDirection Direction, object Value, byte precision, byte scale)
        {
            OleDbParameter Para;

            if (Size > 0)
                Para = new OleDbParameter(ParamName, DbType, Size);
            else
                Para = new OleDbParameter(ParamName, DbType);

            Para.Direction = Direction;
            Para.Precision = precision;
            Para.Scale = scale;

            if (!(Para.Direction == ParameterDirection.Output && Value == null))
            {
                Para.Value = Value;
            }

            return Para;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameName"></param>
        /// <param name="dbType"></param>
        /// <param name="Size"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public OleDbParameter MakeInParam(string ParameName, OleDbType dbType, Int32 Size, object Value)
        {
            return MakeParam(ParameName, dbType, Size, ParameterDirection.Input, Value);
        }

        public OleDbParameter MakeInParam(string ParameName, OleDbType dbType, Int32 Size, object Value, byte precision, byte scale)
        {
            return MakeParam(ParameName, dbType, Size, ParameterDirection.Input, Value, precision, scale);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameName"></param>
        /// <param name="dbType"></param>
        /// <param name="Size"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public OleDbParameter MakeOutParam(string ParameName, OleDbType dbType, Int32 Size, object Value)
        {
            return MakeParam(ParameName, dbType, Size, ParameterDirection.Output, Value);
        }

    }
}
