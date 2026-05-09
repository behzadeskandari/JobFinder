using JobFinder.Contracts.Dtos.Account;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces
{
    public interface IUserSettingService
    {
        Task<UserSettingsDto> GetUserSettingsAsync(string userId);
        Task<UserSettingsDto> UpdateUserSettingsAsync(string userId, UserSettingsDto settingsDto);
    }
}
