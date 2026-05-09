using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class UpdatePricingPlanHandler : IRequestHandler<UpdatePricingPlanCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePricingPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = await _unitOfWork.PricingPlansRepository.GetByIdAsync(request.Id);
            if (pricingPlan == null)
            {
                throw new NotFoundException($"دسته بندی قیمت پیدا نشد");
            }

            pricingPlan.Name = request.Name;
            pricingPlan.Title = request.Title;
            pricingPlan.Subtitle = request.Subtitle;
            pricingPlan.Price = request.Price;
            pricingPlan.Currency = request.Currency;
            pricingPlan.Duration = request.Duration;
            pricingPlan.DurationUnit = request.DurationUnit;
            pricingPlan.JobCount = request.JobCount;
            pricingPlan.DiscountPercentage = request.DiscountPercentage;
            pricingPlan.ButtonText = request.ButtonText;
            pricingPlan.IsPopular = request.IsPopular;
            pricingPlan.Type = request.Type;
            pricingPlan.PricingCategoryId = request.PricingCategoryId;
            pricingPlan.DateModified = DateTime.Now;
            pricingPlan.IsActive = request.IsActive;

            await _unitOfWork.PricingPlansRepository.UpdateAsync(pricingPlan);
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
