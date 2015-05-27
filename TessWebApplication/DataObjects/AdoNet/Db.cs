#region Includes
using System;
using System.Data.Common;
using System.Data;
using System.Configuration;
#endregion

namespace Greenspoon.Tess.DataObjects.AdoNet
{
    public static class Db
    {
        static readonly string _dataProvider         = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly DbProviderFactory factory    = DbProviderFactories.GetFactory(_dataProvider);
        static readonly string _connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        static readonly string _connectionString     = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
        static readonly string _imgConnectionString  = ConfigurationManager.ConnectionStrings["Image.DB"].ConnectionString;

        #region Data Update handlers

        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Number of rows affected.</returns>
        public static bool Update(string sql)
        {
            using (var connection = factory.CreateConnection()) {
                connection.ConnectionString = _connectionString;
                using (var command = factory.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = sql;
                    connection.Open();
                    var tran = connection.BeginTransaction();
                    command.Transaction = tran;
                    try {
                        command.ExecuteNonQuery();
                        tran.Commit();
                        return true;
                    } catch (Exception) {
                        tran.Rollback();
                        connection.Close();
                        return false;
                    }

                }
            }
        }

        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="getId">Value indicating whether newly generated identity is returned.</param>
        /// <returns>Newly generated identity value (autonumber value).</returns>
        public static int Insert(string sql, bool getId)
        {
            using (var connection = factory.CreateConnection()) {
                connection.ConnectionString = _connectionString;

                using (var command = factory.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = sql;
                    connection.Open();
                    command.ExecuteNonQuery();

                    var id = -1;
                    if (getId) {
                        string identitySelect;
                        switch (_dataProvider) {
                            // Access
                            case "System.Data.OleDb":
                                identitySelect = "SELECT @@IDENTITY";
                                break;
                            // Sql Server
                            case "System.Data.SqlClient":
                                identitySelect = "SELECT SCOPE_IDENTITY()";
                                break;
                            // Oracle
                            case "System.Data.OracleClient":
                                identitySelect = "SELECT MySequence.NEXTVAL FROM DUAL";
                                break;
                            default:
                                identitySelect = "SELECT @@IDENTITY";
                                break;
                        }
                        command.CommandText = identitySelect;
                        id = int.Parse(command.ExecuteScalar().ToString());
                    }
                    return id;
                }
            }
        }

        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        public static void Insert(string sql)
        {
            Insert(sql, false);
        }

        #endregion

        #region Data Retrieve Handlers

        /// <summary>
        /// Populates a DataSet according to a Sql statement.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Populated DataSet.</returns>
        public static DataSet GetDataSet(string sql)
        {
            using (var connection = factory.CreateConnection()) {
                connection.ConnectionString = _connectionString;

                using (var command = factory.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    using (var adapter = factory.CreateDataAdapter()) {
                        adapter.SelectCommand = command;
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// Populates a DataTable according to a Sql statement.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="connString"> </param>
        /// <returns>Populated DataTable.</returns>
        public static DataTable GetDataTable(string sql, string connString = "tess")
        {
            using (var connection = factory.CreateConnection()) {
                connection.ConnectionString = connString.Equals("tess") ? _connectionString : _imgConnectionString;

                using (var command = factory.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;

                    using (var adapter = factory.CreateDataAdapter()) {
                        adapter.SelectCommand = command;

                        var dt = new DataTable();
                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Populates a DataRow according to a Sql statement.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Populated DataRow.</returns>
        public static DataRow GetDataRow(string sql)
        {
            DataRow row = null;
            var dt = GetDataTable(sql);
            if (dt.Rows.Count > 0) {
                row = dt.Rows[0];
            }
            return row;
        }

        /// <summary>
        /// Executes a Sql statement and returns a scalar value.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Scalar value.</returns>
        public static object GetScalar(string sql)
        {
            using (var connection = factory.CreateConnection()) {
                connection.ConnectionString = _connectionString;
                using (var command = factory.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = sql;

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Utility methods

        public static string Escape(string s)
        {
            if (String.IsNullOrEmpty(s))
                return "NULL";
            return "'" + s.Trim().Replace("'", "''") + "'";
        }

        public static string Escape(string s, int maxLength)
        {
            if (String.IsNullOrEmpty(s))
                return "NULL";
            s = s.Trim();
            if (s.Length > maxLength) s = s.Substring(0, maxLength - 1);
            return "'" + s.Trim().Replace("'", "''") + "'";
        }

        #endregion
    }
}