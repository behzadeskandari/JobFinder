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
    public class GetPricingCategoryByIdHandler : IRequestHandler<GetPricingCategoryByIdQuery, PricingCategory>
    {
        private readonly IUnitOfWork _context;

        public GetPricingCategoryByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<PricingCategory> Handle(GetPricingCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.PricingCategoriesRepository.GetQueryable()
                 .Include(pc => pc.Plans) // Include the related Plans
                .FirstOrDefaultAsync(pc => pc.Id == request.Id, cancellationToken);
        }
    }
}
