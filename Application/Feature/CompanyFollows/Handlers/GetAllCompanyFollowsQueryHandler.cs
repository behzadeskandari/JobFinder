using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CompanyFollows.Queries;
using JobFinder.Contracts.Dtos.CompanyFollows;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class GetAllCompanyFollowsQueryHandler : IRequestHandler<GetAllCompanyFollowsQuery, Result<IEnumerable<CompanyFollowDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCompanyFollowsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CompanyFollowDto>>> Handle(GetAllCompanyFollowsQuery request, CancellationToken cancellationToken)
        {
            var companyFollows = await _unitOfWork.CompanyFollowRepository
                .GetQueryable()
                .Include(cf => cf.Company)
                .Where(cf => cf.UserId == request.UserId && cf.IsActive == true && cf.Company.IsActive == true)
                .ToListAsync(cancellationToken);

            var companyFollowDtos = _mapper.Map<IEnumerable<CompanyFollowDto>>(companyFollows);
            return Result.Ok(companyFollowDtos);//
        }
    }
}
