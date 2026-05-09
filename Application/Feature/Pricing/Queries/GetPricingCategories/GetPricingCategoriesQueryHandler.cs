using AutoMapper;
using JobFinder.Contracts.Dtos.Pricing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Pricing.Queries.GetPricingCategories
{
    public class GetPricingCategoriesQuery : IRequest<List<PricingCategoryDto>>
    {
        public string Language { get; set; } = "en"; // Default to English
    }

    public class GetPricingCategoriesQueryHandler : IRequestHandler<GetPricingCategoriesQuery, List<PricingCategoryDto>>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public GetPricingCategoriesQueryHandler(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PricingCategoryDto>> Handle(GetPricingCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.PricingCategoriesRepository.GetQueryable()
                .Include(c => c.Plans)
                .ThenInclude(p => p.Features)
                .Where(c => c.Language.Contains(request.Language))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<PricingCategoryDto>>(categories);
        }
    }
}
