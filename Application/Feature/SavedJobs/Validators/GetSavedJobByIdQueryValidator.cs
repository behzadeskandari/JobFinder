using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.SavedJobs.Query;

namespace JobFinder.Application.Feature.SavedJobs.Validators
{
    public class GetSavedJobByIdQueryValidator : AbstractValidator<GetSavedJobByIdQuery>
    {
        public GetSavedJobByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Saved job ID is required");
        }
    }
}
