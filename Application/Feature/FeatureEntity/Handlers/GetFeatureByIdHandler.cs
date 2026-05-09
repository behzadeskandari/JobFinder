using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FeatureEntity.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.FeatureEntity.Handlers
{
    public class GetFeatureByIdHandler : IRequestHandler<GetFeatureByIdQuery, JobFinder.Domain.Common.Entities.Feature>
    {
        private readonly IUnitOfWork _context;

        public GetFeatureByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.Feature> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.FeaturesRepository.GetQueryable().FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        }
    }
}
