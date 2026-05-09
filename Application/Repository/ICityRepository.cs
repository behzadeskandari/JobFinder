using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
    public interface ICityRepository : IWriteRepository<City> //, IReadRepository<City>///ICityReadRepository, ICityWriteRepository//IRepository<City>
    {
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<CityDto> GetCityByIdAsync(int id);
        Task AddCityAsync(CityDto city);
        Task DeleteCityAsync(int id);
        public Task<City> GetByIdWithProvinceAsync(int id);
        Task UpdateCityAsync(CityDto city);
        Task UpdateCityAsync(CityDto city,int provinceId);
    }

    
}
