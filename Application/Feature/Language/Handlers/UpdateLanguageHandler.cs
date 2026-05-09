using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Language.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Language.Handlers
{
    public class UpdateLanguageHandler : IRequestHandler<UpdateLanguageCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateLanguageHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.LanguagesRepository.GetByIdAsync(request.Id);
            if (language == null)
            {
                throw new NotFoundException("زبان مورد نظر پیدا نشد");
            }

            language.ResumeId = request.ResumeId;
            language.Name = request.Name;
            language.ProficiencyLevel = request.ProficiencyLevel;
            language.DateModified = DateTime.Now;
            language.IsActive = request.IsActive;

            await _context.LanguagesRepository.UpdateAsync(language);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
