using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Command
{
    public record DeleteCompanyBenefitCommand(int Id) : IRequest<bool>;
}
