using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Query;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class GetAppliedJobsQueryHandler : IRequestHandler<GetAppliedJobsQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppliedJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(GetAppliedJobsQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetQueryable()
                .FirstOrDefaultAsync(c => c.UserId == request.UserId,cancellationToken);

            if (candidate == null)
                throw new NotFoundException("کاندیدا پیدا نشد");

            var appliedJobs = await _unitOfWork.JobApplication.GetQueryable()
                
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Company)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Candidates)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Cities)
                .Where(ja => ja.CandidateId == candidate.Id && ja.IsActive == true && ja.Job.IsActive == true)
                .Select(ja => ja.Job)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(appliedJobs);
            return Result.Ok(jobDtos);
        }
    }
}
