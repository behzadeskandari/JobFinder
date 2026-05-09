using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Education.Command;
using MediatR;

namespace JobFinder.Application.Feature.Education.Handlers
{

    public class DeleteEducationHandler : IRequestHandler<DeleteEducationCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteEducationHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.EducationsRepository.GetByIdAsync(request.Id);
            if (education == null)
            {
                throw new NotFoundException("برنامه تحصیلی پیدا نشد");
            }

            await _context.EducationsRepository.DeleteAsync(education);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
