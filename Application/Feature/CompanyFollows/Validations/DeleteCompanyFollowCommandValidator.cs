using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.CompanyFollows.Command;

namespace JobFinder.Application.Feature.CompanyFollows.Validations
{
    public class DeleteCompanyFollowCommandValidator : AbstractValidator<DeleteCompanyFollowCommand>
    {
        public DeleteCompanyFollowCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Company follow ID is required");
        }
    }
}
