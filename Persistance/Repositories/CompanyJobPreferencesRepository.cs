using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Domain.WriteRepository;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;

namespace JobFinder.Persistance.Repositories
{
    public class CompanyJobPreferencesRepository : ICompanyJobPreferencesRepository
    {

        //private readonly GenericReadRepository<CompanyJobPreferences> _readRepository;
        private readonly GenericWriteRepository<CompanyJobPreferences> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CompanyJobPreferencesRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readRepository = new GenericReadRepository<CompanyJobPreferences>(_readContext);
            _writeRepository = new GenericWriteRepository<CompanyJobPreferences>(_writeContext);
        }

        public async Task<CompanyJobPreferences> AddAsync(CompanyJobPreferences entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<CompanyJobPreferences> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
             await Task.FromResult(record);
        }

        public async Task<PagedResult<CompanyJobPreferences>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<CompanyJobPreferences, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<CompanyJobPreferences>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<CompanyJobPreferences, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<CompanyJobPreferences>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
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

        public async Task<bool> DeleteAsync(CompanyJobPreferences entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<CompanyJobPreferences> entities)
        { 
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<CompanyJobPreferences, bool>> expression)
        {

            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<CompanyJobPreferences>> FindAsync(Expression<Func<CompanyJobPreferences, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<CompanyJobPreferences>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<CompanyJobPreferences?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<CompanyJobPreferences> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<CompanyJobPreferences> UpdateAsync(CompanyJobPreferences entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<CompanyJobPreferences> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
