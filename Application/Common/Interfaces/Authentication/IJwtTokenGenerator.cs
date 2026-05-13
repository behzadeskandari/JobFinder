using JobFinder.Contracts.Dtos.JwtTokenClaims;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        public Task<string> GenerateToken(User user);

        Task<string> GetToken(User user, string token);

        public JwtTokenClaims ReadToken(string token);

        public Task<string> GenerateRefreshToken();
    }
}
