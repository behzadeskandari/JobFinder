using FluentResults;
using JobFinder.Application.Repositories.Invoice;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        private readonly IProductRepository _productService;
        private readonly IInventoryRepository _inventoryService;
        //private readonly GenericReadRepository<Order> _readRepository;
        private readonly GenericWriteRepository<Order> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public OrderRepository(WriteDbContext writeContext, ReadDbContext readContext,
            ILogger<OrderRepository> logger,
            IProductRepository productService,
            IInventoryRepository inventoryService)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<Order>(_readContext);
            _writeRepository = new GenericWriteRepository<Order>(_writeContext);
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public Result<bool> GenerateOpenOrder(SalesOrder order)
        {
            var now = DateTime.Now;
            _logger.LogInformation("Generating new order");

            try
            {
                foreach (var item in order.SalesOrderItems)
                {
                    item.Product = _productService.GetProductById(item.Product.Id);
                    var inventory = _inventoryService.GetByProductId(item.Product.Id);

                    if (inventory == null)
                    {
                        return Result.Fail<bool>($"Inventory for product {item.Product.Id} not found.")
                            .WithError("InventoryNotFound");
                    }

                    var updateResult = _inventoryService.UpdateUnitsAvailable(inventory.Id, -item.Quantity);

                    if (updateResult.IsFailed)
                    {
                        return Result.Fail<bool>($"Failed to update inventory for product {item.Product.Id}.")
                            .WithErrors(updateResult.Errors);
                    }
                }

                _writeContext.SalesOrders.Add(order);

                return Result.Ok(true).WithSuccess("Order generated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error generating order");
                return Result.Fail<bool>(e.Message);
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _writeContext.SalesOrders
               .Include(so => so.Customer)
                   .ThenInclude(customer => customer.CustomerAddresses)
               .Include(so => so.SalesOrderItems)
                   .ThenInclude(item => item.Product)
               .ToList();
        }

        public Result<bool> MarkFulfilled(int id)
        {
            var now = DateTime.Now;
            var order = _writeContext.SalesOrders.Find(id);

            if (order == null)
            {
                return Result.Fail<bool>("Order not found.")
                    .WithError("OrderNotFound");
            }

            order.UpdatedOn = now;
            order.IsPaid = true;

            try
            {
                _writeContext.SalesOrders.Update(order);
                //_db.SaveChanges();

                return Result.Ok(true).WithSuccess($"Order {order.Id} closed: Invoice paid in full.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error marking order as fulfilled");
                return Result.Fail<bool>(e.Message);
            }
        }

        public async Task<Order?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {

            return await _writeRepository.GetAllAsync();
        }
        public async Task<PagedResult<Order>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Order, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Order>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Order, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Order>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> expression)
        {
            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Order> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Order, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<Order> AddAsync(Order entity)
        {
            var record = _writeRepository.AddAsync(entity).Result;
            return await Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Order> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Order> entities)
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

        public async Task<bool> DeleteAsync(Order entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Order> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
    }
}
