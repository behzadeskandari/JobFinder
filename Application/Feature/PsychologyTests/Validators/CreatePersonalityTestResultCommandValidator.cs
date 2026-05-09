using FluentValidation;
using JobFinder.Application.Feature.PsychologyTests.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Validators
{
    public class CreatePersonalityTestResultCommandValidator : AbstractValidator<CreatePersonalityTestResultCommand>
    {
        public CreatePersonalityTestResultCommandValidator()
        {
            RuleFor(x => x.PersonalityTestId).GreaterThan(0).WithMessage("Personality test ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.ResultData).NotEmpty().WithMessage("Result data is required");
        }
    }
}
