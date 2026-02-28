using Npgsql;
using SafarSync.API.Interfaces;
using System.Data;

namespace SafarSync.API.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException("Connection string not found.");
        }

        IDbConnection IDbConnectionFactory.CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
