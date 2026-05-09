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
    public class ProductInventorySnapshotsRepository : IProductInventorySnapshots
    {
        //private readonly GenericReadRepository<ProductInventorySnapshot> _readRepository;
        private readonly GenericWriteRepository<ProductInventorySnapshot> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public ProductInventorySnapshotsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<ProductInventorySnapshot>(_readContext);
            _writeRepository = new GenericWriteRepository<ProductInventorySnapshot>(_writeContext);
        }

        public async Task<ProductInventorySnapshot> AddAsync(ProductInventorySnapshot entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }
        public async Task<PagedResult<ProductInventorySnapshot>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<ProductInventorySnapshot, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<ProductInventorySnapshot>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<ProductInventorySnapshot, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<ProductInventorySnapshot>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<ProductInventorySnapshot> entities)
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

        public async Task<bool> DeleteAsync(ProductInventorySnapshot entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<ProductInventorySnapshot> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<ProductInventorySnapshot, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<ProductInventorySnapshot>> FindAsync(Expression<Func<ProductInventorySnapshot, bool>> expression)
        {

            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<ProductInventorySnapshot>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<ProductInventorySnapshot?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<ProductInventorySnapshot> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<ProductInventorySnapshot> UpdateAsync(ProductInventorySnapshot entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<ProductInventorySnapshot> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
