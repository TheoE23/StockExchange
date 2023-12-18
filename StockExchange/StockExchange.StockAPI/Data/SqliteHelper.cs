using System.Data.SQLite;

namespace StockAPI.Data
{
    public class SqliteHelper
    {
        private readonly string _connectionString;

        public SqliteHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void ExecuteNonQuery(string query, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
