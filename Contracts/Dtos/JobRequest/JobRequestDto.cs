using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Account;

namespace JobFinder.Contracts.Dtos.JobRequest
{
    public class JobRequestDto
    {
        [Required]
        public string UserId { get; set; }

        public UserDto? User { get; set; }

        //[Required]
        //public int JobPostId { get; set; }

        //public JobPost JobPost { get; set; }

        public string CoverLetter { get; set; }

        public string ResumeUrl { get; set; }


        public JobRequestStatus Status { get; set; } = JobRequestStatus.Pending;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string? UserEmail { get; set; }
    }
}
