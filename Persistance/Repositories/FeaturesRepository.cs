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
    internal class FeaturesRepository : IFeaturesRepository
    {
        //private readonly GenericReadRepository<Feature> _readRepository;
        private readonly GenericWriteRepository<Feature> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        public FeaturesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Feature>(_readContext);
            _writeRepository = new GenericWriteRepository<Feature>(_writeContext);
        }

        public async Task<Feature> AddAsync(Feature entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<Feature> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }
        public async Task<PagedResult<Feature>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Feature, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Feature>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Feature, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Feature>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
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

        public async Task<bool> DeleteAsync(Feature entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Feature> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Feature, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public  async Task<IEnumerable<Feature>> FindAsync(Expression<Func<Feature, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<Feature>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Feature?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<Feature> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<Feature> UpdateAsync(Feature entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<Feature> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
