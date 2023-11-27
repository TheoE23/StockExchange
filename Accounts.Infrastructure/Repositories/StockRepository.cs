using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.Repositories
{
    public class StockRepository : Repository<Stocks>,IStockRepository
    {
        private readonly DatabaseConnections.DatabaseConnection _dbConnection;
        private readonly ICurrencyConverter _currencyConverter;
        public StockRepository(DatabaseConnections.DatabaseConnection dbConnection, ICurrencyConverter currencyConverter)
            : base(dbConnection.ConnectionString)
        {
            _dbConnection = dbConnection;
            _currencyConverter = currencyConverter;
        }
        protected override string TableName => "stocks";
        public async Task UpdateAsync(Stocks stocks)
        {
             using (var connection = new SqlConnection(connectionString))
             {
                connection.Open();
                using (var command = new SqlCommand($"UPDATE {TableName} SET StockName = @StockName, CurrentPrice = @CurrentPrice, Quantity = @Quantity, Currency = @Currency WHERE StockID = @StockID", connection))
                {
                    command.Parameters.AddWithValue("@StockID", stocks.StockID);
                    command.Parameters.AddWithValue("@StockName", stocks.StockName);
                    command.Parameters.AddWithValue("@CurrentPrice", stocks.CurrentPrice);
                    command.Parameters.AddWithValue("@Quantity", stocks.Quantity);
                    if (stocks.Currency != null)
                    {
                        command.Parameters.AddWithValue("@Currency", stocks.Currency);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Currency", DBNull.Value);
                    }
                    await command.ExecuteNonQueryAsync();
                }
             }
        }
        public async Task<Stocks> GetStockByNameAsync(string StockName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE StockName = @StockName", connection))
                {
                    command.Parameters.AddWithValue("@StockName", StockName);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var StocksDTO = MapToDTO(reader);
                            return MapToEntity(StocksDTO);
                        }
                    }
                }
                return null;
            }
        }
        protected override Stocks Map(SqlDataReader reader)
        {
            return new Stocks
            {
                StockID = reader.GetInt32(reader.GetOrdinal("StockID")),
                StockName = reader.GetString(reader.GetOrdinal("StockName")),
                CurrentPrice = reader.GetDecimal(reader.GetOrdinal("CurrentPrice")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                Currency = reader.GetString(reader.GetOrdinal("Currency"))
            };
        }
        private StocksDTO MapToDTO(SqlDataReader reader)
        {
            return new StocksDTO
            {
                StockID = reader.GetInt32(reader.GetOrdinal("StockID")),
                StockName = reader.GetString(reader.GetOrdinal("StockName")),
                CurrentPrice = reader.GetDecimal(reader.GetOrdinal("CurrentPrice")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                Currency = reader.IsDBNull(reader.GetOrdinal("Currency")) ? null : reader.GetString(reader.GetOrdinal("Currency"))
            };
        }
        private Stocks MapToEntity(StocksDTO dto)
        {
            return new Stocks
            {
                StockID = dto.StockID,
                StockName = dto.StockName,
                CurrentPrice = dto.CurrentPrice,
                Quantity = dto.Quantity,
                Currency = dto.Currency
            };
        }
        public async Task CreateAsync(Stocks stocks)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"INSERT INTO {TableName} (StockName, CurrentPrice, Quantity, Currency) VALUES (@StockName, @CurrentPrice, @Quantity, @Currency)", connection))
                {
                    command.Parameters.AddWithValue("@StockName", stocks.StockName);
                    command.Parameters.AddWithValue("@CurrentPrice", stocks.CurrentPrice);
                    command.Parameters.AddWithValue("@Quantity", stocks.Quantity);
                    if (stocks.Currency != null)
                    {
                        command.Parameters.AddWithValue("@Currency", stocks.Currency);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Currency", DBNull.Value);
                    }

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
