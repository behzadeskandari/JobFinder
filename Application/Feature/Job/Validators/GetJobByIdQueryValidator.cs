using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Query;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class GetJobByIdQueryValidator : AbstractValidator<GetJobByIdQuery>
    {
        public GetJobByIdQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Job ID is required");
        }
    }
}
