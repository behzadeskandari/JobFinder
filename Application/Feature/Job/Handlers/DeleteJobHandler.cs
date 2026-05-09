using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Command;
using MediatR;

namespace JobFinder.Application.Feature.Job.Handlers
{

    public class DeleteJobHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteJobHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.JobsRepository.GetByIdAsync(request.Id);
            if (job == null)
            {
                return await Task.FromResult(false);
            }

            await _context.JobsRepository.DeleteAsync(job);
            await _context.CommitAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
