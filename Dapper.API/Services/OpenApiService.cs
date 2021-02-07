using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Dapper.API.Services
{
    public static class OpenApiService
    {
        public static void AddOpenApiService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dapper.API", Version = "v1" });

                // TODO Fix pathing. Dont like how I need to be explicit
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.Repository.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.API.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.Domain.xml"));
            });
        }
    }
}
