using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobRequest.Queries;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Handler
{
    public class GetAllJobRequestsHandler : IRequestHandler<GetAllJobRequestsQuery, Result<List<JobFinder.Domain.Common.Entities.JobRequest>>>
    {
        private readonly IUnitOfWork _context;

        public GetAllJobRequestsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<List<JobFinder.Domain.Common.Entities.JobRequest>>> Handle(GetAllJobRequestsQuery request, CancellationToken cancellationToken)
        {
            var jobRequests = await _context.JobRequestsRepository.GetAllAsync(cancellationToken);
            return Result.Ok(jobRequests.ToList());
        }
    }
}
