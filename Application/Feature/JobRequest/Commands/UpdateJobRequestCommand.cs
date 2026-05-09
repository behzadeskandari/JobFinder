using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Commands
{
    public record UpdateJobRequestCommand(int Id, string CoverLetter, string ResumeUrl, JobRequestStatus Status, bool? IsActive) : IRequest<Result<bool>>;

}
