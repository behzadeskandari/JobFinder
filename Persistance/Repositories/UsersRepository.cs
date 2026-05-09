using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Repository;
using JobFinder.Application.Services;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        //private readonly GenericReadRepository<User> _readRepository;
        private readonly GenericWriteRepository<User> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        private readonly IAccountService _accountService;
        public UsersRepository(WriteDbContext writeContext, ReadDbContext readContext, IAccountService accountService)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<User>(_readContext);
            _writeRepository = new GenericWriteRepository<User>(_writeContext);
            _accountService = accountService; 
        }

        public async Task<User> AddAsync(User entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<User> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            await Task.FromResult(record);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (string)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<User> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public  async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var userWithMail = _accountService.FindByEmailAsync(email);
            return userWithMail != null ? userWithMail : throw new ArgumentNullException(nameof(userWithMail), "User with the specified email does not exist.");
        }

        public async Task<User?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public IQueryable<User> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<IEnumerable<User>> GetRolesAsync(string userId)
        {
            var user = await _writeRepository.FindAsync(x => x.Id == userId);
;
            return user;
        }

        public async Task<PagedResult<User>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<User, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<User>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<User, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<User>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<User> UpdateAsync(User entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<User> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
