using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobOffer
{
    public class CreateJobOfferDto
    {
        [Required]
        public int JobPostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Details { get; set; }

        public decimal? SalaryOffered { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }

}
