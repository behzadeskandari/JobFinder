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

    public class GetAllLanguagesHandler : IRequestHandler<GetAllLanguagesQuery, List<JobFinder.Domain.Common.Entities.Language>>
    {
        private readonly IUnitOfWork _context;

        public GetAllLanguagesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.Language>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            var record  = await _context.LanguagesRepository.GetQueryable()
                 //.Include(l => l.ResumeId) // Include the related Resume
                .ToListAsync(cancellationToken);

            foreach (var item in record)
            {
                var resume = await _context.ResumeRepository.GetByIdAsync(item.ResumeId);
                if (resume != null)
                {
                    item.Resume = resume;
                }else
                {
                    item.Resume = new Domain.Common.Entities.Resume();
                }
            }

            return record;
        }
    }
}
