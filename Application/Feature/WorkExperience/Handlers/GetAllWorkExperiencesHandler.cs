using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.WorkExperience.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.WorkExperience.Handlers
{
    public class GetAllWorkExperiencesHandler : IRequestHandler<GetAllWorkExperiencesQuery, List<JobFinder.Domain.Common.Entities.WorkExperience>>
    {
        private readonly IUnitOfWork _context;

        public GetAllWorkExperiencesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.WorkExperience>> Handle(GetAllWorkExperiencesQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.WorkExperiencesRepository.GetAllAsync();
            //.Include(we => we.Resume) // Include the related Resume


            foreach (var item in record)
            {
                var resume = await _context.ResumeRepository.GetByIdAsync(item.ResumeId);

                if (resume != null)
                {
                    item.Resume = resume;
                }
                else
                {
                    item.Resume = new Domain.Common.Entities.Resume();
                }

            }
            return record.ToList();
        }
    }
}
