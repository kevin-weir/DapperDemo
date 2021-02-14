using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Dapper.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.API.Services
{
    public static class ControllerServices
    {
        public static void AddControllerServices(this IServiceCollection services)
        {
            // TODO Find a better way for FleuntValidation to find assembly with validation rules
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            })

            .AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<CustomerPostDTO>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                fv.ImplicitlyValidateChildProperties = false;
            });
        }
    }
}
