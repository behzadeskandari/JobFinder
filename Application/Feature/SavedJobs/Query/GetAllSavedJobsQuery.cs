using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.SavedJobs;

namespace JobFinder.Application.Feature.SavedJobs.Query
{
    public class GetAllSavedJobsQuery : MediatR.IRequest<Result<IEnumerable<SavedJobDto>>>
    {
        public string UserId { get; set; }
    }
}
