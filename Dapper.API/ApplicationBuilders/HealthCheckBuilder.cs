//using System.Linq;
//using System.Text.Json;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Configuration;
//using ToDo.API.Helpers;

//namespace ToDo.API.ApplicationBuilders
//{
//    // See https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.0
//    public static class HealthCheckBuilder
//    {
//        public static void UseHealthCheckBuilder(this IApplicationBuilder app, IConfiguration configuration) 
//        {
//            var healthCheckSettings = configuration.GetSection(nameof(HealthCheckSettings)).Get<HealthCheckSettings>();

//            var options = new HealthCheckOptions
//            {
//                AllowCachingResponses = false,
//                ResponseWriter = async (c, r) =>
//                {
//                    c.Response.ContentType = "application/json";
//                    var result = JsonSerializer.Serialize(new
//                    {
//                        status = r.Status.ToString(),
//                        errors = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
//                    });

//                    await c.Response.WriteAsync(result);
//                }
//            };
//            app.UseHealthChecks(healthCheckSettings.HealthCheckRelativeUrlPath, options);
//        }
//    }
//}
