using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{

    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, bool>
    {
        private readonly IUnitOfWork _context;
        //private readonly IValidator<JobSeeker.Domain.Common.Entities.Job> _validator;
        //IValidator<JobSeeker.Domain.Common.Entities.Job> validator
        public UpdateJobHandler(IUnitOfWork context)
        {
            _context = context;
            //_validator = validator;
        }

        public async Task<bool> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.JobsRepository.GetByIdAsync(request.Id);
            if (job == null)
            {
                throw new NotFoundException("شغل پیدا نشد");
            }
            job.Title = request.Title;
            job.Level = request.Level;
            job.CompanyId = request.CompanyId;
            job.IsProirity = request.IsProirity;
            job.JobType = request.JobType;
            job.JobDescription = request.JobDescription;
            job.JobRequirment = request.JobRequirment;
            //job.JobRequestsId = request.JobRequestsId;
            job.CityId = request.CityId;
            job.FeaturesId = request.FeaturesId;
            job.TechnicalOptionsId = request.TechnicalOptionsId;
            job.OrderId = request.OrderId;
            job.JobCategoryId = request.JobCategoryId;
            job.IsActive = request.IsActive;
            job.DateModified = DateTime.Now;

            //ValidationResult validationResult = _validator.Validate(job);
            //if (!validationResult.IsValid)
            //{
            //    return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
            //}

            await _context.JobsRepository.UpdateAsync(job);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
