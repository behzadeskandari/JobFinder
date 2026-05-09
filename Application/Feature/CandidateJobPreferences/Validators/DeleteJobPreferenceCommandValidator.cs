using FluentValidation;
using JobFinder.Application.Feature.CandidateJobPreferences.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Validators
{
    public class DeleteJobPreferenceCommandValidator : AbstractValidator<DeleteJobPreferenceCommand>
    {
        public DeleteJobPreferenceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Job preference ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
