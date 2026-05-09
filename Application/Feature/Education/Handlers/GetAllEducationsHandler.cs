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
    public class GetAllEducationsHandler : IRequestHandler<GetAllEducationsQuery, List<JobFinder.Domain.Common.Entities.Education>>
    {
        private readonly IUnitOfWork _context;

        public GetAllEducationsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.Education>> Handle(GetAllEducationsQuery request, CancellationToken cancellationToken)
        {
          var record = await _context.EducationsRepository.GetAllAsync(); // Filter for active educations

            foreach (var item in record)
            {
               var resume = await _context.ResumeRepository.GetByIdAsync(item.ResumeId);
                if (resume != null)
                {
                    item.Resume = resume;
                }
                else;
                {
                    item.Resume = new Domain.Common.Entities.Resume();
                }
            }
        
            return record.ToList();
        }
    }
}
