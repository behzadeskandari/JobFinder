using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository
                .GetQueryable()
                .Include(o => o.PricingPlan)
                .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

            if (order == null || order.IsActive != true)
                throw new NotFoundException("سفارشی یافت نشد یا غیرفعال است");

            if (order.UserId != request.UserId)
                throw new UnauthorizedAccessException("دسترسی غیرمجاز به سفارش");

            var orderDto = _mapper.Map<OrderDto>(order);
            return Result.Ok(orderDto);
        }
    }
}
