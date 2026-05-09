using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Contracts.Dtos.Order;
using Microsoft.EntityFrameworkCore;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Pricing.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public Guid PlanId { get; set; }
        public string? UserId { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IUnitOfWork _context;

        public CreateOrderCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var plan = await _context.PricingPlansRepository.GetQueryable()
                .FirstOrDefaultAsync(p => p.Id == request.PlanId, cancellationToken);

            if (plan == null)
            {
                throw new NotFoundException(nameof(PricingPlan), request.PlanId);
            }

            var order = new Domain.Common.Entities.Order()
            {
                PricingPlanId = request.PlanId,
                UserId = request.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = plan.Price,
                Status = "Pending"
            };

            await _context.OrderRepository.AddAsync(order);
            await _context.CommitAsync(cancellationToken);

            return new CreateOrderResponse
            {
                OrderId = order.Id,
                Status = order.Status,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
