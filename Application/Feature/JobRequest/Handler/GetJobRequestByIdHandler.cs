using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobRequest.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.JobRequest.Handler
{

    public class GetJobRequestByIdHandler : IRequestHandler<GetJobRequestByIdQuery, Result<JobFinder.Domain.Common.Entities.JobRequest>>
    {
        private readonly IUnitOfWork _context;

        public GetJobRequestByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<JobFinder.Domain.Common.Entities.JobRequest>> Handle(GetJobRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var jobRequest = await _context.JobRequestsRepository.GetQueryable().FirstOrDefaultAsync(jr => jr.Id == request.Id, cancellationToken);
            if (jobRequest == null)
            {
                throw new NotFoundException($"درخواست کار با شناسه {request.Id} یافت نشد.");
            }
            return Result.Ok(jobRequest);
        }
    }
}
