using FluentResults;
using JobFinder.Contracts.Dtos.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Product.Queries.GetProductQuery
{
    public class GetProductQuery : IRequest<Result<List<ProductDto>>>
    {
    }
}
