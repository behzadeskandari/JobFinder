using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CompanyFollows.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class DeleteCompanyFollowCommandHandler : IRequestHandler<DeleteCompanyFollowCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompanyFollowCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var companyFollow = await _unitOfWork.CompanyFollowRepository.GetByIdAsync(request.Id);

            if (companyFollow == null)
                throw new NotFoundException("دنبال کننده شرکت یافت نشد");

            companyFollow.IsActive = false; // Soft delete
            await _unitOfWork.CompanyFollowRepository.UpdateAsync(companyFollow);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
