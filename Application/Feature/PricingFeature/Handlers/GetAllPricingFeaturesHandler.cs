using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingFeature.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingFeature.Handlers
{
    public class GetAllPricingFeaturesHandler : IRequestHandler<GetAllPricingFeaturesQuery, List<JobFinder.Domain.Common.Entities.PricingFeature>>
    {
        private readonly IUnitOfWork _context;

        public GetAllPricingFeaturesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.PricingFeature>> Handle(GetAllPricingFeaturesQuery request, CancellationToken cancellationToken)
        {
            return await _context.PricingFeaturesRepository.GetQueryable()
                .Include(pf => pf.PricingPlan) // Include the related PricingPlan
                .ToListAsync(cancellationToken);
        }
    }
}
