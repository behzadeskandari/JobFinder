using FluentResults;
using JobFinder.Application.Repositories.Invoice;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class InventoryRepository : IInventoryRepository
    {


        //private readonly WriteDbContext _writeContext;
        //private readonly ReadDbContext _readContext;
        //private readonly DbSet<ProductInventory> _writeDbSet; // Explicit DbSet for Write
        //private readonly DbSet<ProductInventory> _readDbSet;
        //private readonly ILogger<InventoryRepository> _logger;

        //public InventoryRepository(WriteDbContext writeContext, ReadDbContext readContext, ILogger<InventoryRepository> logger)
        //{
        //    _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
        //    _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
        //    _writeDbSet = _writeContext.Set<ProductInventory>();
        //    _readDbSet = _readContext.Set<ProductInventory>();
        //    _logger = logger;
        //}
        private readonly ILogger<InventoryRepository> _logger;
        //private readonly GenericReadRepository<ProductInventory> _readRepository;
        private readonly GenericWriteRepository<ProductInventory> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public InventoryRepository(WriteDbContext writeContext, ReadDbContext readContext, ILogger<InventoryRepository> logger)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<ProductInventory>(_readContext);
            _writeRepository = new GenericWriteRepository<ProductInventory>(_writeContext);
            _logger = logger;
        }


        public ProductInventory GetByProductId(Guid productId)
        {
            return _writeContext.ProductInventories
                .Include(pi => pi.Product)
               .AsNoTracking().FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _writeContext.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
               .AsNoTracking().ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.Now - TimeSpan.FromHours(2);

            return _writeContext.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap
                    => snap.SnapshotTime > earliest
                       && !snap.Product.IsArchived)
                .AsNoTracking().ToList();
        }

        public Result<ProductInventory> UpdateUnitsAvailable(Guid id, int adjustment)
        {
            var now = DateTime.Now;

            try
            {
                var inventory = _writeContext.ProductInventories
                    .Include(inv => inv.Product)
                    .FirstOrDefault(inv => inv.Product.Id == id);

                if (inventory == null)
                {
                    return Result.Fail<ProductInventory>($"Product with id {id} not found");
                }

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot();
                }
                catch (Exception e)
                {
                    _logger.LogError("Error creating inventory snapshot.");
                    _logger.LogError(e.StackTrace);
                }

                //_db.SaveChanges();

                return Result.Ok(inventory)
                             .WithSuccess($"Product {id} inventory adjusted");
            }
            catch (Exception e)
            {
                return Result.Fail<ProductInventory>("Error updating ProductInventory QuantityOnHand")
                             .WithError(e.Message);
            }
        }


        private void CreateSnapshot()
        {
            var now = DateTime.Now;

            var inventories = _writeContext.ProductInventories
                .Include(inv => inv.Product)
                .ToList();

            foreach (var inventory in inventories)
            {
                var snapshot = new ProductInventorySnapshot
                {
                    SnapshotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
                };

                _writeContext.Add(snapshot);
            }
        }

        public async Task<ProductInventory?> GetByIdAsync(object id)
        {
            var record = await _writeRepository.GetByIdAsync(id);
            return record;
        }

        public async Task<IEnumerable<ProductInventory>> GetAllAsync()
        {

            return await _writeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductInventory>> FindAsync(Expression<Func<ProductInventory, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<ProductInventory> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<ProductInventory, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<ProductInventory> AddAsync(ProductInventory entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task AddRangeAsync(IEnumerable<ProductInventory> entities)
        {
           await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<ProductInventory> UpdateAsync(ProductInventory entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<ProductInventory> entities)
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

        public  async Task<bool> DeleteAsync(ProductInventory entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<ProductInventory> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<ProductInventory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<PagedResult<ProductInventory>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<ProductInventory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<ProductInventory>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<ProductInventory, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<ProductInventory>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
