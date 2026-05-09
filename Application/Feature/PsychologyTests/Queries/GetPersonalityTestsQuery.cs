using FluentResults;
using JobFinder.Contracts.Dtos.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Queries
{
    public class GetPersonalityTestsQuery : MediatR.IRequest<Result<IEnumerable<PersonalityTestDto>>>
    {
    }
}
