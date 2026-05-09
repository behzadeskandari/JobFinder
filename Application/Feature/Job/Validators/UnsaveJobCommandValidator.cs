using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Command;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class UnsaveJobCommandValidator : AbstractValidator<UnsaveJobCommand>
    {
        public UnsaveJobCommandValidator()
        {
            //RuleFor(x => x.JobId).GreaterThan(0).WithMessage("Job ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
