using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public class CandidateJobActivity
    {
        public Guid CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public int SavedJobsCount { get; set; }
        public int ApplicationsCount { get; set; }
    }
}
