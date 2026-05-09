using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingFeature.Command;
using MediatR;

namespace JobFinder.Application.Feature.PricingFeature.Handlers
{

    public class DeletePricingFeatureHandler : IRequestHandler<DeletePricingFeatureCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePricingFeatureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingFeature = await _unitOfWork.PricingFeaturesRepository.GetByIdAsync(request.Id);
            if (pricingFeature == null)
            {
               throw new NotFoundException($"دسته بندی قیمت پیدا نشد");
            }

            await _unitOfWork.PricingFeaturesRepository.DeleteAsync(pricingFeature);
            await _unitOfWork.CommitAsync(); // Use Unit of Work
            return true;
        }
    }
}
