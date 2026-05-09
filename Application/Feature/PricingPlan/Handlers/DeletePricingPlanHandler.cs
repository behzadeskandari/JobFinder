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

    public class DeletePricingPlanHandler : IRequestHandler<DeletePricingPlanCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePricingPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = await _unitOfWork.PricingPlansRepository.GetByIdAsync(request.Id);
            if (pricingPlan == null)
            {
                throw new NotFoundException($"دسته بندی قیمت پیدا نشد");
            }

            await _unitOfWork.PricingPlansRepository.DeleteAsync(pricingPlan);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
