using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Command.UpdateProductCommand;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Product.Handlers.UpdateProductHandler
{

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _context;

        public UpdateProductHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"محصولی با این ایدی پیدا نشد ${request.Id}");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.IsTaxable = request.IsTaxable;
            product.IsArchived = request.IsArchived;
            product.UpdatedOn = DateTime.Now;
            product.IsActive = request.IsActive;

            await _context.ProductRepository.UpdateAsync(product);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok(true);
        }
    }
}
