using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Domain.Common.Entities;

using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
    public interface IUserSettingRepository : IWriteRepository<UserSetting> //, IReadRepository<UserSetting>
    {
        Task<UserSetting> GetByUserIdAsync(string userId);
        Task<UserSetting> CreateAsync(UserSetting userSetting);
        Task<UserSetting> UpdateAsync(UserSetting userSetting);

        public Task<UserSettingsDto> GetUserSettingsAsync(string userId);

    }
}
