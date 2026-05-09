using FluentResults;
using JobFinder.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Orders.Queries
{
    public class GetOrderByIdQuery : MediatR.IRequest<Result<OrderDto>>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
