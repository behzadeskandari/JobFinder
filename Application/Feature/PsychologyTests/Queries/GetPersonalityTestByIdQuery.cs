using FluentResults;
using JobFinder.Contracts.Dtos.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Queries
{
    public class GetPersonalityTestByIdQuery : MediatR.IRequest<Result<PersonalityTestDto>>
    {
        public int Id { get; set; }
    }
}
