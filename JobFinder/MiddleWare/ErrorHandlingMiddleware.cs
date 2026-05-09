using System.Net;
using System.Text.Json;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Persistance.DatabaseContext.LogContext;

namespace JobFinder.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly IMiddleware _middleware;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly RequestDelegate _next;
        //ILogger logger,
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> looger)
        {
            _logger = looger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                DeleteFailureException => (int)HttpStatusCode.NotModified,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ForbiddenAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = ex.GetType().Name,
                Detail = ex.Message,
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = context.TraceIdentifier }
            };

            // Log to repository
            var unit = context.RequestServices.GetRequiredService<IUnitOfWork>();
            var log = new Logs
            {
                DateCreated = DateTime.Now,
                Detail = ex.Message,
                HttpContextRequest = context.Request.ToString(),
                HttpContextResponse = context.Response.ToString(),
                HttpContextUser = context.User?.Identity?.Name ?? "Anonymous",
                Instance = context.Request.Path,
                StatusCode = statusCode,
                Title = ex.GetType().Name,
                Type = ex.GetType().Name,
                IsActive = true,
                TraceId = context.TraceIdentifier,
                DateModified = DateTime.Now
            };
            unit.LogsRepository.AddLogs(log);
            await unit.CommitAsync();

            // Log to ExceptionLog
            var exceptionContext = context.RequestServices.GetRequiredService<ExceptionContext>();
            var exceptionLog = new ExceptionLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                ExceptionType = ex.GetType().FullName,
                DateCreated = DateTime.Now,
                ClassName = ex.TargetSite?.DeclaringType?.FullName,
                MethodName = ex.TargetSite?.Name
            };
            exceptionContext.ExceptionLog.Add(exceptionLog);
            await exceptionContext.SaveChangesAsync();

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }

}
