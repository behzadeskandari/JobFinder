using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingFeature.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingFeature.Handlers
{
    public class UpdatePricingFeatureHandler : IRequestHandler<UpdatePricingFeatureCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePricingFeatureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingFeature = await _unitOfWork.PricingFeaturesRepository.GetByIdAsync(request.Id);
            if (pricingFeature == null)
            {
                throw new NotFoundException($"دسته بندی قیمت پیدا نشد");
            }

            pricingFeature.PricingPlanId = request.PricingPlanId;
            pricingFeature.Description = request.Description;
            pricingFeature.IconName = request.IconName;
            pricingFeature.DateModified = DateTime.Now;
            pricingFeature.IsActive = request.IsActive;

            await _unitOfWork.PricingFeaturesRepository.UpdateAsync(pricingFeature);
            await _unitOfWork.CommitAsync(); // Use Unit of Work
            return true;
        }
    }
}
