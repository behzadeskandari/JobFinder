using FluentResults;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Product.Command.AddProductCommand
{

    public class AddProductCommand : IRequest<Result<ProductDto>>
    {
        public ProductDto Product { get; set; }
    }
}
