using FluentValidation;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Validators
{
    public class GetPersonalityTestsQueryValidator : AbstractValidator<GetPersonalityTestsQuery>
    {
        public GetPersonalityTestsQueryValidator()
        {
            // No validation needed
        }
    }
}
