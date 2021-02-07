//// API Versioning makes use of the following Nuget packages:
//// Microsoft.AspNetCore.Mvc.Versioning
//// Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
 
//// Refer to:
//// See https://github.com/microsoft/aspnet-api-versioning/wiki

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Versioning;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using ToDo.API.Helpers;

//namespace ToDo.API.Services
//{
//    public static class ApiVersioningService
//    {
//        public static void AddApiVersioningService(this IServiceCollection services, IConfiguration configuration)
//        {
//            var apiVersionSettings = configuration.GetSection(nameof(ApiVersionSettings)).Get<ApiVersionSettings>();

//            services.AddApiVersioning(options =>
//            {
//                options.DefaultApiVersion = new ApiVersion(apiVersionSettings.DefaultMajorVersion, apiVersionSettings.DefaultMinorVersion);
//                options.AssumeDefaultVersionWhenUnspecified = apiVersionSettings.AssumeDefaultVersionWhenUnspecified;
//                options.ApiVersionReader = new HeaderApiVersionReader(apiVersionSettings.HeaderVariable);
//                options.ReportApiVersions = apiVersionSettings.ReportApiVersions;
//            });

//            services.AddVersionedApiExplorer(options => 
//            {
//                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
//                // note: the specified format code will format the version as "'v'major[.minor][-status]"
//                options.GroupNameFormat = apiVersionSettings.GroupNameFormat;

//                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
//                // can also be used to control the format of the API version in route templates
//                options.SubstituteApiVersionInUrl = apiVersionSettings.SubstituteApiVersionInUrl;
//            });
//        }
//    }
//}
