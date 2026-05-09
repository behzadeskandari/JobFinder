using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Job;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Queries
{
    public class GetJobsByCategoryQuery : MediatR.IRequest<Result<IEnumerable<JobGetDto>>>
    {
        public string Slug { get; set; }
    }
}
