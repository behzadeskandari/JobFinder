using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Command.ArchiveProductCommand;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Product.Handlers.ArchiveProductHandler
{
    public class ArchiveProductHandler : IRequestHandler<ArchiveProductCommand, Result<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result =  _unitOfWork.ProductRepository.ArchiveProduct(request.Id);
                var productDto = new ProductDto()
                {
                    Description = result.Value.Description,
                    Id = result.Value.Id,
                    IsArchived = result.Value.IsArchived,
                    IsTaxable = result.Value.IsTaxable,
                    Name = result.Value.Name,
                    Price = result.Value.Price,
                };

                if (result.IsSuccess)
                {
                    return Result.Ok(productDto);
                }
                else
                {
                    throw new NotFoundException(result.Errors.ToString());
                }
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
