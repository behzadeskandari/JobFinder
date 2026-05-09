using System.Diagnostics;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Common
{
    public class JobSekeerProblemDetailFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        private readonly Action<ProblemDetailsContext>? _configure;
        private readonly IServiceProvider _serviceProvider;

        public JobSekeerProblemDetailFactory(
            IOptions<ApiBehaviorOptions> options,
                IServiceProvider serviceProvider,
            IOptions<ProblemDetailsOptions>? problemDetailsOptions = null
           )
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
            _serviceProvider = serviceProvider;
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            var logs = new Logs
            {
                StatusCode = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
                DateCreated = DateTime.Now,
                IsActive = true,
            };
            if (httpContext != null)
            {
                logs.HttpContextUser = httpContext.User.ToString() ?? string.Empty;
                logs.HttpContextResponse = httpContext.Response.ToString() ?? string.Empty;
                logs.HttpContextRequest = httpContext.Request.ToString() ?? string.Empty;
                logs.TraceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            }
            unitOfWork.Logs.Add(logs);
            unitOfWork.SaveChangesAsync();

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            ArgumentNullException.ThrowIfNull(modelStateDictionary);

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }
        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            problemDetails.Extensions.Add("customProperty", problemDetails);
            _configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
        }
    }

}
