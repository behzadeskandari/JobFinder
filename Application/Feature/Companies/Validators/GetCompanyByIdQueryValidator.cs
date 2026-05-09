using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyByIdQuery;

namespace JobFinder.Application.Feature.Companies.Validators
{
    public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        public GetCompanyByIdQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Company ID is required");
        }
    }
}
