using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
 using System.Collections.Generic;
    using System.Data.SqlClient;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DTO;

namespace Accounts.Infrastructure.Repositories
{
    
        public abstract class Repository<T>
        {
            public string connectionString;

            public Repository(string _connectionString)
            {
                connectionString = _connectionString;
            }

            protected abstract string TableName { get; }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var entities = new List<T>();

                    while (await reader.ReadAsync())
                    {
                        entities.Add( Map(reader));
                    }

                    return entities;
                }
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return  Map(reader);
                        }
                    }
                }

                return default;
            }
        }

     

        public virtual async Task DeleteAsync(int ID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        protected abstract T Map(SqlDataReader reader);



    }

}




