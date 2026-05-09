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
    public class InterviewDetailsRepository : IInterviewDetails
    {

        //private readonly GenericReadRepository<InterviewDetail> _readRepository;
        private readonly GenericWriteRepository<InterviewDetail> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public InterviewDetailsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<InterviewDetail>(_readContext);
            _writeRepository = new GenericWriteRepository<InterviewDetail>(_writeContext);
        }

        public async Task<InterviewDetail> AddAsync(InterviewDetail entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<InterviewDetail> entities)
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

        public async Task<bool> DeleteAsync(InterviewDetail entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<InterviewDetail> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<InterviewDetail, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<InterviewDetail>> FindAsync(Expression<Func<InterviewDetail, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<InterviewDetail>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<InterviewDetail?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<InterviewDetail> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<InterviewDetail> UpdateAsync(InterviewDetail entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<InterviewDetail> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<PagedResult<InterviewDetail>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<InterviewDetail, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<InterviewDetail>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<InterviewDetail, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<InterviewDetail>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
