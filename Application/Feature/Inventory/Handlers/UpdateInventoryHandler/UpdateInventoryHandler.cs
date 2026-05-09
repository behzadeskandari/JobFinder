using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Inventory.Command.UpdateInventoryCommand;
using JobFinder.Contracts.Dtos.Product;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Inventory.Handlers.UpdateInventoryHandler
{

    public class UpdateInventoryHandler : IRequestHandler<UpdateInventoryCommand, Result<ProductInventoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInventoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductInventoryDto>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var id = request.Shipment.ProductId;
                var adjustment = request.Shipment.Adjustment;
                var inventory =  _unitOfWork.InventoryRepository.UpdateUnitsAvailable(id, adjustment);

                if (inventory.IsSuccess)
                {
                    var inventoryDto = new ProductInventoryDto
                    {
                        Id = inventory.Value.Id,
                        Product = SerializeProductModel(inventory.Value.Product),
                        IdealQuantity = inventory.Value.IdealQuantity,
                        QuantityOnHand = inventory.Value.QuantityOnHand
                    };

                    return Result.Ok(inventoryDto);
                }

                return Result.Fail<ProductInventoryDto>(inventory.Errors);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public static ProductDto SerializeProductModel(JobFinder.Domain.Common.Entities.Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

        /// <summary>
        /// Maps a ProductModel view model to a Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static JobFinder.Domain.Common.Entities.Product SerializeProductModel(ProductDto product)
        {
            return new JobFinder.Domain.Common.Entities.Product
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }



}
