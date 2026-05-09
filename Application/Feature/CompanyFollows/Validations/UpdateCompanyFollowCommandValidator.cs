using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Command;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class UpdateCompanyFollowCommandValidator : AbstractValidator<UpdateCompanyFollowCommand>
    {
        public UpdateCompanyFollowCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Company follow ID is required");
            //RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Company ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
