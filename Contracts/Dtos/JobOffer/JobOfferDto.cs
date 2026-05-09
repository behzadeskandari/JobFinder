using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Account;

namespace JobFinder.Contracts.Dtos.JobOffer
{
    public class JobOfferDto
    {
        [Required]
        public string UserId { get; set; }

        public UserDto? User { get; set; }

        //[Required]
        //public int JobPostId { get; set; }

        //public JobPost JobPost { get; set; }

        [Required]
        public string Details { get; set; }

        public decimal? SalaryOffered { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public JobOfferStatus Status { get; set; } = JobOfferStatus.Pending;
        public string Title { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        public string StatusString => Status.ToString();
        public string? UserEmail { get; set; }
        public string? JobPostTitle { get; set; }
    }
}
