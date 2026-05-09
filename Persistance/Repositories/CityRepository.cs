using FluentResults;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using Persistance.Exceptions;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class CityRepository : ICityRepository
    {
   
        //private readonly GenericReadRepository<City> _readRepository;
        private readonly GenericWriteRepository<City> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CityRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           // _readRepository = new GenericReadRepository<City>(_readContext);
            _writeRepository = new GenericWriteRepository<City>(_writeContext);
        }



      
        public async Task AddCityAsync(CityDto city)
        {
            var cityEntity = new City
            {
                Id = city.Id,
                Label = city.Label,
                ProvinceId = city.ProvinceId.Value
            };

            await _writeRepository.AddAsync(cityEntity);

        }

        public Task AddRangeAsync(IEnumerable<City> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await  Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(City entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task DeleteCityAsync(int id)
        {
            var records =   await _writeRepository.FindAsync(x => x.Id == id);
            await _writeRepository.DeleteRangeAsync(records);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<City> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<City, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }
     

        public async Task<IEnumerable<City>> FindAsync(Expression<Func<City, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {

            return await _writeRepository.GetAllAsync();
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var result = await _writeRepository.GetQueryable().Select(x =>
            new CityDto {
                Id = x.Id,
                Label = x.Label,
                Value= x.Value,
            }).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<City?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<City> GetByIdWithProvinceAsync(int id)
        {
            var record = await _writeRepository.GetQueryable()
                //.Include(c => c.Province)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (record is null)
            {
                Enumerable.Empty<City>();
            }
            return record;
        }

        public async Task<CityDto> GetCityByIdAsync(int id)
        {
            var record = await _writeRepository.GetQueryable()
                //.Include(c => c.Province)
                .Select(x => new CityDto
                {
                    Id = x.Id,
                    Label = x.Label,
                    Value = x.Value,
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return record; 

        }

        public IQueryable<City> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<City> UpdateAsync(City entity)
        {

            return await Task.FromResult(await _writeRepository.UpdateAsync(entity));
        }

        public async Task UpdateCityAsync(CityDto city)
        {
            var record = await _writeRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == city.Id);
            if (record != null)
            {
                //record.Province = new Province();
                record.Label = city.Label;
                record.ProvinceId = city.ProvinceId.Value;
                record.IsActive = city.IsActive;
                record.Value = city.Value.ToString();

                await _writeRepository.UpdateAsync(record);
            }
            else
            {
                throw new DataBaseExcption("City not found");
            }
        }

        public async Task UpdateCityAsync(CityDto city, int provinceId)
        {
            var record = await _writeRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == city.Id);
            Province province = null;
            if (record != null)
            {
                if (city.ProvinceId != null)
                {
                    province = await _writeContext.Provinces.FirstOrDefaultAsync(x => x.Id == provinceId);
                    if (province == null)
                    {
                        throw new DataBaseExcption("Province not found");
                    }
                    else
                    {
                        ///record.Province = province;
                    }
                }
                else
                {
                    //record.Province = new Province();
                }
                record.Label = city.Label;
                record.ProvinceId = city.ProvinceId.Value;
                record.IsActive = city.IsActive;
                record.Value = city.Value.ToString();

                await _writeRepository.UpdateAsync(record);
            }
            else
            {
                throw new DataBaseExcption("City not found");
            }
        }
        public async Task<City> AddAsync(City entity)
        {
           var records =  await _writeRepository.AddAsync(entity);
                
            return records;
        }

        public async Task<IEnumerable<CityDto>> GetCities()
        {
           var record = await _writeRepository.GetAllAsync();
           var cityDto = record.Select(x => new CityDto
           {
               Id = x.Id,
               Label = x.Label,
               Value = x.Value
           }).ToList();
           return cityDto;
        }
        public async Task UpdateRangeAsync(IEnumerable<City> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<PagedResult<City>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<City, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<City>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<City, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<City>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
