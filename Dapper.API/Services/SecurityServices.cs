//// Security added to this project depends on Microsoft.Identity.Web project found here:  Microsoft is working on converting it to a Nuget Package.  
//// It uses Nuget package Microsoft.Identity among others.
//// https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/master/Microsoft.Identity.Web


//// TODO: Update AD security code
//// Updated
//// https://github.com/AzureAD/microsoft-identity-web
//// https://github.com/AzureAD/microsoft-identity-web/wiki
//// https://www.nuget.org/packages/Microsoft.Identity.Web


//// Refer to:
//// https://docs.microsoft.com/en-ca/azure/active-directory-b2c/tutorial-create-tenant
//// https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-overview
//// https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-overview
//// https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon
//// https://github.com/Azure-Samples/active-directory-dotnetcore-daemon-v2 (sample for above link)
//// https://stackoverflow.com/questions/57379397/why-is-application-permissions-disabled-in-azure-ads-request-api-permissions
//// https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-3.0

//using System.Linq;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authentication.AzureAD.UI;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Identity.Web;
//using ToDo.API.Helpers;

//namespace ToDo.API.Services
//{
//    public static class SecurityServices
//    {
//        public static void AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            var oauth2Settings = configuration.GetSection(nameof(Oauth2Settings)).Get<Oauth2Settings>();

//            // This is required to be instantiated before the OpenIdConnectOptions starts getting configured.
//            // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
//            // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
//            // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token
//            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

//            // Note: AddProtectedWebApi expects an AzureAd section to exist in appsettings.json with the following values:
//            // Instance, ClientId, Domain and TenantId.  This extension method is located in the Microsoft.Identity.Web project.
//            //services.AddProtectedWebApi(configuration);

//            //services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
//            //{
//            //    options.TokenValidationParameters.RoleClaimType = "roles";
//            //});

//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                    .AddMicrosoftIdentityWebApi(configuration);

//            //services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
//            services.Configure<JwtBearerOptions>(options =>
//            {
//                options.TokenValidationParameters.RoleClaimType = "roles";
//            });

//            // Add authorization service including Oauth2 roles and policies
//            services.AddAuthorization(options =>
//            {
//                foreach (var policy in oauth2Settings.Oauth2Policies)
//                {
//                    string[] roles = policy.Roles.Select(x => x.Name).ToArray();
//                    options.AddPolicy(policy.PolicyName, policy => policy.RequireRole(roles));
//                }
//            });
//        }
//    }
//}
