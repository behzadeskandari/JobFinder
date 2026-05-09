using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.Job.Command;

namespace JobFinder.Application.Feature.Job.Validators
{
    public class ApplyJobCommandValidator : AbstractValidator<ApplyJobCommand>
    {
        public ApplyJobCommandValidator()
        {
            //RuleFor(x => x.JobId).GreaterThan(0).WithMessage("Job ID is required");
            RuleFor(x => x.CandidateId).NotEmpty().WithMessage("Candidate ID is required");
            RuleFor(x => x.ResumeFileName).NotEmpty().WithMessage("Resume file name is required");
            RuleFor(x => x.ResumeFileUrl).NotEmpty().WithMessage("Resume file URL is required");
            RuleFor(x => x.CoverLetter).NotEmpty().WithMessage("Cover letter is required");
        }
    }
}
