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
    public class ProductInventoriesRepository : IProductInventories
    {
        //private readonly GenericReadRepository<ProductInventory> _readRepository;
        private readonly GenericWriteRepository<ProductInventory> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public ProductInventoriesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
          //  _readRepository = new GenericReadRepository<ProductInventory>(_readContext);
            _writeRepository = new GenericWriteRepository<ProductInventory>(_writeContext);
        }

        public async Task<ProductInventory> AddAsync(ProductInventory entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }
        public async Task<PagedResult<ProductInventory>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<ProductInventory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<ProductInventory>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<ProductInventory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<ProductInventory>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<ProductInventory> entities)
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

        public async Task<bool> DeleteAsync(ProductInventory entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<ProductInventory> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<ProductInventory, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<ProductInventory>> FindAsync(Expression<Func<ProductInventory, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<ProductInventory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<ProductInventory?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<ProductInventory> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<ProductInventory> UpdateAsync(ProductInventory entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<ProductInventory> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
