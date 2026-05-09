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
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.JobOffer.Handler
{

    //public class UpdateJobOfferHandler : IRequestHandler<UpdateJobOfferCommand, Result<bool>>
    //{
    //    private readonly IUnitOfWork _context;
    //    private readonly IValidator<JobFinder.Domain.Common.Entities.JobOffer> _validator;

    //    public UpdateJobOfferHandler(IUnitOfWork context, IValidator<JobFinder.Domain.Common.Entities.JobOffer> validator)
    //    {
    //        _context = context;
    //        _validator = validator;
    //    }

    //    public async Task<Result<bool>> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
    //    {
    //        var jobOffer = await _context.JobOffersRepository.GetByIdAsync(request.Id);
    //        if (jobOffer == null)
    //        {
    //            return Result.Fail($"JobOffer with Id {request.Id} not found.");
    //        }

    //        jobOffer.Details = request.Details;
    //        jobOffer.SalaryOffered = request.SalaryOffered;
    //        jobOffer.ExpiresAt = request.ExpiresAt;
    //        jobOffer.Status = request.Status;
    //        jobOffer.DateModified = DateTime.UtcNow;
    //        jobOffer.IsActive = request.IsActive;
    //        jobOffer.Title = request.Title;

    //        ValidationResult validationResult = _validator.Validate(jobOffer);
    //        if (!validationResult.IsValid)
    //        {
    //            return Result.Fail(validationResult.Errors.ConvertAll(e => new Error(e.ErrorMessage)));
    //        }

    //        await _context.JobOffersRepository.UpdateAsync(jobOffer);
    //        await _context.CommitAsync(cancellationToken);
    //        return Result.Ok(true);
    //    }
    //}
}
