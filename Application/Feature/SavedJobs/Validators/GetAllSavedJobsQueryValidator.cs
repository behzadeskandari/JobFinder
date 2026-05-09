using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.SavedJobs.Query;

namespace JobFinder.Application.Feature.SavedJobs.Validators
{
    public class GetAllSavedJobsQueryValidator : AbstractValidator<GetAllSavedJobsQuery>
    {
        public GetAllSavedJobsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
