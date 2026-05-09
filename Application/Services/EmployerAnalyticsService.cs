using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Services
{
    public class EmployerAnalyticsService : IEmployerAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployerAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployerJobPostPerformance>> GetEmployerJobPostPerformances(int employerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var id = Convert.ToString(employerId);
            var jobPosts = await _unitOfWork.JobPostsRepository.FindAsync(jp => jp.StaffId == id);
            var jobApplications = await _unitOfWork.JobApplication.GetAllAsync();

            if (startDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DateModified >= startDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate >= startDate.Value && jobPosts.Any(jp => jp.Id == ja.JobId));
            }

            if (endDate.HasValue)
            {
                jobPosts = jobPosts.Where(jp => jp.DateModified <= endDate.Value);
                jobApplications = jobApplications.Where(ja => ja.ApplicationDate <= endDate.Value && jobPosts.Any(jp => jp.Id == ja.JobId));
            }

            return jobPosts.Select(jp => new EmployerJobPostPerformance
            {
                JobPostId = jp.Id,
                Title = jp.Title,
                Views = jp.ViewCount ?? 0,
                Applications = jobApplications.Count(ja => ja.JobId == jp.Id)
                // Add more metrics as needed
            }).ToList();
        }

        public async Task<IEnumerable<ApplicationSource>> GetApplicationSources(int employerId, DateTime? startDate = null, DateTime? endDate = null)
        {

            var id = Convert.ToString(employerId);
            var jobPosts = await _unitOfWork.JobPostsRepository.FindAsync(jp => jp.StaffId == id);
            IEnumerable<JobApplication> jobApplications = await _unitOfWork.JobApplication.GetAllAsync();

            var employerApplications = jobApplications.Where(ja => jobPosts.Any(jp => jp.Id == ja.JobId));

            if (startDate.HasValue)
            {
                employerApplications = employerApplications.Where(ja => ja.ApplicationDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                employerApplications = employerApplications.Where(ja => ja.ApplicationDate <= endDate.Value);
            }

            return employerApplications
                .GroupBy(ja => ja.JobId) 
                .Select(group => new ApplicationSource
                {
                    Source = group.Key.ToString(),
                    Count = group.Count()
                })
                .ToList();
        }
    }
}
