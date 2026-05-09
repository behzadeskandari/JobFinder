using Domain.WriteRepository;
using FluentResults;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository.Invoice
{
    
    public interface IOrderRepository : IWriteRepository<Order> //, IReadRepository<Order>//IRepository<Order>
    {
        List<SalesOrder> GetOrders();
        Result<bool> GenerateOpenOrder(SalesOrder order);
        Result<bool> MarkFulfilled(int id);
    }
}
