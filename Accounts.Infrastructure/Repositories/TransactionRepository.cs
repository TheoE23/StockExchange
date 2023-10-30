using Accounts.Infrastructure.Repositories.MyProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Accounts.Domain.Entities;

namespace Accounts.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transactions>
    {
        public TransactionRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "transactions";

        protected override Transactions Map(SqlDataReader reader)
        {
            return new Transactions
            {
                TransactionID = reader.GetInt32(0),
                UserID = reader.GetInt32(1),
                UserName = reader.GetString(2),
                StockID = reader.GetInt32(3),
                StockName = reader.GetString(4),
                Quantity = reader.GetInt32(5),
                Price = reader.GetDecimal(6),
                TransactionType = (TransactionType)reader.GetInt32(7),
                TransactionDate = reader.GetDateTime(8)
            };
        }
    }
}
