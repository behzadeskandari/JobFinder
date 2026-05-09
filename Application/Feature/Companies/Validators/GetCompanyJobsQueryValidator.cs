using FluentValidation;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyJobsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Companies.Validators
{
    public class GetCompanyJobsQueryValidator : AbstractValidator<GetCompanyJobsQuery>
    {
        public GetCompanyJobsQueryValidator()
        {
            //RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Company ID is required");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than zero");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }
}
