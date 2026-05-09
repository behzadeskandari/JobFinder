using FluentValidation;
using JobFinder.Application.Feature.Resume.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Validators
{
    public class UpdateResumeCommandValidator : AbstractValidator<UpdateResumeCommand>
    {
        public UpdateResumeCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Resume ID is required");
            RuleFor(x => x.Resume.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.Resume.ProfilePictureUrl).NotEmpty().WithMessage("ProfilePictureUrl is required");
            RuleFor(x => x.Resume.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Resume.Phone).NotEmpty().WithMessage("Phone is required");
        }
    }
}
