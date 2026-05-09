using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Application.Services.interfaces;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsAsync()
        {
            return await _advertisementRepository.GetAdvertisementsAsync();
        }

        public async Task<Advertisement> GetAdvertisementByIdAsync(Guid id)
        {
            return await _advertisementRepository.GetAdvertisementByIdAsync(id);
        }

        public async Task AddAdvertisementAsync(Advertisement advertisement)
        {
            await _advertisementRepository.AddAdvertisementAsync(advertisement);
        }

        public async Task UpdateAdvertisementAsync(Advertisement advertisement)
        {
            await _advertisementRepository.UpdateAdvertisementAsync(advertisement);
        }

        public async Task DeleteAdvertisementAsync(Guid id)
        {
            await _advertisementRepository.DeleteAdvertisementAsync(id);
        }
    }
}
