using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Infrastructure.DTO;
using Accounts.Domain.Entities;
using System.Data.SqlClient;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DTO;

namespace Accounts.Infrastructure.Repositories
{
    public class UserRepository : Repository<UsersDTO>,IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Users";

        public async Task CreateAsync(Users entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"INSERT INTO {TableName} (UserName, Password, Email, Birthday) VALUES (@UserName, @Password, @Email, @Birthday)", connection);
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@Password", entity.UserPassword);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@Birthday", entity.Birthday != DateTime.MinValue ? entity.Birthday : (object)DBNull.Value);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Users users)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"UPDATE {TableName} SET UserName = @UserName, Password = @Password, Email = @Email, Birthday = @Birthday WHERE UserID = @UserID", connection);
                command.Parameters.AddWithValue("@UserID", users.UserID);
                command.Parameters.AddWithValue("@UserName", users.UserName);
                command.Parameters.AddWithValue("@Password", users.UserPassword);
                command.Parameters.AddWithValue("@Email", users.Email);
                command.Parameters.AddWithValue("@Birthday", users.Birthday != DateTime.MinValue ? users.Birthday : (object)DBNull.Value);
                await command.ExecuteNonQueryAsync();
            }
        }




        protected override UsersDTO Map(SqlDataReader reader)
        {
            return new UsersDTO
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"))
            };
        }
    }
}
