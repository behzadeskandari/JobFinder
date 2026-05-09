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
    public class FaqCategoriesRepository : IFaqCategoriesRepository
    {
        //private readonly GenericReadRepository<FaqCategory> _readRepository;
        private readonly GenericWriteRepository<FaqCategory> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        public FaqCategoriesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<FaqCategory>(_readContext);
            _writeRepository = new GenericWriteRepository<FaqCategory>(_writeContext);
        }

        public async Task<FaqCategory> AddAsync(FaqCategory entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<FaqCategory> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (int)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(FaqCategory entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<FaqCategory> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<FaqCategory, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<FaqCategory>> FindAsync(Expression<Func<FaqCategory, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<FaqCategory>> GetAllAsync(CancellationToken cancellation =default)
        {
            return await _writeRepository.GetAllAsync(cancellation);
        }

        public async Task<IEnumerable<FaqCategory>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<FaqCategory?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<FaqCategory> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<FaqCategory> UpdateAsync(FaqCategory entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<FaqCategory> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<PagedResult<FaqCategory>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<FaqCategory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<FaqCategory>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<FaqCategory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<FaqCategory>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
