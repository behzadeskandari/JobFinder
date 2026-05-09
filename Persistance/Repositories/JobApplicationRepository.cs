using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;

namespace JobFinder.Persistance.Repositories
{
    public class JobApplicationRepository : IJobApplication
    {
        //private readonly GenericReadRepository<JobApplication> _readRepository;
        private readonly GenericWriteRepository<JobApplication> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public JobApplicationRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobApplication>(_readContext);
            _writeRepository = new GenericWriteRepository<JobApplication>(_writeContext);
        }

        public async Task<JobApplication> AddAsync(JobApplication entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }
        public async Task<PagedResult<JobApplication>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<JobApplication, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<JobApplication>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<JobApplication, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<JobApplication>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<JobApplication> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            await Task.FromResult(record);
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

        public async Task<bool> DeleteAsync(JobApplication entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<JobApplication> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<JobApplication, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<JobApplication>> FindAsync(Expression<Func<JobApplication, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<JobApplication?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<JobApplication> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<JobApplication> UpdateAsync(JobApplication entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<JobApplication> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
