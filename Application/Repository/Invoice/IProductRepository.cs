using Domain.WriteRepository;
using FluentResults;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository.Invoice
{
    
    public interface IProductRepository : IWriteRepository<Product> //, IReadRepository<Product>//IRepository<Product>
    {
        List<Product> GetAllProducts();
        Product GetProductById(Guid id);
        Result<Product> CreateProduct(Product product);
        Result<Product> ArchiveProduct(Guid id);
    }
}
