using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsOfService.Command;
using MediatR;

namespace JobFinder.Application.Feature.TermsOfService.Handlers
{
    public class CreateTermsOfServiceHandler : IRequestHandler<CreateTermsOfServiceCommand, int>
    {
        private readonly IUnitOfWork _context;
        private readonly IMediator _mediator;

        public CreateTermsOfServiceHandler(IUnitOfWork context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateTermsOfServiceCommand request, CancellationToken cancellationToken)
        {
            var termsOfService = new JobFinder.Domain.Common.Entities.TermsOfService
            {
                Version = request.Version,
                LastUpdated = request.LastUpdated,
                DateCreated = DateTime.Now,
                IsActive = request.IsActive
            };

            var termDbRecord =  _context.TermsOfServicesRepository.AddAsync(termsOfService);
            var id = await _context.CommitAsync(cancellationToken);

            // Create the related sections.
            if (request.Sections != null)
            {
                foreach (var sectionCommand in request.Sections)
                {
                    var section = new JobFinder.Domain.Common.Entities.TermsSection
                    {
                        Title = sectionCommand.Title,
                        Content = sectionCommand.Content,
                        IsActive = true,
                        DateCreated = DateTime.Now,
                        TermsOfServiceId = id,
                    };
                    await _context.TermsSectionsRepository.AddAsync(section);
                }
                await _context.CommitAsync(cancellationToken);
            }

            return termsOfService.Id;
        }
    }
}
