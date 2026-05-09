using FluentResults;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.File;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Advertisement;
using Domain.WriteRepository;

namespace JobFinder.Application.Repository
{

    
    public interface ICandidateRepository : IWriteRepository<Candidate> //, IReadRepository<Candidate>//IRepository<Candidate>
    {
        Task<Result> CreateCandidate(CandidateDto candidateDto, string resumeFileName);

        Task<Result<IEnumerable<CandidateDto>>> GetCandidate();

        Task<Result<Candidate>> CandidateFindAsync(Guid Id);

        Task<Result<string>> RemoveCandidate(Candidate candidate);

        Task<Result<string>> UpdateCandidateAsync(Guid Id, CandidateDto candidateDto);

        Task<Result<FileDownloadDto>> GetPdfFile(string url);

        Task<Result<CandidateGetDto>> GetCandidateDto(Guid Id);

        Task<IEnumerable<Candidate>> GetCandidatesWithJobsAsync();
        Task<int> CountAsync();
        Task<IEnumerable<Candidate>> GetAllWithSkillsAsync();
        Task<Candidate> GetByIdWithSkillsAsync(Guid candidateId);

        Task<IEnumerable<Candidate>> SearchCandidatesAsync(CandidateSearchCriteria criteria);
        Task<Candidate?> GetCandidateByUserIdAsync(string userId);
        Task<ICollection<Skill>> GetCandidateSkillsAsync(Guid candidateId);

    }
}
