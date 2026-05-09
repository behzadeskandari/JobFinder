using FluentResults;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Command
{
    public class CreateCustomerCommand : IRequest<Result<Customer>>
    {
        public CustomerDto Customer { get; set; }
    }

}
