using System.Data;
using Microsoft.Data.SqlClient;

namespace Dapper.Repository
{
    public class ConnectionFactory
    {
        public static IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
