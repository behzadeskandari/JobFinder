using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Customers.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Handlers.DeleteCustomerHandler
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository
        .GetQueryable()
        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null || customer.IsActive != true)
                throw new NotFoundException("مشتری پیدا نشد یا قبلاً حذف شده است");

            customer.IsActive = false;
            customer.DateModified = DateTime.Now;

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
