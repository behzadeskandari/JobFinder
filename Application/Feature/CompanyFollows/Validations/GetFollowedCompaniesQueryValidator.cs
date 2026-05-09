using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Queries;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class GetFollowedCompaniesQueryValidator : AbstractValidator<GetFollowedCompaniesQuery>
    {
        public GetFollowedCompaniesQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
