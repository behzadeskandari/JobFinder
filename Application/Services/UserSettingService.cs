using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Common.Interfaces;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Services
{
    public class UserSettingService : IUserSettingService
    {
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly IMapper _mapper;

        public UserSettingService(IUserSettingRepository userSettingRepository, IMapper mapper)
        {
            _userSettingRepository = userSettingRepository;
            _mapper = mapper;
        }

        public async Task<UserSettingsDto> GetUserSettingsAsync(string userId)
        {
            var userSettings = await _userSettingRepository.GetByUserIdAsync(userId);
            if (userSettings == null)
            {
                // Create default settings if not exists
                var defaultSettings = new UserSetting { UserId = userId };
                userSettings = await _userSettingRepository.CreateAsync(defaultSettings);
            }

            return _mapper.Map<UserSettingsDto>(userSettings);
        }

        public async Task<UserSettingsDto> UpdateUserSettingsAsync(string userId, UserSettingsDto settingsDto)
        {
            var userSettings = await _userSettingRepository.GetByUserIdAsync(userId);
            
            if (userSettings == null)
            {
                // Create new settings if not exists
                var newSettings = _mapper.Map<UserSetting>(settingsDto);
                newSettings.UserId = userId;
                userSettings = await _userSettingRepository.CreateAsync(newSettings);
            }
            else
            {
                // Update existing settings
                _mapper.Map(settingsDto, userSettings);
                userSettings = await _userSettingRepository.UpdateAsync(userSettings);
            }

            return _mapper.Map<UserSettingsDto>(userSettings);
        }
    }
}
