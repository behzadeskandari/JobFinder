using FluentResults;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Queries
{
    public class GetCustomersQuery : IRequest<Result<PagedResult<CustomerDto>>>
    {
        public string? SearchTerm { get; set; }
        public string CustomerType { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
