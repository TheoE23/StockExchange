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

        public bool Connect()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return true;
            }
        }
    }
}

