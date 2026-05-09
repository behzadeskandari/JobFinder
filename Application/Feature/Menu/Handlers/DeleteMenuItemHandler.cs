using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Menu.Commands;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using FluentResults;
using JobFinder.Application.Repository;

namespace JobFinder.Application.Feature.Menu.Handlers
{
    public class DeleteMenuItemHandler : IRequestHandler<DeleteMenuItemCommand, Result<bool>>
    {
        private readonly IMenuRepository _unitOfWork;

        public DeleteMenuItemHandler(IMenuRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GetByIdAsync(request.Id);
            if (entity == null)

                throw new NotFoundException(nameof(MenuItem), request.Id);

            _unitOfWork.Delete(entity);
            _unitOfWork.save();
            
            return Result.Ok(true).WithSuccess("Customer deleted successfully");
          
        }
    }
}
