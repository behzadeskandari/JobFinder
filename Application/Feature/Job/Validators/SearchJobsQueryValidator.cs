using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Query;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class SearchJobsQueryValidator : AbstractValidator<SearchJobsQuery>
    {
        public SearchJobsQueryValidator()
        {

            //RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than zero");
            //RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
            //RuleFor(x => x.SearchCriteria.jobCategory).Null().When(x => x.SearchCriteria.jobCategory == null).WithMessage("Invalid job CategorySlug");
            //RuleFor(x => x.SearchCriteria).Null().WithMessage("Search criteria is required");
            //RuleFor(x => x.SearchCriteria.city).GreaterThan(0).When(x => x.SearchCriteria.city.HasValue).WithMessage("Invalid city ID");
        }
    }
}
