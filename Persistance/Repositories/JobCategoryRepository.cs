using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Persistance.Repositories.GenericRepository;
using JobFinder.Domain.Common.Models;
using Microsoft.Data.SqlClient;
using Persistance.DatabaseContext.WriteDbContext;
using Persistance.DatabaseContext.ReadDbContext;

namespace JobFinder.Persistance.Repositories
{

    public class JobCategoryRepository :  IJobCategoryRepository
    {
        //private readonly GenericReadRepository<JobCategory> _readRepository;
        private readonly GenericWriteRepository<JobCategory> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public JobCategoryRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobCategory>(_readContext);
            _writeRepository = new GenericWriteRepository<JobCategory>(_writeContext);
        }

        public async Task<JobCategory?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }
        public async Task<PagedResult<JobCategory>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<JobCategory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<JobCategory>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<JobCategory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<JobCategory>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<IEnumerable<JobCategory>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }
        public async Task<IEnumerable<JobCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<IEnumerable<JobCategory>> FindAsync(Expression<Func<JobCategory, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<JobCategory> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<JobCategory, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<JobCategory> GetByIdAsync(int id)
        {
            var jobCategories = await _writeRepository.GetByIdAsync(id);
            return jobCategories;
        }

        public async Task<IEnumerable<JobCategoryDto>> GetJobCategories()
        {
            var jobCategories = await _writeContext.JobCategories.Select(j => new JobCategoryDto
            {
                Label = j.Name,
                Value = j.Value
            }).ToListAsync();
            return jobCategories;
        }


        public async Task<JobCategory> AddAsyncJobCategory(JobCategory jobCategory)
        {
            await _writeRepository.AddAsync(jobCategory);
            //await _context.SaveChangesAsync();
            return jobCategory;
        }

        public async Task<JobCategory> AddAsync(JobCategory entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            //await _context.SaveChangesAsync();
            return record;
        }

        public async Task AddRangeAsync(IEnumerable<JobCategory> entities)
        { 
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<JobCategory> UpdateAsync(JobCategory entity)
        {
            _writeContext.Entry(entity).State = EntityState.Modified;
          var record= await  _writeRepository.UpdateAsync(entity);
            //await _context.SaveChangesAsync();
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<JobCategory> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (int)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(JobCategory entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<JobCategory> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task JobCategoryUpdateAsync(JobCategory jobCategory)
        {
            _writeContext.Entry(jobCategory).State = EntityState.Modified;
            _writeRepository.UpdateAsync(jobCategory);
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var jobCategory = await _writeRepository.FindAsync(x => x.Id == id);
            if (jobCategory != null)
            {
                var cat = jobCategory as JobCategory;
                await _writeRepository.DeleteAsync(id);
                //await _context.SaveChangesAsync();
            }
        }
    }
}
