using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FeatureEntity.Command;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Handlers
{

    public class CreateFeatureHandler : IRequestHandler<CreateFeatureCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateFeatureHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = new JobFinder.Domain.Common.Entities.Feature
            {
                Title = request.Title,
                Description = request.Description,
                IconName = request.IconName,
                Language = request.Language,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.FeaturesRepository.AddAsync(feature);
            await _context.CommitAsync(cancellationToken);
            return feature.Id;
        }
    }

}
