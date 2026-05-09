using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Education.Command;
using MediatR;

namespace JobFinder.Application.Feature.Education.Handlers
{
    public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateEducationHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = new JobFinder.Domain.Common.Entities.Education
            {
                ResumeId = request.ResumeId,
                Degree = request.Degree,
                Institution = request.Institution,
                Field = request.Field,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.EducationsRepository.AddAsync(education);
            await _context.CommitAsync(cancellationToken);
            return education.Id;
        }
    }
}
