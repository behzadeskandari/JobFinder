using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Persistance.Repositories
{
    public  class ResumeRepository : IResumeRepository
    {
        //private readonly GenericReadRepository<Resume> _readRepository;
        private readonly GenericWriteRepository<Resume> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public ResumeRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Resume>(_readContext);
            _writeRepository = new GenericWriteRepository<Resume>(_writeContext);
        }

        public async Task<PagedResult<Resume>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Resume, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Resume>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Resume, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Resume>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<Resume> GetResume(Guid id)
        {
            var resume = await _writeRepository.GetQueryable()
                .Include(r => r.WorkExperiences)
                .Include(r => r.Educations)
                .Include(r => r.Skills)
                .Include(r => r.Languages)
                .FirstOrDefaultAsync(r => r.Id == id);

            return resume;
        }

        public async Task<Resume> CreateResume(Resume resume)
        {
            resume.CreatedAt = DateTime.Now;
            resume.UpdatedAt = DateTime.Now;

            await _writeRepository.AddAsync(resume);
            //await _context.SaveChangesAsync();
            resume.IsPersisted = true;
            return resume;
        }

        public async Task<Resume> UpdateResume(Guid id,Resume resume)
        {
            var resumeNull = new Resume();
            if (id != resume.Id)
            {
                return null;
            }

            resume.UpdatedAt = DateTime.Now;
            _writeContext.Entry(resume).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                resume.IsPersisted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ResumeExists(id))
                {
                    resume.IsPersisted = false;
                    return resume;
                }
                else
                {
                    throw;
                }
            }

            return resume;
        }

        public async Task<Resume> DeleteResume(Resume resume)
        {
            await _writeRepository.DeleteAsync(resume);
            //await _context.SaveChangesAsync();
            return resume;
        }

        public async Task<Resume> GetResumePdf(Guid id)
        {
            var resume = await _writeRepository.GetQueryable()
                .Include(r => r.WorkExperiences)
                .Include(r => r.Educations)
                .Include(r => r.Skills)
                .Include(r => r.Languages)
                .FirstOrDefaultAsync(r => r.Id == id);
            return resume;
        }

        public async Task<bool> ResumeExists(Guid id)
        {
            return await _writeRepository.ExistsAsync(e => e.Id == id);
        }


        public Task<Resume> AddAsync(Resume entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Resume> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);

        }

        public async Task<Resume> UpdateAsync(Resume entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public  async Task UpdateRangeAsync(IEnumerable<Resume> entities)
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

        public async Task<bool> DeleteAsync(Resume entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Resume> entities)
        {

            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<Resume?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Resume>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Resume>> FindAsync(Expression<Func<Resume, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Resume> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Resume, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<Resume>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
