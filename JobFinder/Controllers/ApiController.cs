using System.Security.Claims;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace JobFinder.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ISender _mediator;
        private string _user;
        private IUnitOfWork _unitOfWork;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
        protected string userName => _user ??= User.FindFirst(ClaimTypes.Name)?.Value;
        protected string userId => _user ??= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType)
        {
            return base.PhysicalFile(physicalPath, contentType);
        }
        protected ActionResult Problem(List<IError> errors)
        {
            if (errors.Count == 0)
            {
                return Problem();
            }

            if (errors.All(error => error is ValidationError))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        private ObjectResult Problem(IError error)
        {
            var statusCode = error switch
            {
                ConflictError => StatusCodes.Status409Conflict,
                ValidationError => StatusCodes.Status400BadRequest,
                NotFoundError => StatusCodes.Status404NotFound,
                UnauthorizedError => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Message);
        }

        private ActionResult ValidationProblem(List<IError> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
            int errorKeyCounter = 0;
            errors.ForEach(error => modelStateDictionary.AddModelError($"Error{errorKeyCounter++}", error.Message));

            return ValidationProblem(modelStateDictionary);
        }
    }

}
