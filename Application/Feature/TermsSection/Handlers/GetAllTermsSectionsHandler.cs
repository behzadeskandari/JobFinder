using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsSection.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.TermsSection.Handlers
{
    public class GetAllTermsSectionsHandler : IRequestHandler<GetAllTermsSectionsQuery, List<JobFinder.Domain.Common.Entities.TermsSection>>
    {
        private readonly IUnitOfWork _context;

        public GetAllTermsSectionsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.TermsSection>> Handle(GetAllTermsSectionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TermsSectionsRepository.GetQueryable().ToListAsync(cancellationToken);
        }
    }
}
