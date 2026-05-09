using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.Job.Query
{
    public record GetAllJobsQuery : IRequest<Result<List<JobFinder.Domain.Common.Entities.Job>>>;

}
