using Npgsql;

namespace Dashboard.DbService
{
    public class NpgsqlDbService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public NpgsqlDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PlSqlConnection");
        }

        public void ExecuteNonQuery(string sql)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public NpgsqlDataReader ExecuteQuery(string sql)
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var command = new NpgsqlCommand(sql, connection);
            return command.ExecuteReader();
        }
     
    }
}
