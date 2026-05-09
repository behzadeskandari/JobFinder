using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Companies.Queries.GetCompaniesQuery;

namespace JobFinder.Application.Feature.Companies.Validators
{
    public class GetCompaniesQueryValidator : AbstractValidator<GetCompaniesQuery>
    {
        public GetCompaniesQueryValidator()
        {
            RuleFor(x => x.SearchCriteria).NotNull().WithMessage("Search criteria is required");
            RuleFor(x => x.SearchCriteria.IndustryId).GreaterThan(0).When(x => x.SearchCriteria.IndustryId.HasValue).WithMessage("Invalid industry ID");
            RuleFor(x => x.SearchCriteria.CityId).GreaterThan(0).When(x => x.SearchCriteria.CityId.HasValue).WithMessage("Invalid city ID");
            RuleFor(x => x.SearchCriteria.MinRating).InclusiveBetween(0, 5).When(x => x.SearchCriteria.MinRating.HasValue).WithMessage("Rating must be between 0 and 5");
        }
    }
}
