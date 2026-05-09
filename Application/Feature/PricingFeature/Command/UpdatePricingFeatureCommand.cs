using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingFeature.Command
{
    public record UpdatePricingFeatureCommand(
        Guid Id,
        Guid PricingPlanId,
        string Description,
        string IconName,
        bool? IsActive) : IRequest<bool>;
}
