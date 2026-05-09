using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
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
    public class ProvinceRepository : IProvinceRepository
    {

        //private readonly GenericReadRepository<Province> _readRepository;
        private readonly GenericWriteRepository<Province> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public ProvinceRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readRepository = new GenericReadRepository<Province>(_readContext);
            _writeRepository = new GenericWriteRepository<Province>(_writeContext);
        }
        public async Task<PagedResult<Province>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Province, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Province>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Province, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Province>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task AddProvinceAsync(ProvinceDto province)
        {
            var provinceEntity = new Province
            {
                Id = province.Id,
                Label = province.Label,
                Cities = province.Cities.Select(c => new City
                {
                    Id = c.Id,
                    Label = c.Label,
                    ProvinceId = c.ProvinceId.HasValue ? c.ProvinceId.Value : province.Id, // Ensure ProvinceId is set
                }).ToList()
            };

            await _writeRepository.AddAsync(provinceEntity);
        }

        public async Task<List<ProvinceDto>> GetAllProvincesAsync()
        {
            return await _writeRepository.GetQueryable().Include(p => p.Cities).Select(x => new ProvinceDto
            {
                Cities = x.Cities.Select(c => new CityDto
                {
                    Id = c.Id,
                    Label = c.Label,
                    ProvinceId = c.ProvinceId
                }).ToList(),
                Id = x.Id,
                Label = x.Label,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
        }

        public async Task<List<CityDto>> GetProvinceByCityId(int cityId)
        {
            //var cities = await _context.Provinces.Include(p => p.Cities)
            //    .Select(x => new CityDto
            //{
            //    Id = x.Id,
            //    Label = x.Label,
            //}).Where(x => x.ProvinceId == cityId).ToListAsync();
            var cities = await _writeRepository.GetQueryable()
            .Where(p => p.Cities.Any(c => c.Id == cityId)) // Ensure city exists in province
            .SelectMany(p => p.Cities) // Flatten to cities
            .Select(c => new CityDto
            {
                Id = c.Id,
                Label = c.Label
            }).AsNoTracking()
            .ToListAsync();
            return cities;
        }

        public async Task<ProvinceDto> GetProvinceById(int provinceId)
        {
            var province = await _writeRepository.GetQueryable()
                .Where(p => p.Id == provinceId) // Ensure city exists in province
                .SelectMany(p => p.Cities) // Flatten to cities
                .Select(c => new ProvinceDto()
                {
                    Id = c.Id,
                    Label = c.Label,
                    Value = c.Value,
                }).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == provinceId);
            return province;
        }

    

        public async Task<List<CityDto>> GetProvinceWithCityByIdAsync(int id)
        {
            var province = await _writeRepository.GetQueryable()
                .Include(p => p.Cities) // Include related cities
                .Where(p => p.Id == id) // Filter by province ID
                .Select(p => new ProvinceDto
                {
                    Id = p.Id,
                    Label = p.Label,
                    Cities = p.Cities.Select(c => new CityDto
                    {
                        Id = c.Id,
                        Label = c.Label
                    }).ToList()
                }).AsNoTracking()
                .FirstOrDefaultAsync();

            return province.Cities;
        }



        public Task DeleteAysnc(ProvinceDto province)
        {
            var provinceRecord = new Province
            {
                IsActive = true,
                Label = province.Label,
                Value = province.Value,
            };
            _writeRepository.DeleteAsync(provinceRecord);
            return Task.CompletedTask;
        }

        public Task<Province> AddAsync(Province entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Province> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<Province> UpdateAsync(Province entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Province> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (int)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Province entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Province> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<Province?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Province>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Province>> FindAsync(Expression<Func<Province, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Province> GetQueryable()
        {

            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Province, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
