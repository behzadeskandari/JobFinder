using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Queries
{
    public record GetPricingPlanByIdQuery(Guid Id) : IRequest<JobFinder.Domain.Common.Entities.PricingPlan>;

}
