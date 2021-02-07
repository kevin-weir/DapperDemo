//// This service uses the following Nuget packages:
//// NSwag.AspNetCore

//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using NSwag.Generation.AspNetCore;
//using NSwag.Generation.Processors;
//using NSwag.Generation.Processors.Contexts;
//using NSwag.Generation.Processors.Security;
//using NSwag;
//using ToDo.API.Helpers;
//using System;

//namespace ToDo.API.Services
//{
//    public static class NSwagService
//    {
//        public static void AddNSwagService(this IServiceCollection services, IConfiguration configuration)
//        {
//            var apiVersions = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
//            var nswagSettings = configuration.GetSection(nameof(NSwagSettings)).Get<NSwagSettings>();
//            var azureadSettings = configuration.GetSection(nameof(AzureAd)).Get<AzureAd>();

//            foreach (var version in apiVersions.ApiVersionDescriptions)
//            {
//                var documentName = nswagSettings.DocumentNamePrefix + version.GroupName;

//                services.AddOpenApiDocument(document =>
//                {
//                    document.DocumentName = documentName;
//                    document.ApiGroupNames = new[] { version.GroupName };

//                    document.AddSecurity(nswagSettings.SecuritySchemeName, Array.Empty<string>(), new OpenApiSecurityScheme
//                    {
//                        Description = nswagSettings.SecuritySchemeDescription,
//                        Type = OpenApiSecuritySchemeType.OAuth2,
//                        Flow = OpenApiOAuth2Flow.Implicit,

//                        AuthorizationUrl = azureadSettings.AuthorizationUrl,
//                        Scopes = new Dictionary<string, string>
//                        {
//                            [azureadSettings.Scopes] = "The resource we are accessing"
//                        }
//                    });
//                    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor(nswagSettings.SecuritySchemeName));
//                    document.OperationProcessors.Add(new Oauth2OperationProcessor());

//                    document.PostProcess = doc =>
//                    {
//                        doc.Info.Version = documentName;
//                        doc.Info.Title = nswagSettings.Title;
//                        doc.Info.Description = nswagSettings.Description;
//                        doc.Info.TermsOfService = nswagSettings.TermsOfService;

//                        doc.Info.Contact = new OpenApiContact
//                        {
//                            Email = nswagSettings.Contact.Email,
//                            Name = nswagSettings.Contact.Name
//                        };

//                        doc.Info.License = new OpenApiLicense
//                        {
//                            Url = nswagSettings.License.Url.ToString()
//                        };
//                    };

//                    document.OperationProcessors.Add(new ReponseCleanupOperationProcessor());
//                    document.OperationProcessors.Add(new APIVersionParameterOperationProcessor());
//                });
//            }
//        }
//    }

//    public class ReponseCleanupOperationProcessor : IOperationProcessor
//    {
//        public bool Process(OperationProcessorContext context)
//        {
//            var reponseCodes = new string[] { "200", "201", "204", "400" };

//            // Find response codes that are not in response code list and clear their content
//            var openApiReponses = from resp in context.OperationDescription.Operation.Responses
//                                    where reponseCodes.Contains(resp.Key.ToString()) == false
//                                    select resp.Value;

//            foreach (var response in openApiReponses)
//            {
//                response.Content.Clear();
//            }

//            // return false to exclude the operation from the document
//            return true;
//        }
//    }

//    public class APIVersionParameterOperationProcessor : IOperationProcessor
//    {
//        public bool Process(OperationProcessorContext context)
//        {
//            const string Header = "Header";
//            const string XVersion = "X-Version";

//            var headerParameters = 
//                from parms in context.OperationDescription.Operation.Parameters
//                where parms.Kind.ToString() == Header && parms.Name == XVersion && parms.Default == null
//                select parms;

//            if (headerParameters.Any())
//                {
//                    foreach (var parameter in headerParameters)
//                {
//                    // We need to Cast to the base class of context to get at the useful API GroupName (API Version)
//                    parameter.Default = ((AspNetCoreOperationProcessorContext)context).ApiDescription.GroupName;
//                }
//            }

//            // return false to exclude the operation from the document
//            return true;
//        }
//    }

//    public class Oauth2OperationProcessor : IOperationProcessor
//    {
//        public bool Process(OperationProcessorContext context)
//        {
//            var policies = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
//                .Union(context.MethodInfo.GetCustomAttributes(true))
//                .OfType<AuthorizeAttribute>()
//                .Where(attr => attr.Policy != null)
//                .Select(attr => attr.Policy).Distinct();

//            if (policies.Any())
//            {
//                var operationSummary = context.OperationDescription.Operation.Summary + " (Auth policies: ";
//                foreach (var policy in policies)
//                {
//                    operationSummary = operationSummary + policy + ", ";
//                }
//                operationSummary = operationSummary.Remove(operationSummary.Length - 2) + ")";

//                context.OperationDescription.Operation.Summary = operationSummary;
//            }

//            // return false to exclude the operation from the document
//            return true;
//        }
//    }
//}
