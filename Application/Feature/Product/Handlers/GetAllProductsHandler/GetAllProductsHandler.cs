using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Queries.GetAllProductsQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Product.Handlers.GetAllProductsHandler
{

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Result<List<JobFinder.Domain.Common.Entities.Product>>>
    {
        private readonly IUnitOfWork _context;

        public GetAllProductsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<List<JobFinder.Domain.Common.Entities.Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.ProductRepository.GetQueryable().ToListAsync(cancellationToken);
            return Result.Ok(products);
        }
    }
}
