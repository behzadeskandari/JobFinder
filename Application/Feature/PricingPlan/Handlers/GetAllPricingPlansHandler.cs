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

    public class GetAllPricingPlansHandler : IRequestHandler<GetAllPricingPlansQuery, List<JobFinder.Domain.Common.Entities.PricingPlan>>
    {
        private readonly IUnitOfWork _context;

        public GetAllPricingPlansHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.PricingPlan>> Handle(GetAllPricingPlansQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.PricingPlansRepository.GetQueryable()
                .Include(pp => pp.PricingCategory) // Include the related PricingCategory
                .Include(pp => pp.Features)
                .ToListAsync(cancellationToken);

            return record;
        }
    }
}
