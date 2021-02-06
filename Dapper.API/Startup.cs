using System.IO;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Dapper.Repository;
using Dapper.Repository.Interfaces;
using Dapper.API.Helpers;

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
            services.AddScoped<IDbConnection>(db => new SqlConnection(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRespository, CustomerRespository>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers()
                .AddFluentValidation(fv => {
                    fv.RegisterValidatorsFromAssemblyContaining<CustomerRespository>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                    fv.ImplicitlyValidateChildProperties = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dapper.API", Version = "v1" });

                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.Repository.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.API.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Dapper.Domain.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper.API v1"));
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
