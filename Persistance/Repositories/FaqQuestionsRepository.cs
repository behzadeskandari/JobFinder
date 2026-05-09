using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class FaqQuestionsRepository  : IFaqQuestionsRepository
    {
        //private readonly GenericReadRepository<FaqQuestion> _readRepository;
        private readonly GenericWriteRepository<FaqQuestion> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        public FaqQuestionsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
         //   _readRepository = new GenericReadRepository<FaqQuestion>(_readContext);
            _writeRepository = new GenericWriteRepository<FaqQuestion>(_writeContext);
        }

        public async Task<FaqQuestion> AddAsync(FaqQuestion entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<FaqQuestion> entities)
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

        public async Task<bool> DeleteAsync(FaqQuestion entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<FaqQuestion> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<FaqQuestion, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public  async Task<IEnumerable<FaqQuestion>> FindAsync(Expression<Func<FaqQuestion, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }
        public async Task<PagedResult<FaqQuestion>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<FaqQuestion, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<FaqQuestion>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<FaqQuestion, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<FaqQuestion>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<IEnumerable<FaqQuestion>> GetAllAsync()
        {

            return await _writeRepository.GetAllAsync();
        }
        public async Task<IEnumerable<FaqQuestion>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<FaqQuestion?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public  IQueryable<FaqQuestion> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<FaqQuestion> UpdateAsync(FaqQuestion entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<FaqQuestion> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
