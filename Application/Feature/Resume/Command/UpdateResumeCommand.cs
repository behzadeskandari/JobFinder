using FluentResults;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Command
{
    public class UpdateResumeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Domain.Common.Entities.Resume Resume { get; set; }
    }
}
