using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.SavedJobs.Commands;

namespace JobFinder.Application.Feature.SavedJobs.Validators
{
    public class UpdateSavedJobCommandValidator : AbstractValidator<UpdateSavedJobCommand>
    {
        public UpdateSavedJobCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Saved job ID is required");
            //RuleFor(x => x.JobId).GreaterThan(0).WithMessage("Job ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
