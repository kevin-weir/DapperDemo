using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            if (context is null || context.Error is null)
            {
                return Problem();
            }
            else
            {
                return Problem(
                    detail: context.Error.StackTrace,
                    title: context.Error.Message);
            }
        }
    }
}
