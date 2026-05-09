using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;

namespace JobFinder.Application.Feature.Candidate.Queries.SearchCandidatesQuery
{
    public class SearchCandidatesQuery : IRequest<CandidateSearchResponseDto>
    {
        public CandidateSearchCriteria Criteria { get; set; } = new CandidateSearchCriteria();
    }
}
