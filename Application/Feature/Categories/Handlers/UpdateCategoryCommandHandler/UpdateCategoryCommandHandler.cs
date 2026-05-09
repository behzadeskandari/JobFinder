using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Feature.Categories.Commands.UpdateCategoryCommand;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Categories.Handlers.UpdateCategoryCommandHandler
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        //private readonly IApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            //var entity = await _context.Categories
            //    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            var entity =  _unitOfWork.CategoryRepository.GetByIdAsyncWithAdvertisements(request.Id,cancellationToken);

            entity.Result.Description = request.Category.Description;
            entity.Result.Name = request.Category.Name;
            entity.Result.Id = request.Id;
            entity.Result.IsActive = request.Category.IsActive;
            entity.Result.DateModified = DateTime.Now;
            
            
            var record = _unitOfWork.CategoryRepository.UpdateAsync(entity.Result);
            if (record.Result == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            record.Result.Name = request.Category.Name;
            record.Result.Description = request.Category.Description;
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
