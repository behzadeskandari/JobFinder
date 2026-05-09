using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Query;
using JobFinder.Domain.Common.Entities;
using MediatR;
using JobFinder.Contracts.Dtos.Job;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class GetSavedJobsQueryHandler : IRequestHandler<GetSavedJobsQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSavedJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(GetSavedJobsQuery request, CancellationToken cancellationToken)
        {
            var savedJobs = await _unitOfWork.SavedJob
                .GetQueryable()
                .Include(sj => sj.Job)
                    .ThenInclude(j => j.Company)
                //.Include(sj => sj.Job)
                //    .ThenInclude(j => j.City)
                .Include(sj => sj.Job)
                    .ThenInclude(j => j.JobCategories)
                .Where(sj => sj.UserId == request.UserId && sj.IsActive == true && sj.Job.IsActive == true)
                .Select(sj => sj.Job)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(savedJobs);
            return Result.Ok(jobDtos);
        }
    }
}
