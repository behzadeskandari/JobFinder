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
    public class GetTopRatedCompaniesQueryHandler : IRequestHandler<GetTopRatedCompaniesQuery, Result<IEnumerable<CompanyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopRatedCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CompanyDto>>> Handle(GetTopRatedCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _unitOfWork.CompanyFollowRepository.GetQueryable()
                .Include(c => c.Company)
                .Where(c => c.IsActive == true)
                .OrderByDescending(c => c.Rating)
                .Take(10)
                .ToListAsync(cancellationToken);

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Result.Ok(companyDtos);//
        }
    }
}
