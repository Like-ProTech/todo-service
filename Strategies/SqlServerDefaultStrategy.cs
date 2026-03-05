using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Todo_Service.Options;

namespace Todo_Service.Strategies
{
    public class SqlServerDefaultStrategy : IDbConnectionStrategy
    {
        public string Key => "main";
        private readonly string _connectionString;
        public SqlServerDefaultStrategy(IOptions<DefaultConnectionOption> options) {
            this._connectionString = options.Value.DefaultConnection;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}
