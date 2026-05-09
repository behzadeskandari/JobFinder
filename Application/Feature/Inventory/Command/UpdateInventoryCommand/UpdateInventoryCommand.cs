using FluentResults;
using JobFinder.Contracts.Dtos.Product;
using JobFinder.Contracts.Dtos.Shipment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Inventory.Command.UpdateInventoryCommand
{
    public class UpdateInventoryCommand : IRequest<Result<ProductInventoryDto>>
    {
        public ShipmentDto Shipment { get; set; }
    }
}
