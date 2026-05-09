using AutoMapper;
using JobFinder.Contracts.Dtos.Feature;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Pricing.Queries.GetFeature
{
    public class GetFeaturesQuery : IRequest<List<FeatureDto>>
    {
        public string Language { get; set; } = "en";
    }

    public class GetFeaturesQueryHandler : IRequestHandler<GetFeaturesQuery, List<FeatureDto>>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public GetFeaturesQueryHandler(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FeatureDto>> Handle(GetFeaturesQuery request, CancellationToken cancellationToken)
        {
            var features = await _context.FeaturesRepository.GetQueryable()
                .Where(x => x.Language == request.Language).AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<FeatureDto>>(features);
        }
    }
}
