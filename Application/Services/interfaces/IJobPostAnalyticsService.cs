using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Services.interfaces
{
    public interface IJobPostAnalyticsService
    {
        Task<IEnumerable<JobPostPerformance>> GetJobPostPerformances(DateTime? startDate = null, DateTime? endDate = null);
        Task<JobPostPerformance> GetJobPostPerformance(Guid jobPostId);
        Task<IEnumerable<TopJobCategory>> GetTopPerformingJobCategories(int topCount = 5, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<ApplicantLocation>> GetApplicantDistribution(Guid jobPostId);
    }
}
