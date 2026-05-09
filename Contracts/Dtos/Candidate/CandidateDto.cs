using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Resume;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? CoverLetter { get; set; }
        public string? ResumeUrl { get; set; }
        public DateTime? LastAppliedDate { get; set; }
        public string UserId { get; set; }
        public string? MBTIType { get; set; }
        public int YearsOfExperience { get; set; }
        public int EducationLevelId { get; set; }
        public string EducationLevelName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
