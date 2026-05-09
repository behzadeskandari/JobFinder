using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Queries.GetCompaniesQuery;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, Result<PagedResult<CompanyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.companyRepository
                .GetQueryable()
                .Include(c => c.Industry)
               //.Include(c => c.City)
                .Where(c => c.IsActive == true);

            var criteria = request.SearchCriteria ?? new SearchCompaniesQueryDto();
            if (!string.IsNullOrEmpty(criteria.Name))
                query = query.Where(c => c.Name.Contains(criteria.Name));
            if (criteria.IndustryId.HasValue)
                query = query.Where(c => c.JobCategoryId == criteria.IndustryId);
            if (criteria.CityId.HasValue)
                query = query.Where(c => c.CityId == criteria.CityId);
            if (!string.IsNullOrEmpty(criteria.Size.ToString()))
                query = query.Where(c => c.Size == criteria.Size);
            //if (!string.IsNullOrEmpty(criteria.Benefits.Name)) {
            //    query = query.Select(c => c.Benefits.Where(x => x.Name == criteria.Benefits.Name));
            //}
            //:TODO Change so company benefits added based on the criteria 
            if (criteria.MinRating.HasValue)
                query = query.Where(c => c.Rating >= criteria.MinRating);
            var totalCount = await query.CountAsync(cancellationToken);
            var companies = await query.Take(50).ToListAsync(cancellationToken);
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);


            var result = new PagedResult<CompanyDto>
            {
                Items = companyDtos,
                TotalItems = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return Result.Ok(result);
        }
    }
}
