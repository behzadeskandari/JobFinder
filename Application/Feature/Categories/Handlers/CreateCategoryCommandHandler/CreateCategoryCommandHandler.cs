using JobFinder.Application.Feature.Categories.Commands.CreateCategoryCommand;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Categories.Handlers.CreateCategoryCommandHandler
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unit;

        public CreateCategoryCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = new Category
            {
                Name = request.Category.Name,
                Description = request.Category.Description
            };
            await _unit.CategoryRepository.AddAsync(entity, cancellationToken);
            await _unit.CommitAsync(cancellationToken);

            return entity.Id;
        }
    }
}
