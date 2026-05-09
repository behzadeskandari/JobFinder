using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsSection.Command;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Handlers
{

    public class CreateTermsSectionHandler : IRequestHandler<CreateTermsSectionCommand, int>
    {
        private readonly IUnitOfWork _context;

        public CreateTermsSectionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTermsSectionCommand request, CancellationToken cancellationToken)
        {
            var termServiceRecord = await _context.TermsOfServicesRepository.GetByIdAsync(request.TermsOfServiceId);
            if (termServiceRecord == null) 
            {
                throw new NotFoundException("شرایط سرویس دادن به مشتری پیدا نشد");
            }
            else
            {
                var termsSection = new JobFinder.Domain.Common.Entities.TermsSection
                {
                    Title = request.Title,
                    Content = request.Content,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    TermsOfServiceId = request.TermsOfServiceId
                };

                await _context.TermsSectionsRepository.AddAsync(termsSection);
                await _context.CommitAsync(cancellationToken);
                return termsSection.Id;
            }
            
        }
    }
}
