using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.MbtiTest;
using JobFinder.Contracts.Enums;

namespace JobFinder.Application.Services.interfaces
{
    public interface ICandidateAnalyticsService
    {
        Task<IEnumerable<CandidateJobActivity>> GetCandidateJobActivities(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<CandidateActivityOverTime>> GetCandidateActivityOverTime(DateTime? startDate = null, DateTime? endDate = null, ActivityType activityType = ActivityType.All);
        Task<IEnumerable<TopSkill>> GetTopSkillsAmongActiveCandidates(int topCount = 10);
        Task<IEnumerable<MBTIDistribution>> GetMBTIDistribution();
    }
}
