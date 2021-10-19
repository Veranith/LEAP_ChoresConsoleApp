using System;
using System.Data.SqlClient;
using System.Text;

namespace ChoresLibrary
{
    public class SQLConnection
    {
        public string connectionString { get; private set; }
        public string databaseName { get; private set; }
        public string tableName { get; private set; }

        public SQLConnection()
        {
            ConnectionBuilder();
        }

        private void ConnectionBuilder()
        {
            
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLPlayground";
            builder.UserID = "app";
            builder.Password = "password123";
            builder.InitialCatalog = "master";

            connectionString = builder.ConnectionString;
        }

        public void createDatabase(string dbName)
        {
            databaseName = dbName;
            String sql = $"DROP DATABASE IF EXISTS [{databaseName}]; CREATE DATABASE [{databaseName}];";
            
            executeNonQuery(sql);
        }

        public void createTable(string Name)
        {
            tableName = Name;
            string sql = $"USE {databaseName}; " +
                $"CREATE TABLE {tableName} ( " +
                $" ChoreId int IDENTITY(1,1) NOT NULL PRIMARY KEY," +
                $" ChoreName nvarchar(max) NOT NULL," +
                $" ChoreAssignment nvarchar(max)" +
                $")";

            executeNonQuery(sql);
        }

        public int insertTable(string table, string choreName, string choreAssignment)
        {
            var sql = new StringBuilder();
            sql.Append($"USE {databaseName};");
            sql.Append($"INSERT INTO {table} ");
            if (choreAssignment is null)
            {
                sql.Append($"(ChoreName) " +
                           $"VALUES ('{choreName}');");
            }
            else
            {
                sql.Append( $"(ChoreName, ChoreAssignment) " +
                            $"VALUES ('{choreName}', '{choreAssignment}');");
            }
            
            return executeNonQuery (sql.ToString());
        }

        public int updateTable(string table, int id, string choreName, string choreAssignment)
        {
            var sql = new StringBuilder();
            sql.Append($"USE {databaseName};");
            sql.Append($"UPDATE {table} ");
            
            if (choreAssignment is null)
            {
                sql.Append($"SET ChoreName = '{choreName}' ");
            }
            else if (choreName is null)
            {
                sql.Append($"SET ChoreAssignment = '{choreAssignment}' ");
            }
            else
            {
                sql.Append($"SET ChoreName = '{choreName}', ChoreAssignment = '{choreAssignment}' ");
            }
            sql.Append($"WHERE ChoreId = {id};");
            
            return executeNonQuery(sql.ToString());
        }

        public int deleteFromTable(string table, int id)
        {
            var sql = new StringBuilder();
            sql.Append($"USE {databaseName};");
            sql.Append($"DELETE FROM {table} ");
            sql.Append($"WHERE ChoreId = {id};");

            return executeNonQuery(sql.ToString());
        }

        public void readQuery(string query)
        {
            throw new NotImplementedException();
        }

        private int executeNonQuery(string query)
        {
            Console.Write("Connecting to SQL Server .... ");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
