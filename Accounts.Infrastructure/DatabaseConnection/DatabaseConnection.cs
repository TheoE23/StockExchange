using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Accounts.Infrastructure.DatabaseConnection
{
    public class DatabaseConnection
    {
        private readonly string connectionString = "Server=localhost;Database=StockAccountSystem;User Id=sa; Password=sqldocker2022;";

        public bool Connect()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                
                    connection.Open();
                return true;
              
            }
        }
    }
}
