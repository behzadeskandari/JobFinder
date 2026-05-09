using FluentValidation;
using JobFinder.Application.Feature.CandidateJobPreferences.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Validators
{
    public class UpdateJobPreferenceCommandValidator : AbstractValidator<UpdateJobPreferenceCommand>
    {
        public UpdateJobPreferenceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Job preference ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.JobCategoryId).GreaterThan(0).When(x => x.JobCategoryId.HasValue).WithMessage("Invalid job category ID");
            RuleFor(x => x.CityId).GreaterThan(0).When(x => x.CityId.HasValue).WithMessage("Invalid city ID");
            RuleFor(x => x.MinSalary).GreaterThanOrEqualTo(0).When(x => x.MinSalary.HasValue).WithMessage("Minimum salary cannot be negative");
        }
    }
}
