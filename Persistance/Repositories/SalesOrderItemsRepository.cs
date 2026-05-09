using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class SalesOrderItemsRepository : ISalesOrderItems
    {
        //private readonly GenericReadRepository<SalesOrderItem> _readRepository;
        private readonly GenericWriteRepository<SalesOrderItem> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public SalesOrderItemsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<SalesOrderItem>(_readContext);
            _writeRepository = new GenericWriteRepository<SalesOrderItem>(_writeContext);
        }

        public async Task<SalesOrderItem> AddAsync(SalesOrderItem entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }
        public async Task<PagedResult<SalesOrderItem>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<SalesOrderItem, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<SalesOrderItem>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<SalesOrderItem, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<SalesOrderItem>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<SalesOrderItem> entities)
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

        public async Task<bool> DeleteAsync(SalesOrderItem entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<SalesOrderItem> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<SalesOrderItem, bool>> expression)
        {

            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<SalesOrderItem>> FindAsync(Expression<Func<SalesOrderItem, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<SalesOrderItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SalesOrderItem?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<SalesOrderItem> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<SalesOrderItem> UpdateAsync(SalesOrderItem entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<SalesOrderItem> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
