using FluentResults;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Queries.GetCandidatesQuery
{
    public class GetCandidatesQuery : IRequest<Result<IEnumerable<CandidateGetDto>>>
    {
    }
}
