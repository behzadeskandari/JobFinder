using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsOfService.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.TermsOfService.Handlers
{
    public class UpdateTermsOfServiceHandler : IRequestHandler<UpdateTermsOfServiceCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateTermsOfServiceHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTermsOfServiceCommand request, CancellationToken cancellationToken)
        {
            var termsOfService = await _context.TermsOfServicesRepository.GetByIdAsync(request.Id);
            if (termsOfService == null)
            {
                throw new NotFoundException("قسمت شرایط سرویس دادن به مشتری پیدا نشد");
            }

            termsOfService.Version = request.Version;
            termsOfService.LastUpdated = request.LastUpdated;
            termsOfService.DateModified = DateTime.Now;
            termsOfService.IsActive = request.IsActive;
            List<JobFinder.Domain.Common.Entities.TermsSection> terms = new();
            foreach (var item in request.Sections)
            {
                var section = new JobFinder.Domain.Common.Entities.TermsSection()
                {
                    Content = item.Content,
                    Title = item.Title,
                };
                terms.Add(section);
            }
            await _context.TermsSectionsRepository.UpdateRangeAsync(terms);
            await _context.TermsOfServicesRepository.UpdateAsync(termsOfService);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
