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
    public class PsychologyTestResponseRepository : IPsychologyTestResponse
    {
        ///private readonly GenericReadRepository<PsychologyTestResponse> _readRepository;
        private readonly GenericWriteRepository<PsychologyTestResponse> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public PsychologyTestResponseRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           /// _readRepository = new GenericReadRepository<PsychologyTestResponse>(_readContext);
            _writeRepository = new GenericWriteRepository<PsychologyTestResponse>(_writeContext);
        }

        public async Task<PsychologyTestResponse> AddAsync(PsychologyTestResponse entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }
        public async Task<PagedResult<PsychologyTestResponse>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<PsychologyTestResponse, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<PsychologyTestResponse>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<PsychologyTestResponse, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<PsychologyTestResponse>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<PsychologyTestResponse> entities)
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

        public async Task<bool> DeleteAsync(PsychologyTestResponse entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<PsychologyTestResponse> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<PsychologyTestResponse, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public  async Task<IEnumerable<PsychologyTestResponse>> FindAsync(Expression<Func<PsychologyTestResponse, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<PsychologyTestResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<PsychologyTestResponse?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<PsychologyTestResponse> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<PsychologyTestResponse> UpdateAsync(PsychologyTestResponse entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<PsychologyTestResponse> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
