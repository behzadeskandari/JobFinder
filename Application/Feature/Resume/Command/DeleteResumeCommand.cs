using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Command
{
    public record DeleteResumeCommand(Guid Id) : IRequest<bool>;
}
