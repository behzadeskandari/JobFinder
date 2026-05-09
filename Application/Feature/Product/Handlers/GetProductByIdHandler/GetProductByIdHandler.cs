using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Product.Handlers.GetProductByIdHandler
{

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Result<JobFinder.Domain.Common.Entities.Product>>
    {
        private readonly IUnitOfWork _context;

        public GetProductByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<JobFinder.Domain.Common.Entities.Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.ProductRepository.GetQueryable()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException($"محصولی با این ایدی پیدا نشد ${request.Id}");
            }
            return Result.Ok(product);
        }
    }
}
