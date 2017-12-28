using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEEIPro.Utils
{
    public static class DBHelper
    {
        //获取Web.config文件中数据库连接的配置信息
        public static readonly string connstr =
         ConfigurationManager.ConnectionStrings["seeiExpertsDB"].ConnectionString;
        /// <summary>
        /// 打开数据库链接
        /// </summary>
        /// <returns>返回SqlConnection类型对象</returns>
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 执行非查询操作
        /// </summary>
        /// <param name="cmdText">非查询Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回执行所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteNonQuery(conn, cmdText, parameters);
            }
        }

        /// <summary>
        /// 执行非查询操作
        /// </summary>
        /// <param name="cmdText">非查询Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回执行所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteNonQuery(conn, cmdText);
            }
        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回查询结果</returns>
        public static object ExecuteScalar(string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteScalar(conn, cmdText, parameters);
            }
        }
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable ExecuteDataTable(string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteDataTable(conn, cmdText, parameters);
            }
        }

        /// <summary>
        /// 无参数查询登陆日志
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteDataTable(conn, cmdText);
            }
        }


        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters"></param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, string cmdText,
           params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 无参数执行非查询语句
        /// </summary>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters"></param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, string cmdText)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                return cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回查询结果</returns>
        public static object ExecuteScalar(SqlConnection conn, string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="cmdText">查询sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataTable类型的结果</returns>
        public static DataTable ExecuteDataTable(SqlConnection conn, string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable ExecuteDataTable(SqlConnection conn, string cmdText)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 数据库从Model获得空值
        /// </summary>
        /// <param name="value"></param>
        /// <returns>需要获取它的空值，否则获得value</returns>
        public static object ToDBValue(this object value)
        {
            return value == null ? DBNull.Value : value;
        }

        /// <summary>
        /// 从数据库中得到null值
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns>如果为空值，返回NULL，否则返回value</returns>
        public static object FromDBValue(this object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }

        /// <summary>
        /// 执行sql 返回dataset
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet ReturnDataSet(string strSql)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    using (SqlDataAdapter command = new SqlDataAdapter(strSql, connection))
                    {
                        command.Fill(ds, "ds");
                    }
                }
                catch (SqlException e)
                {
                    return null;
                }
                return ds;
            }
        }

        /// <summary>
        /// 组成命令
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static SqlCommand CreateCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                DataSet ds = new DataSet();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = CreateCommand(connection, storedProcName, parameters);
                    sda.Fill(ds, tableName);
                }
                connection.Close();
                return ds;
            }
        }


        public static SqlDataReader ExecuteReader(string strSql)
        {
            SqlConnection connection = new SqlConnection(connstr);
            using (SqlCommand cmd = new SqlCommand(strSql, connection))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                try
                {
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (SqlException e)
                {

                }
            }
            return null;
        }
    }
}