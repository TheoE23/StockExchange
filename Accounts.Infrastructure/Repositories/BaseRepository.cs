using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Accounts.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    namespace MyProject.Repositories
    {
        public abstract class BaseRepository<T>
        {
            private readonly string connectionString= "Server=localhost;Database=StockAccountSystem;User Id=sa; Password=sqldocker2022;";

            protected BaseRepository(string _connectionString)
            {
                connectionString = _connectionString;
            }

            protected abstract string TableName { get; }

            public virtual IEnumerable<T> GetAll()
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand($"SELECT * FROM {TableName}", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return Map(reader);
                        }
                    }
                }
            }

            public virtual T GetById(int id)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Map(reader);
                            }
                        }
                    }
                }

                return default;
            }

            public virtual void Add(T entity)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand($"INSERT INTO {TableName} VALUES (@value1, @value2, @value3)", connection))
                    {
                        command.Parameters.AddWithValue("@value1", "some value");
                        command.Parameters.AddWithValue("@value2", "some other value");
                        command.Parameters.AddWithValue("@value3", "yet another value");

                        command.ExecuteNonQuery();
                    }
                }
            }

            public virtual void Update<U>(int id, string columnName, U value)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand($"UPDATE {TableName} SET {columnName} = @value WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@value", value);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }

            public virtual void Delete(int id)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }

            protected abstract T Map(SqlDataReader reader);
        }
    }

}


