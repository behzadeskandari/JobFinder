using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string ResumeFileName { get; set; }
        public string ResumeFileUrl { get; set; }
        public string CoverLetter { get; set; }
        public string Status { get; set; }

        public int? ResumeId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
