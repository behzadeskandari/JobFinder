using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries;
using JobFinder.Contracts.Dtos.Job;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class GetSimilarJobsQueryHandler : IRequestHandler<GetSimilarJobsQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSimilarJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(GetSimilarJobsQuery request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.JobsRepository
                .GetQueryable()
                .Include(j => j.JobCategories)
                .FirstOrDefaultAsync(x=> x.Id == request.Id,cancellationToken);

            if (job == null || job.IsActive != true)
                throw new NotFoundException(" شغل پیدا نشد");

            var similarJobs = await _unitOfWork.JobsRepository
                .GetQueryable()
                .Include(j => j.Company)
                .Include(j => j.Cities)
                .Include(j => j.JobCategories)
                .Where(j => j.JobCategoryId == job.JobCategoryId && j.Id != request.Id && j.IsActive == true)
                .Take(5)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(similarJobs);
            return Result.Ok(jobDtos);
        }
    }
}
