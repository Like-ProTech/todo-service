using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace Todo_Service.Strategies
{
    public interface IDbConnectionStrategy
    {
        string Key { get; }
        SqlConnection GetConnection();
    }
}
