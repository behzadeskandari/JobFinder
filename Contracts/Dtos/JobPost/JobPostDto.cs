using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobPost
{
    public class JobPostDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Requirements { get; set; }

        public string Benefits { get; set; }

        [Required]
        public string Location { get; set; }

        public decimal? Salary { get; set; }

        public string StaffId { get; set; }

        public string StaffEmail { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public bool IsActive { get; set; }
    }
}
