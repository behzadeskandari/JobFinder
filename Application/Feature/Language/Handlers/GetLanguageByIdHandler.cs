using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Language.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Language.Handlers
{
    public class GetLanguageByIdHandler : IRequestHandler<GetLanguageByIdQuery, JobFinder.Domain.Common.Entities.Language>
    {
        private readonly IUnitOfWork _context;

        public GetLanguageByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.Language> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            //return await _context.Languages
            //     .Include(l => l.Resume) // Include the related Resume
            //    .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
            var record = await _context.LanguagesRepository.GetQueryable()
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (record != null)
            {
                var resume = await _context.ResumeRepository.GetByIdAsync(record.ResumeId);
                if (resume != null)
                {
                    record.Resume = resume;
                }
                else
                {
                    record.Resume = new Domain.Common.Entities.Resume();
                }
            }

            return record;
        }
    }
}
