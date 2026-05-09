using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobRequest
{
    public class CreateJobRequestDto
    {
        [Required]
        public int JobPostId { get; set; }

        public string CoverLetter { get; set; }

        public string ResumeUrl { get; set; }
    }
}
