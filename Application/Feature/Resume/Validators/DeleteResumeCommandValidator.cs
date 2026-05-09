using FluentValidation;
using JobFinder.Application.Feature.Resume.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Validators
{
    public class DeleteResumeCommandValidator : AbstractValidator<DeleteResumeCommand>
    {
        public DeleteResumeCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Resume ID is required");
        }
    }
}
