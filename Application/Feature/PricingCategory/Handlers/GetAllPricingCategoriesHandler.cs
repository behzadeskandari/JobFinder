using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PricingPlan.Queries;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.PricingPlan.Handlers
{

    public class GetAllPricingCategoriesHandler : IRequestHandler<GetAllPricingCategoriesQuery, List<PricingCategory>>
    {
        private readonly IUnitOfWork _context;

        public GetAllPricingCategoriesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<PricingCategory>> Handle(GetAllPricingCategoriesQuery request, CancellationToken cancellationToken)
        {
            var t = await _context.PricingCategoriesRepository.GetQueryable()
                 .Include(pc => pc.Plans) // Include the related Plans
                .ToListAsync(cancellationToken);

            return t;
        }
    }
}
