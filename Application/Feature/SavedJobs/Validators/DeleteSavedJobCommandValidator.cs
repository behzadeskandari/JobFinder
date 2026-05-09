using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.SavedJobs.Commands;

namespace JobFinder.Application.Feature.SavedJobs.Validators
{
    public class DeleteSavedJobCommandValidator : AbstractValidator<DeleteSavedJobCommand>
    {
        public DeleteSavedJobCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
