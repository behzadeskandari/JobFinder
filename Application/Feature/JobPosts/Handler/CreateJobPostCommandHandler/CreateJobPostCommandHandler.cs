using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.JobPosts.Commands.CreateJobPost;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.JobPosts.Handler.CreateJobPostCommandHandler
{
    public class CreateJobPostCommandHandler : IRequestHandler<CreateJobPostCommand, Guid>
    {
        private readonly IUnitOfWork _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateJobPostCommandHandler(IUnitOfWork context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateJobPostCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.CitiesRepository.GetByIdAsync(request.JobPost.CityId);
            var entity = new JobPost
            {
                Title = request.JobPost.Title,
                Description = request.JobPost.Description,
                Requirements = request.JobPost.Requirements,
                BenefitId = request.JobPost.BenefitsId,
                Location = request.JobPost.Location,
                Salary = request.JobPost.Salary,
                ExpiresAt = request.JobPost.ExpiresAt,
                StaffId = _currentUserService.UserId,
                IsActive = false,
                ApplicationCount = 0,
                MinimumEducationLevelDegree = request.JobPost.MinimumEducationLevelDegree,
                MinimumEducationLevelDescription = request.JobPost.MinimumEducationLevelDescription,
                MinimumEducationLevelField = request.JobPost.MinimumEducationLevelField,
                MinimumEducationLevelInstitution = request.JobPost.MinimumEducationLevelInstitution,
                Source = "Application",
                SyncStatus = "Not_Synced",
                CityId = city.Id,
            };

            await _context.JobPostsRepository.AddAsync(entity);
            await _context.CommitAsync(cancellationToken);

            return entity.Id;
        }
    }
}
