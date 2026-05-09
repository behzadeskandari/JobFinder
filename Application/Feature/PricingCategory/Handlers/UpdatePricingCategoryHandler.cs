using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class UpdatePricingCategoryHandler : IRequestHandler<UpdatePricingCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePricingCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = await _unitOfWork.PricingCategoriesRepository.GetByIdAsync(request.Id);
            if (pricingCategory == null)
            {
                return false;
            }

            pricingCategory.Name = request.Name;
            pricingCategory.Description = request.Description;
            pricingCategory.IconName = request.IconName;
            pricingCategory.Language = request.Language;
            pricingCategory.DateModified = DateTime.Now;
            pricingCategory.IsActive = request.IsActive;

            await _unitOfWork.PricingCategoriesRepository.UpdateAsync(pricingCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
