using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsSection.Command;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Handlers
{
    public class DeleteTermsSectionHandler : IRequestHandler<DeleteTermsSectionCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteTermsSectionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTermsSectionCommand request, CancellationToken cancellationToken)
        {
            var termsSection = await _context.TermsSectionsRepository.GetByIdAsync(request.Id);
            if (termsSection == null)
            {
                throw new NotFoundException("قسمت شرایط سرویس دادن به مشتری پیدا نشد");
            }

            await _context.TermsSectionsRepository.DeleteAsync(termsSection);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
