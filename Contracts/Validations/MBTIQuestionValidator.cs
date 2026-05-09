using FluentValidation;
using JobFinder.Contracts.Dtos.MbtiTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Validations
{
    public class MBTIQuestionValidator : AbstractValidator<MBTIQuestionDTO>
    {
        public MBTIQuestionValidator()
        {
            RuleFor(q => q.QuestionText)
                .NotEmpty().WithMessage("Question text is required.")
                .Length(5, 200).WithMessage("Question text must be between 5 and 200 characters.");
        }
    }
}
