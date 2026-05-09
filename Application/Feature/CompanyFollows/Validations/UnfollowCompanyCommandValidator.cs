using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Command;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class UnfollowCompanyCommandValidator : AbstractValidator<UnfollowCompanyCommand>
    {
        public UnfollowCompanyCommandValidator()
        {
            //RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Company ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
