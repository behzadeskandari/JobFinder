using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobRequest.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Application.Feature.JobRequest.Handler
{

    public class CreateJobRequestHandler : IRequestHandler<CreateJobRequestCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _context;
        //private readonly IValidator<JobSeeker.Domain.Common.Entities.JobRequest> _validator;
        private readonly IAccountService _userManager;
        //IValidator<JobSeeker.Domain.Common.Entities.JobRequest> validator,
        public CreateJobRequestHandler(IUnitOfWork context,  IAccountService userManager)
        {
            _context = context;
            //_validator = validator;
            _userManager = userManager;
        }

        public async Task<Result<Guid>> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
        {
            // Verify User exists.
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"کاربری با شناسه {request.UserId} یافت نشد.");
            }

            var jobRequest = new JobFinder.Domain.Common.Entities.JobRequest
            {
                UserId = request.UserId,
                CoverLetter = request.CoverLetter,
                ResumeUrl = request.ResumeUrl,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true
            };

            //ValidationResult validationResult = _validator.Validate(jobRequest);
            //if (!validationResult.IsValid)
            //{
            //    return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
            //}

            await _context.JobRequestsRepository.AddAsync(jobRequest);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok(jobRequest.Id);
        }
    }
}
