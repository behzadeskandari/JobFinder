using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Command;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class CreatePricingPlanHandler : IRequestHandler<CreatePricingPlanCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePricingPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = new JobFinder.Domain.Common.Entities.PricingPlan
            {
                Name = request.Name,
                Title = request.Title,
                Subtitle = request.Subtitle,
                Price = request.Price,
                Currency = request.Currency,
                Duration = request.Duration,
                DurationUnit = request.DurationUnit,
                JobCount = request.JobCount,
                DiscountPercentage = request.DiscountPercentage,
                ButtonText = request.ButtonText,
                IsPopular = request.IsPopular,
                Type = request.Type,
                PricingCategoryId = request.PricingCategoryId,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.PricingPlansRepository.AddAsync(pricingPlan);
            await _unitOfWork.CommitAsync();
            return pricingPlan.Id;
        }
    }
}
