using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Queries
{
    public record GetCompanyBenefitByIdQuery(Guid Id) : IRequest<CompanyBenefit>;
}
