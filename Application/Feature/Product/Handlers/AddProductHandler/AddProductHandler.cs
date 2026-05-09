using Application.MappingProfile;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Command.AddProductCommand;
using JobFinder.Contracts.Dtos.Product;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Product.Handlers.AddProductHandler
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Result<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newProduct = ProductMapper.SerializeProductModel(request.Product);
                var record = _mapper.Map<JobFinder.Domain.Common.Entities.Product>(request.Product);

                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(record.CategoryId);
                if (category == null)
                {
                    throw new NotFoundException("دسته بندی انتخاب نشده لطفا یک دسته بندی انتخاب نمایید");
                }
                record.Category = category;                
                var result =  _unitOfWork.ProductRepository.CreateProduct(record);
                await _unitOfWork.CommitAsync(cancellationToken);
                var productDto = new ProductDto()
                {
                    Description = result.Value.Description,
                    Id = result.Value.Id,
                    IsArchived = result.Value.IsArchived,
                    IsTaxable = result.Value.IsTaxable,
                    Name = result.Value.Name,
                    Price = result.Value.Price,
                    Attributes  = result.Value.Attributes,
                    Cost = result.Value.Cost,
                    Dimensions = result.Value.Dimensions,
                    SalePrice = result.Value.SalePrice,
                    FeaturedImageUrl = result.Value.FeaturedImageUrl,
                    GalleryImageUrls = result.Value.GalleryImageUrls,
                    status = result.Value.status,
                    Tags = result.Value.Tags,
                    TaxRate = result.Value.TaxRate,
                    type = result.Value.type,
                    sku = result.Value.sku,
                    Weight = result.Value.Weight,
                    
                };
                if (result.IsSuccess)
                {
                    return Result.Ok(productDto);
                }
                else
                {
                    throw new NotFoundException(result.Errors.FirstOrDefault().ToString());
                }
            }
            catch (Exception e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
