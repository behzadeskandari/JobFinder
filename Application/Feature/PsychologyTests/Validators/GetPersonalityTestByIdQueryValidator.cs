using FluentValidation;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Validators
{
    public class GetPersonalityTestByIdQueryValidator : AbstractValidator<GetPersonalityTestByIdQuery>
    {
        public GetPersonalityTestByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Personality test ID is required");
        }
    }
}
