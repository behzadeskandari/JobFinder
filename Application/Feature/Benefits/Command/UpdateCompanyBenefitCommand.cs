using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Command
{
    public record UpdateCompanyBenefitCommand(Guid Id, Guid CompanyId, string Name, string Description, bool? IsActive) : IRequest<bool>;

}
