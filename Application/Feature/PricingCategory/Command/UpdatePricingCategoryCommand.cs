using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Command
{
    public record UpdatePricingCategoryCommand(
        int Id,
        string Name,
        string Description,
        string IconName,
        string Language,
        bool? IsActive) : IRequest<bool>;
}
