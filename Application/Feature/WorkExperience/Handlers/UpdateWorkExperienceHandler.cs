using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.WorkExperience.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.WorkExperience.Handlers
{

    public class UpdateWorkExperienceHandler : IRequestHandler<UpdateWorkExperienceCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateWorkExperienceHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = await _context.WorkExperiencesRepository.GetByIdAsync(request.Id);
            if (workExperience == null)
            {
                throw new NotFoundException("تجربه کاری پیدا نشد");
            }

            workExperience.ResumeId = request.ResumeId;
            workExperience.JobTitle = request.JobTitle;
            workExperience.CompanyName = request.CompanyName;
            workExperience.IsCurrentJob = request.IsCurrentJob;
            workExperience.Description = request.Description;
            workExperience.DateModified = DateTime.Now;
            workExperience.IsActive = request.IsActive;

            await _context.WorkExperiencesRepository.UpdateAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
