using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Persistance.Repositories
{
    public class PricingFeaturesRepository : IPricingFeaturesRepository
    {
       // private readonly GenericReadRepository<PricingFeature> _readRepository;
        private readonly GenericWriteRepository<PricingFeature> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public PricingFeaturesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
       //     _readRepository = new GenericReadRepository<PricingFeature>(_readContext);
            _writeRepository = new GenericWriteRepository<PricingFeature>(_writeContext);
        }

        public async Task<PricingFeature> AddAsync(PricingFeature entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<PricingFeature> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }
        public async Task<PagedResult<PricingFeature>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<PricingFeature, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<PricingFeature>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<PricingFeature, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<PricingFeature>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
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

        public async Task<bool> DeleteAsync(PricingFeature entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<PricingFeature> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<PricingFeature, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public  async Task<IEnumerable<PricingFeature>> FindAsync(Expression<Func<PricingFeature, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<PricingFeature>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<PricingFeature>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<PricingFeature?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<PricingFeature> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<PricingFeature> UpdateAsync(PricingFeature entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<PricingFeature> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
