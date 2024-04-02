using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MEJORA.Infrastructure.Context
{
    public class ApplicationDdContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationDdContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DBConnection")!;
        }

        public IDbConnection CreateConnection => new SqlConnection(_connectionString);
    }
}
