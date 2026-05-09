using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class MBTIResultRepository : IMBTIResultRepository
    {

        //private readonly GenericReadRepository<MBTIResult> _readRepository;
        private readonly GenericWriteRepository<MBTIResult> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public MBTIResultRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
          //  _readRepository = new GenericReadRepository<MBTIResult>(_readContext);
            _writeRepository = new GenericWriteRepository<MBTIResult>(_writeContext);
        }

        public async Task<IEnumerable<MBTIResult>> GetAllAsyncMBTI()
        {
            return await _writeRepository.GetAllAsync();
        }
        public async Task<PagedResult<MBTIResult>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<MBTIResult, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<MBTIResult>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<MBTIResult, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<MBTIResult>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<MBTIResult> GetByIdAsyncMBTI(int id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task AddAsyncMBTI(MBTIResult entity)
        {
            await _writeContext.MBTIResults.AddAsync(entity);
        }

        public void UpdateMBTI(MBTIResult entity)
        {
            _writeContext.MBTIResults.Update(entity);
        }

        public void DeleteMBTI(MBTIResult entity)
        {
            _writeContext.MBTIResults.Remove(entity);
        }

        public async Task<MBTIResult> AddAsync(MBTIResult entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<MBTIResult> entities)
        { 
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<MBTIResult> UpdateAsync(MBTIResult entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<MBTIResult> entities)
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

        public async Task<bool> DeleteAsync(MBTIResult entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<MBTIResult> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public  async Task<MBTIResult?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MBTIResult>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<MBTIResult>> FindAsync(Expression<Func<MBTIResult, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<MBTIResult> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<MBTIResult, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<MBTIResult>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
