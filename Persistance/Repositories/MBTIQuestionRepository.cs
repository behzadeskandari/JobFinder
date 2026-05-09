using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class MBTIQuestionRepository : IMBTIQuestionRepository
    {

       // private readonly GenericReadRepository<MBTIQuestion> _readRepository;
        private readonly GenericWriteRepository<MBTIQuestion> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public MBTIQuestionRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readRepository = new GenericReadRepository<MBTIQuestion>(_readContext);
            _writeRepository = new GenericWriteRepository<MBTIQuestion>(_writeContext);
        }
        public Task<MBTIQuestion?> GetByIdAsync(object id)
        {
            return _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MBTIQuestion>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public Task<IEnumerable<MBTIQuestion>> FindAsync(Expression<Func<MBTIQuestion, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MBTIQuestion> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<MBTIQuestion, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<MBTIQuestion> GetByIdAsyncMBTI(int id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task AddAsyncMBTI(MBTIQuestion entity)
        {
            await _writeRepository.AddAsync(entity);
        }

        public void UpdateMBTI(MBTIQuestion entity)
        {
            _writeContext.Update(entity);
        }

        public void DeleteMBTI(MBTIQuestion entity)
        {
            _writeContext.Remove(entity);
        }

        public async Task<MBTIQuestion> AddAsync(MBTIQuestion entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<MBTIQuestion> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<MBTIQuestion> UpdateAsync(MBTIQuestion entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<MBTIQuestion> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
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

        public async Task<bool> DeleteAsync(MBTIQuestion entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<MBTIQuestion> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<MBTIQuestion>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }


        public async Task<PagedResult<MBTIQuestion>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<MBTIQuestion, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<MBTIQuestion>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<MBTIQuestion, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<MBTIQuestion>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
