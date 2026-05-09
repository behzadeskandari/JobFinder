using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Contracts.Dtos.DropDown;

namespace JobFinder.Contracts.Validations
{
    public class CreateTechnicalOptionDtoValidator : AbstractValidator<CreateTechnicalOptionDto>
    {
        public CreateTechnicalOptionDtoValidator()
        {
            RuleFor(x => x.Label)
                .NotEmpty().WithMessage("Label is required.")
                .MaximumLength(100).WithMessage("Label cannot exceed 100 characters.");


            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required.")
                .MaximumLength(100).WithMessage("Value cannot exceed 100 characters.");
        }
    }
}
