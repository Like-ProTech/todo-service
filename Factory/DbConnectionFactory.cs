using Microsoft.Data.SqlClient;
using Todo_Service.Strategies;

namespace Todo_Service.Factory
{
    public class DbConnectionFactory
    {
        private readonly IEnumerable<IDbConnectionStrategy> _stratgies;
        public DbConnectionFactory(IEnumerable<IDbConnectionStrategy> connectionStrategies) {
            this._stratgies = connectionStrategies;    
        }

        public SqlConnection GetDatabaseConnection(string key)
        {
            var strategy = this._stratgies.FirstOrDefault(x => x.Key == key);
            if (strategy == null)
                throw new InvalidOperationException($"Invalide key : ${key} ; Not a strategy !");
            return strategy.GetConnection();
        }
    }
}
