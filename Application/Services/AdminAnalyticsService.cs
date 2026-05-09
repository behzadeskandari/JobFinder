using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Dtos.Job;
using Microsoft.EntityFrameworkCore;
using JobFinder.Application.Repository;
using Domain.Extensions;
using Domain.Roles;

namespace JobFinder.Application.Services
{
    public class AdminAnalyticsService : IAdminAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PlatformOverview> GetPlatformOverview()
        {
            var totalCandidates = await _unitOfWork.CandidateRepository.CountAsync();
            var totalEmployers = await _unitOfWork.UsersRepository.GetQueryable().Where(x => x.Role == Roles.Role_Staff).CountAsync();
            var activeJobPosts = await _unitOfWork.JobPostsRepository.GetQueryable().Where(jp => jp.IsActive == true).CountAsync(); // Assuming IsActive property
            var totalApplications = await _unitOfWork.JobApplication.GetQueryable().CountAsync();
            var totalUser = await _unitOfWork.UsersRepository.GetQueryable().CountAsync();

            return new PlatformOverview
            {
                TotalCandidates = totalCandidates,
                TotalEmployers = totalEmployers,
                ActiveJobPosts = activeJobPosts,
                TotalApplications = totalApplications,
                TotalUser = totalUser,
            };
        }

        public async Task<IEnumerable<UserRegistrationTrend>> GetUserRegistrationTrends(DateTime? startDate = null, DateTime? endDate = null)
        {
            var candidates = await _unitOfWork.CandidateRepository.GetAllAsync();
            var employers = await _unitOfWork.UsersRepository.GetQueryable().Where(x => x.Role == Roles.Role_Staff).ToListAsync();

            if (startDate.HasValue)
            {
                candidates = candidates.Where(c => c.LastAppliedDate >= startDate.Value);
                employers = employers.Where(e => e.DateModified >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                candidates = candidates.Where(c => c.LastAppliedDate <= endDate.Value);
                employers = employers.Where(e => e.DateModified <= endDate.Value).ToList();
            }

            var candidateRegistrations = candidates
                .GroupBy(c => c.DateModified.Value)
                .Select(g => new { Date = g.Key, Count = g.Count() });

            var employerRegistrations = employers
                .GroupBy(e => e.DateModified.Value)
                .Select(g => new { Date = g.Key, Count = g.Count() });

            return candidateRegistrations.FullOuterJoin(
                    employerRegistrations,
                    c => c.Date,
                    e => e.Date,
                    (c, e, date) => new UserRegistrationTrend
                    {
                        RegistrationDate = date,
                        NewCandidates = c?.Count ?? 0,
                        NewEmployers = e?.Count ?? 0
                    })
                .OrderBy(t => t.RegistrationDate)
                .ToList();
        }

        public async Task<IEnumerable<PlatformUsageMetrics>> GetPlatformUsageMetrics(DateTime? startDate = null, DateTime? endDate = null)
        {
            var logs = await _unitOfWork.LogsRepository.GetAllAsync(); // Assuming Logs entity tracks user activity

            if (startDate.HasValue)
            {
                logs = logs.Where(l => l.DateModified>= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                logs = logs.Where(l => l.DateModified <= endDate.Value.Date);
            }

            //return logs
            //    .GroupBy(l => l.DateModified)
            //    .Select(group => new PlatformUsageMetrics
            //    {
            //        Date = group.Key,
            //        CandidateLogins = group.Count(l => l.UserType == "Candidate" && l.ActivityType == "Login"), // Adjust log properties
            //        EmployerLogins = group.Count(l => l.UserType == "Employer" && l.ActivityType == "Login"),
            //        JobPostsCreated = group.Count(l => l.EntityType == "JobPost" && l.ActivityType == "Created"),
            //        ApplicationsSubmitted = group.Count(l => l.EntityType == "JobApplication" && l.ActivityType == "Created")
            //    })
            //    .OrderBy(m => m.Date)
            //    .ToList();

            return Enumerable.Empty<PlatformUsageMetrics>();
        }
    }

}
