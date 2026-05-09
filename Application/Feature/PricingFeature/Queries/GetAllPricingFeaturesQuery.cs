using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingFeature.Queries
{

    public record GetAllPricingFeaturesQuery : IRequest<List<JobFinder.Domain.Common.Entities.PricingFeature>>;
}
