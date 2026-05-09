using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Query;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class GetAppliedJobsQueryValidator : AbstractValidator<GetAppliedJobsQuery>
    {
        public GetAppliedJobsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
