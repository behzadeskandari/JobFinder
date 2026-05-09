using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Job;

namespace JobFinder.Application.Feature.Job.Query
{
    public class GetAppliedJobsQuery : MediatR.IRequest<Result<IEnumerable<JobGetDto>>>
    {
        public string UserId { get; set; }
    }
}
