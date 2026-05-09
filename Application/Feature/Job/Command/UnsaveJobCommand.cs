using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace JobFinder.Application.Feature.Job.Command
{
    public class UnsaveJobCommand : MediatR.IRequest<Result>
    {
        public Guid JobId { get; set; }
        public string UserId { get; set; }
    }
}
