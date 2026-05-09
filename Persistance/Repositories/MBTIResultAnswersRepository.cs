using AutoMapper;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class MBTIResultAnswersRepository : IMBTIResultAnswersRepository
    {
        private readonly GenericWriteRepository<MBTIResultAnswer> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        private IMapper _mapper;

        public MBTIResultAnswersRepository(IMapper mapper, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Blog>(_readContext);
            _writeRepository = new GenericWriteRepository<MBTIResultAnswer>(_writeContext);
            _mapper = mapper;
        }
        public async Task<MBTIResultAnswer> AddAsync(MBTIResultAnswer entity)
        {
            await _writeRepository.AddAsync(entity);
            //await .SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<MBTIResultAnswer> entities)
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

        public async Task<bool> DeleteAsync(MBTIResultAnswer entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<MBTIResultAnswer> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<MBTIResultAnswer>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

        public async Task<bool> ExistsAsync(Expression<Func<MBTIResultAnswer, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<MBTIResultAnswer>> FindAsync(Expression<Func<MBTIResultAnswer, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<MBTIResultAnswer>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            return await _writeRepository.GetAllAsync();
        }

        public async Task<MBTIResultAnswer?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<PagedResult<MBTIResultAnswer>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<MBTIResultAnswer, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<MBTIResultAnswer>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<MBTIResultAnswer, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }

        public IQueryable<MBTIResultAnswer> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<MBTIResultAnswer> UpdateAsync(MBTIResultAnswer entity)
        {
            await _writeRepository.UpdateAsync(entity);
            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<MBTIResultAnswer> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
