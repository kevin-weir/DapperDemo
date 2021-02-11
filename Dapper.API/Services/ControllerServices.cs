using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Dapper.Domain.Models;

namespace Dapper.API.Services
{
    public static class ControllerServices
    {
        public static void AddControllerServices(this IServiceCollection services)
        {
            // TODO Find a better way for FleuntValidation to find assembly with validation rules
            services.AddControllers()
               .AddFluentValidation(fv => {
                   fv.RegisterValidatorsFromAssemblyContaining<CustomerDtoInsert>();
                   fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                   fv.ImplicitlyValidateChildProperties = false;
               });
        }
    }
}
