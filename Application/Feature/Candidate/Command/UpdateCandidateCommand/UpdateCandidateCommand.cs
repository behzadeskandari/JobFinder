using FluentResults;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Command.UpdateCandidateCommand
{
    public class UpdateCandidateCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public CandidateUpdateDto CandidateDto { get; set; }
    }
}
