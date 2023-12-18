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
using System.Security.Principal;

namespace Accounts.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>,IAccountRepository
    {
        private readonly DatabaseConnections.DatabaseConnection _dbConnection;
        private readonly ICurrencyConverter _currencyConverter;
        public AccountRepository(DatabaseConnections.DatabaseConnection dbConnection, ICurrencyConverter currencyConverter)
            : base(dbConnection.ConnectionString)
        {
            _dbConnection = dbConnection;
            _currencyConverter = currencyConverter;
        }
        protected override string TableName => "accounts";
        public async Task CreateAsync(Account accounts)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"INSERT INTO {TableName} (UserID, AccountBalance, Currency) VALUES (@UserID, @AccountBalance, @Currency)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", accounts.UserID);
                    command.Parameters.AddWithValue("@AccountBalance", accounts.AccountBalance);
                    if (accounts.Currency != null)
                    {
                        command.Parameters.AddWithValue("@Currency", accounts.Currency);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Currency", DBNull.Value);
                    }

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<decimal> GetBalanceAsync(int UserID)
        {
            decimal totalBalance = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT SUM(AccountBalance) as TotalBalance FROM accounts WHERE UserID = @UserID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            totalBalance = await reader.IsDBNullAsync(0) ? 0 : reader.GetDecimal(0);
                        }
                    }
                }
            }
            return totalBalance;
        }
        public async Task UpdateAsync(Account accounts)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"UPDATE {TableName} SET UserID = @UserID, AccountBalance = @AccountBalance, Currency = @Currency WHERE AccountID = @AccountID", connection))
                {
                    command.Parameters.AddWithValue("@AccountID", accounts.AccountID);
                    command.Parameters.AddWithValue("@UserID", accounts.UserID);
                    command.Parameters.AddWithValue("@AccountBalance", accounts.AccountBalance);
                    if (accounts.Currency != null)
                    {
                        command.Parameters.AddWithValue("@Currency", accounts.Currency);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Currency", DBNull.Value);
                    }

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        protected override Account Map(SqlDataReader reader)
        {
            return new Account
            {
                AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                AccountBalance = reader.GetDecimal(reader.GetOrdinal("AccountBalance")),
                Currency = reader.IsDBNull(reader.GetOrdinal("Currency")) ? null : reader.GetString(reader.GetOrdinal("Currency"))
            };
        }
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            List<Account> accounts = new List<Account>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var account = Map(reader);
                            accounts.Add(account);
                        }
                    }
                }
            }

            return accounts;
        }
        public async Task<Account> GetByUserIdAsync(int UserID)
        {
            Account account = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE AccountID = @AccountID", connection))
                {
                    command.Parameters.AddWithValue("@AccountID", UserID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            account = Map(reader);
                        }
                    }
                }
            }

            return account;
        }

       
    }

}
