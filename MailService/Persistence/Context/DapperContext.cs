using MySql.Data.MySqlClient;
using System.Data;

namespace MailService.Persistence.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_configuration.GetConnectionString("MySQL"));
    }
}
