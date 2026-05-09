using FluentResults;
using JobFinder.Contracts.Dtos.PsychologyTestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Queries
{
    public class GetPsychologyTestResultByIdQuery : MediatR.IRequest<Result<PsychologyTestResultDto>>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
