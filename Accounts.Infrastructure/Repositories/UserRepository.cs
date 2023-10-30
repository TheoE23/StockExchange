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
    public class UserRepository : BaseRepository<Users>
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "users";

        protected override Users Map(SqlDataReader reader)
        {
            return new Users
            {
                UserID = reader.GetInt32(0),
                UserName = reader.GetString(1),
                Email = reader.GetString(2),
                Birthday = reader.GetDateTime(3)
            };
        }
    }
}
