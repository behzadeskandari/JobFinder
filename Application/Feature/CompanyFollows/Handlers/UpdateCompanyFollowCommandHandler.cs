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
    public class UpdateCompanyFollowCommandHandler : IRequestHandler<UpdateCompanyFollowCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyFollowCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var companyFollow = await _unitOfWork.CompanyFollowRepository.GetByIdAsync(request.Id);

            if (companyFollow == null)
                throw new NotFoundException("دنبال کننده شرکت یافت نشد");

            var company = await _unitOfWork.CompanyFollowRepository.GetByIdAsync(request.CompanyId);

            if (company == null || company.IsActive != true)
                throw new NotFoundException("شرکت یافت نشد یا غیرفعال است");

            var user = await _unitOfWork.UsersRepository
                .GetByIdAsync(request.UserId);

            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

            companyFollow.CompanyId = request.CompanyId;
            companyFollow.UserId = request.UserId;
            companyFollow.IsActive = request.IsActive ?? companyFollow.IsActive;

            await _unitOfWork.CompanyFollowRepository.UpdateAsync(companyFollow);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
