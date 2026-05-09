using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Product.Command.DeleteProductCommand;
using MediatR;

namespace JobFinder.Application.Feature.Product.Handlers.DeleteProductHandler
{

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _context;

        public DeleteProductHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"محصولی با این ایدی پیدا نشد ${request.Id}");
            }

            await _context.ProductRepository.DeleteAsync(product);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok(true);
        }
    }

}
