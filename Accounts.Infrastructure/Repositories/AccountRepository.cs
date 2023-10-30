using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Infrastructure.DTO_s;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Repositories.MyProject.Repositories;
using System.Data.SqlClient;



namespace Accounts.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "accounts";

        protected override Account Map(SqlDataReader reader)
        {
            return new Account
            {
                AccountID = reader.GetInt32(0),
                UserID = reader.GetInt32(1),
                AccountBalance = reader.GetDecimal(2),
                Currency = reader.GetString(3)
            };
        }
    }
}
