using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Models;
using Microsoft.Data.SqlClient;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Persistance.Repositories
{
    public class TechnicalOptionsRepository :  ITechnicalOptionsRepository
    {

        //private readonly GenericReadRepository<TechnicalOption> _readRepository;
        private readonly GenericWriteRepository<TechnicalOption> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public TechnicalOptionsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readRepository = new GenericReadRepository<TechnicalOption>(_readContext);
            _writeRepository = new GenericWriteRepository<TechnicalOption>(_writeContext);
        }


        public async Task AddAsyncTechnical(TechnicalOption option)
        {
            var result = await _writeRepository.AddAsync(option);
        }

        public async void DeleteTechnical(TechnicalOption option)
        {
            await _writeRepository.GetQueryable()
                .Where(x => x.Id == option.Id)
                .ExecuteDeleteAsync();
        }

        public async Task<TechnicalOption> GetByIdAsyncTechnical(int id)
        {
            var result = await _writeRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<TechnicalOptionDto>> GetTechnicalOptionsTechnical()
        {
            var result = await _writeRepository.GetAllAsync();
            return result.Select(option => new TechnicalOptionDto
            {
                Id = option.Id,
                Label = option.Label,
                Value = option.Value
            });
        }

        public async Task<TechnicalOption> AddAsync(TechnicalOption entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            return record;
        }

        public Task AddRangeAsync(IEnumerable<TechnicalOption> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<TechnicalOption> UpdateAsync(TechnicalOption entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<TechnicalOption> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
        public async Task<PagedResult<TechnicalOption>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TechnicalOption, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<TechnicalOption>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<TechnicalOption, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<TechnicalOption>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
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

        public async Task<bool> DeleteAsync(TechnicalOption entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TechnicalOption> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<TechnicalOption?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TechnicalOption>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TechnicalOption>> FindAsync(Expression<Func<TechnicalOption, bool>> expression)
        {

            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<TechnicalOption> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TechnicalOption, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<TechnicalOption>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
