using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Language.Command;
using MediatR;

namespace JobFinder.Application.Feature.Language.Handlers
{

    public class DeleteLanguageHandler : IRequestHandler<DeleteLanguageCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteLanguageHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.LanguagesRepository.GetByIdAsync(request.Id);
            if (language == null)
            {
                throw new NotFoundException("زبان پیدا نشد");
            }

            await _context.LanguagesRepository.DeleteAsync(language);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
