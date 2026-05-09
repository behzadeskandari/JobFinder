using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.WorkExperience.Command;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Handlers
{
    public class DeleteWorkExperienceHandler : IRequestHandler<DeleteWorkExperienceCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteWorkExperienceHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = await _context.WorkExperiencesRepository.GetByIdAsync(request.Id);
            if (workExperience == null)
            {
                throw new NotFoundException("تجربه کاری پیدا نشد");
            }

            await _context.WorkExperiencesRepository.DeleteAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
