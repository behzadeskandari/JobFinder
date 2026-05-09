using FluentValidation;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Validators
{
    public class GetPsychologyTestResultByIdQueryValidator : AbstractValidator<GetPsychologyTestResultByIdQuery>
    {
        public GetPsychologyTestResultByIdQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Psychology test result ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
