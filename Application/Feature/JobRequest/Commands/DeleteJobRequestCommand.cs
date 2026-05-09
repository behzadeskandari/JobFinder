using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Commands
{
    public record DeleteJobRequestCommand(int Id) : IRequest<Result<bool>>;

}
