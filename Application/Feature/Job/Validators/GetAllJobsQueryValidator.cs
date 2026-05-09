using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Query;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class GetAllJobsQueryValidator : AbstractValidator<GetAllJobsQuery>
    {
        public GetAllJobsQueryValidator()
        {
            // Add validation rules if needed, e.g., for query parameters
        }
    }
}
