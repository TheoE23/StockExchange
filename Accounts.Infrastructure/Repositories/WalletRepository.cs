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
    public class WalletRepository : BaseRepository<Wallet>
    {
        public WalletRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "wallets";

        protected override Wallet Map(SqlDataReader reader)
        {
            return new Wallet
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                UserName = reader.GetString(2),
                StockId = reader.GetInt32(3),
                StockName = reader.GetString(4),
                Quantity = reader.GetInt32(5),
                PurchasePrice = reader.GetDecimal(6)
            };
        }
    }
}

