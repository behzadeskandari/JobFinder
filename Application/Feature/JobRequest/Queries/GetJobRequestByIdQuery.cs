using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Queries
{
    public record GetJobRequestByIdQuery(Guid Id) : IRequest<Result<JobFinder.Domain.Common.Entities.JobRequest>>;

}
