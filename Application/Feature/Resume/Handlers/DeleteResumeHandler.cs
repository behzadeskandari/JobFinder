using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Resume.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteResumeHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = await _context.ResumeRepository.GetByIdAsync(request.Id);
            if (resume == null)
            {

                throw new NotFoundException("رزومه مورد نظر پیدا نشد");
            }

            await _context.ResumeRepository.DeleteAsync(resume);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
