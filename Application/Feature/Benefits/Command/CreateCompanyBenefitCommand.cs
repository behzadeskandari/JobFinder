using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Command
{
    public record CreateCompanyBenefitCommand(Guid CompanyId, string Name, string Description) : IRequest<Guid>;
}
