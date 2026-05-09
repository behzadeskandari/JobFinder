using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{
    public class PlatformOverview
    {
        public int TotalCandidates { get; set; }
        public int TotalEmployers { get; set; }
        public int ActiveJobPosts { get; set; }
        public int TotalApplications { get; set; }
        public int TotalUser { get; set; }
    }

    public class UserRegistrationTrend
    {
        public DateTime RegistrationDate { get; set; }
        public int NewCandidates { get; set; }
        public int NewEmployers { get; set; }
    }

    public class PlatformUsageMetrics
    {
        public DateTime Date { get; set; }
        public int CandidateLogins { get; set; }
        public int EmployerLogins { get; set; }
        public int JobPostsCreated { get; set; }
        public int ApplicationsSubmitted { get; set; }
    }
}
