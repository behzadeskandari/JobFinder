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
    public class TermsSectionsRepository : ITermsSectionsRepository
    {
       // private readonly GenericReadRepository<TermsSection> _readRepository;
        private readonly GenericWriteRepository<TermsSection> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public TermsSectionsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readRepository = new GenericReadRepository<TermsSection>(_readContext);
            _writeRepository = new GenericWriteRepository<TermsSection>(_writeContext);
        }

        public async Task<TermsSection> AddAsync(TermsSection entity)
        {
            await _writeRepository.AddAsync(entity);
            //await .SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TermsSection> entities)
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

        public async Task<bool> DeleteAsync(TermsSection entity)
        {

            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TermsSection> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TermsSection, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<TermsSection>> FindAsync(Expression<Func<TermsSection, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<TermsSection>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TermsSection>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<TermsSection?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<TermsSection> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<TermsSection> UpdateAsync(TermsSection entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<TermsSection> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }


        public async Task<PagedResult<TermsSection>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TermsSection, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<TermsSection>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<TermsSection, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<TermsSection>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

    }
}
