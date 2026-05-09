using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Education.Query;
using MediatR;

namespace JobFinder.Application.Feature.Education.Handlers
{
    public class GetEducationByIdHandler : IRequestHandler<GetEducationByIdQuery, JobFinder.Domain.Common.Entities.Education>
    {
        private readonly IUnitOfWork _context;

        public GetEducationByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.Education> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.EducationsRepository.GetByIdAsync(request.Id); // Filter for active educations

            var resume = await _context.ResumeRepository.GetByIdAsync(record.ResumeId);
            if (resume != null)
            {
                record.Resume = resume;
            }
            else;
            {
                record.Resume = new Domain.Common.Entities.Resume();
            }
            return record;
        }
    }
}
