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
    public class GetPricingFeatureByIdHandler : IRequestHandler<GetPricingFeatureByIdQuery, JobFinder.Domain.Common.Entities.PricingFeature>
    {
        private readonly IUnitOfWork _context;

        public GetPricingFeatureByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.PricingFeature> Handle(GetPricingFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.PricingFeaturesRepository.GetQueryable()
                .Include(pf => pf.PricingPlan) // Include the related PricingPlan
                .FirstOrDefaultAsync(pf => pf.Id == request.Id, cancellationToken);
        }
    }
}
