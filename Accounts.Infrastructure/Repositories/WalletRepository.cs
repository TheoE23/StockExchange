using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.Repositories
{
    public class WalletRepository : Repository<Wallets>,IWalletRepository
    {
        private readonly DatabaseConnections.DatabaseConnection _dbConnection;

        public WalletRepository(DatabaseConnections.DatabaseConnection dbConnection)
            : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
        protected override string TableName => "wallets";
        public  async Task CreateAsync(Wallets wallets)
        {
            using (var connection = _dbConnection.Connect())
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"INSERT INTO {TableName} (UserID, StockID, Quantity, PurchasePrice) VALUES (@UserID, @StockID, @Quantity, @PurchasePrice)", connection);
                command.Parameters.AddWithValue("@UserID", wallets.UserID);
                command.Parameters.AddWithValue("@StockID", wallets.StockID);
                command.Parameters.AddWithValue("@Quantity", wallets.Quantity);
                command.Parameters.AddWithValue("@PurchasePrice", wallets.PurchasePrice);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<decimal> GetTotalPurchasePriceForWalletAsync(int WalletID)
        {
            decimal totalPurchasePrice = 0;
            using (var connection = _dbConnection.Connect())
            {
                var sql = "SELECT SUM(PurchasePrice * Quantity) as TotalPurchasePrice FROM wallets WHERE WalletID = @WalletID GROUP BY WalletID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@WalletID", WalletID);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            totalPurchasePrice = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                        }
                    }
                }
            }
            return totalPurchasePrice;
        }
        public async Task UpdateAsync(Wallets wallets)
        {
            using (var connection = _dbConnection.Connect())
            {
                await connection.OpenAsync();
                var command = new SqlCommand($"UPDATE {TableName} SET UserID = @UserID, StockID = @StockID, Quantity = @Quantity, PurchasePrice = @PurchasePrice WHERE WalletID = @WalletID", connection);
                command.Parameters.AddWithValue("@WalletID", wallets.WalletID);
                command.Parameters.AddWithValue("@UserID", wallets.UserID);
                command.Parameters.AddWithValue("@StockID", wallets.StockID);
                command.Parameters.AddWithValue("@Quantity", wallets.Quantity);
                command.Parameters.AddWithValue("@PurchasePrice", wallets.PurchasePrice);
                await command.ExecuteNonQueryAsync();
            }
        }
        protected override Wallets Map(SqlDataReader reader)
        {
            return new Wallets
            {
                WalletID = reader.GetInt32(reader.GetOrdinal("WalletID")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                StockID = reader.GetInt32(reader.GetOrdinal("StockID")),
                StockName = reader.GetString(reader.GetOrdinal("StockName")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                PurchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice"))
            };
        }
        public async Task<IEnumerable<Wallets>> GetAllAsync()
        {
            List<Wallets> wallets = new List<Wallets>();

            using (var connection = _dbConnection.Connect())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var wallet = Map(reader);
                            wallets.Add(wallet);
                        }
                    }
                }
            }
            return wallets;
        }
        public async Task<Wallets> GetByIdAsync(int id)
        {
            Wallets wallet = null;

            using (var connection = _dbConnection.Connect())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE WalletID = @WalletID", connection))
                {
                    command.Parameters.AddWithValue("@WalletID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            wallet = Map(reader);
                        }
                    }
                }
            }
            return wallet;
        }

    }
}

