using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces.Authentication;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Contracts.Dtos.JwtTokenClaims;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JobFinder.Application.Services
{
    public class JwtService : IJwtTokenGenerator
    {
        //private readonly IMemoryCache _cache;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtKey;
        public JwtService(IConfiguration config, IDistributedCache cache)
        {
            _config = config;
            _cache = cache;
            // jwtToken is used for bath encrypting and description the JWT token 
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

        }
        public async Task<string> GenerateToken(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var creadentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.Now.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                SigningCredentials = creadentials,
            };
            //await _signInManager.SignInAsync(user, isPersistent: false);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(jwt);
            if (string.IsNullOrEmpty(token) || token.Split('.').Length != 3)
            {

                var redisKey = $"verification:{token}";

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                };

                await _cache.SetStringAsync(
                    redisKey,
                    user.UserName,
                    options
                );
                //_cache.Set(user.Id, token, TimeSpan.FromDays(int.Parse(_config["JWT:ExpiresInDays"])));
                throw new Exception("Generated JWT is invalid");
            }
            return token;
        }

        public async Task<string> GetToken(User user, string Incomingtoken)
        {
            var token = string.Empty;
            if (Incomingtoken != null)
            {
                token = await GenerateToken(user);
            }
            else  if (string.IsNullOrWhiteSpace(token))
            {
                await _cache.SetStringAsync(user.UserName, user.UserName);
                token = await GenerateToken(user);
                await _cache.SetStringAsync(user.UserName, token);
                return token;
            }
           
            return token;
        }

        public JwtTokenClaims ReadToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
                throw new ArgumentException("Invalid JWT token");


            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken.ValidTo <= DateTime.Now)
            {
                return new JwtTokenClaims();
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var roles = jwtToken.Claims.FirstOrDefault(X => X.Value == Roles.Staff | X.Value == Roles.Admin | X.Value == Roles.User);
            var given_name = jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name");
            var family_name = jwtToken.Claims.FirstOrDefault(x => x.Type == "family_name");
            var userId = jwtToken.Claims.First().Value;

            return new JwtTokenClaims
            {
                UserId = userId,
                Email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                FirstName = given_name.ToString(),
                LastName = family_name.ToString(),
                Role = roles.Value,
                ExpiresAt = jwtToken.ValidTo
            };
        }

        public async Task<string> GenerateRefreshToken()
        {
            // Get refresh token length from configuration (default to 32 bytes if not specified)
            int tokenLength = int.TryParse(_config["JWT:RefreshTokenLength"], out var length) ? length : 32;

            // Generate a cryptographically secure random string
            byte[] randomBytes = RandomNumberGenerator.GetBytes(tokenLength);
            string refreshToken = Convert.ToBase64String(randomBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "")
                .Substring(0, tokenLength);

            // Set expiration for refresh token (default to 7 days if not specified)
            int refreshTokenExpiryDays = int.TryParse(_config["JWT:RefreshTokenExpiresInDays"], out var expiry) ? expiry : 7;

            // Store the refresh token in cache with expiration
            //_cache.Set($"RefreshToken_{refreshToken}", true, TimeSpan.FromDays(refreshTokenExpiryDays));
            var redisKey = $"verification:{refreshToken}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            await _cache.SetStringAsync(
                redisKey,
                refreshToken,
                options
            );
            return await Task.FromResult(refreshToken);
        }
    }
}
