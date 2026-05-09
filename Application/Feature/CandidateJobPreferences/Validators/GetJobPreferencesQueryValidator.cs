using FluentValidation;
using JobFinder.Application.Feature.CandidateJobPreferences.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Validators
{
    public class GetJobPreferencesQueryValidator : AbstractValidator<GetJobPreferencesQuery>
    {
        public GetJobPreferencesQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
