using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CandidateJobPreferences.Queries;
using JobFinder.Contracts.Dtos.Job;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Handlers
{
    public class GetJobPreferencesQueryHandler : IRequestHandler<GetJobPreferencesQuery, Result<IEnumerable<CandidateJobPreferenceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobPreferencesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CandidateJobPreferenceDto>>> Handle(GetJobPreferencesQuery request, CancellationToken cancellationToken)
        {
            var preferences = await _unitOfWork.candidateJobPreferences
                .GetQueryable()
                .Include(cjp => cjp.JobCategory)
                .Include(cjp => cjp.City)
                .Where(cjp => cjp.UserId == request.UserId && cjp.IsActive == true)
                .ToListAsync(cancellationToken);

            var preferenceDtos = _mapper.Map<IEnumerable<CandidateJobPreferenceDto>>(preferences);
            return Result.Ok(preferenceDtos);
        }
    }
}
