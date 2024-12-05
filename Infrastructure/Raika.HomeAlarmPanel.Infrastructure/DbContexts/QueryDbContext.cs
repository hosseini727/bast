using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Raika.HomeAlarmPanel.Infrastructure.DbContexts
{
    public class QueryDbContext
    {
        private readonly IConfiguration _configuration;
        public QueryDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("MainDbConnection"));
    }
}
