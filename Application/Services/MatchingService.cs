using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Contracts.Dtos.JobPost;
using JobFinder.Contracts.Dtos.Skill;
using JobFinder.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly IUnitOfWork _unitOfWork;
        // Assuming you have a service to generate text embeddings
        // private readonly INlpEmbeddingService _nlpEmbeddingService;

        // Weights for different matching criteria
        private const double SkillWeight = 0.6;
        private const double ExperienceWeight = 0.15;
        private const double EducationWeight = 0.1;
        private const double LocationWeight = 0.05;
        private const double DescriptionWeight = 0.1; // Weight for job description matching

        public MatchingService(IUnitOfWork unitOfWork /*, INlpEmbeddingService nlpEmbeddingService */)
        {
            _unitOfWork = unitOfWork;
            // _nlpEmbeddingService = nlpEmbeddingService;
        }

        public async Task<IEnumerable<JobMatch>> GetJobMatchesForCandidate(Guid candidateId)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetByIdAsync(candidateId);
            var candidateSkills = (await _unitOfWork.SkillsRepository.FindAsync(cs => cs.Resume.Candidates.Any(x => x.Id == candidateId))).ToList();
            var jobPosts = await _unitOfWork.JobPostsRepository.GetAllAsync();
            var requiredSkills = _unitOfWork.SkillsRepository.GetQueryable(); //.(x => x.Resume.Candidates.Where(x => x.Id == candidateId));


            if (candidate == null || !jobPosts.Any())
            {
                return Enumerable.Empty<JobMatch>();
            }

            var matches = new List<JobMatch>();

            foreach (var jobPost in jobPosts)
            {
                var jobRequiredSkills = requiredSkills.Where(rs => rs.JobPostId == jobPost.Id).ToList();
                //List<CandidateSkill> candidateSkill = new List<CandidateSkill>();


                var skill = candidateSkills.Select(x => new CandidateSkill
                {
                    CandidateId = candidateId,
                    ProficiencyLevel = x.ProficiencyLevel,
                    SkillId = x.Id,

                }).ToList();

                var JobrequiredSkills = jobRequiredSkills.Select(x => new RequiredSkill
                {
                    JobPostId = jobPost.Id,
                    MinimumProficiencyLevel = x.ProficiencyLevel,
                    SkillId = x.Id,
                }).ToList();
                //candidateSkills , jobRequiredSkills
                double matchScore = await CalculateMatchScore(candidate, skill , jobPost, JobrequiredSkills);
                if (matchScore > 0) // Define a reasonable threshold (e.g., 0.5)
                {
                    matches.Add(new JobMatch { JobPost = jobPost, Score = matchScore });
                }
            }

            return matches.OrderByDescending(m => m.Score).ToList();
        }

        public async Task<IEnumerable<CandidateMatch>> GetCandidateMatchesForJob(Guid jobId)
        {
            var jobPost = await _unitOfWork.JobPostsRepository.GetByIdAsync(jobId);
            var jobRequiredSkills = (await _unitOfWork.SkillsRepository.FindAsync(rs => rs.JobPostId == jobId)).ToList();
            var candidates = await _unitOfWork.CandidateRepository.GetAllAsync();
            var candidateSkills = await _unitOfWork.SkillsRepository.GetByCandidatesIds(candidates);

            if (jobPost == null || !candidates.Any())
            {
                return Enumerable.Empty<CandidateMatch>();
            }

            var matches = new List<CandidateMatch>();

            foreach (var candidate in candidates)
            {
                var candidateSpecificSkills = candidateSkills.Where(cs => cs.CandidateId == candidate.Id).ToList();



                var skill = candidateSkills.Select(x => new CandidateSkill
                {
                    CandidateId = candidate.Id,
                    ProficiencyLevel = x.ProficiencyLevel,
                    SkillId = x.Id,

                }).ToList();

                var JobrequiredSkills = jobRequiredSkills.Select(x => new RequiredSkill
                {
                    JobPostId = jobPost.Id,
                    MinimumProficiencyLevel = x.ProficiencyLevel,
                    SkillId = x.Id,
                }).ToList();
                //candidateSpecificSkills , //jobRequiredSkills
                double matchScore = await CalculateMatchScore(candidate, skill , jobPost, JobrequiredSkills);
                if (matchScore > 0) // Define a reasonable threshold
                {
                    matches.Add(new CandidateMatch { Candidate = candidate, Score = matchScore });
                }
            }

            return matches.OrderByDescending(m => m.Score).ToList();
        }

        private async Task<double> CalculateMatchScore(Candidate candidate, List<CandidateSkill>? candidateSkills, JobPost jobPost, List<RequiredSkill> requiredSkills)
        {
            double totalScore = 0;

            // 1. Skill Matching
            var skillScore = await CalculateSkillMatchScore(candidateSkills, requiredSkills);
            totalScore += skillScore.Item1 * SkillWeight;

            // 2. Experience Matching (Simple comparison - you might need more complex logic)
            if (candidate.YearsOfExperience >= jobPost.MinimumExperience)
            {
                totalScore += ExperienceWeight;
            }

            // 3. Education Matching (Simple comparison - you might need to handle levels)
            if (candidate.EducationLevelId >= jobPost.MinimumEducationLevelId)
            {
                totalScore += EducationWeight;
            }

            // 4. Location Matching (Simple exact match - consider proximity)
            if (candidate.CityId == jobPost.CityId)
            {
                totalScore += LocationWeight;
            }

            // 5. Job Description Matching (Using cosine similarity of embeddings - requires NLP service)
            // if (candidate.ResumeEmbedding != null && jobPost.DescriptionEmbedding != null)
            // {
            //     double descriptionSimilarity = CosineSimilarity(candidate.ResumeEmbedding, jobPost.DescriptionEmbedding);
            //     totalScore += descriptionSimilarity * DescriptionWeight;
            // }

            // Ensure score is within 0 to 1
            return Math.Clamp(totalScore, 0, 1);
        }

        private async Task<(double, double)> CalculateSkillMatchScore(List<CandidateSkill> candidateSkills, List<RequiredSkill> requiredSkills)
        {
            if (!requiredSkills.Any())
            {
                // Depending on requirements, return a default or throw an exception
                return default; // Example: Return (0,0) if no required skills
            }
            var matchedSkillsCount = candidateSkills.Count(cs => requiredSkills.Any(rs => rs.SkillId == cs.SkillId));

            var advancedCalculate = await CalculateAdvancedSkillMatchScore(candidateSkills, requiredSkills);
            var regularCalculate = (double)matchedSkillsCount / requiredSkills.Count();

            return (advancedCalculate, regularCalculate);
        }

        public async Task<double> CalculateAdvancedSkillMatchScore(List<CandidateSkill> candidateSkills, List<RequiredSkill> requiredSkills)
        {
            if (!requiredSkills.Any()) return 1.0;

            var matchedSkills = 0;
            var skillIds = candidateSkills.Select(x => x.SkillId);
            var skillRelationships = await _unitOfWork.SkillsRepository.FindAsync(x => skillIds.Contains(x.Id));

            foreach (var requiredSkill in requiredSkills)
            {
                if (candidateSkills.Any(cs => cs.SkillId == requiredSkill.SkillId))
                {
                    matchedSkills++;
                    continue;
                }

                // Check for synonyms and related skills
                if (candidateSkills.Any(cs => skillRelationships.Any(sr =>
                    (sr.Id == requiredSkill.SkillId && sr.Id == cs.SkillId) ||
                    (sr.Id == requiredSkill.SkillId && sr.Id == cs.SkillId))))
                {
                    matchedSkills++;
                }
            }

            return (double)matchedSkills / requiredSkills.Count();
        }

        // Helper function for cosine similarity (if you implement text embeddings)
        // private double CosineSimilarity(float[] vectorA, float[] vectorB)
        // {
        //     if (vectorA == null || vectorB == null || vectorA.Length != vectorB.Length) return 0.0;

        //     double dotProduct = 0;
        //     double normA = 0;
        //     double normB = 0;

        //     for (int i = 0; i < vectorA.Length; i++)
        //     {
        //         dotProduct += vectorA[i] * vectorB[i];
        //         normA += Math.Pow(vectorA[i], 2);
        //         normB += Math.Pow(vectorB[i], 2);
        //     }

        //     double denominator = Math.Sqrt(normA) * Math.Sqrt(normB);
        //     return denominator == 0 ? 0 : dotProduct / denominator;
        // }
    }

    public class JobMatch
    {
        public JobPost JobPost { get; set; }
        public double Score { get; set; }
    }

    public class CandidateMatch
    {
        public Candidate Candidate { get; set; }
        public double Score { get; set; }
    }
}

