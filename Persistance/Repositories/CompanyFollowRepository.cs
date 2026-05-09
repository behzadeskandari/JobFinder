using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;

namespace JobFinder.Persistance.Repositories
{
    public class CompanyFollowRepository : ICompanyFollowRepository
    {
        //private readonly GenericReadRepository<CompanyFollow> _readRepository;
        private readonly GenericWriteRepository<CompanyFollow> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        private IMapper _mapper;

        public CompanyFollowRepository(IMapper mapper, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<CompanyFollow>(_readContext);
            _writeRepository = new GenericWriteRepository<CompanyFollow>(_writeContext);
            _mapper = mapper;
        }

        public async Task<CompanyFollow> AddAsync(CompanyFollow entity)
        {
            await _writeRepository.AddAsync(entity);
            //await .SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<CompanyFollow> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
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

        public async Task<bool> DeleteAsync(CompanyFollow entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<CompanyFollow> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<CompanyFollow, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<CompanyFollow>> FindAsync(Expression<Func<CompanyFollow, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<CompanyFollow>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<CompanyFollow?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<CompanyFollow> GetQueryable()
        {

            return _writeRepository.GetQueryable();
        }

        public async Task<CompanyFollow> UpdateAsync(CompanyFollow entity)
        {
            await _writeRepository.UpdateAsync(entity);
            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<CompanyFollow> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<PagedResult<CompanyFollow>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<CompanyFollow, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<CompanyFollow>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<CompanyFollow, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<CompanyFollow>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
