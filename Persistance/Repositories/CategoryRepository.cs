using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Domain.WriteRepository;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using Persistance.Exceptions;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Persistance.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        //private readonly GenericReadRepository<Category> _readRepository;
        private readonly GenericWriteRepository<Category> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CategoryRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Category>(_readContext);
            _writeRepository = new GenericWriteRepository<Category>(_writeContext);
        }



        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
           var record = await _writeRepository.AddAsync(category);
           //return record.Entity
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<Category> GetByIdAsyncWithAdvertisements(int id, CancellationToken cancellationToken)
        {
            var record = await _writeRepository.GetQueryable().Include(x => x.Advertisements)
                .FirstOrDefaultAsync(r => r.Id == id,cancellationToken);
            return record;
        }

        public Task<List<CategoryDto>> GetAllAsyncCategory(CancellationToken cancellationToken)
        {
           var record = _writeRepository.GetQueryable()
               .Include(c => c.Advertisements)
               .OrderBy(c => c.Name)
               .Select(c => new CategoryDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   Description = c.Description,
                   AdvertisementCount = c.Advertisements.Count
               })
               .ToListAsync(cancellationToken);
           return record; 
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var record =await _writeRepository.UpdateAsync(category);
            return record;
        }



        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {

            var record = _writeRepository.GetByIdAsync(id);
            if (record != null)
            {
                await  _writeRepository.DeleteAsync(record.Result);
            }
            else
            {
                throw new DataBaseExcption("Record not found");
            }
            
        }

        public Task<bool> ExistsAsync(int id)
        {
            var record = _writeRepository.GetByIdAsync(id);
            if (record != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> ExistsAsync(string name)
        {
            var record = _writeRepository.ExistsAsync(r => r.Name.Contains(name));
            if (record != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> ExistsAsync(string name, int id)
        {
            
            var record = _writeRepository.ExistsAsync(r => r.Name.Contains(name) || r.Id.Equals(id));
            if (record != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }

        }

        public Task<bool> ExistsAsync(string name, string description)
        {
            var record = _writeRepository.ExistsAsync(r => r.Name.Contains(name) || r.Description.Contains(description));
            if (record != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> ExistsAsync(string name, string description, int id)
        {
            var record = _writeRepository.ExistsAsync(r => r.Name.Contains(name) || r.Name.Contains(description) || r.Id.Equals(id));
            if (record != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }


        public async Task<Category> AddAsync(Category entity)
        {
          var record =  await _writeRepository.AddAsync(entity);
            return record;
        }

        public async Task AddRangeAsync(IEnumerable<Category> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public Task<bool> DeleteAsync(object id)
        {
            var record = _writeRepository.GetByIdAsync(id);
            var result = _writeRepository.DeleteAsync(record);
            if (result != null)
            {
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(Category entity)
        {
            var record = _writeRepository.GetByIdAsync(entity.Id);
            var result = await _writeRepository.DeleteAsync(record);
            if (result != null)
            {
                return true;
            }
            else
                return false;

        }

        public Task<bool> DeleteRangeAsync(IEnumerable<Category> entities)
        {
            _writeRepository.DeleteRangeAsync(entities);
            return Task.FromResult(true);
        }
        public async Task<Category?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsyncCategory()
        {
            var record = await _writeRepository.GetAllAsync();

            return record;
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Category> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> expression)
        {
            var record = await _writeRepository.ExistsAsync(expression);
            return record;
        }
        public async Task UpdateRangeAsync(IEnumerable<Category> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var record = await _writeRepository.GetAllAsync();
            return record;
        }

        public async Task<PagedResult<Category>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Category, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Category>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Category, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Category>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
