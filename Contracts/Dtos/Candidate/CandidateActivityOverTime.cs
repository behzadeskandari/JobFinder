using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public class CandidateActivityOverTime
    {
        public DateTime ActivityDate { get; set; }
        public int Logins { get; set; }
        public int ProfileUpdates { get; set; }
        public int JobsSaved { get; set; }
        public int ApplicationsSubmitted { get; set; }
        public int ApplicationRemovedForCandidate { get; set; }
    }
}
