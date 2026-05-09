using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.SavedJobs.Query;
using JobFinder.Contracts.Dtos.SavedJobs;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.SavedJobs.Handlers
{
    public class GetAllSavedJobsQueryHandler : IRequestHandler<GetAllSavedJobsQuery, Result<IEnumerable<SavedJobDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSavedJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SavedJobDto>>> Handle(GetAllSavedJobsQuery request, CancellationToken cancellationToken)
        {
            var savedJobs = await _unitOfWork.SavedJob
                .GetQueryable()
                .Include(sj => sj.Job)
                .Where(sj => sj.UserId == request.UserId && sj.IsActive == true && sj.Job.IsActive == true)
                .ToListAsync(cancellationToken);

            var savedJobDtos = _mapper.Map<IEnumerable<SavedJobDto>>(savedJobs);
            return Result.Ok(savedJobDtos);
        }
    }
}
