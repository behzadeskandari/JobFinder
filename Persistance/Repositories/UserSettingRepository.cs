using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure.Persistence.Repositories
{
    public class UserSettingRepository : IUserSettingRepository
    {
        //private readonly GenericReadRepository<UserSetting> _readRepository;
        private readonly GenericWriteRepository<UserSetting> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic
        private IMapper _mapper;

        public UserSettingRepository(IMapper mapper,WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<UserSetting>(_readContext);
            _writeRepository = new GenericWriteRepository<UserSetting>(_writeContext);
            _mapper = mapper;
        }

        public async Task<UserSetting> GetByUserIdAsync(string userId)
        {
            return await _writeRepository.GetQueryable()
                .FirstOrDefaultAsync(us => us.UserId == userId);
        }

        public async Task<UserSetting> CreateAsync(UserSetting userSetting)
        {
            await _writeRepository.AddAsync(userSetting);
            return userSetting;
        }

        public async Task<UserSetting> UpdateAsync(UserSetting userSetting)
        {
            await _writeRepository.UpdateAsync(userSetting);
            return userSetting;
        }

        public async Task<UserSetting> AddAsync(UserSetting entity)
        {
            await _writeRepository.AddAsync(entity);
            //await .SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<UserSetting> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<UserSetting> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
        public async Task<PagedResult<UserSetting>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<UserSetting, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<UserSetting>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<UserSetting, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<UserSetting>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
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

        public async Task<bool> DeleteAsync(UserSetting entity)
        {

            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<UserSetting> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<UserSetting?> GetByIdAsync(object id)
        {

            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserSetting>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserSetting>> FindAsync(Expression<Func<UserSetting, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public IQueryable<UserSetting> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<UserSetting, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<UserSettingsDto> GetUserSettingsAsync(string userId)
        {

            var userSettings = await _writeRepository.GetByIdAsync(userId);
            if (userSettings == null)
            {
                // Create default settings if not exists
                var defaultSettings = new UserSetting { UserId = userId };
                userSettings = await _writeRepository.AddAsync(defaultSettings);
            }

            return _mapper.Map<UserSettingsDto>(userSettings);

        }
    }
}
