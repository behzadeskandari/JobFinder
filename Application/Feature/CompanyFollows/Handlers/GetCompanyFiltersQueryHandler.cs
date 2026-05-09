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
using JobFinder.Contracts.Dtos.CompanyBenefit;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class GetCompanyFiltersQueryHandler : IRequestHandler<GetCompanyFiltersQuery, Result<CompanyFiltersDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyFiltersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CompanyFiltersDto>> Handle(GetCompanyFiltersQuery request, CancellationToken cancellationToken)
        {
            var industries = await _unitOfWork.JobCategoryRepository.GetQueryable()
                .Select(i => new FilterOption { Id = i.Id, Name = i.Industry })
                .ToListAsync(cancellationToken);

            var cities = await _unitOfWork.CitiesRepository
                .GetQueryable()
                .Select(c => new FilterOption { Id = c.Id, Name = c.Label })
                .ToListAsync(cancellationToken);

            var sizes = await _unitOfWork.companyRepository.GetQueryable()
                .Where(c => c.IsActive == true)
                .Select(c => c.Size)
                .Distinct()
                .ToListAsync(cancellationToken);

            var benefits = await _unitOfWork.companyRepository.GetQueryable()
                .Where(c => c.IsActive == true && c.Benefits != null)
                .SelectMany(c => c.Benefits)
                .Distinct()
                .ToListAsync(cancellationToken);

            var filters = new CompanyFiltersDto
            {
                Industries = industries,
                Cities = cities,
                Sizes = sizes,
                Benefits = benefits.Select(b => new CompanyBenefitDto { Name = b.Name, Description = b.Description,   }).AsEnumerable()
            };

            return Result.Ok(filters);
        }
    }
}
