using Application.MappingProfile;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Order.Queries.GetOrdersQuery;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Contracts.Dtos.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Handlers.GetOrdersHandler
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, Result<List<OrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders =  _unitOfWork.OrderRepository.GetOrders();
                var orderModels = OrderMapper.SerializeOrdersToViewModels(orders);

                return Result.Ok(orderModels);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
