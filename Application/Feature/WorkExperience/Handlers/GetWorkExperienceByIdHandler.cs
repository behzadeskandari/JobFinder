using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.WorkExperience.Queries;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Handlers
{
    public class GetWorkExperienceByIdHandler : IRequestHandler<GetWorkExperienceByIdQuery, JobFinder.Domain.Common.Entities.WorkExperience>
    {
        private readonly IUnitOfWork _context;

        public GetWorkExperienceByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.WorkExperience> Handle(GetWorkExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.WorkExperiencesRepository.GetByIdAsync(request.Id);
            //.Include(we => we.Resume) // Include the related Resume
            //.FirstOrDefaultAsync(we => we.Id == request.Id, cancellationToken);

            var resume = await _context.ResumeRepository.GetByIdAsync(record.ResumeId);

            if (resume != null)
            {
                record.Resume = resume;
            }
            else
            {
                record.Resume = new Domain.Common.Entities.Resume();
            }

            return record;
        }
    }
}
