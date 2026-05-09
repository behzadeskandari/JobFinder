using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyJobsQuery;
using MediaBrowser.Model.Querying;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Job;
using Microsoft.EntityFrameworkCore;
using JobFinder.Domain.Common.Models;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetCompanyJobsQueryHandler : IRequestHandler<GetCompanyJobsQuery, Result<PagedResult<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<JobGetDto>>> Handle(GetCompanyJobsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.JobsRepository
                .GetQueryable()
                .Include(j => j.Company)
                .Include(j => j.Cities)
                .Include(j => j.JobCategories)
                .Where(j => j.CompanyId == request.CompanyId && j.IsActive == true);

            var totalCount = await query.CountAsync(cancellationToken);

            var jobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            var result = new PagedResult<JobGetDto>
            {
                Items = jobDtos,
                TotalItems = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result.Ok(result);
        }
    }
}
