using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.TermsOfService.Command
{
    public record DeleteTermsOfServiceCommand(int Id) : IRequest<bool>;

}
