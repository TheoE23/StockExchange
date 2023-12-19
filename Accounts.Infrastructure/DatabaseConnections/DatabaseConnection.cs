using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Accounts.Infrastructure.DatabaseConnections
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string ConnectionString => _connectionString;

        public SqlConnection Connect()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                    connection.Open();
                    return connection;
                
            }
            catch (SqlException ex)
            {
               
                Console.WriteLine("Error with connection: " + ex.Message);
                return null;
            }
        }
    }
}




