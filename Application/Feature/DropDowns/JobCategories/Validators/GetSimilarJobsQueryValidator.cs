using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Validators
{
    public class GetSimilarJobsQueryValidator : AbstractValidator<GetSimilarJobsQuery>
    {
        public GetSimilarJobsQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Job ID is required");
        }
    }
}
