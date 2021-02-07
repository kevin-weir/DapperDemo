using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper.API.Services;
using Dapper.API.ApplicationBuilders;

namespace Dapper.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add SQL Server database service
            services.AddDatabaseService(Configuration);

            // Add repository services
            services.AddRepositoryServices();

            // Add AutoMapper service
            services.AddAutoMapperService();

            // Add ASP.NET Core controller services
            services.AddControllerServices();

            // Add Swagger OpenAPI service to document the API
            services.AddOpenApiService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseOpenApiBuilder();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
