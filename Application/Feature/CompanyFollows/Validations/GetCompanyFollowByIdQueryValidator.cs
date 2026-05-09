using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Queries;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class GetCompanyFollowByIdQueryValidator : AbstractValidator<GetCompanyFollowByIdQuery>
    {
        public GetCompanyFollowByIdQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Company follow ID is required");
        }
    }
}
