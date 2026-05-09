using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Command;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class DeletePricingCategoryHandler : IRequestHandler<DeletePricingCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePricingCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = await _unitOfWork.PricingCategoriesRepository.GetByIdAsync(request.Id);
            if (pricingCategory == null)
            {
                throw new NotFoundException($"دسته بندی قیمت پیدا نشد");
            }

            await _unitOfWork.PricingCategoriesRepository.DeleteAsync(pricingCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }

}
