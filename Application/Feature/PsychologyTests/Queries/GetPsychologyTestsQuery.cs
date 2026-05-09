using FluentResults;
using JobFinder.Contracts.Dtos.PsychologyTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Queries
{
    public class GetPsychologyTestsQuery : MediatR.IRequest<Result<IEnumerable<PsychologyTestDto>>>
    {
    }
}
