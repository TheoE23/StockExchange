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

namespace Accounts.Infrastructure.Repositories
{
    public class UserRepository : Repository<Users>,IUserRepository
    {
        private readonly DatabaseConnections.DatabaseConnection _dbConnection;
        public UserRepository(DatabaseConnections.DatabaseConnection dbConnection)
            : base(dbConnection.ConnectionString)
        {
            _dbConnection = dbConnection;
        }
        protected override string TableName => "Users";
        public async Task CreateAsync(Users entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"INSERT INTO {TableName} (UserName, UserPassword, Email, Birthday) VALUES (@UserName, @UserPassword, @Email, @Birthday)", connection);
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
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
                var command = new SqlCommand($"UPDATE {TableName} SET UserName = @UserName, UserPassword = @UserPassword, Email = @Email, Birthday = @Birthday WHERE UserID = @UserID", connection);
                command.Parameters.AddWithValue("@UserID", users.UserID);
                command.Parameters.AddWithValue("@UserName", users.UserName);
                command.Parameters.AddWithValue("@userPassword", users.UserPassword);
                command.Parameters.AddWithValue("@Email", users.Email);
                command.Parameters.AddWithValue("@Birthday", users.Birthday != DateTime.MinValue ? users.Birthday : (object)DBNull.Value);
                await command.ExecuteNonQueryAsync();
            }
        }
        protected override Users Map(SqlDataReader reader)
        {
            return new Users
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"))
            };
        }
        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            List<Users> users = new List<Users>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = Map(reader);
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public async Task<Users> GetByIdAsync(int ID)
        {
            Users user = null;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", ID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = Map(reader);
                        }
                    }
                }
            }
            return user;
        }
        public async Task<Users> GetByUsername(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE UserName = @UserName", connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return Map(reader);
                        }
                    }
                }
            }
            return null;
        }
        public async Task<Users> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            Users user = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE UserName = @UserName AND UserPassword = @UserPassword", connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@UserPassword", password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = Map(reader);
                        }
                    }
                }
            }
            return user;
        
        }
    }
}
