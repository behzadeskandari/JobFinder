using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingFeature.Queries
{
    public record GetPricingFeatureByIdQuery(Guid Id) : IRequest<JobFinder.Domain.Common.Entities.PricingFeature>;

}
