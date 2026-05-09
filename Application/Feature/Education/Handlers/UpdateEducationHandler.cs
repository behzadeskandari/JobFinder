using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Education.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Education.Handlers
{
    public class UpdateEducationHandler : IRequestHandler<UpdateEducationCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateEducationHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.EducationsRepository.GetByIdAsync(request.Id);
            if (education == null)
            {
                throw new NotFoundException("برنامه تحصیلی پیدا نشد");
            }

            education.ResumeId = request.ResumeId;
            education.Degree = request.Degree;
            education.Institution = request.Institution;
            education.Field = request.Field;
            education.StartDate = request.StartDate;
            education.EndDate = request.EndDate;
            education.Description = request.Description;
            education.DateModified = DateTime.Now;
            education.IsActive = request.IsActive;

            await _context.EducationsRepository.UpdateAsync(education);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
