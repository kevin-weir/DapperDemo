using Microsoft.Extensions.DependencyInjection;
using Dapper.Repository;
using Dapper.Repository.Interfaces;

namespace Dapper.API.Services
{
    public static class RepositoryServices
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            // TODO Is this correct scoping
            services.AddScoped<ICustomerRespository, CustomerRespository>();
            services.AddScoped<ICountryRespository, CountryRespository>();
            services.AddScoped<IProvinceRespository, ProvinceRespository>();
        }
    }
}
