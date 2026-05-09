using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.TermsSection.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.TermsSection.Handlers
{
    public class UpdateTermsSectionHandler : IRequestHandler<UpdateTermsSectionCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateTermsSectionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTermsSectionCommand request, CancellationToken cancellationToken)
        {
            var termsSection = await _context.TermsSectionsRepository.GetByIdAsync(request.Id);
            if (termsSection == null)
            {
                throw new NotFoundException("قسمت شرایط سرویس دادن به مشتری پیدا نشد");
            }

            termsSection.Title = request.Title;
            termsSection.Content = request.Content;
            termsSection.DateModified = DateTime.Now;
            termsSection.IsActive = request.IsActive;

            await _context.TermsSectionsRepository.UpdateAsync(termsSection);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
