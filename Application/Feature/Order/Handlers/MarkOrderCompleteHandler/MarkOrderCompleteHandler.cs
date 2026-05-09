using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Order.Command.MarkOrderCompleteCommand;
using JobFinder.Application.Repository.Invoice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Handlers.MarkOrderCompleteHandler
{
    public class MarkOrderCompleteHandler : IRequestHandler<MarkOrderCompleteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkOrderCompleteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(MarkOrderCompleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result =  _unitOfWork.OrderRepository.MarkFulfilled(request.Id);

                if (result.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    throw new NotFoundException(result.Errors.ToString());
                }
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
