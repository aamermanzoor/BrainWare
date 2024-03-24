namespace Api.Infrastructure
{
    using System.Data;
    using System.Data.SqlClient;

    public class Database : IDatabase
    {
        private readonly SqlConnection _connection;

        public Database()
        {
            //var connectionString = "Data Source=LOCALHOST;Initial Catalog=BrainWare;Integrated Security=SSPI";
            // connection string should be in appsettings.Development.json and for deployment it (or server name and credentials) should
            // be injected exteranlly i.e. from deployment pipelines variable groups etc. and should be read from Configuration
            // Leaving it here for now.
            var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            var mdf = $@"{baseDirectory}\Api.Data\Data\BrainWare.mdf";
            var connectionString = $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename={mdf}";

            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public async Task<IDataReader> ExecuteReader(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);
            var reader = await sqlQuery.ExecuteReaderAsync();

            return reader;
        }

        public Task<int> ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQueryAsync();
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}