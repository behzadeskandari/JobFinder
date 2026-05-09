using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobOffer.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Application.Feature.JobOffer.Handler
{

    //public class CreateJobOfferHandler : IRequestHandler<CreateJobOfferCommand, Result<int>>
    //{
    //    private readonly IUnitOfWork _context;
    //    private readonly IValidator<JobFinder.Domain.Common.Entities.JobOffer> _validator;
    //    private readonly UserManager<IdentityUser> _userManager;

    //    public CreateJobOfferHandler(IUnitOfWork context, IValidator<JobFinder.Domain.Common.Entities.JobOffer> validator, UserManager<IdentityUser> userManager)
    //    {
    //        _context = context;
    //        _validator = validator;
    //        _userManager = userManager;
    //    }

    //    public async Task<Result<int>> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
    //    {
    //        // Verify User exists
    //        var user = await _userManager.FindByIdAsync(request.UserId);
    //        if (user == null)
    //        {
    //            return Result.Fail($"User with ID {request.UserId} not found.");
    //        }

    //        var jobOffer = new JobFinder.Domain.Common.Entities.JobOffer
    //        {
    //            UserId = request.UserId,
    //            Details = request.Details,
    //            SalaryOffered = request.SalaryOffered,
    //            ExpiresAt = request.ExpiresAt,
    //            CreatedAt = DateTime.Now,
    //            DateCreated = DateTime.UtcNow,
    //            DateModified = DateTime.UtcNow,
    //            IsActive = true,
    //            Title = request.Title
    //        };

    //        ValidationResult validationResult = _validator.Validate(jobOffer);
    //        if (!validationResult.IsValid)
    //        {
    //            return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
    //        }

    //        await _context.JobOffersRepository.AddAsync(jobOffer);
    //        await _context.CommitAsync(cancellationToken);
    //        return Result.Ok(jobOffer.Id);
    //    }
    //}

}
