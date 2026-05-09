using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Command
{

    public record DeletePricingCategoryCommand(Guid Id) : IRequest<bool>;
}
