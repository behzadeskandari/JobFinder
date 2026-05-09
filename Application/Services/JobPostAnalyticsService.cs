using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Services
{
    public class JobPostAnalyticsService : IJobPostAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobPostAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<JobPostPerformance>> GetJobPostPerformances(DateTime? startDate = null, DateTime? endDate = null)
        {
            var jobPosts = await _unitOfWork.JobPostsRepository.GetAllAsync();
            var jobApplications = await _unitOfWork.JobApplication.GetAllAsync(); // Assuming you have this entity

            if (startDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DatePublished >= startDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DatePublished <= endDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate <= endDate.Value);
            }

            return jobPosts.Select(jp => new JobPostPerformance
            {
                JobPostId = jp.Id,
                Title = jp.Title,
                Views = jp.ViewCount.Value, // Assuming you have a ViewCount property
                Applications = jobApplications.Count(ja => ja.JobId == jp.Id),
                // Calculate TimeToFill if you have relevant dates (e.g., ClosingDate, FilledDate)
                // ConversionRate = (double)Applications / Views if Views > 0 else 0
            }).ToList();
        }

        public async Task<JobPostPerformance> GetJobPostPerformance(Guid jobPostId)
        {
            var jobPost = await _unitOfWork.JobPostsRepository.GetByIdAsync(jobPostId);
            if (jobPost == null)
            {
                return null;
            }
            var jobApplications = await _unitOfWork.JobApplication.FindAsync(ja => ja.JobId == jobPostId);

            return new JobPostPerformance
            {
                JobPostId = jobPost.Id,
                Title = jobPost.Title,
                Views = jobPost.ViewCount ?? 0,
                Applications = jobApplications.Count(),
                // ... other calculations
            };
        }

        public async Task<IEnumerable<TopJobCategory>> GetTopPerformingJobCategories(int topCount = 5, DateTime? startDate = null, DateTime? endDate = null)
        {
            var jobPosts = await _unitOfWork.JobPostsRepository.GetAllAsync();
            var jobApplications = await _unitOfWork.JobApplication.GetAllAsync();
            var jobs = await _unitOfWork.JobsRepository.GetAllAsync();
            var jobCategories = await _unitOfWork.JobCategoryRepository.GetAllAsync();

            if (startDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DatePublished >= startDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DatePublished <= endDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate <= endDate.Value);
            }

            var result = jobPosts
                // وصل کردن JobPost به Job
                .Join(jobs,
                      jp => jp.JobId,
                      j => j.Id,
                      (jp, j) => new { JobPost = jp, Job = j })

                // وصل کردن Job به JobCategory
                .Join(jobCategories,
                      jpj => jpj.Job.JobCategoryId,
                      jc => jc.Id,
                      (jpj, jc) => new { jpj.JobPost, JobCategory = jc })

                // جوین با JobApplications
                .GroupJoin(jobApplications,
                           jpc => jpc.JobPost.Id,
                           ja => ja.JobId,
                           (jpc, applications) => new
                           {
                               CategoryName = jpc.JobCategory.Name,
                               ApplicationCount = applications.Count()
                           })

                // گروه‌بندی بر اساس دسته‌بندی شغلی
                .GroupBy(g => g.CategoryName)
                .Select(g => new TopJobCategory
                {
                    CategoryName = g.Key,
                    TotalApplications = g.Sum(x => x.ApplicationCount)
                })
                .OrderByDescending(tc => tc.TotalApplications)
                .Take(topCount)
                .ToList();

            return result;
        }

        public async Task<IEnumerable<ApplicantLocation>> GetApplicantDistribution(Guid jobPostId)
        {
            //var jobApplications = await _unitOfWork.JobApplication.FindAsync(ja => ja.Job.JobPosts.Any(jp => jp.Id == jobPostId));
            //var candidates = await _unitOfWork.CandidateRepository.GetAllAsync(); // Assuming Candidate entity has location info  

            //return jobApplications
            //   .Join(candidates, ja => ja.CandidateId, c => c.Id, (ja, c) => c.CityId) // Assuming CityId in Candidate  
            //   .GroupBy(cityId => cityId)
            //   .Select(group => new ApplicantLocation
            //   {
            //       CityId = group.Key,
            //       ApplicantCount = group.Count()
            //   })
            //   .OrderByDescending(loc => loc.ApplicantCount)
            //   .ToList();

            return Enumerable.Empty<ApplicantLocation>();
        }
    }
}
