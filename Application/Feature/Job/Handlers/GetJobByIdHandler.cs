using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobFinder.Domain.Common.Entities.Job>
    {
        private readonly IUnitOfWork _context;

        public GetJobByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.Job> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _context.JobsRepository.GetQueryable().FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);
            if (job == null)
            {
                return new Domain.Common.Entities.Job();
            }
            return job;
        }
    }
}
