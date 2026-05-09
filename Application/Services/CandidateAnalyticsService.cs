using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.MbtiTest;
using JobFinder.Contracts.Enums;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Services
{
    public class CandidateAnalyticsService : ICandidateAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CandidateAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CandidateJobActivity>> GetCandidateJobActivities(DateTime? startDate = null, DateTime? endDate = null)
        {
            var candidates = await _unitOfWork.CandidateRepository.GetAllAsync();
            var savedJobs = await _unitOfWork.JobsRepository.GetAllAsync(); // Assuming you have a SavedJobs entity
            var jobApplications = await _unitOfWork.JobApplication.GetAllAsync();

            return candidates.Select(c => new CandidateJobActivity
            {
                CandidateId = c.Id,
                CandidateName = $"{c.FirstName} {c.LastName}", // Adjust based on your Candidate model
                SavedJobsCount = savedJobs.Count(sj => sj.Candidates.Where(y => y.Id == c.Id).First().Id == c.Id),
                ApplicationsCount = jobApplications.Count(ja => ja.CandidateId == c.Id)
            }).ToList();
        }


        public async Task<IEnumerable<TopSkill>> GetTopSkillsAmongActiveCandidates(int topCount = 10)
        {
            var candidates = await _unitOfWork.CandidateRepository.GetAllAsync();
            var candidateSkills = await _unitOfWork.SkillsRepository.GetAllAsync();

            var result = candidateSkills
                .Join(candidates,
                      skill => skill.CandidateId,
                      candidate => candidate.Id,
                      (skill, candidate) => skill) // ما خود Skill رو نگه می‌داریم
                .GroupBy(skill => skill.Name) // گروه‌بندی بر اساس اسم Skill
                .Select(group => new TopSkill
                {
                    SkillName = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(ts => ts.Count)
                .Take(topCount)
                .ToList();

            return result;
        }

        public async Task<IEnumerable<MBTIDistribution>> GetMBTIDistribution()
        {
            IEnumerable<Candidate> candidates = await _unitOfWork.CandidateRepository.GetAllAsync();

            return candidates
                .Where(c => !string.IsNullOrEmpty(c.MBTIType)) // Assuming MBTIType property in Candidate
                .GroupBy(c => c.MBTIType)
                .Select(group => new MBTIDistribution
                {
                    MBTIType = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(m => m.Count)
                .ToList();
        }

        public async Task<IEnumerable<CandidateActivityOverTime>> GetCandidateActivityOverTime(DateTime? startDate = null, DateTime? endDate = null, ActivityType activityType = ActivityType.All)
        {
            var logs = await _unitOfWork.LogsRepository.GetAllAsync(); // Assuming you have a Logs entity tracking activity

            if (startDate.HasValue)
            {
                logs = logs.Where(l => l.DateModified >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                logs = logs.Where(l => l.DateModified <= endDate.Value);
            }

            var groupedLogs = logs.GroupBy(l => l.DateModified);
            var activityOverTime = new List<CandidateActivityOverTime>();

            foreach (var group in groupedLogs)
            {
                var activity = new CandidateActivityOverTime
                {
                    ActivityDate = group.Key.Value,
                    Logins = group.Count(l => l.ActivityType == ActivityType.Login), // Adjust based on your log types
                    ProfileUpdates = group.Count(l => l.ActivityType == ActivityType.ProfileUpdate),
                    JobsSaved = group.Count(l => l.ActivityType == ActivityType.JobSaved),
                    ApplicationsSubmitted = group.Count(l => l.ActivityType == ActivityType.ApplicationSubmitted),
                    ApplicationRemovedForCandidate = group.Count(l => l.ActivityType == ActivityType.ApplicationRemovedForCandidate)
                };

                if (activityType == ActivityType.All ||
                    (activityType == ActivityType.Login && activity.Logins > 0) ||
                    (activityType == ActivityType.ProfileUpdate && activity.ProfileUpdates > 0) ||
                    (activityType == ActivityType.JobSaved && activity.JobsSaved > 0) ||
                    (activityType == ActivityType.ApplicationSubmitted && activity.ApplicationsSubmitted > 0) ||
                    (activityType == ActivityType.ApplicationRemovedForCandidate && activity.ApplicationRemovedForCandidate > 0))
                {
                    activityOverTime.Add(activity);
                }
            }

            return activityOverTime.OrderBy(a => a.ActivityDate).ToList();
        }
    }
}
