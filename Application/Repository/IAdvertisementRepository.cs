using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{


    public interface IAdvertisementRepository  : IWriteRepository<Advertisement> //, IReadRepository<Advertisement>//: IRepository<Advertisement>
    {
        Task<IEnumerable<Advertisement>> GetAdvertisementsAsync();
        Task<Advertisement> GetAdvertisementByIdAsync(Guid id);
        Task AddAdvertisementAsync(Advertisement advertisement);
        Task UpdateAdvertisementAsync(Advertisement advertisement);
        Task DeleteAdvertisementAsync(Guid id);

        Task<IEnumerable<AdvertisementDto>> GetAdvertisement();
    }
}
