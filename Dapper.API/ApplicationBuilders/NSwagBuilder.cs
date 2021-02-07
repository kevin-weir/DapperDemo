//using System.Linq;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.Extensions.Configuration;
//using NSwag.AspNetCore;
//using ToDo.API.Helpers;

//namespace ToDo.API.ApplicationBuilders
//{
//    public static class NSwagBuilder
//    {
//        public static void UseNSwagBuilder(this IApplicationBuilder app, IConfiguration configuration, IApiVersionDescriptionProvider apiDescriptionProvider)
//        {
//            var nswagSettings = configuration.GetSection(nameof(NSwagSettings)).Get<NSwagSettings>();

//            foreach (var version in apiDescriptionProvider.ApiVersionDescriptions)
//            {
//                var documentName = nswagSettings.DocumentNamePrefix + version.GroupName;

//                app.UseOpenApi(settings =>
//                {
//                    settings.DocumentName = documentName;
//                    settings.Path = $"/openapi/{documentName}/openapi.json";
//                });
//            }

//            app.UseSwaggerUi3(settings =>
//            {
//                settings.DocExpansion = "list";

//                settings.OAuth2Client = new OAuth2ClientSettings
//                {
//                    ClientId = nswagSettings.ActiveDirectoryClientID,
//                    AppName = nswagSettings.Title
//                };

//                // build a swagger endpoint for each discovered API version
//                foreach (var version in apiDescriptionProvider.ApiVersionDescriptions.Reverse<ApiVersionDescription>())
//                {
//                    var documentName = nswagSettings.DocumentNamePrefix + version.GroupName;

//                    settings.SwaggerRoutes.Add(new SwaggerUi3Route(documentName, $"/openapi/{documentName}/openapi.json"));
//                }

//                // Define web UI route
//                settings.Path = "/openapi";
//            });
//        }
//    }
//}
