using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(
            IUnitOfWork unitOfWork,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Skip transaction for queries
            if (typeof(TRequest).Name.EndsWith("Query"))
            {
                return await next();
            }

            _logger.LogInformation("Beginning transaction for {RequestType}", typeof(TRequest).Name);

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var response = await next();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation("Transaction committed for {RequestType}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during transaction for {RequestType}", typeof(TRequest).Name);
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new Exception(ex.Message);
            }
        }
    }
}
