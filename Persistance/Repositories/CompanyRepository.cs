using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Contracts.Dtos.CompanyBenefit;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Domain.WriteRepository;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.SqlClient;

namespace JobFinder.Persistance.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
   

        //private readonly GenericReadRepository<Company> _readRepository;
        private readonly GenericWriteRepository<Company> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CompanyRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Company>(_readContext);
            _writeRepository = new GenericWriteRepository<Company>(_writeContext);
        }
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Company>> SearchAsync(string searchTerm, string industry = null, string location = null)
        {

            var query = _writeRepository.GetQueryable()
                .Include(c => c.Benefits)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(searchTerm) ||
                                        c.Description.ToLower().Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(industry))
            {
                query = query.Where(c => c.Industry == industry);
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                query = query.Where(c => c.Location == location);
            }

            return await query.ToListAsync();
            //if (string.IsNullOrWhiteSpace(searchTerm))
            //    return await GetAllAsyncMBTI();

            //searchTerm = searchTerm.ToLower();

            //return await _context.Companies
            //    .Where(c => c.Name.ToLower().Contains(searchTerm) ||
            //               c.Industry.ToLower().Contains(searchTerm) ||
            //               c.Location.ToLower().Contains(searchTerm) ||
            //               c.Description.ToLower().Contains(searchTerm))
            //    .ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<Company> AddAsync(Company company)
        {
             await _writeRepository.AddAsync(company);
            //await .SaveChangesAsync();
            return company;
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _writeContext.Entry(company).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            _writeRepository.UpdateAsync(entity: company);
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _writeRepository.GetByIdAsync(id);
            if (company != null)
            {
                await _writeRepository.DeleteAsync(company);
                //await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetAllIndustriesAsync()
        {
            return await _writeRepository.GetQueryable()
                .Select(c => c.Industry)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllLocationsAsync()
        {
            return await _writeRepository.GetQueryable()
                .Select(c => c.Location)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(object id)
        {
            var company = await _writeRepository.GetByIdAsync(id);
            return company;
        }

        public async Task<IEnumerable<Company>> FindAsync(Expression<Func<Company, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return  record;
        }

        public IQueryable<Company> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public Task<bool> ExistsAsync(Expression<Func<Company, bool>> expression)
        {
            return _writeRepository.ExistsAsync(expression);
        }

        public async Task AddRangeAsync(IEnumerable<Company> entities)
        {
             await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<Company> UpdateAsync(Company entity)
        {
           var record = await _writeRepository.UpdateAsync(entity);
           return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<Company> entities)
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

        public async Task<bool> DeleteAsync(Company entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Company> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<PagedResult<Company>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Company, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Company>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Company, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Company>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

        public async Task<CompanyDto> GetCompanyByUserIdAsync(string userId)
        {
            var user = await _writeContext.Users
                .Where(x => x.Id == userId)
                .ToListAsync();

            var company = _writeRepository.GetQueryable();

            var record = (from u in user
                          join c in company on u.Id equals c.UserId // Fix: Ensure the join is based on matching types (User.Id and Company.UserId)  
                          select new CompanyDto
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Description = c.Description,
                              IndustryName = c.Industry,
                              CityName = c.Location,
                              Benefits = c.Benefits.Select(b => new CompanyBenefitDto
                              {
                                  Id = b.Id,
                                  Name = b.Name,
                                  Description = b.Description
                              }).ToList(),
                              IsActive = c.IsActive,
                              IsVerified = c.IsVerified,
                          }).FirstOrDefault();

            return record;
        }

        public async Task<Result<CompanyDto>> UpdateCompanyProfileAsync(string userId, CompanyDto updateDto)
        {
            var user = await _writeContext.Users
                .Where(x => x.Id == userId)
                .ToListAsync();

            var company = _writeRepository.GetQueryable();

            var record = (from u in user
                          join c in company on u.Id equals c.UserId // Fix: Ensure the join is based on matching types (User.Id and Company.UserId)  
                          select new Company
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Description = c.Description,
                              Industry = c.Industry,
                             // City = c.City,
                              CityId = c.CityId,
                              Benefits = c.Benefits.Select(b => new CompanyBenefit
                              {
                                  Id = b.Id,
                                  Name = b.Name,
                                  Description = b.Description
                              }).ToList(),
                              IsActive = c.IsActive,
                              IsVerified = c.IsVerified,
                          }).FirstOrDefault();

            var updatedReocrd = await _writeRepository.UpdateAsync(record);

            return Result.Ok(new CompanyDto()
            {
                 Benefits = updatedReocrd.Benefits.Select(b => new CompanyBenefitDto
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Description = b.Description
                 }).ToList(),
                Id = updatedReocrd.Id,
                Name = updatedReocrd.Name,
                Description = updatedReocrd.Description,
                IndustryName = updatedReocrd.Industry,
               // CityName = updatedReocrd.City.Value,
                IsActive = updatedReocrd.IsActive,
                IsVerified = updatedReocrd.IsVerified,
                LogoUrl = updatedReocrd.LogoUrl,
                Rating = updatedReocrd.Rating,
                Size = updatedReocrd.Size.ToString(),
            });
        }

        public Task<CompanyDto> GetDashboardStatsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
