using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FeatureEntity.Command;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Handlers
{

    public class DeleteFeatureHandler : IRequestHandler<DeleteFeatureCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteFeatureHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = await _context.FeaturesRepository.GetByIdAsync(request.Id);
            if (feature == null)
            {
                throw new NotFoundException("فیچر مورد نظر پیدا نشد");
            }

            await _context.FeaturesRepository.DeleteAsync(feature);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
