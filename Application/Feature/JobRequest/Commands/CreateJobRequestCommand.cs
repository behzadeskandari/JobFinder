using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Commands
{
    public record CreateJobRequestCommand(string UserId, string CoverLetter, string ResumeUrl) : IRequest<Result<Guid>>;

}
