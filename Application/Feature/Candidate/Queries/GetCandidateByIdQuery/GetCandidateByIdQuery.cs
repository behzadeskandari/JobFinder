using FluentResults;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Queries.GetCandidateByIdQuery
{
    public class GetCandidateByIdQuery : IRequest<Result<CandidateGetDto>>
    {
        public Guid Id { get; set; }
    }

}
