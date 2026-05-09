using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Inventory.Query.GetSnapshotHistoryQuery;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Inventory.Handlers.GetSnapshotHistoryHandler
{


    public class GetSnapshotHistoryHandler : IRequestHandler<GetSnapshotHistoryQuery, Result<SnapshotResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSnapshotHistoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SnapshotResponse>> Handle(GetSnapshotHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var snapshotHistory =  _unitOfWork.InventoryRepository.GetSnapshotHistory();

                var timelineMarkers = snapshotHistory
                    .Select(t => t.SnapshotTime)
                    .Distinct()
                    .ToList();

                var snapshots = snapshotHistory
                    .GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                        (key, g) => new ProductInventorySnapshotDto
                        {
                            ProductId = key.Id,
                            QuantityOnHand = g.ToList()
                        })
                    .OrderBy(hist => hist.ProductId)
                    .ToList();

                var viewModel = new SnapshotResponse
                {
                    Timeline = timelineMarkers,
                    ProductInventorySnapshots = snapshots
                };

                return Result.Ok(viewModel);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }


}

