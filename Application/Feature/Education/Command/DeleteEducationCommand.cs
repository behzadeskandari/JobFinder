using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Education.Command
{

    public record DeleteEducationCommand(Guid Id) : IRequest<bool>;
}
