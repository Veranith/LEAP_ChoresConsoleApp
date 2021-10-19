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
            Console.WriteLine("Query: " + sql);
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

        public void insertTable(string table, string choreName, string choreAssignment)
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
            Console.WriteLine(sql.ToString());
            executeNonQuery(sql.ToString());
        }

        public void updateTable(string Table)
        {
            throw new NotImplementedException();
        }

        public void readQuery(string query)
        {
            throw new NotImplementedException();
        }

        private void executeNonQuery(string query)
        {
            Console.Write("Connecting to SQL Server .... ");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
