using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Job
{
    public class JobDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public JobLevel Level { get; set; }

        // Relations
        [Required]
        public int CompanyId { get; set; }
        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        //public int? JobRequestsId { get; set; }
        //public JobRequest? JobRequests { get; set; }
        public int? CityId { get; set; }
        public int? FeaturesId { get; set; }
        public int? TechnicalOptionsId { get; set; }
        public int? OrderId { get; set; }
        [Required]
        public int JobCategoryId { get; set; }

        public bool? IsActive { get; set; }
        public JobOfferStatus Status { get; set; }
    }
}
