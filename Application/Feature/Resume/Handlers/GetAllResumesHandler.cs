using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Resume.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class GetAllResumesHandler : IRequestHandler<GetAllResumesQuery, List<JobFinder.Domain.Common.Entities.Resume>>
    {
        private readonly IUnitOfWork _context;

        public GetAllResumesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.Resume>> Handle(GetAllResumesQuery request, CancellationToken cancellationToken)
        {
            return await _context.ResumeRepository.GetQueryable()
                .Include(r => r.WorkExperiences)
                .Include(r => r.Educations)
                .Include(r => r.Skills)
                .Include(r => r.Languages)
                .ToListAsync(cancellationToken);
        }
    }

}
