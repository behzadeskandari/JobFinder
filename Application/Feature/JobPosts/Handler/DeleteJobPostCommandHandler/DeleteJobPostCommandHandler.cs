using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobPosts.Commands.DeleteJobPost;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Handler.DeleteJobPostCommandHandler
{
    public class DeleteJobPostCommandHandler : IRequestHandler<DeleteJobPostCommand>
    {
        private readonly IUnitOfWork _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteJobPostCommandHandler(IUnitOfWork context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteJobPostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobPostsRepository.GetQueryable()
                .FirstOrDefaultAsync(jp => jp.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobPost), request.Id);
            }

            // Only allow staff who created the job post or admins to delete it
            if (entity.StaffId != _currentUserService.UserId && !_currentUserService.IsInRole("Admin"))
            {
                throw new ForbiddenAccessException();
            }

            await _context.JobPostsRepository.DeleteAsync(entity);
            await _context.CommitAsync(cancellationToken);
            
        }
    }


}
