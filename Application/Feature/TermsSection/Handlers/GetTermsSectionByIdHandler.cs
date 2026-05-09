using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsSection.Queries;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Handlers
{
    public class GetTermsSectionByIdHandler : IRequestHandler<GetTermsSectionByIdQuery, JobFinder.Domain.Common.Entities.TermsSection>
    {
        private readonly IUnitOfWork _context;

        public GetTermsSectionByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.TermsSection> Handle(GetTermsSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var termsSection = await _context.TermsSectionsRepository.GetByIdAsync(request.Id);
            return termsSection;
        }
    }
}
