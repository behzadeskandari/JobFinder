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
using JobFinder.Application.Feature.JobRequest.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.JobRequest.Handler
{

    public class UpdateJobRequestHandler : IRequestHandler<UpdateJobRequestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _context;
        //private readonly IValidator<JobSeeker.Domain.Common.Entities.JobRequest> _validator;
        //IValidator<JobSeeker.Domain.Common.Entities.JobRequest> validator
        public UpdateJobRequestHandler(IUnitOfWork context)
        {
            _context = context;
            //_validator = validator;
        }

        public async Task<Result<bool>> Handle(UpdateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequest = await _context.JobRequestsRepository.GetByIdAsync(request.Id);
            if (jobRequest == null)
            {
                throw new NotFoundException($"درخواست کار با شناسه {request.Id} یافت نشد.");
            }

            jobRequest.CoverLetter = request.CoverLetter;
            jobRequest.ResumeUrl = request.ResumeUrl;
            jobRequest.Status = request.Status;
            jobRequest.DateModified = DateTime.Now;
            jobRequest.IsActive = request.IsActive;

            //ValidationResult validationResult = _validator.Validate(jobRequest);
            //if (!validationResult.IsValid)
            //{
            //    return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
            //}

            await _context.JobRequestsRepository.UpdateAsync(jobRequest);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok(true);
        }
    }
}
