using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.JobRequest.Queries
{
    public record GetAllJobRequestsQuery : IRequest<Result<List<JobFinder.Domain.Common.Entities.JobRequest>>>;

}
