using JobFinder.Domain.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    /// <summary>
    /// Returning The error for applications 
    /// </summary>
    [Route("error/[controller]")]
    public class ErrorController : ApiController
    {
        [HttpPost]
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? excption = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var (statusCode, message) = excption switch
            {
                // DuplicateEmailException => (StatusCodes.Status409Conflict, "Email Already Exists"),
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred"),
            };
            return Problem(statusCode: statusCode, title: message);
        }
    }
}
