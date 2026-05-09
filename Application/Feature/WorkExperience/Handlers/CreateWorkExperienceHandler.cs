using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.WorkExperience.Command;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Handlers
{
    public class CreateWorkExperienceHandler : IRequestHandler<CreateWorkExperienceCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateWorkExperienceHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = new JobFinder.Domain.Common.Entities.WorkExperience
            {
                ResumeId = request.ResumeId,
                JobTitle = request.JobTitle,
                CompanyName = request.CompanyName,
                IsCurrentJob = request.IsCurrentJob,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.WorkExperiencesRepository.AddAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return workExperience.Id;
        }
    }
}
