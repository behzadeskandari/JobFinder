using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Contracts.Dtos.Payment;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Persistance.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {


       // private readonly GenericReadRepository<Advertisement> _readRepository;
        private readonly GenericWriteRepository<Advertisement> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public AdvertisementRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           // _readRepository = new GenericReadRepository<Advertisement>(_readContext);
            _writeRepository = new GenericWriteRepository<Advertisement>(_writeContext);
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsAsync()
        {
            var record = await _writeRepository.GetAllAsync();
            return record;
        }

        public async Task<Advertisement> GetAdvertisementByIdAsync(Guid id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task AddAdvertisementAsync(Advertisement advertisement)
        {
            await _writeRepository.AddAsync(advertisement);
        //    await _writeContext.SaveChangesAsync();
        }

        public async Task UpdateAdvertisementAsync(Advertisement advertisement)
        {
            await _writeRepository.UpdateAsync(advertisement);
           // await _writeContext.SaveChangesAsync();
        }

        public async Task DeleteAdvertisementAsync(Guid id)
        {
            var advertisement = await _writeRepository.FindAsync(x => x.Id == id);
            if (advertisement != null)
            {
                await _writeRepository.DeleteAsync(advertisement);
            //    await _writeContext.SaveChangesAsync();
            }
        }

        public async Task<Advertisement?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<IEnumerable<Advertisement>> FindAsync(Expression<Func<Advertisement, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Advertisement> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Advertisement, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisement()
        {
          var record = await _writeRepository.GetAllAsync();
            //var result = record.Select(x => new AdvertisementDto()
            //{
            //    Category = new CategoryDto()
            //    {
            //        Description = x.Category.Description,
            //        Id = x.Category.Id,
            //        Name = x.Category.Name,
            //        AdvertisementCount = record.Count(),
            //    },
            //    Description = x.Description,
            //    Id = x.Id,
            //    CategoryName = x.Category.Name,
            //    Company = new CompanyGetDto()
            //    {
            //        CreatedAt = DateTime.Now,
            //       Name = x.Company.Name,
            //       Size = x.Company.Size,
            //       ID = x.Company.Id,
            //    },
            //    CategoryId = x.CategoryId,
            //    IsActive = x.IsActive,
            //    ExpiresAt = x.ExpiresAt,
            //    ImageUrl = x.ImageUrl,
            //    Staff = new   UserDto()
            //    {

            //    },
            //    StaffEmail = string.Empty,
            //    IsApproved = x.IsApproved,
            //    IsPaid = x.IsPaid,
            //    Title = x.Title,
            //    JobADVCreatedAt = x.JobADVCreatedAt,
            //    CompanyId = x.CompanyId,
            //    StaffId = x.StaffId,
            //    Payment = new PaymentDto()
            //    {
            //        Amount = x.Payment.Amount,
            //        CreatedAt = x.Payment.CreatedAt,
            //        Id = x.Payment.Id,
            //        PaymentMethod = x.Payment.PaymentMethod,
            //    },
            //    DateCreated = x.DateCreated,
            //    DateModified = x.DateModified,
            //}).ToList();


            return Enumerable.Empty<AdvertisementDto>(); // AdvertisementDto();
        }

        public  Task<Advertisement> AddAsync(Advertisement entity)
        {
            var  record =  _writeRepository.AddAsync(entity).Result;
            return Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Advertisement> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<Advertisement> UpdateAsync(Advertisement entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Advertisement> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (Guid)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Advertisement entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Advertisement> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }
        public async Task<PagedResult<Advertisement>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Advertisement, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Advertisement>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Advertisement, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Advertisement>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
