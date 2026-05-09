using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Categories.Commands.DeleteCategoryCommand;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Categories.Handlers.DeleteCategoryCommandHandler
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        //private readonly IApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var record = _unitOfWork.CategoryRepository.GetByIdAsyncWithAdvertisements(request.Id, cancellationToken);
            
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            if (record.Result.Advertisements.Count > 0)
            {
                throw new DeleteFailureException(nameof(Category), request.Id, "There are existing advertisements associated with this category.");
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
