using Application.MappingProfile;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Queries.GetProductQuery;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Product.Handlers.GetProductHandler
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, Result<List<ProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ProductDto>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products =  _unitOfWork.ProductRepository.GetAllProducts();
                var productDtos = products.Select(ProductMapper.SerializeProductModel).ToList();

                return Result.Ok(productDtos);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
