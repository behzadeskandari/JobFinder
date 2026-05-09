using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingFeature.Command;
using MediatR;

namespace JobFinder.Application.Feature.PricingFeature.Handlers
{
    public class CreatePricingFeatureHandler : IRequestHandler<CreatePricingFeatureCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePricingFeatureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingFeature = new JobFinder.Domain.Common.Entities.PricingFeature
            {
                PricingPlanId = request.PricingPlanId,
                Description = request.Description,
                IconName = request.IconName,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.PricingFeaturesRepository.AddAsync(pricingFeature);
            await _unitOfWork.CommitAsync(); // Use Unit of Work
            return pricingFeature.Id;
        }
    }

}
