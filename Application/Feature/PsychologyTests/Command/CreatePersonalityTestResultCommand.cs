using FluentResults;
using JobFinder.Contracts.Dtos.PersonalityTestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Command
{
    public class CreatePersonalityTestResultCommand : MediatR.IRequest<Result<PersonalityTestResultDto>>
    {
        public int PersonalityTestId { get; set; }
        public string UserId { get; set; }
        public string ResultData { get; set; }
    }
}
