using AutoMapper;
using Azure.Core;
using FluentResults;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.File;
using JobFinder.Domain.Common.Entities;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Domain.Common.Models;
using Microsoft.Data.SqlClient;

namespace JobFinder.Persistance.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {

        //private readonly GenericReadRepository<Candidate> _readRepository;
        private readonly GenericWriteRepository<Candidate> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        private readonly IMapper _mapper;

        public CandidateRepository(WriteDbContext writeContext, ReadDbContext readContext,IMapper mapper)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
          //  _readRepository = new GenericReadRepository<Candidate>(_readContext);
            _writeRepository = new GenericWriteRepository<Candidate>(_writeContext);
            _mapper = mapper;

        }


        public async Task<IEnumerable<Candidate>> GetCandidatesWithJobsAsync()
        {
            return await _writeRepository.GetQueryable().Include(c => c.Job).ToListAsync();
        }
        public async Task<Result<Candidate>> CandidateFindAsync(Guid Id)
        {
            try
            {
                var candidate = await _writeRepository.GetByIdAsync(Id);

                if (candidate == null)
                {
                    return Result.Fail($"Candidate with ID {Id} not found");
                }

                return Result.Ok(candidate); 
                //////_mapper.Map(candidateDto, candidate);
                ////_context.Entry(candidate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ////await _context.SaveChangesAsync();

                //return Result.Ok("Candidate updated successfully");
            }
            catch (Exception ex)
            {
                return Result.Fail("Error updating candidate")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result> CreateCandidate(CandidateDto candidateDto,string resumeFileName)
        {
            try
            {
                var newCandidate = _mapper.Map<Candidate>(candidateDto);
                newCandidate.ResumeUrl = resumeFileName;

                await _writeRepository.AddAsync(newCandidate);
               // await _writeContext.SaveChangesAsync();

                var result = Task.FromResult(Result.Ok("Candidate saved successfully"));
                return null;
            }
            catch (Exception ex)
            {
                return Result.Fail("Error creating candidate")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<CandidateDto>>> GetCandidate()
        {
            try
            {
                var candidates = await _writeRepository.GetQueryable()
                    .Include(c => c.Job)
                    .OrderByDescending(c => c.DateCreated)
                    .AsNoTracking().ToListAsync();

                var candidateDtos = _mapper.Map<IEnumerable<CandidateDto>>(candidates);

                return Result.Ok(candidateDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<CandidateDto>>("Error retrieving candidates")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result<CandidateGetDto>> GetCandidateDto(Guid Id)
        {
            try
            {
                var candidate = await _writeRepository.GetQueryable() 
                    .Include(c => c.Job)
                    .AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);

                if (candidate == null)
                {
                    return Result.Fail<CandidateGetDto>($"Candidate with ID {Id} not found");
                }

                var candidateDto = _mapper.Map<CandidateGetDto>(candidate);
                return Result.Ok(candidateDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<CandidateGetDto>($"Error retrieving candidate with ID {Id}")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result<string>> RemoveCandidate(Candidate candidate)
        {
            try
            {
                await _writeRepository.DeleteAsync(candidate);
                return Result.Ok("Candidate updated successfully");
            }
            catch (Exception ex)
            {
                return Result.Fail("Error updating candidate")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result<string>> UpdateCandidateAsync(Guid Id, CandidateDto candidateDto)
        {

            try
            {
                var candidate = await _writeRepository.GetByIdAsync(Id);

                if (candidate == null)
                {
                    return Result.Fail($"Candidate with ID {Id} not found");
                }

                _mapper.Map(candidateDto, candidate);
                _writeContext.Entry(candidate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //    await _writeContext.SaveChangesAsync();

                return Result.Ok("Candidate updated successfully");
            }
            catch (Exception ex)
            {
                return Result.Fail("Error updating candidate")
                             .WithError(ex.Message);
            }
        }

        public async Task<Result<FileDownloadDto>> GetPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", url);

            if (!System.IO.File.Exists(filePath))
            {
                return Result.Fail<FileDownloadDto>("File not found");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var fileDownloadDto = new FileDownloadDto
            {
                FileStream = fileStream,
                FileDownloadName = url,
                EnableRangeProcessing = true
            };

            return Result.Ok(fileDownloadDto);
        }

        public async Task<Candidate?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Candidate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<IEnumerable<Candidate>> FindAsync(Expression<Func<Candidate, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Candidate> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Candidate, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }


        public Task<Candidate> AddAsync(Candidate entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Candidate> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<Candidate> UpdateAsync(Candidate entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Candidate> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (Guid)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Candidate entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Candidate> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<int> CountAsync()
        {
            return await _writeContext.Candidates.CountAsync();
        }

        public async Task<IEnumerable<Candidate>> GetAllWithSkillsAsync()
        {
            var record = await _writeContext.Candidates.Include(x => x.Skill).ToListAsync();
            return record;
        }

        public Task<Candidate> GetByIdWithSkillsAsync(Guid candidateId)
        {
            var candidate = _writeContext.Candidates
                .Include(c => c.Skill)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            return candidate;
        }

        public async Task<IEnumerable<Candidate>> SearchCandidatesAsync(CandidateSearchCriteria criteria)
        {
            IQueryable<Candidate> query = _writeRepository.EntityQueryable
               .Include(c => c.Skill)
               .Include(c => c.City)
               //.Include(c => c.EducationLevelId)
               .Include(c => c.CandidateJobPreferences); // Include related data as needed for search results

            var candidate = _readContext.Educations.AsQueryable();

            var joinedData = (from cand in query
                              join edu in candidate
                              on cand.EducationLevelId equals edu.Id
                              select new { Candidate = cand, Education = edu });



            if (!string.IsNullOrEmpty(criteria.FirstName))
            {
                joinedData = joinedData.Where(c => c.Candidate.FirstName.Contains(criteria.FirstName));
            }
            if (!string.IsNullOrEmpty(criteria.LastName))
            {
                joinedData = joinedData.Where(c => c.Candidate.LastName.Contains(criteria.LastName));
            }
            if (!string.IsNullOrEmpty(criteria.Email))
            {
                joinedData = joinedData.Where(c => c.Candidate.Email.Contains(criteria.Email));
            }
            if (criteria.YearsOfExperience.HasValue)
            {
                joinedData = joinedData.Where(c => c.Candidate.YearsOfExperience >= criteria.YearsOfExperience.Value);
            }
            if (criteria.EducationLevelId.HasValue)
            {
                joinedData = joinedData.Where(c => c.Candidate.EducationLevelId == criteria.EducationLevelId.Value);
            }
            if (criteria.CityId.HasValue)
            {
                joinedData = joinedData.Where(c => c.Candidate.CityId == criteria.CityId.Value);
            }
            if (!string.IsNullOrEmpty(criteria.MBTIType))
            {
                joinedData = joinedData.Where(c => c.Candidate.MBTIType == criteria.MBTIType);
            }
            if (criteria.SkillIds != null && criteria.SkillIds.Any())
            {
                // This assumes many-to-many relationship is configured correctly.
                // For performance, consider a .Where(c => c.Skills.Any(s => criteria.SkillIds.Contains(s.Id)))
                // or a join operation.
                foreach (var skillId in criteria.SkillIds)
                {
                    joinedData = joinedData.Where(c => c.Candidate.Skill.Any(s => s.Id == skillId));
                }
            }
            if (criteria.IsActive.HasValue)
            {
                joinedData = joinedData.Where(c => c.Candidate.IsActive == criteria.IsActive.Value);
            }
            // Add more criteria as needed (e.g., LastAppliedDateRange, Keywords in CoverLetter/Resume)

            // Pagination & Sorting (Example)
            if (!string.IsNullOrEmpty(criteria.SortBy))
            {
                // Implement dynamic sorting (e.g., using System.Linq.Dynamic.Core)
                // For simplicity, hardcode for now
                if (criteria.SortBy.Equals("DateCreated", StringComparison.OrdinalIgnoreCase))
                {
                    joinedData = criteria.SortOrder == "desc" ? joinedData.OrderByDescending(c => c.Candidate.DateCreated) : joinedData.OrderBy(c => c.Candidate.DateCreated);
                }
                // Add other sort options
            }

            if (criteria.PageSize > 0 && criteria.PageIndex >= 0)
            {
                joinedData = joinedData.Skip(criteria.PageIndex * criteria.PageSize).Take(criteria.PageSize);
            }

            query = joinedData.Select(x => new Candidate { 
            
            CandidateJobPreferences = x.Candidate.CandidateJobPreferences,
            Skill = x.Candidate.Skill,
            FirstName = x.Candidate.FirstName,
            LastName = x.Candidate.LastName,    
            CandidateJobPreferencesId = x.Candidate.CandidateJobPreferencesId,
            City = x.Candidate.City,
            CityId = x.Candidate.CityId,
            CoverLetter = x.Candidate.CoverLetter,  
            DateCreated = x.Candidate.DateCreated,
            DateModified = x.Candidate.DateModified,
            EducationLevelId = x.Candidate.EducationLevelId,
            Email = x.Candidate.Email,
            Id = x.Candidate.Id,
            IsActive = x.Candidate.IsActive,
            Job = x.Candidate.Job,
            JobId = x.Candidate.JobId,
            LastAppliedDate = x.Candidate.LastAppliedDate,
            MBTIType = x.Candidate.MBTIType,
            PersonalityTestResult = x.Candidate.PersonalityTestResult,
            PersonalityTestResultsId = x.Candidate.PersonalityTestResultsId,
            Phone = x.Candidate.Phone,
            PsychologyTestResult = x.Candidate.PsychologyTestResult,
            PsychologyTestResultsId = x.Candidate.PsychologyTestResultsId,
            Resume = x.Candidate.Resume,
            ResumeId = x.Candidate.ResumeId,
            ResumeUrl = x.Candidate.ResumeUrl,
            User = x.Candidate.User,
            UserId = x.Candidate.UserId,
            YearsOfExperience = x.Candidate.YearsOfExperience });
            return await query.ToListAsync();
        }

        public async Task<Candidate?> GetCandidateByUserIdAsync(string userId)
        {
            return await _writeRepository.EntityQueryable
                     .Include(c => c.CandidateJobPreferences) // Eager load related data
                     .Include(c => c.Skill)
                     .SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<ICollection<Skill>> GetCandidateSkillsAsync(Guid candidateId)
        {
            var candidate = await _writeRepository.EntityQueryable.Include(c => c.Skill).FirstOrDefaultAsync(c => c.Id == candidateId);
            return candidate?.Skill ?? new List<Skill>();

        }


        public async Task<PagedResult<Candidate>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Candidate, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Candidate>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Candidate, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Candidate>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
