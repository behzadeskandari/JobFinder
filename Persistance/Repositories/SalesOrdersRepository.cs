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
    public class SalesOrdersRepository : ISalesOrders
    {
        //private readonly GenericReadRepository<SalesOrder> _readRepository;
        private readonly GenericWriteRepository<SalesOrder> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public SalesOrdersRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           // _readRepository = new GenericReadRepository<SalesOrder>(_readContext);
            _writeRepository = new GenericWriteRepository<SalesOrder>(_writeContext);
        }

        public async Task<SalesOrder> AddAsync(SalesOrder entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<SalesOrder> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            await Task.FromResult(record);
        }
        public async Task<PagedResult<SalesOrder>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<SalesOrder, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<SalesOrder>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<SalesOrder, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<SalesOrder>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
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

        public async Task<bool> DeleteAsync(SalesOrder entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<SalesOrder> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<SalesOrder, bool>> expression)
        {

            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<SalesOrder>> FindAsync(Expression<Func<SalesOrder, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<SalesOrder>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SalesOrder?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<SalesOrder> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<SalesOrder> UpdateAsync(SalesOrder entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<SalesOrder> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
