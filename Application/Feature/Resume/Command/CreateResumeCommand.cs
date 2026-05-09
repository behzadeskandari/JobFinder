using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Command
{
    public class CreateResumeCommand : IRequest<Result<Domain.Common.Entities.Resume>>
    {
        public Domain.Common.Entities.Resume Resume { get; set; }
    }
}
