using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobPost
{
    public class CreateJobPostDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Requirements { get; set; }

        public Guid BenefitsId { get; set; }

        [Required]
        public string Location { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public ProficiencyLevelEnum MinimumExperience { get; set; }
        public string MinimumEducationLevelDegree { get; set; }
        public string MinimumEducationLevelInstitution { get; set; }
        public string MinimumEducationLevelField { get; set; }
        public string MinimumEducationLevelDescription { get; set; }

        public int CityId { get; set; }


    }
}
