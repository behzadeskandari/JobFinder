using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Invoice;
using JobFinder.Contracts.Dtos.Order;
using JobFinder.Contracts.Dtos.Product;
using JobFinder.Domain.Common.Entities;

namespace Application.MappingProfile
{
    public static class ProductMapper
    {

        /// <summary>
        /// Maps a Product data model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductDto SerializeProductModel(Product product)
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
        public static Product SerializeProductModel(ProductDto product)
        {
            return new Product
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
