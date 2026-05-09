using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FeatureEntity.Query;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Handlers
{
    public class GetAllFeaturesHandler : IRequestHandler<GetAllFeaturesQuery, List<JobFinder.Domain.Common.Entities.Feature>>
    {
        private readonly IUnitOfWork _context;

        public GetAllFeaturesHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.Feature>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
        {
            var record =  await _context.FeaturesRepository.GetAllAsync(cancellationToken);
        
            return record.ToList();
        }
    }
}
