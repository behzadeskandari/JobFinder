using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Command
{
    public class DeleteCustomerCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
