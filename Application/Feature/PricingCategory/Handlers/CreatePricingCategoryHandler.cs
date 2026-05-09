using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{
    public class CreatePricingCategoryHandler : IRequestHandler<CreatePricingCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePricingCategoryHandler( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = new PricingCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconName = request.IconName,
                Language = request.Language,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.PricingCategoriesRepository.AddAsync(pricingCategory);
            await _unitOfWork.CommitAsync();
            return pricingCategory.Id;
        }
    }
}
