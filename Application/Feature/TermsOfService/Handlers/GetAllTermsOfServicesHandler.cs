using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsOfService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.TermsOfService.Handlers
{
    public class GetAllTermsOfServicesHandler : IRequestHandler<GetAllTermsOfServicesQuery, List<JobFinder.Domain.Common.Entities.TermsOfService>>
    {
        private readonly IUnitOfWork _context;

        public GetAllTermsOfServicesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.TermsOfService>> Handle(GetAllTermsOfServicesQuery request, CancellationToken cancellationToken)
        {
            return await _context.TermsOfServicesRepository.GetQueryable()
                .Include(tos => tos.Sections)
                .ToListAsync(cancellationToken);
        }
    }
}
