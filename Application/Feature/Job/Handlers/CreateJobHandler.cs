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

namespace JobFinder.Application.Feature.Job.Handlers
{

    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IUnitOfWork _context;
        //private readonly IValidator<JobSeeker.Domain.Common.Entities.Job> _validator;
        //, IValidator<JobSeeker.Domain.Common.Entities.Job> validator
        public CreateJobHandler(IUnitOfWork context)
        {
            _context = context;
            //_validator = validator;
        }

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.OrderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                request.OrderId = null;
            }
             var technicalOptions = await _context.TechnicalOptionsRepository.GetByIdAsync(request.TechnicalOptionsId);
            if (technicalOptions == null)
            {
                throw new NotFoundException("گزینه‌های فنی یافت نشد");
            }
             var feature = await _context.FeaturesRepository.GetByIdAsync(request.FeaturesId);
            if (feature == null)
            {
                request.FeaturesId = null;
            }
            var JobCategory = await _context.JobCategoryRepository.GetByIdAsync(request.JobCategoryId);
            if (JobCategory == null)
            {
                throw new NotFoundException("رده شغلی یافت نشد");
            }  var Company = await _context.companyRepository.GetByIdAsync(request.CompanyId);
            if (Company == null)
            {
                throw new NotFoundException("شرکت پیدا نشد");
            }

            var job = new JobFinder.Domain.Common.Entities.Job
            {
                Title = request.Title,
                Level = request.Level,
                CompanyId = request.CompanyId,
                IsProirity = request.IsProirity,
                JobType = request.JobType,
                JobDescription = request.JobDescription,
                JobRequirment = request.JobRequirment,
                //JobRequestsId = request.JobRequestsId,
                CityId = request.CityId,
                FeaturesId = request.FeaturesId,
                TechnicalOptionsId = request.TechnicalOptionsId,
                OrderId = request.OrderId,
                Order = order,
                JobCategoryId = request.JobCategoryId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
            };

            //ValidationResult validationResult = _validator.Validate(job);
            //if (!validationResult.IsValid)
            //{
            //    return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
            //}

            await _context.JobsRepository.AddAsync(job);
            await _context.CommitAsync(cancellationToken);
            return job.Id;
        }
    }

}
