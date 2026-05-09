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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobFinder.Persistance.Repositories
{
    public class CompanyBenefitsReposity : ICompanyBenefitsReposity
    {
       // private readonly GenericReadRepository<CompanyBenefit> _readRepository;
        private readonly GenericWriteRepository<CompanyBenefit> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CompanyBenefitsReposity(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
       //     _readRepository = new GenericReadRepository<CompanyBenefit>(_readContext);
            _writeRepository = new GenericWriteRepository<CompanyBenefit>(_writeContext);
        }

        public async Task<CompanyBenefit> AddAsync(CompanyBenefit entity)
        {
            await _writeRepository.AddAsync(entity);
            //await .SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<CompanyBenefit> entities)
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

        public async Task<bool> DeleteAsync(CompanyBenefit entity)
        {

            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<CompanyBenefit> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public  async Task<bool> ExistsAsync(Expression<Func<CompanyBenefit, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<CompanyBenefit>> FindAsync(Expression<Func<CompanyBenefit, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<CompanyBenefit>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }
        public async Task<IEnumerable<CompanyBenefit>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<CompanyBenefit?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<CompanyBenefit> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<CompanyBenefit> UpdateAsync(CompanyBenefit entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<CompanyBenefit> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }


        public async Task<PagedResult<CompanyBenefit>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<CompanyBenefit, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<CompanyBenefit>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<CompanyBenefit, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<CompanyBenefit>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

        public IQueryable<CompanyBenefit> GetByCompanyId(Guid companyId)
        {
            var record = _writeRepository.GetQueryable().Where(x => x.CompanyId == companyId);
            return record;
        }
    }
}
