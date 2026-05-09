using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Command
{
    public record CreatePricingPlanCommand(
       string Name,
       string Title,
       string Subtitle,
       decimal Price,
       string Currency,
       int Duration,
       string DurationUnit,
       int JobCount,
       int? DiscountPercentage,
       string ButtonText,
       bool? IsPopular,
       string Type,
       Guid PricingCategoryId) : IRequest<Guid>;
}
