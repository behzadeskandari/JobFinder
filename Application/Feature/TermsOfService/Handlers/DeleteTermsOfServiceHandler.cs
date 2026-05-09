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
    public class DeleteTermsOfServiceHandler : IRequestHandler<DeleteTermsOfServiceCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteTermsOfServiceHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTermsOfServiceCommand request, CancellationToken cancellationToken)
        {
            var termsOfService = await _context.TermsOfServicesRepository.GetByIdAsync(request.Id);
            if (termsOfService == null)
            {
                throw new NotFoundException("شرایط سرویس دادن به مشتری پیدا نشد");
            }
            var sections  =  await _context.TermsSectionsRepository.GetQueryable().FirstOrDefaultAsync(x => x.TermsOfServiceId == request.Id);
            if (sections == null)
            {
                throw new NotFoundException("توضیحات شرایط سرویس دادن به مشتری پیدا نشد");
            }

            await _context.TermsSectionsRepository.DeleteAsync(sections);
            await _context.TermsOfServicesRepository.DeleteAsync(termsOfService);
            
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
