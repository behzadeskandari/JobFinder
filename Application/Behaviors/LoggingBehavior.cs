using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Start of the {typeof(TRequest).Name}");
            try
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogInformation("Handling {RequestName}", requestName);
                var stopwatch = Stopwatch.StartNew();
                var response = await next();
                stopwatch.Stop();
                _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms", requestName, stopwatch.ElapsedMilliseconds);
                _logger.LogInformation($"Handled {typeof(TRequest).Name} successfully");
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error handling {typeof(TRequest).Name}");
                throw ex;
            }
        }
    }
}
