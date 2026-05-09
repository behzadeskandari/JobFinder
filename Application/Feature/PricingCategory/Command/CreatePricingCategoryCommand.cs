using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Command
{
    public record CreatePricingCategoryCommand(
       string Name,
       string Description,
       string IconName,
       string Language) : IRequest<Guid>;

}
