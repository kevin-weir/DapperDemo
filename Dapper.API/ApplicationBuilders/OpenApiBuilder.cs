using Microsoft.AspNetCore.Builder;

namespace Dapper.API.ApplicationBuilders
{
    public static class OpenApiBuilder
    {
        public static void UseOpenApiBuilder(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper.API v1"));
        }
    }
}
