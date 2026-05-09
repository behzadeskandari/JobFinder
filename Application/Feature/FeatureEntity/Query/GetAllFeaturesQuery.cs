using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Query
{

    public record GetAllFeaturesQuery : IRequest<List<JobFinder.Domain.Common.Entities.Feature>>;
}
