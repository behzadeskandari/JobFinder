using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class GetPricingPlanByIdHandler : IRequestHandler<GetPricingPlanByIdQuery, JobFinder.Domain.Common.Entities.PricingPlan>
    {
        private readonly IUnitOfWork _context;

        public GetPricingPlanByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.PricingPlan> Handle(GetPricingPlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.PricingPlansRepository.GetQueryable()
                .Include(pp => pp.PricingCategory) // Include the related PricingCategory
                 .Include(pp => pp.Features)
                .FirstOrDefaultAsync(pp => pp.Id == request.Id, cancellationToken);
        }
    }
}
