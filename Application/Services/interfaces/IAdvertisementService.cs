using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Services.interfaces
{
    public interface IAdvertisementService
    {
        Task<IEnumerable<Advertisement>> GetAdvertisementsAsync();
        Task<Advertisement> GetAdvertisementByIdAsync(Guid id);
        Task AddAdvertisementAsync(Advertisement advertisement);
        Task UpdateAdvertisementAsync(Advertisement advertisement);
        Task DeleteAdvertisementAsync(Guid id);
    }
}
