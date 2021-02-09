using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
//using Dapper.Contrib.Extensions;

namespace Dapper.API.Services
{
    public static class DatabaseService
    {
        public static void AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO At some point remove the TableNameMapper code once Dapper problem resolved
            // Hopefully this is temporary.  This code prevents Dapper from pluralizing table names
            //SqlMapperExtensions.TableNameMapper = (type) => type.Name;

            // TODO Is this correct scoping
            services.AddScoped<IDbConnection>(db => new SqlConnection(
                    configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
