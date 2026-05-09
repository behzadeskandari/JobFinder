using FluentValidation;
using JobFinder.Contracts.Dtos.MbtiTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Validations
{
    public class MBTIResultValidator : AbstractValidator<MBTIResultDTO>
    {
        public MBTIResultValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(r => r.Type)
                .NotEmpty().WithMessage("Type is required.")
                .Length(2, 50).WithMessage("Type must be between 2 and 50 characters.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");
        }
    }
}
