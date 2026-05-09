using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Language.Command;
using MediatR;

namespace JobFinder.Application.Feature.Language.Handlers
{
    public class CreateLanguageHandler : IRequestHandler<CreateLanguageCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateLanguageHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new JobFinder.Domain.Common.Entities.Language
            {
                ResumeId = request.ResumeId,
                Name = request.Name,
                ProficiencyLevel = request.ProficiencyLevel,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.LanguagesRepository.AddAsync(language);
            await _context.CommitAsync(cancellationToken);
            return language.Id;
        }
    }
}
