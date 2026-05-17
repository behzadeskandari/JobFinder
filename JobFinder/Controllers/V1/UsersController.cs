using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces;
using JobFinder.Application.Common.Interfaces.Authentication;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Contracts.Dtos.Account.Auth;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobFinder.Controllers.V1
{
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAccountService _accountService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IConfiguration _config;

        public UsersController(
                IUnitOfWork unitOfWork,
                ICurrentUserService currentUserService,
                IAccountService accountService,
                IJwtTokenGenerator jwtTokenGenerator,
                IUserSettingService userSettingService,
                IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            //_userSettingService = userSettingService;
            _accountService = accountService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _config = config;
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            return Ok(users);
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id}")]
        [HttpPost("{id}")]
        public async Task<IActionResult> GetUser(Guid id) // ID is string for ApplicationUser
        {
            var ids = id.ToString();
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(ids);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(user);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id}")]
        [HttpPost("{id}")]
        public async Task<IActionResult> DisableUser(Guid id) // ID is string for ApplicationUser
        {
            var ids = id.ToString();
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(ids);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            user.IsActive = false;
            await _unitOfWork.UsersRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return Ok(user);

        }

        [Authorize(Roles = Roles.All)]
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");
            User user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");

            var userDto = await _accountService.CraeteApplicationUserDto(user);
            return Ok(userDto);
        }

        [HttpPost("auth/refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(request.Token);
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Invalid token");

                var user = await _accountService.FindByIdAsync(userId);
                if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");


                var newToken = await _jwtTokenGenerator.GenerateToken(user);
                var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken().Result;

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _accountService.UpdateUserAsync(user);

                return Ok(new AuthenticationResponse
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = (int)TimeSpan.FromMinutes(15).TotalSeconds
                });
            }
            catch (SecurityTokenException ex)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: ex.Message);
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "An error occurred while refreshing token");
            }
        }

        [HttpGet("{id}/roles")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var roles = await _accountService.GetUserRolesAsync(id);
            return Ok(roles);
        }

        [HttpPost("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");//Unauthorized();

            var user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");

            // Update user properties
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _accountService.UpdateUserAsync(user);
            if (!result)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to update profile");

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");

            var user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");

            var result = await _accountService.ChangePasswordAsync(user.Id, request.CurrentPassword, request.NewPassword);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to change password");
            }

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }

        [HttpPost("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequestDto request)
        {
            var user = await _accountService.FindByEmailAsync(request.Email);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");

            var result = await _accountService.ConfirmEmailAsync(user.Id, request.Token);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid verification token");
            }

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }

        [HttpPost("send-verification-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeRequestDto request)
        {
            var user = await _accountService.FindByEmailAsync(request.Email);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");

            // Generate a verification token
            var token = await _accountService.GenerateEmailConfirmationTokenAsync(user.Id);

            // In a real application, you would send this token to the user's email
            // For now, we'll just return it in the response for testing
            return Ok(new { Token = token });
        }


        [HttpPost("delete-account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("NotFound");
            }
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Unauthorized();

            var result = await _accountService.DeleteUserAsync(user.Id);
            if (!result)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to delete account");
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }


        [HttpGet("settings")]
        [Authorize]
        public async Task<ActionResult<UserSettingsDto>> GetSettings()
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");

            var user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");

            // In a real application, you would map user settings from the database
            var settings = await _unitOfWork.UserSettingRepository.GetUserSettingsAsync(user.Id);
            return Ok(settings);
        }



        [HttpGet("CreateSettings/{userSettingsDto}")]
        [Authorize]
        public async Task<ActionResult<UserSettingsDto>> CreateSettings(UserSettingsDto userSettingsDto)
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");

            var user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");
            var UserSettings = new UserSetting()
            {
                UserId = user.Id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                EmailNotifications = userSettingsDto.EmailNotifications,
                IsActive = true,
                IsProfilePublic = userSettingsDto.IsProfilePublic,
                Language = userSettingsDto.Language,
                NotificationPriority = userSettingsDto.NotificationPriority,
                ReceiveJobRecommendations = userSettingsDto.ReceiveJobRecommendations,
                PushNotifications = userSettingsDto.PushNotifications,
                SavedSearchFilters = userSettingsDto.SavedSearchFilters,
                SmsNotifications = userSettingsDto.SmsNotifications,
                TimeZone = userSettingsDto.TimeZone,
                TwoFactorEnabled = userSettingsDto.TwoFactorEnabled,
            };
            // In a real application, you would map user settings from the database
            var settings = await _unitOfWork.UserSettingRepository.CreateAsync(UserSettings);
            await _unitOfWork.CommitAsync();
            return Ok(settings);
        }

        [HttpPut("settings")]
        [Authorize]
        public async Task<ActionResult<UserSettingsDto>> UpdateSettings([FromBody] UserSettingsDto settings)
        {
            if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "Unauthorized");

            var user = await _accountService.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User not found");
            //_currentUserService.UserId
            var UserSettings = new UserSetting()
            {
                UserId = _currentUserService.UserId,
                EmailNotifications = settings.EmailNotifications,
                Language = settings.Language,
                TimeZone = settings.TimeZone,
                SmsNotifications = settings.SmsNotifications,

            };
            var updatedSettings = await _unitOfWork.UserSettingRepository.UpdateAsync(UserSettings);
            return Ok(updatedSettings);
        }

        #region Private
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), // Replace with your actual secret key from configuration
                ValidateLifetime = false // We're validating an expired token here
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        
        #endregion Private
    }
}
