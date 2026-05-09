using JobFinder.Contracts.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{
    public class Job :IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public JobLevel Level { get; set; }

        // Relations
        [Required]
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        //public int? JobRequestsId { get; set; }
        //public JobRequest? JobRequests { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public City? Cities { get; set; }
        [ForeignKey("Feature")]
        public Guid? FeaturesId { get; set; }
        public Feature? Features { get; set; }
        [ForeignKey("TechnicalOption")]
        public int? TechnicalOptionsId { get; set; }
        public TechnicalOption? TechnicalOptions { get; set; }
        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        [Required]
        [ForeignKey("JobCategory")]
        public int JobCategoryId { get; set; }
        public JobCategory JobCategories { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public JobOfferStatus Status { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }
    }
}
