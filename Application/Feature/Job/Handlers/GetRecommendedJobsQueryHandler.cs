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
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class GetRecommendedJobsQueryHandler : IRequestHandler<GetRecommendedJobsQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRecommendedJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(GetRecommendedJobsQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetQueryable()
                .Include(c => c.CandidateJobPreferences)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId,cancellationToken);

            if (candidate == null)
                throw new NotFoundException("کاندیدا پیدا نشد");

            var preferences = candidate.CandidateJobPreferences;
            var jobs = await _unitOfWork.CandidateRepository
                .GetQueryable()
                .Include(j => j.Skill)
                .Include(j => j.City)
                .Include(j => j.Job)
                .Include(j => j.Resume)
                .Include(j => j.User)
                .Include(j => j.Skill)
                .Include(j => j.PsychologyTestResult)
                .Include(j => j.PersonalityTestResult)
                .Include(j => j.CandidateJobPreferences)
                .Where(j => j.IsActive == true &&
                            (j.Job.JobCategories.Name.Contains(preferences.PreferredIndustry) ||
                             j.CityId == preferences.PreferredCityId))
                .Take(10)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(jobs);
            return Result.Ok(jobDtos);
        }
    }
}
