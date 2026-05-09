using FluentResults;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Persistance.DatabaseContext;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFinder.Persistance.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        
        ///private readonly GenericReadRepository<Customer> _readRepository;
        private readonly GenericWriteRepository<Customer> _writeRepository;
        private readonly ReadDbContext _readContext; // You might need this for specific read logic
        private readonly WriteDbContext _writeContext; // You might need this for specific write logic

        public CustomerRepository(WriteDbContext writeContext, ReadDbContext readContext)
        {
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
       //     _readRepository = new GenericReadRepository<Customer>(_readContext);
            _writeRepository = new GenericWriteRepository<Customer>(_writeContext);
        }

        /// <summary>
        /// Adds a new Customer record
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <returns>ServiceResponse<Customer></returns>
        //public Result<CustomerDto> CreateCustomer(Customer customer)
        //{
        //    try
        //    {
        //        _writeContext.Add(customer);
        //        //_db.SaveChanges();

        //        var customerDto = new CustomerDto();
        //        customerDto.Id = customer.Id;
        //        customerDto.CreatedOn = customer.CreatedOn;
        //        customerDto.UpdatedOn = customer.UpdatedOn;
        //        customerDto.FirstName = customer.FirstName;
        //        customerDto.LastName = customer.LastName;
        //        customerDto.PrimaryAddress = new CustomerAddressDto(){
        //            City = customer.PrimaryAddress.City,
        //            Country = customer.PrimaryAddress.Country,
        //            AddressLine1 = customer.PrimaryAddress.AddressLine1,
        //            AddressLine2 = customer.PrimaryAddress.AddressLine2,
        //            CreatedOn = customer.PrimaryAddress.CreatedOn,
        //            PostalCode = customer.PrimaryAddress.PostalCode,
        //            Id = customer.PrimaryAddress.Id,
        //            UpdatedOn = customer.PrimaryAddress.UpdatedOn,
        //            State = customer.PrimaryAddress.State,
        //        };

        //        return Result.Ok<CustomerDto>(customerDto)
        //                 .WithSuccess("New customer added");
        //    }
        //    catch (Exception e)
        //    {
        //        return Result.Fail<CustomerDto>(e.Message)
        //                     .WithError(e.StackTrace);
        //    }
        //}
        /// <summary>
        /// Deletes a customer record
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>ServiceResponse<bool></returns>
        public Result<bool> DeleteCustomer(int id)
        {
            var customer = _writeRepository.GetByIdAsync(id);
            var now = DateTime.Now;

            if (customer == null)
            {
                return Result.Fail<bool>("Customer to delete not found!");
            }

            try
            {
                _writeRepository.DeleteAsync(customer);
                //_db.SaveChanges();

                return Result.Ok(true)
                             .WithSuccess("Customer deleted!");
            }
            catch (Exception e)
            {
                return Result.Fail<bool>(e.Message)
                             .WithError(e.StackTrace);
            }
        }
        /// <summary>
        /// Returns a list of Customers from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        //public Result<List<CustomerDto>> GetAllCustomers()
        //{
        //    var result = _writeRepository.GetQueryable()
        //        .Include(customer => customer.CustomerAddresses)
        //        .OrderBy(customer => customer.LastName)
        //        .Select(x => new CustomerDto
        //        {
        //            FirstName = x.FirstName,
        //            CreatedOn = x.CreatedOn,
        //            Id = x.Id,
        //            LastName = x.LastName,
        //            UpdatedOn = x.UpdatedOn,
        //            PrimaryAddress = new List<CustomerAddressDto>
        //            {
        //                new CustomerAddressDto()
        //                {

        //                }
        //            }
        //        }).AsNoTracking().ToList();
        //    return result;

        //}

        /// <summary>
        /// Gets a customer record by primary key
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>Customer</returns>
        //public Result<CustomerDto> GetById(int id)
        //{
        //    var result = _writeRepository.GetByIdAsync(id).Result;//.Find(id);
        //    CustomerDto customerDto = new CustomerDto
        //    {
        //        Id = result.Id,
        //        CreatedOn = result.CreatedOn,
        //        FirstName = result.FirstName,
        //        LastName = result.LastName,
        //        UpdatedOn = result.UpdatedOn,
        //        PrimaryAddress = new CustomerAddressDto
        //        {
                   

        //        },
        //    };
        //    foreach (var customer in result.CustomerAddresses) { 
        //        customerDto.PrimaryAddress = new CustomerAddressDto
        //        {
        //            UpdatedOn = customer.Customer.UpdatedOn,
        //            Id = customer.Customer.Id,
        //            AddressLine1 = customer.Customer.AddressLine1,
        //            AddressLine2 = customer.Customer.AddressLine2,
        //            City = result.PrimaryAddress.City,
        //            Country = result.PrimaryAddress.Country,
        //            PostalCode = result.PrimaryAddress.PostalCode,
        //            State = result.PrimaryAddress.State,
        //            CreatedOn = result.PrimaryAddress.CreatedOn,
        //        }
        //    }
        //    return customerDto;
        //}

        public Task IsExists(Guid id)
        {
            var result = _writeRepository.ExistsAsync(x => x.Id == id);
            return result;
        }

        public async Task<Customer?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
           return await _writeRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }
        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> expression)
        {

            return await _writeRepository.FindAsync(expression);
        }

        public IQueryable<Customer> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            var record = await _writeRepository.AddAsync(entity);
            return await Task.FromResult(record);
        }

        public Task AddRangeAsync(IEnumerable<Customer> entities)
        {
            var record = _writeRepository.AddRangeAsync(entities);
            return Task.FromResult(record);
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return await Task.FromResult(record);
        }

        public async Task UpdateRangeAsync(IEnumerable<Customer> entities)
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

        public async Task<bool> DeleteAsync(Customer entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public  async Task<bool> DeleteRangeAsync(IEnumerable<Customer> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<PagedResult<Customer>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Customer, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Customer>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Customer, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<Customer>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
