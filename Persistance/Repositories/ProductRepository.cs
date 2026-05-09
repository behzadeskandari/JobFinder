using FluentResults;
using JobFinder.Application.Repository.Invoice;
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
    public class ProductRepository : IProductRepository
    {
        //private readonly GenericReadRepository<Product> _readRepository;
        private readonly GenericWriteRepository<Product> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public ProductRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
           // _readRepository = new GenericReadRepository<Product>(_readContext);
            _writeRepository = new GenericWriteRepository<Product>(_writeContext);
        }

        public async Task<PagedResult<Product>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Product>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Product>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public Result<Product> ArchiveProduct(Guid id)
        {
            var now = DateTime.Now;

            try
            {
                var product = _writeRepository.GetByIdAsync(id).Result;

                if (product == null)
                {
                    return Result.Fail<Product>($"Product with id {id} not found");
                }

                product.IsArchived = true;

                return Result.Ok(product)
                             .WithSuccess("Archived Product");
            }
            catch (Exception e)
            {
                return Result.Fail<Product>("Error archiving product")
                             .WithError(e.Message);
            }
        }

        public Result<Product> CreateProduct(Product product)
        {
            var now = DateTime.Now;

            try
            {
                _writeRepository.AddAsync(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _writeContext.ProductInventories.Add(newInventory);
                //_db.SaveChanges();
                
                return Result.Ok(product)
                             .WithSuccess("Saved new product");
            }
            catch (Exception e)
            {
                return Result.Fail<Product>("Error saving new product")
                             .WithError(e.Message);
            }
        }

        public List<Product> GetAllProducts()
        {
            return _writeRepository.GetAllAsync().Result.ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _writeRepository.GetByIdAsync(id).Result;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
             await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Product> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }

        public  async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (Guid)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Product entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Product> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<Product?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Product> GetQueryable()
        {

            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Product, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
