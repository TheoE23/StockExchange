using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DTO;

namespace Accounts.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<TransactionsDTO>,ITransactionRepository
    {
        public TransactionRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Transactions";

        public IEnumerable<Transactions> GetTransactionsByType(string transactionType)
        {
            var transactions = new List<Transactions>();
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Transactions WHERE TransactionType = @transactionType";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@transactionType", transactionType);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(MapToEntity(Map(reader)));
                        }
                    }
                }
            }
            return transactions;
        }

        protected override TransactionsDTO Map(SqlDataReader reader)
        {
            return new TransactionsDTO
            {
                TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? null : reader.GetString(reader.GetOrdinal("UserName")),
                StockID = reader.GetInt32(reader.GetOrdinal("StockID")),
                StockName = reader.IsDBNull(reader.GetOrdinal("StockName")) ? null : reader.GetString(reader.GetOrdinal("StockName")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                TransactionType = reader.IsDBNull(reader.GetOrdinal("TransactionType")) ? null : reader.GetString(reader.GetOrdinal("TransactionType")),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
            };
        }
       
            
            private Transactions MapToEntity(TransactionsDTO dto)
            {
                return new Transactions
                {
                    TransactionID = dto.TransactionID,
                    UserID = dto.UserID,
                    UserName = dto.UserName,
                    StockID = dto.StockID,
                    StockName = dto.StockName,
                    Quantity = dto.Quantity,
                    Price = dto.Price,
                    TransactionType = dto.TransactionType,
                    TransactionDate = dto.TransactionDate
                };
            }

            private TransactionsDTO MapToDTO(Transactions entity)
            {
                return new TransactionsDTO
                {
                    TransactionID = entity.TransactionID,
                    UserID = entity.UserID,
                    UserName = entity.UserName,
                    StockID = entity.StockID,
                    StockName = entity.StockName,
                    Quantity = entity.Quantity,
                    Price = entity.Price,
                    TransactionType = entity.TransactionType,
                    TransactionDate = entity.TransactionDate
                };
            }

        public async Task CreateAsync(Transactions transactions)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"INSERT INTO {TableName} (UserID, UserName, StockID, StockName, Quantity, Price, TransactionType, TransactionDate) VALUES (@UserID, @UserName, @StockID, @StockName, @Quantity, @Price, @TransactionType, @TransactionDate)", connection);
                command.Parameters.AddWithValue("@UserID", transactions.UserID);
                command.Parameters.AddWithValue("@UserName", transactions.UserName);
                command.Parameters.AddWithValue("@StockID", transactions.StockID);
                command.Parameters.AddWithValue("@StockName", transactions.StockName);
                command.Parameters.AddWithValue("@Quantity", transactions.Quantity);
                command.Parameters.AddWithValue("@Price", transactions.Price);
                command.Parameters.AddWithValue("@TransactionType", transactions.TransactionType);
                command.Parameters.AddWithValue("@TransactionDate", transactions.TransactionDate);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Transactions transactions)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"UPDATE {TableName} SET UserID = @UserID, UserName = @UserName, StockID = @StockID, StockName = @StockName, Quantity = @Quantity, Price = @Price, TransactionType = @TransactionType, TransactionDate = @TransactionDate WHERE TransactionID = @TransactionID", connection);
                command.Parameters.AddWithValue("@TransactionID", transactions.TransactionID);
                command.Parameters.AddWithValue("@UserID", transactions.UserID);
                command.Parameters.AddWithValue("@UserName", transactions.UserName);
                command.Parameters.AddWithValue("@StockID", transactions.StockID);
                command.Parameters.AddWithValue("@StockName", transactions.StockName);
                command.Parameters.AddWithValue("@Quantity", transactions.Quantity);
                command.Parameters.AddWithValue("@Price", transactions.Price);
                command.Parameters.AddWithValue("@TransactionType", transactions.TransactionType);
                command.Parameters.AddWithValue("@TransactionDate", transactions.TransactionDate);
                await command.ExecuteNonQueryAsync();
            }
        }

    }


    }

