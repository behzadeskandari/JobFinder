using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.PricingPlan.Queries
{
    public record GetPricingCategoryByIdQuery(Guid Id) : IRequest<PricingCategory>;

}
