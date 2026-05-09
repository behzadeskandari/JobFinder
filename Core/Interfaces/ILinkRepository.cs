using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface ILinkRepository
    {
        Task<GeneratedLink> AddAsync(GeneratedLink link);
        Task<GeneratedLink?> GetByTokenAsync(string token);
        Task<bool> IsValidAsync(string token);
    }
}
