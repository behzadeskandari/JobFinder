using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobRequest.Commands;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Handler
{
    public class DeleteJobRequestHandler : IRequestHandler<DeleteJobRequestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _context;

        public DeleteJobRequestHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequest = await _context.JobRequestsRepository.GetByIdAsync(request.Id);
            if (jobRequest == null)
            {
                throw new NotFoundException($"درخواست کار با شناسه {request.Id} یافت نشد.");
            }

            await _context.JobRequestsRepository.DeleteAsync(jobRequest);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok(true);
        }
    }
}
