namespace Api.Infrastructure
{
    using System.Data;

    public interface IDatabase : IDisposable
    {
        Task<int> ExecuteNonQuery(string query);
        Task<IDataReader> ExecuteReader(string query);
    }
}