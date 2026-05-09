using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Orders.Queries;
using JobFinder.Contracts.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Orders.Handlers
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, Result<IEnumerable<OrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<OrderDto>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.OrderRepository
                .GetQueryable()
                .Include(o => o.PricingPlan)
                .Where(o => o.UserId == request.UserId && o.IsActive == true)
                .ToListAsync(cancellationToken);

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Result.Ok(orderDtos);
        }
    }
}
