using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobPosts.Commands.UpdateJobPost;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Handler.UpdateJobPostCommandHandler
{
    public class UpdateJobPostCommandHandler : IRequestHandler<UpdateJobPostCommand>
    {
        private readonly IUnitOfWork _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateJobPostCommandHandler(IUnitOfWork context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateJobPostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobPostsRepository.GetQueryable()
                .FirstOrDefaultAsync(jp => jp.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobPost), request.Id);
            }

            // Only allow staff who created the job post or admins to update it
            if (entity.StaffId != _currentUserService.UserId && !_currentUserService.IsInRole("Admin"))
            {
                throw new ForbiddenAccessException();
            }

            entity.Title = request.JobPost.Title;
            entity.Description = request.JobPost.Description;
            entity.Requirements = request.JobPost.Requirements;
            entity.BenefitId = request.JobPost.BenefitId;
            entity.Location = request.JobPost.Location;
            entity.Salary = request.JobPost.Salary;
            entity.ExpiresAt = request.JobPost.ExpiresAt;
            entity.IsActive = request.JobPost.IsActive;

            await _context.JobPostsRepository.UpdateAsync(entity);
            await _context.CommitAsync(cancellationToken);
        }
    }
}
