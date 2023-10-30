using Accounts.Domain.Entities;
using Accounts.Infrastructure.Repositories.MyProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.Repositories
{
    public class StockRepository : BaseRepository<Stocks>
    {
        public StockRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "stocks";

        protected override Stocks Map(SqlDataReader reader)
        {
            return new Stocks
            {
                StockID = reader.GetInt32(0),
                StockName = reader.GetString(1),
                CurrentPrice = reader.GetDecimal(2),
                Quantity = reader.GetInt32(3),
                Currency = reader.GetString(4)
            };
        }
    }
}
