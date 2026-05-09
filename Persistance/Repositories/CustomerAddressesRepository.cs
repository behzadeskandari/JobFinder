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
    public class CustomerAddressesRepository : ICustomerAddressesRepository
    {
        //private readonly GenericReadRepository<CustomerAddress> _readRepository;
        private readonly GenericWriteRepository<CustomerAddress> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        public CustomerAddressesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
       //     _readRepository = new GenericReadRepository<CustomerAddress>(_readContext);
            _writeRepository = new GenericWriteRepository<CustomerAddress>(_writeContext);
        }
        public async Task<PagedResult<CustomerAddress>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<CustomerAddress, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<CustomerAddress>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<CustomerAddress, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<CustomerAddress>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<CustomerAddress> AddAsync(CustomerAddress entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<CustomerAddress> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public  async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (Guid)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(CustomerAddress entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<CustomerAddress> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<CustomerAddress, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<CustomerAddress>> FindAsync(Expression<Func<CustomerAddress, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<CustomerAddress>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<CustomerAddress?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<CustomerAddress> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<CustomerAddress> UpdateAsync(CustomerAddress entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<CustomerAddress> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
