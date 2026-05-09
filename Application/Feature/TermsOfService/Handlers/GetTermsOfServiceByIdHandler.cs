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
    public class GetTermsOfServiceByIdHandler : IRequestHandler<GetTermsOfServiceByIdQuery, JobFinder.Domain.Common.Entities.TermsOfService>
    {
        private readonly IUnitOfWork _context;

        public GetTermsOfServiceByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.TermsOfService> Handle(GetTermsOfServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var record =  await _context.TermsOfServicesRepository.GetQueryable()
                //.Include(tos => tos.Sections)
                .FirstOrDefaultAsync(tos => tos.Id == request.Id, cancellationToken);

            var termSection = await _context.TermsSectionsRepository.GetQueryable().FirstOrDefaultAsync(x => x.TermsOfServiceId == request.Id);
            record.Sections.Add(termSection);

            return record;
        }
    }
}
