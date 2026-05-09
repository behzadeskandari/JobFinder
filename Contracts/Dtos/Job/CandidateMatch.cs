using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Candidate;

namespace JobFinder.Contracts.Dtos.Job
{
   public class CandidateMatch
    {
        public CandidateDto Candidate { get; set; }
        public double Score { get; set; }
    }
}
