using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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
    public class SubmissionDetailsRepository : ISubmissionDetails
    {
        //private readonly GenericReadRepository<SubmissionDetails> _readRepository;
        private readonly GenericWriteRepository<SubmissionDetails> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public SubmissionDetailsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<SubmissionDetails>(_readContext);
            _writeRepository = new GenericWriteRepository<SubmissionDetails>(_writeContext);
        }

        public async Task<SubmissionDetails> AddAsync(SubmissionDetails entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<SubmissionDetails> entities)
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

        public async Task<bool> DeleteAsync(SubmissionDetails entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<SubmissionDetails> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<SubmissionDetails, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }
        public async Task<PagedResult<SubmissionDetails>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<SubmissionDetails, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<SubmissionDetails>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<SubmissionDetails, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<SubmissionDetails>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<IEnumerable<SubmissionDetails>> FindAsync(Expression<Func<SubmissionDetails, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<SubmissionDetails>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SubmissionDetails?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<SubmissionDetails> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<SubmissionDetails> UpdateAsync(SubmissionDetails entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<SubmissionDetails> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
