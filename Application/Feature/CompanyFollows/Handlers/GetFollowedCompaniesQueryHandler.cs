using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CompanyFollows.Queries;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class GetFollowedCompaniesQueryHandler : IRequestHandler<GetFollowedCompaniesQuery, Result<IEnumerable<CompanyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFollowedCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CompanyDto>>> Handle(GetFollowedCompaniesQuery request, CancellationToken cancellationToken)
        {
            var followedCompanies = await _unitOfWork.CompanyFollowRepository
                .GetQueryable()
                .Include(cf => cf.Company)
                    .ThenInclude(c => c.Industry)
                .Include(cf => cf.Company)
                   // .ThenInclude(c => c.City)
                .Where(cf => cf.UserId == request.UserId && cf.IsActive == true && cf.Company.IsActive == true)
                .Select(cf => cf.Company)
                .ToListAsync(cancellationToken);

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(followedCompanies);
            return Result.Ok(companyDtos);//
        }
    }
}
