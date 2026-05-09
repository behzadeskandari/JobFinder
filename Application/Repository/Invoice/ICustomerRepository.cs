using Domain.WriteRepository;
using FluentResults;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFinder.Application.Repository.Invoice
{
    public interface ICustomerRepository :  IWriteRepository<Customer>//IReadRepository<Customer> ,//IRepository<Customer>
    {
        //Result<List<CustomerDto>> GetAllCustomers();

        Task IsExists(Guid id); 
        //Result<CustomerDto> CreateCustomer(Customer customer);
        //Result<bool> DeleteCustomer(int id);
        //Result<CustomerDto> GetById(int id);
    }
}
