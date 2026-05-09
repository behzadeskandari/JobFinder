using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public class CandidateSearchResponseDto
    {
        public List<CandidateDto> Candidates { get; set; } = new List<CandidateDto>();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
