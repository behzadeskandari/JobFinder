using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Query;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.JobsRepository
                .GetQueryable()
                .Include(j => j.Company)
                .Include(j => j.Cities)
                .Include(j => j.JobCategories)
                .Include(j => j.TechnicalOptions)
                .Where(j => j.IsActive == true);

            var criteria = request.SearchCriteria;
            if (!string.IsNullOrEmpty(criteria.technicalOptions.ToString()))
                query = query.Where(j => j.TechnicalOptionsId == criteria.technicalOptions);
            if (!string.IsNullOrEmpty(criteria.jobCategory.ToString()))
                query = query.Where(j => j.JobCategoryId == criteria.jobCategory);
            if (criteria.city.HasValue)
                query = query.Where(j => j.CityId == criteria.city);
            if (!string.IsNullOrEmpty(criteria.province.ToString()))
                query = query.Where(j => j.Cities.ProvinceId == criteria.province);
            var totalCount = await query.CountAsync();
            //var jobs = await query.Take(50).ToListAsync(cancellationToken);
            var jobs = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            var result = new PagedResult<JobGetDto>
            {
                Items = jobDtos,
                TotalItems = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return Result.Ok(jobDtos);
        }
    }
}
