using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobPost
{
    public class UpdateJobPostDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Requirements { get; set; }

        public Guid BenefitId { get; set; }

        [Required]
        public string Location { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public bool IsActive { get; set; }
    }
}
