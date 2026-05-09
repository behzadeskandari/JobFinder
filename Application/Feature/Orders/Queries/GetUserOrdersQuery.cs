using FluentResults;
using JobFinder.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Orders.Queries
{
    public class GetUserOrdersQuery : MediatR.IRequest<Result<IEnumerable<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
