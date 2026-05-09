using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface ILinkService
    {
        Task<string> GenerateUniqueLinkAsync(string purpose, DateTime? expirationDate = null, string? associatedData = null);
        Task<bool> ValidateLinkAsync(string token);
        Task<GeneratedLink?> GetLinkDetailsAsync(string token);
    }
}
