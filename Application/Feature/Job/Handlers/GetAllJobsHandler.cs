using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Query;
using MediatR;

namespace JobFinder.Application.Feature.Job.Handlers
{

    public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, Result<List<JobFinder.Domain.Common.Entities.Job>>>
    {
        private readonly IUnitOfWork _context;

        public GetAllJobsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<List<JobFinder.Domain.Common.Entities.Job>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _context.JobsRepository.GetAllAsync(cancellationToken);
            return Result.Ok(jobs.ToList());
        }
    }
}
