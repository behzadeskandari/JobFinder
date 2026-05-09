using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Job;
using MediatR;

namespace JobFinder.Application.Feature.Job.Query
{
    public class SearchJobsQuery : IRequest<Result<IEnumerable<JobGetDto>>>
    {
        public SearchJobsQueryDto SearchCriteria { get; set; }
        public int PageNumber { get;  set; } 
        public int PageSize { get;  set; }
    }
}
