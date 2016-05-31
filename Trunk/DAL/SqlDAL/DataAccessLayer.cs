using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL.SqlDAL
{
    /// <summary>
    /// 数据库处理层
    /// </summary>
    class DataAccessLayer
    {
        private SqlConnection m_cnn;
        private string m_strCnn;

        /// <summary>
        /// 数据库处理层
        /// </summary>
        public DataAccessLayer()
        {
            // TODO: Add constructor logic here
            m_strCnn = ConfigurationSettings.AppSettings["SqlCnnStr"].Trim();

        }
        /// <summary>
        /// 数据库处理层
        /// </summary>
        /// <param name="m_strCnnKey">获得配置文件中数据库连接字符串的Key值</param>
        public DataAccessLayer(string m_strCnnKey)
        {
            m_strCnn = ConfigurationSettings.AppSettings[m_strCnnKey].ToString();
        }

        //~DataAccessLayer()
        //{
        //    Dispose();
        //}

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (m_cnn == null)
            {
                m_cnn = new SqlConnection(m_strCnn);
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
        public SqlConnection GetCnn()
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
        public void Close(SqlConnection Cnn)
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
        public void CloseDataReader(SqlDataReader drObject)
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
            SqlDataAdapter l_da = new SqlDataAdapter(strSql, m_cnn);
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
        /// <param name="Parameters">SqlParameter对象数组</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string SPName, SqlParameter[] Parameters)
        {
            SqlCommand cmd = this.GetSqlCommand(SPName, Parameters);
            SqlDataAdapter l_da = new SqlDataAdapter(cmd);
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
        public SqlDataReader GetDataReader(string StrSql)
        {
            this.Open();
            SqlCommand command = new SqlCommand(StrSql, m_cnn);
            command.CommandType = CommandType.Text;
            SqlDataReader l_dr;

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
        public SqlDataReader GetDataReader(string spName, SqlParameter[] Parameters)
        {
            this.Open();
            SqlDataReader l_dr;
            SqlCommand command = new SqlCommand(spName, m_cnn);
            command.CommandType = CommandType.StoredProcedure;


            if (Parameters != null)
            {
                foreach (SqlParameter Parameter in Parameters)
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

                SqlCommand l_Command = new SqlCommand();
                l_Command.CommandText = strSQL;
                l_Command.Connection = m_cnn;

                SqlDataAdapter l_da = new SqlDataAdapter(l_Command);
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
        public SqlCommand GetSqlCommand(string strSql)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand(strSql, m_cnn);

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
        public SqlCommand GetSqlCommand(string spName, SqlParameter[] Parameters)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand(spName, m_cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (SqlParameter Parameter in Parameters)
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
            SqlCommand cmd = new SqlCommand(StrSql, m_cnn);

            cmd.CommandType = CommandType.Text;

            SqlTransaction Tran = m_cnn.BeginTransaction();

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
        public bool ExecNonQuery(string StrSql, SqlTransaction tran, SqlConnection cnn)
        {
            bool l_exesucess;

            l_exesucess = true;

            SqlCommand cmd = new SqlCommand(StrSql, cnn);

            cmd.CommandType = CommandType.Text;

            //SqlTransaction Tran = m_cnn.BeginTransaction();

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
        public bool ExecNonQuery_WithoutTran(string SPName, SqlParameter[] Parameters)
        {

            if (Parameters == null)
            {
                return false;
            }
            bool l_exesucess;

            l_exesucess = true;

            Open();

            SqlCommand cmd = new SqlCommand(SPName, m_cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (SqlParameter para in Parameters)
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
        public bool ExecNonQuery(string SPName, SqlParameter[] Parameters)
        {

            bool l_exesucess;

            l_exesucess = true;

            Open();

            SqlCommand cmd = new SqlCommand(SPName, m_cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (SqlParameter para in Parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }

            SqlTransaction tran = m_cnn.BeginTransaction();

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
        public bool ExecNonQuery(string SPName, SqlParameter[] Parameters, SqlTransaction tran, SqlConnection cnn)
        {

            bool l_exesucess;

            l_exesucess = true;

            SqlCommand cmd = new SqlCommand(SPName, cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (SqlParameter para in Parameters)
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
        private SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter Para;

            if (Size > 0)
                Para = new SqlParameter(ParamName, DbType, Size);
            else
                Para = new SqlParameter(ParamName, DbType);

            Para.Direction = Direction;

            if (!(Para.Direction == ParameterDirection.Output && Value == null))
            {
                Para.Value = Value;
            }

            return Para;

        }

        private SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value, byte precision, byte scale)
        {
            SqlParameter Para;

            if (Size > 0)
                Para = new SqlParameter(ParamName, DbType, Size);
            else
                Para = new SqlParameter(ParamName, DbType);

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
        public SqlParameter MakeInParam(string ParameName, SqlDbType dbType, Int32 Size, object Value)
        {
            return MakeParam(ParameName, dbType, Size, ParameterDirection.Input, Value);
        }

        public SqlParameter MakeInParam(string ParameName, SqlDbType dbType, Int32 Size, object Value, byte precision, byte scale)
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
        public SqlParameter MakeOutParam(string ParameName, SqlDbType dbType, Int32 Size, object Value)
        {
            return MakeParam(ParameName, dbType, Size, ParameterDirection.Output, Value);
        }

    }
}
