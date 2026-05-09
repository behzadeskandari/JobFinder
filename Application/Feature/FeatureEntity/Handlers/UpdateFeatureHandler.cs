using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FeatureEntity.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.FeatureEntity.Handlers
{

    public class UpdateFeatureHandler : IRequestHandler<UpdateFeatureCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateFeatureHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = await _context.FeaturesRepository.GetByIdAsync(request.Id);
            if (feature == null)
            {
                throw new NotFoundException("فیچر مورد نظر پیدا نشد");
            }

            feature.Title = request.Title;
            feature.Description = request.Description;
            feature.IconName = request.IconName;
            feature.Language = request.Language;
            feature.DateModified = DateTime.Now;
            feature.IsActive = request.IsActive;

            await _context.FeaturesRepository.UpdateAsync(feature);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
