using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Skill
{
    public partial class RequiredSkill
    {
        public int SkillId { get; set; }
        public Guid JobPostId { get; set; }
        public ProficiencyLevelEnum MinimumProficiencyLevel { get; set; } // e.g., 1-5
    }
}
