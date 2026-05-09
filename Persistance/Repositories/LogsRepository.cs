using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.WriteDbContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Persistance.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly WriteDbContext _context;
        public LogsRepository(WriteDbContext context)
        {
            _context = context;
        }
        public Logs AddLogs(Logs logs)
        {
            var record = _context.Logs.Add(logs);
            return record.Entity;
        }

        public async Task<int> DeleteLogs(Logs logs)
        {
            var record = await _context.Logs.Where(x => x.Id == logs.Id).ExecuteDeleteAsync();
            return record;
        }

        public async Task DeleteLogsBatch(List<Logs> logs)
        {
            _context.Logs.RemoveRange(logs);
             await Task.FromResult(Task.CompletedTask);
        }

        public async Task<IEnumerable<Logs>> GetAllAsync()
        {
            var recrod =  _context.Logs.AsEnumerable();
            return await Task.FromResult<IEnumerable<Logs>>(recrod);
        }

        public Task<Logs> AddAsync(Logs entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Logs> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Logs> UpdateAsync(Logs entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<Logs> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Logs entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<Logs> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Logs>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<Logs?> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Logs>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Logs>> FindAsync(Expression<Func<Logs, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Logs> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<Logs, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Logs>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Logs, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Logs>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Logs, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
