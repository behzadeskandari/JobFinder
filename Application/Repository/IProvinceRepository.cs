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
    public interface IProvinceRepository: IWriteRepository<Province> //, IReadRepository<Province> //: IRepository<Province>
    {
    Task<List<ProvinceDto>> GetAllProvincesAsync();
    Task<List<CityDto>> GetProvinceWithCityByIdAsync(int id);
    Task AddProvinceAsync(ProvinceDto province);

    Task<List<CityDto>> GetProvinceByCityId(int cityId);
    Task<ProvinceDto> GetProvinceById(int provinceId);
    Task DeleteAysnc(ProvinceDto province);
    }
}
