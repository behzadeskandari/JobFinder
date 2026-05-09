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
    public class UnfollowCompanyCommandHandler : IRequestHandler<UnfollowCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnfollowCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnfollowCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyFollow = await _unitOfWork.CompanyFollowRepository.FindAsync(cf => cf.CompanyId == request.CompanyId && cf.UserId == request.UserId && cf.IsActive == true);

            if (companyFollow == null)
                throw new NotFoundException("شرکت مورد نظر یافت نشد");

            foreach (var item in companyFollow)
            {
                item.IsActive = false;
            }
            await _unitOfWork.CompanyFollowRepository.UpdateRangeAsync(companyFollow);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
