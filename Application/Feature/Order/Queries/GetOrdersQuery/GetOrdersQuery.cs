using FluentResults;
using JobFinder.Contracts.Dtos.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Queries.GetOrdersQuery
{
    public class GetOrdersQuery : IRequest<Result<List<OrderDto>>>
    {
    }
}
