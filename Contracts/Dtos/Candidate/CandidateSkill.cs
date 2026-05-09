using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public partial class CandidateSkill
    {
        public int SkillId { get; set; }
        public Guid CandidateId { get; set; }
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } // e.g., 1-5
                                                  // ...
    }
}
