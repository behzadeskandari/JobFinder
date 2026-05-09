using Application.MappingProfile;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Inventory.Query.GetCurrentInventoryQuery;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Inventory.Handlers.GetCurrentInventoryHandler
{

    public class GetCurrentInventoryHandler : IRequestHandler<GetCurrentInventoryQuery, Result<List<ProductInventoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCurrentInventoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ProductInventoryDto>>> Handle(GetCurrentInventoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventory =  _unitOfWork.InventoryRepository.GetCurrentInventory();
                var inventoryDto = inventory
                    .Select(pi => new ProductInventoryDto
                    {
                        Id = pi.Id,
                        Product = ProductMapper.SerializeProductModel(pi.Product),
                        IdealQuantity = pi.IdealQuantity,
                        QuantityOnHand = pi.QuantityOnHand
                    })
                    .OrderBy(inv => inv.Product.Name)
                    .ToList();

                return Result.Ok(inventoryDto);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }


}
