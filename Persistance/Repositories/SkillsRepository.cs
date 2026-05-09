using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Persistance.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        //private readonly GenericReadRepository<Skill> _readRepository;
        private readonly GenericWriteRepository<Skill> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public SkillsRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           // _readRepository = new GenericReadRepository<Skill>(_readContext);
            _writeRepository = new GenericWriteRepository<Skill>(_writeContext);
        }

        public async Task<Skill> AddAsync(Skill entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }
        public async Task<PagedResult<Skill>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Skill, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Skill>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Skill, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Skill>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddRangeAsync(IEnumerable<Skill> entities)
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

        public async Task<bool> DeleteAsync(Skill entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Skill> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Skill, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<Skill>> FindAsync(Expression<Func<Skill, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Skill>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Skill>> GetByCandidatesIds(IEnumerable<Candidate> candidates)
        {
            var candidateIds = candidates.Select(c => c.Id).ToList();

            var skills = await GetQueryable()
                .Where(x => candidateIds.Contains(x.CandidateId))
                .ToListAsync(); 

            return skills;
        }


        public async Task<Skill?> GetByIdAsync(object id)
        {

            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<Skill> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<Skill> UpdateAsync(Skill entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<Skill> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
