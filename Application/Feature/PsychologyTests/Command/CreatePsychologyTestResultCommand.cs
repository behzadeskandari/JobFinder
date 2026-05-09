using FluentResults;
using JobFinder.Contracts.Dtos.PsychologyTestResult;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Command
{
    public class CreatePsychologyTestResultCommand : MediatR.IRequest<Result<PsychologyTestResultDto>>
    {
        public int PsychologyTestId { get; set; }
        public string UserId { get; set; }
        public decimal TotalScore { get; set; }
        public PsychologyTestInterpretation Interpretation { get; set; }
        public string ResultData { get; set; }
    }
}
