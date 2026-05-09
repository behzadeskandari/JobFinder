using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Contracts.Dtos.Skill;

namespace JobFinder.Application.Services.interfaces
{
    public interface IMatchingService
    {
        Task<IEnumerable<JobMatch>> GetJobMatchesForCandidate(Guid candidateId);
        Task<IEnumerable<CandidateMatch>> GetCandidateMatchesForJob(Guid jobId);

        Task<double> CalculateAdvancedSkillMatchScore(List<CandidateSkill> candidateSkills, List<RequiredSkill> requiredSkills);
    }
}
