using FluentResults;
using JobFinder.Contracts.Dtos.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Queries.GetCandidateResumesQuery
{
    public class GetCandidateResumesQuery : MediatR.IRequest<Result<IEnumerable<ResumeDto>>>
    {
        public int CandidateId { get; set; }
    }
}
