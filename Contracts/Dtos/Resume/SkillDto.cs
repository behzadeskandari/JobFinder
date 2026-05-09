using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Resume
{
    public class SkillDto
    {
        public Guid ResumeId { get; set; }
        public string Name { get; set; }
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } // 1-5
    }
}
