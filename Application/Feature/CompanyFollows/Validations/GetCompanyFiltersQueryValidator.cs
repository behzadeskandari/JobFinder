using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Queries;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class GetCompanyFiltersQueryValidator : AbstractValidator<GetCompanyFiltersQuery>
    {
        public GetCompanyFiltersQueryValidator()
        {
            // No validation needed
        }
    }
}
